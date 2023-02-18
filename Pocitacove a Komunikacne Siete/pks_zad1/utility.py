import json
def are_ip_same_icmp(frame1, frame2):
    return (frame1.src_ip == frame2.src_ip) and (frame1.dst_ip == frame2.dst_ip)
#icmp
def find_offset_more_then_zero(idx_l, icmp_list, find, icmp_dir):
    
    for idx in range(idx_l, len(icmp_list)):

        pckt = icmp_list[idx]
        
        if are_ip_same_icmp(pckt, find) and pckt.frag_offset > 0:
            try:
                pckt.set_type(find.raw_hex, icmp_dir)
                return {
                    "icmp_type": pckt.icmp_type,
                    "src_ip": pckt.src_ip,
                    "dst_ip": pckt.dst_ip,
                    "packets": [find, pckt] 
                }
                

            except KeyError:
                return False

def find_icmp_com(icmp_list, icmp_dir):
    complete, partial = [], []

    fragment = []
    dont_fragment = []

    #unfragmented = icmp_list.deepcopy()
    #pri fragmentovani type v tom predchadzajucom na 68:70b
    #toto hladam fragmentovane
    #com = {src_ip, dst_ip, icmp_type, packets}
    
    for idx, pckt in enumerate(icmp_list):
        if pckt.m_f == True:
            com = find_offset_more_then_zero(idx + 1, icmp_list, pckt, icmp_dir)
            if com is not False:
                fragment.append(com)
            else:
                partial.append(pckt)
            
        else:
            #44 36B 07 20B
            shift = int(28 + int(pckt.raw_hex[29:30], 16)*8)
            if "08" == pckt.raw_hex[shift: shift +2]:
                pckt.set_via_argument("request")
            elif "00" == pckt.raw_hex[shift: shift +2]:
                pckt.set_via_argument("response")
            else:
                pckt.set_via_argument("partial")
            dont_fragment.append(pckt)

    #ak mf_1 zapis do premennej
    #nasledne ak najdes paket ktory ma offset>0 vrat sa k ulozenemu paketu
    # tmp = {packets[1] - paruj}
    #TODO naparuj
    
    # for pckt in icmp_list:
    #     tmp=[]
    #     if pckt.icmp_type == "partial":
    #         partial.append(pckt)
    #     if pckt.icmp_type == "request":

        
        
    return complete, partial
#arp 
def find_com_arp(list, arp_dic):
    # musim najst request a vyparsovat z nej data
   
    list_requests = []
    complete_com = []
    #find requests
    for idx, pckt in enumerate(list):
        if pckt.opcode == "request":
            list_requests.append(pckt)
    count = 1
    for request in list_requests:
        idx = list.index(request) + 1
        packets = []
        packets.append(request)
        for idx_l in range(idx, len(list)):
            pckt = list[idx_l]

            if "opcode" in pckt.__dict__ and pckt.opcode == "request":
                 if pckt.src_ip == request.dst_ip and pckt.dst_ip == request.src_ip:
                    packets.append(pckt)

            if "opcode" in pckt.__dict__ and pckt.opcode == "reply":
                if pckt.src_ip == request.dst_ip and pckt.dst_ip == request.src_ip:
                    packets.append(pckt)

                    complete_com.append({
                        "number_comm":count,
                        "src_comm": request.src_ip,
                        "dst_comm": request.dst_ip,
                        "packets": packets
                    })
                    count += 1
                    break

    return complete_com

def pop_none(list):
    for n in list:
        if n == None:
            list.remove(n)
#udp
def are_ip_same(frame, tmp):
    return (frame.src_ip == tmp["sender_ip"] or frame.src_ip == tmp["receiver_ip"]) and (frame.dst_ip == tmp["receiver_ip"] or frame.dst_ip == tmp["sender_ip"])

def find_com_udp(start, tmp, all_list):
    find_start = False
    tmp["sender_ip"] = start.src_ip
    tmp["receiver_ip"] = start.dst_ip
    tmp["sender_port"] = start.src_port
    idx_l = all_list.index(start) + 1
    #ip adresy sa musia rovnat
    #a frame dst_port == tmp["sender_port"]
    for idx in range(idx_l, len(all_list)):
        pckt = all_list[idx]
        if "protocol" in pckt.__dict__ and pckt.protocol == "UDP":
            if pckt.dst_port == 69:
                return tmp

            if find_start and are_ports_ips_same(pckt, tmp):
                tmp["packets"].append(pckt)

            if are_ip_same(pckt, tmp) and pckt.dst_port == tmp["sender_port"] and not find_start:
                tmp["receiver_port"] = pckt.src_port 
                tmp["packets"].append(pckt)
                find_start = True
    return tmp

