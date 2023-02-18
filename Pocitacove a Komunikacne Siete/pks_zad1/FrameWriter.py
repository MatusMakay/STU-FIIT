from ruamel.yaml.representer import RoundTripRepresenter
from ruamel.yaml import YAML

def repr_str(dumper: RoundTripRepresenter, data: str):
        if '\n' in data:
            return dumper.represent_scalar('tag:yaml.org,2002:str', data, style='|')
        return dumper.represent_scalar('tag:yaml.org,2002:str', data)

class FrameWriter:
    """
        First call init function send it path where data should be writen and name of file where data come from. Then call function write_in_yaml and pass it class IEEE or ETHERNET2
        Contains eth_types dictionary[RAW, SNAP, LLC, ETHERNET2] and call function to create dictionary which will be writen in file
    """
    count = 1
    def __init__(self):
        self.dic = {
            "IEEE 802.3 RAW": self.create_dic_raw,
            "IEEE 802.3 LLC": self.create_dic_llc,
            "IEEE 802.3 LLC & SNAP": self.create_dic_llc_and_snap,
            "ETHERNET II": self.create_dic_eth,
        }

        self.eth_dic = {
            "LLDP": self.cr_lldp_etcp_ipv6,
            "ECTP": self.cr_lldp_etcp_ipv6,
            "Ipv6": self.cr_lldp_etcp_ipv6,
            #ip na inom mieste
            "ARP": self.cr_arp,
            #ipv4
            "Ipv4": self.cr_ipv4,
        }

        self.dic_wr_in_file = {}

        self.yaml = YAML()
        self.yaml.default_flow_style = False
        self.yaml.representer.add_representer(str, repr_str)


    def ret_str(s):
        return s

    def pop_none(self, list):
        for n in list:
            if n is None:
                list.remove(n)

    def cr_yaml_4c(self, node, idx):
        return {
            "number_comm": idx,
            "src_comm": node["sender_ip"],
            "dst_comm": node["receiver_ip"],
            "packets": node["packets"]
        }
    def write_in_file_4d(self, complete):
        self.count = 1
        
        for f in complete:
            packets = f["packets"]
            list_dic = []

            for pckt in packets:
                self.count += 1
                list_dic.append(self.dic[pckt.frame_type](pckt))
            f["packets"] = list_dic

        
        self.dic_wr_in_file["complete_comms"] = complete

        if len(complete) == 0:
            self.dic_wr_in_file.pop("complete_comms")

        with open(self.path, 'w') as fp:
            self.yaml.dump(self.dic_wr_in_file, fp)

    def init_ul_4d(self, path, filename, filter):
        self.path = path
        self.dic_wr_in_file = {
            "name" : "PKS2022/23",
            "pcap_name": filename,
            "filter_name" : filter,
            "complete_comms" : [],
        }

    def init_ul_4b(self, path, filename, filter):
        self.path = path
        self.dic_wr_in_file = {
            "name" : "PKS2022/23",
            "pcap_name": filename,
             "filter_name" : filter,
            "complete_comms" : [],
        }

    def cr_yaml_node_tftp(self, com, idx):
        return {
            "number_comm": idx,
            "src_comm": com["sender_ip"],
            "dst_comm": com["receiver_ip"],
            "packets": com["packets"]
        }

    def write_in_yaml_4b(self, communications):
        self.count = 1

        for f in communications:

            packets = f["packets"]
            list_dic = []

            for pckt in packets:
                self.count += 1
                list_dic.append(self.dic[pckt.frame_type](pckt))
            f["packets"] = list_dic

        final_list = []
        for idx,f in enumerate(communications):
            final_list.append(self.cr_yaml_node_tftp(f, idx+1))

        self.dic_wr_in_file["complete_comms"] = final_list
        if len(communications) == 0:
            self.dic_wr_in_file.pop("complete_comms")

        with open(self.path, 'w') as fp:
            self.yaml.dump(self.dic_wr_in_file, fp)

    def init_ul_1_2_3(self, path, filename):
        """Set initial header for a file."""
        self.path = path
        self.dic_wr_in_file = {
            "name" : "PKS2022/23",
            "pcap_name": filename,
        }

    def init_ul_4a(self, path, filename, filter):
        self.path = path
        self.dic_wr_in_file = {
            "name" : "PKS2022/23",
            "pcap_name": filename,
            "filter_name" : filter,
            "complete_comms" : [],
            "partial_comms": []
        }

    def write_in_yaml_4a(self, complete, uncomplete):
        
        self.count = 1

        for f in complete:
            packets = f["packets"]
            list_dic = []

            for pckt in packets:
                self.count += 1
                list_dic.append(self.dic[pckt.frame_type](pckt))
            list_dic.pop(0)
            f["packets"] = list_dic
        for f in uncomplete:
            packets = f["packets"]
            list_dic = []

            for pckt in packets:
                self.count += 1
                list_dic.append(self.dic[pckt.frame_type](pckt))
            
            f["packets"] = list_dic

       
        
        self.dic_wr_in_file["complete_comms"] = complete
        self.dic_wr_in_file["partial_comms"] = uncomplete

        if len(complete) == 0:
            self.dic_wr_in_file.pop("complete_comms")
        
        
        if len(uncomplete) == 0:
            self.dic_wr_in_file.pop("partial_comms")

        with open(self.path, 'w') as fp:
            self.yaml.dump(self.dic_wr_in_file, fp)

    def write_in_yaml_ul_1_2_3(self):
        with open(self.path, 'w') as fp:
            self.yaml.dump(self.dic_wr_in_file, fp)

    def add_frames_in_dir_ul_1_2_3(self, frames):
        """Via dictonary call function on basis frame.type. """

        list_dic = []

        for f in frames:
            self.count += 1
            list_dic.append(self.dic[f.frame_type](f))
        self.pop_none(list_dic)

    #add list of frames to dictionary which will     be written into yaml
        self.dic_wr_in_file["packets"] = list_dic
    
    def add_senders_in_dir(self, max_senders, senders):
        self.dic_wr_in_file["ipv4_senders"] = senders
        self.dic_wr_in_file["max_send_packets_by"] = max_senders

    def pop_false_el_dic(self, dic):
        
        keys_to_pop = []

        for key, value in dic.items():
            if value is False:
                keys_to_pop.append(key)
        
        for key in keys_to_pop:
            dic.pop(key)

    def create_dic_raw(self, frame):
        dic = {}
        dic["frame_number"] = frame.frame_number
        dic["len_frame_pcap"] = frame.len_frame_pcap
        dic["len_frame_medium"] = frame.len_frame_medium
        dic["frame_type"] = frame.frame_type
        dic["src_mac"] = frame.src_mac
        dic["dst_mac"] = frame.dst_mac
        dic["hexa_frame"] = frame.hexa_frame

        return dic

    def create_dic_llc_and_snap(self, frame):
        dic = {}
        dic["frame_number"] = frame.frame_number
        dic["len_frame_pcap"] = frame.len_frame_pcap
        dic["len_frame_medium"] = frame.len_frame_medium
        dic["frame_type"] = frame.frame_type
        dic["src_mac"] = frame.src_mac
        dic["dst_mac"] = frame.dst_mac
        dic["pid"] = frame.pid
        dic["hexa_frame"] = frame.hexa_frame
        
        self.pop_false_el_dic(dic)
      
        return dic

    def create_dic_llc(self, frame):
        dic = {}
        dic["frame_number"] = frame.frame_number
        dic["len_frame_pcap"] = frame.len_frame_pcap
        dic["len_frame_medium"] = frame.len_frame_medium
        dic["frame_type"] = frame.frame_type
        dic["src_mac"] = frame.src_mac
        dic["dst_mac"] = frame.dst_mac
        dic["sap"] = frame.sap
        dic["hexa_frame"] = frame.hexa_frame

        self.pop_false_el_dic(dic)
       
        return dic


    def cr_lldp_etcp_ipv6(self, frame):
        dic = {}
        dic["frame_number"] = frame.frame_number
        dic["len_frame_pcap"] = frame.len_frame_pcap
        dic["len_frame_medium"] = frame.len_frame_medium
        dic["frame_type"] = frame.frame_type
        dic["src_mac"] = frame.src_mac
        dic["dst_mac"] = frame.dst_mac.strip()
        dic["ether_type"] = frame.ether_type
        dic["hexa_frame"] = frame.hexa_frame
        return dic

    def cr_ipv4(self, frame):
        dic = {}
        dic["frame_number"] = frame.frame_number
        dic["len_frame_pcap"] = frame.len_frame_pcap
        dic["len_frame_medium"] = frame.len_frame_medium
        dic["frame_type"] = frame.frame_type
        dic["src_mac"] = frame.src_mac
        dic["dst_mac"] = frame.dst_mac.strip()
        dic["ether_type"] = frame.ether_type
        dic["src_ip"] = frame.src_ip
        dic["dst_ip"] = frame.dst_ip
        dic["protocol"] = frame.protocol
        dic["src_port"] = frame.src_port
        dic["dst_port"] = frame.dst_port
        dic["app_protocol"] = frame.app_protocol
        dic["hexa_frame"] = frame.hexa_frame

        self.pop_false_el_dic(dic)
        return dic

    def cr_arp(self, frame):
        dic = {}
        dic["frame_number"] = frame.frame_number
        dic["len_frame_pcap"] = frame.len_frame_pcap
        dic["len_frame_medium"] = frame.len_frame_medium
        dic["frame_type"] = frame.frame_type
        dic["src_mac"] = frame.src_mac
        dic["dst_mac"] = frame.dst_mac.strip()
        dic["ether_type"] = frame.ether_type
        dic["arp_opcode"] = frame.opcode
        dic["src_ip"] = frame.src_ip
        dic["dst_ip"] = frame.dst_ip
        dic["hexa_frame"] = frame.hexa_frame
        return dic

    def create_dic_eth(self, frame):
        try:
            return self.eth_dic[frame.ether_type](frame)
        except KeyError:
            print(frame.ether_type)
            return
            