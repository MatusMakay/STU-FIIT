from importlib.resources import path
from scapy.all import rdpcap, raw
from binascii import hexlify

import sys

from FrameCreator import FrameCreator
from FrameWriter import FrameWriter

from utility import find_complete_uncomplete, find_tftp_comunications, pars_mac_and_len_and_type, create_eth_dics, create_4_dics, find_com_arp, find_icmp_com

eth_path = "./protocols/ethernet2.json"
ieee_path = "./protocols/ieee.json"

know_app_protocol = ["HTTP", "HTTPS", "TELNET", "SSH", "FTP-DATA", "FTP-CONTROL", "TFTP", "ARP", "ICMP"]

tcp_protocols = ["HTTP", "HTTPS", "TELNET", "SSH", "FTP-DATA", "FTP-CONTROL"]
count = 1
# read data from output/ dir and create dictionary for IEEE and ETHERNET2

eth_dics, ieee_dics = create_eth_dics(eth_path, ieee_path)

creator = FrameCreator(eth_dics, ieee_dics)
writer = FrameWriter()

dic_4 = create_4_dics("protocols/tcp_com.json")



#done
def ipv4_tcp(app_protocol, path_pcap):
    count = 1

    ipv4_tcp_list = []
    
    op_samples = dic_4["openings"]
    cl_samples = dic_4["closings"]

    for packet in packets:
        hex_packet = hexlify(raw(packet)).decode('utf-8')
        
        #parsne mac adresy, dlzku ramca, typ ramca a returne values
        src_adr, dst_adr, frame_len_pcap, frame_len_medium, type_frame = pars_mac_and_len_and_type(hex_packet, count)

        #IEE, ETHERNET2
        frame = creator.search_eth_type_dic(type_frame, count, src_adr, dst_adr,frame_len_pcap, frame_len_medium, hex_packet)

        if "protocol" in frame.__dict__ and frame.protocol == "TCP" and "app_protocol" in frame.__dict__ and frame.app_protocol == app_protocol:
            frame.set_raw_frame(hex_packet)
            ipv4_tcp_list.append(frame)
        
        count += 1

    yaml_path = "output/" + path_pcap[0: len(path_pcap)-4] + "yaml"
    #len tcp komunikacia
    complete, uncomplete = find_complete_uncomplete(ipv4_tcp_list, op_samples, cl_samples)
    if not complete and not uncomplete:
        return
    writer.init_ul_4a(yaml_path, path_pcap, app_protocol)
    # TODO posef situaciu ked complete a uncomplete budu prazdne alebo kombinacie
    writer.write_in_yaml_4a(complete, uncomplete)
    

    
def ipv4_udp(path_pcap):
    count = 1

    udp_dic = dic_4["flags_for_udp"]

    ipv4_udp_list_start = []
    all_list = []
    for packet in packets:
        hex_packet = hexlify(raw(packet)).decode('utf-8')

        #parsne mac adresy, dlzku ramca, typ ramca a returne values
        src_adr, dst_adr, frame_len_pcap, frame_len_medium, type_frame = pars_mac_and_len_and_type(hex_packet, count)

        #IEE, ETHERNET2
        frame = creator.search_eth_type_dic(type_frame, count, src_adr, dst_adr,frame_len_pcap, frame_len_medium, hex_packet)
        all_list.append(frame)

        if "protocol" in frame.__dict__ and frame.protocol == "UDP" and "app_protocol" in frame.__dict__ and frame.app_protocol == "TFTP":
            frame.set_raw_frame(hex_packet)
            ipv4_udp_list_start.append(frame)


        count += 1

    yaml_path = "output/" + path_pcap[0: len(path_pcap)-4] + "yaml"
    #len tcp komunikacia
    comunications = find_tftp_comunications(ipv4_udp_list_start, all_list, udp_dic)
    if not comunications:
        return
    writer.init_ul_4b(yaml_path, path_pcap)
    writer.write_in_yaml_4b(comunications)