def find_tftp_comunications(udp_list, all_list, udp_dic):
    comunications = []
    #84-88 b = opcode
    tmp = {
        "sender_ip": "x",
        "receiver_ip": "x",
        "sender_port": "x",
        "receiver_port": "x",
        "packets": []
    }

    #prva komunikacia musi prist od sendera na 69 port, potom z opacnej strany z hociakeho portu na senderov port
    #postup: 1. najdi zaciatok komunikacie
    #2. najdi vsetky pakety az dokym nenajdes port 69

    for frame in udp_list:
        frame.set_op_code_udp(udp_dic)
        if (frame.opcode == "WRITE REQUEST" or frame.opcode == "READ REQUEST") and frame.dst_port == 69:
             
            tmp["packets"].append(frame)
            tmp = find_com_udp(frame, tmp, all_list)
            comunications.append(tmp)
            tmp = {
                "packets": []
            }
    return comunications

# From opening and endings get a complete and ucomplete comunication
def cr_yaml_node(node, packets, number_comm):
    return {
        "number_comm": number_comm,
        "src_comm": node["sender_ip"],
        "dst_comm": node["receiver_ip"],
        "packets": packets
    }   

def find_packets_for_com(first_packet, last_packet, src_ip, dst_ip, src_port, dst_port, list):
    add = False
    packets = []
    packets.append(first_packet)
    #created for function are_ports_ips_same(frame, tmp) to work properly
    tmp = {
        "receiver_ip": dst_ip,
        "sender_ip": src_ip,
        "receiver_port": dst_port,
        "sender_port": src_port,
    }
    for frame in list:
        if frame == first_packet:
            add = True
        if frame == last_packet:
            packets.append(last_packet)
            break

        if add and are_ports_ips_same(frame, tmp):
            packets.append(frame)

    return packets

def set_atributes_for_complete(open, end):
    first_packet = open["packets"][0]
    last_packet = end["packets"][len(end["packets"]) - 1]
    src_ip = open["sender_ip"]
    dst_ip = open["receiver_ip"]
    src_port = open["sender_port"]
    dst_port = open["receiver_port"]
    return first_packet,last_packet,src_ip, dst_ip, src_port, dst_port

def set_atributes_for_uncomplete(com):
    #chcem najst prvy alebo posledny packet 
    packet = {}
    if com["type"] == "open":
        packet = com["packets"][0]
    else:
        packet = com["packets"][len(com["packets"]) - 1]

    src_ip = com["sender_ip"]
    dst_ip = com["receiver_ip"]
    src_port = com["sender_port"]
    dst_port = com["receiver_port"]

    return packet, src_ip, dst_ip, src_port, dst_port

def find_packets_for_uncomplete_end(list, l_pckt, src_ip, dst_ip, src_port, dst_port):
    packets = []
    # prechadzaj list kym najdes l_pckt vtedy breakni
    # porovnavaj pckty v liste s tmp
    #created for function are_ports_ips_same(frame, tmp) to work properly
    tmp = {
        "receiver_ip": dst_ip,
        "sender_ip": src_ip,
        "receiver_port": dst_port,
        "sender_port": src_port,
    }

    for pckt in list:
        if pckt == l_pckt:
            packets.append(l_pckt)
        
        if are_ports_ips_same(pckt, tmp):
            packets.append(pckt)
    
    return packets

def find_packets_for_uncomplete_open(list, f_pckt, src_ip, dst_ip, src_port, dst_port):
    packets = []
    find = False
    # prechadzaj list kym najdes l_pckt vtedy breakni
    # porovnavaj pckty v liste s tmp
    #created for function are_ports_ips_same(frame, tmp) to work properly
    tmp = {
        "receiver_ip": dst_ip,
        "sender_ip": src_ip,
        "receiver_port": dst_port,
        "sender_port": src_port,
    }
    for pckt in list:
        if pckt == f_pckt:
            packets.append(f_pckt)
            find = True
        
        if find and are_ports_ips_same(pckt, tmp):
            packets.append(pckt)
    
    return packets