def icmp():
    count = 1

    icmp_dir =  dic_4["icmp_type"]

    all_list = []
    icmp_list = []

    for packet in packets:
        hex_packet = hexlify(raw(packet)).decode('utf-8')

        #parsne mac adresy, dlzku ramca, typ ramca a returne values
        src_adr, dst_adr, frame_len_pcap, frame_len_medium, type_frame = pars_mac_and_len_and_type(hex_packet, count)

        #IEE, ETHERNET2
        frame = creator.search_eth_type_dic(type_frame, count, src_adr, dst_adr,frame_len_pcap, frame_len_medium, hex_packet)

        if "protocol" in frame.__dict__ and frame.protocol == "ICMP":
            frame.set_raw_frame(hex_packet)
            icmp_list.append(frame)


        count += 1

    count=1
    for pckt in icmp_list:
        print(pckt.frame_number, pckt.protocol, pckt.m_f, sep="\n")
        count+=1

    complete, uncomplete = find_icmp_com(icmp_list, icmp_dir)

def arp(path_pcap):
    count = 1

    arp_dic = dic_4["arp_flags"]

    arp_list = []

    for packet in packets:
        hex_packet = hexlify(raw(packet)).decode('utf-8')

        #parsne mac adresy, dlzku ramca, typ ramca a returne values
        src_adr, dst_adr, frame_len_pcap, frame_len_medium, type_frame = pars_mac_and_len_and_type(hex_packet, count)

        #IEE, ETHERNET2
        frame = creator.search_eth_type_dic(type_frame, count, src_adr, dst_adr,frame_len_pcap, frame_len_medium, hex_packet)

        if "ether_type" in frame.__dict__ and frame.ether_type == "ARP":
            arp_list.append(frame)
            frame.set_raw_frame(hex_packet)
            frame.set_op_code_arp(arp_dic)
        count += 1

    complete = find_com_arp(arp_list, arp_dic)
    if not complete:
        return
    yaml_path = "output/" + path_pcap[0: len(path_pcap)-4] + "yaml"

    writer.init_ul_4d(yaml_path, path_pcap, "ARP")
    writer.write_in_file_4d(complete)
     



decider = {
    "HTTP": ipv4_tcp,
    "HTTPS": ipv4_tcp, 
    "TELNET": ipv4_tcp, 
    "SSH": ipv4_tcp, 
    "FTP-DATA": ipv4_tcp,
    "FTP-CONTROL": ipv4_tcp,
    "TFTP": ipv4_udp,
    "ARP": arp,
    "ICMP": icmp
}

while (True):
    
    path_pcap = input("write name of trace: ")


    if path_pcap == "exit":
        exit("Thanks for using")    
    try:
        packets = rdpcap("vzorky/"+path_pcap)

    except FileNotFoundError:
        print("File not found")
        continue


    output_path = "output/" + path_pcap

    protocol = input("write a protocol: ")

    if protocol not in know_app_protocol:
        print("Wrong protocol")
        while(True):
            protocol = input("try again: ")
            if protocol not in know_app_protocol:
                continue
            else:
                break 


    elif protocol in tcp_protocols:
        decider[protocol](protocol, path_pcap)
    else:
        decider[protocol](path_pcap)

    print("run succesfull")



# for packet in packets:
#     hex_packet = hexlify(raw(packet)).decode('utf-8')

#     #parsne mac adresy, dlzku ramca, typ ramca a returne values
#     src_adr, dst_adr, frame_len_pcap, frame_len_medium, type_frame = pars_mac_and_len_and_type(hex_packet, count)

#     #IEE, ETHERNET2
#     frame = creator.search_eth_type_dic(type_frame, count, src_adr, dst_adr,frame_len_pcap, frame_len_medium, hex_packet)

#     if "protocol" in frame.__dict__ and frame.protocol == "TCP" and "app_protocol" in frame.__dict__ and frame.app_protocol == app_protocol:
#         ipv4_tcp.append(frame)
    
#     if "protocol" in frame.__dict__ and frame.protocol == "UDP" and "app_protocol" in frame.__dict__ and frame.app_protocol == "TFTP":
#         ipv4_udp.append(frame)

    
#     count += 1

# yaml_path = "output/" + path_pcap[0: len(path_pcap)-4] + "yaml"

# #len tcp komunikacia
# find_openings_endings(ipv4_tcp, op_samples, cl_samples)

# writer.init(yaml_path ,path_pcap)
# writer.add_frames_in_dir(list_packets)
# writer.add_senders_in_dir(max_send_packets_by, ipv4_senders)
# writer.write_in_yaml()