def find_complete_uncomplete(list, op_samples, cl_samples):
    complete = []
    uncomplete = []
    count = 1
    #endings and openings work just perfect 
    openings, endings = find_openings_endings(list, op_samples, cl_samples)
    
    for open_pckt in openings:
        for end_pckt in endings:
            if open_pckt["sender_ip"] == end_pckt["sender_ip"] and open_pckt["receiver_ip"] == end_pckt["receiver_ip"] and open_pckt["receiver_port"] == end_pckt["receiver_port"] and open_pckt["sender_port"] == end_pckt["sender_port"]:
                first_packet,last_packet,src_ip, dst_ip, src_port, dst_port = set_atributes_for_complete(open_pckt, end_pckt)
                packets = find_packets_for_com(first_packet, last_packet, src_ip, dst_ip, src_port, dst_port, list)
                complete.append(cr_yaml_node(open_pckt, packets, count))
                openings.remove(open_pckt)
                endings.remove(end_pckt)
                count += 1
    if len(openings) == 0 and len(endings) == 0:
        return complete, uncomplete
    if len(openings) != 0:
        open_pckt = openings.pop()
        first_packet,src_ip, dst_ip, src_port, dst_port = set_atributes_for_uncomplete(open_pckt)
        packets = find_packets_for_uncomplete_open(list, first_packet, src_ip, dst_ip, src_port, dst_port)
        uncomplete.append(cr_yaml_node(open_pckt, packets, 1))
        return complete, uncomplete
    elif len(endings) != 0:
        end_pckt = endings.pop()
        last_packet,src_ip, dst_ip, src_port, dst_port = set_atributes_for_uncomplete(end_pckt)
        packets = find_packets_for_uncomplete_end(list, last_packet, src_ip, dst_ip, src_port, dst_port)
        uncomplete.append(cr_yaml_node(end_pckt, packets, 1))
        return complete, uncomplete
    else:
        return False, False

# Finding openings and endings
def cr_node(tmp):
    keys = tmp.keys()
    max_packets = []
    node = {}
    for k in keys:
        if "packets" in k and int(len(tmp[k])) > int(len(max_packets)):
            max_packets = tmp[k]
            
    for k in keys:
        if "packet" not in k:
            node[k] = tmp[k]
    
    node["packets"] = max_packets

    if node["type"] == "close":
        node["used"] = False
    return node    

def are_ports_ips_same(frame, tmp):
    return( (frame.src_ip == tmp["sender_ip"] or frame.src_ip == tmp["receiver_ip"]) and (frame.dst_ip == tmp["sender_ip"] or 
    frame.dst_ip == tmp["receiver_ip"]) and (frame.src_port == tmp["sender_port"] or frame.src_port == tmp["receiver_port"]) and
    (frame.dst_port == tmp["sender_port"] or frame.dst_port == tmp["receiver_port"]) )

def is_second_sample(frame, tmp, sample):
    find = False

    for idx,s in enumerate(sample):
        pattern = s["samples"]
        #ak sa paket,flags zhoduju s vzorkou a sedia nam aj ipcky a porty posuniem point na dalsiu vzorku a pridan paket ku komunikacii 
        if pattern[s["point"]] == frame.flags and are_ports_ips_same(frame, tmp):
            find = True
            s["point"] += 1
            tmp[f"packets{idx}"].append(frame)

    return find

def is_complete(samples):
    """when sample point at last pattern function return True"""
    for s in samples:
        if s["point"] == len(s["samples"]):
            return True
    return False

def reset_samples(samples):
    for s in samples:
        s["point"] = 0

def set_tmp(tmp, samples):
    for idx, s in enumerate(samples):
        tmp[f"packets{idx}"] = []

# function which are looking for openings and endings of comunication
def find_com(tmp, tcp_list, l_idx, samples, list):

    for i in range(l_idx, len(tcp_list)):
        frame = tcp_list[i]
        #ma frame rovnake flags ako niektory zo samplov, 
        #is complete len skontruluje ci uz sa point == dlzke pola so vzorkami => splnilo podmienky
        if is_second_sample(frame, tmp, samples) and is_complete(samples):
            node = cr_node(tmp)
            list.append(node)
            return

def is_in_samples(frame, tmp, samples, type):
    """return True if sample.point pointing on pattern which is equal to frame.flags"""
    find = False

    for idx, sample in enumerate(samples):
        #vypytam si od kazdej vzorky paterny na ukoncenie/zacatie komunikacie
        patterns = sample["samples"]
        #porovnam pattern s flagmi framu 
        if patterns[sample["point"]] == frame.flags:
            #posuniem pointer na dalsiu flagu
            sample["point"] += 1
            tmp["sender_ip"] = frame.src_ip
            tmp["receiver_ip"] = frame.dst_ip
            tmp["sender_port"] = frame.src_port
            tmp["receiver_port"] = frame.dst_port
            tmp["type"] = type
            #priradim packet konretnej vzorke
            tmp[f"packets{idx}"].append(frame)
            find = True

    return find

def find_openings_endings(list, op_samples, cl_samples):
    openings = []
    endings = []
    tmp_open = {}
    tmp_closed = {}
    #nastavi atributy tmp
    set_tmp(tmp_open, op_samples)
    set_tmp(tmp_closed, cl_samples)

    for idx, frame in enumerate(list):
        #nachadza sa frame.flags v niektorom zo samplov
        if is_in_samples(frame, tmp_open, op_samples, "open"):
            find_com(tmp_open, list, idx + 1 ,op_samples, openings)
            set_tmp(tmp_open, op_samples)
             #resetne pointre pre kazdu vzorku na 0
            reset_samples(op_samples)
        #to iste len kontrolujem pre zatvorenia
        elif is_in_samples(frame, tmp_closed, cl_samples, "close"):
            find_com(tmp_closed, list, idx + 1 ,cl_samples, endings)
            set_tmp(tmp_closed, cl_samples)
            #resetne pointre pre kazdu vzorku na 0
            reset_samples(cl_samples)

    return openings, endings

# from tcp_senders find a sender with max. sended packets
def find_max_in_senders(senders):

    max_senders = []
    max_send = {
        "node": "",
        "number_of_sent_packets": 0
    }

    for sender in senders:
        if sender["number_of_sent_packets"] > max_send["number_of_sent_packets"]:
            max_send["number_of_sent_packets"] = sender["number_of_sent_packets"]
            max_send["node"] = sender["node"]

    for sender in senders:
        if sender["node"] != max_send["node"] and sender["number_of_sent_packets"] == max_send["number_of_sent_packets"]:
            max_senders.append(sender["node"])
    
    max_senders.append(max_send["node"])
    return max_senders

def create_list_of_senders(list):
    # vytvor list src_adr 
    ip_senders = []
    ip_adr = []
    
    for pckt in list:
        if pckt.src_ip not in ip_adr:
            ip_adr.append(pckt.src_ip)
    
    for ip in ip_adr:
        ip_senders.append({
            "node": ip,
            "number_of_sent_packets" : 0
        })

    for sender in ip_senders:
        for pckt in list:
            if pckt.src_ip == sender["node"]:
                sender["number_of_sent_packets"] += 1

    return find_max_in_senders(ip_senders), ip_senders


#Generate basic atributes for frames
def convert_mac(src_adr, dst_adr, count):
    src_adr_mod = src_adr[0:2] + ":" + src_adr[2:4] + ":" + src_adr[4:6] + ":" + src_adr[6:8] + ":" + src_adr[8:10] + ":" + src_adr[10:12]
    dst_adr_mod = dst_adr[0:2] + ":" + dst_adr[2:4] + ":" + dst_adr[4:6] + ":" + dst_adr[6:8] + ":" + dst_adr[8:10] + ":" + dst_adr[10:12]
    # src_adr_mod = src_adr_mod.upper()
    # dst_adr_mod = dst_adr_mod.upper()

    return str(src_adr_mod), str(dst_adr_mod)

def is_eth_or_iee(packet):
    hexa = packet[24:28]
    dec = int(hexa, base=16)
    type_frame = ""

    # rozhodne ci je ethernet alebo IEEE
    if dec == 1536 or dec > 1500:
        type_frame = "ETHERNET II"
    else:
        type_frame = "IEEE 802.3"
    return type_frame

def calc_len_pckt(pckt):
    #ak je len menej ako 60 potom cez medium prejde 64 inak medium = len +4
    len_pycap = int(len(pckt)/2)

    if len_pycap < 60:
        len_medium = 64
        return len_pycap, len_medium
    
    return len_pycap, len_pycap + 4

def pars_mac_and_len_and_type(hex_packet,count):
    dst_adr = hex_packet[0:12]
    src_adr = hex_packet[12:24]

    src_adr, dst_adr = convert_mac(src_adr, dst_adr, count)
   
    #supacke pocitanie rozmeru mozno bude treba fix
    frame_len_pcap, frame_len_medium = calc_len_pckt(hex_packet)

    return src_adr, dst_adr, frame_len_pcap, frame_len_medium, is_eth_or_iee(hex_packet)

#Read external files
def create_eth_dics(eth_path, ieee_path):
    """
    return a dictionaris, first are eth_dictionaris, second are ieee dictionaris.
    eth_dics = [ipv_versions], ieee_dics = [FRAME_TYPE, SAP, PID]
    """
    eth_file = open(eth_path)
    ieee_file = open(ieee_path)
    # [0] = ipv
    dic_list_eth = json.load(eth_file)

    # [0] = RAW,LLC & SNAP [1] = SAP, [2] = PID
    dic_list_ieee = json.load(ieee_file)
    
    eth_file.close()
    ieee_file.close()
    
    return dic_list_eth, dic_list_ieee

def create_4_dics(tcp_path):

    tcp_file = open(tcp_path)

    dic_tcp = json.load(tcp_file)

    # openings = dic_tcp["openings"]
    # closings = dic_tcp["closings"]

    # for n in openings:
    #     n["point"] = 0
    # for n in closings:
    #     n["point"] = 0

    tcp_file.close()
    
    return dic_tcp
