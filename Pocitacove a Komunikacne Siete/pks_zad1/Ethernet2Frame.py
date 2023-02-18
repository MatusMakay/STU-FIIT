from pickle import FALSE
from Frame import Frame 

class Ethernet2Frame(Frame):

    def __init__(self, eth_type_dic, protocols_dic, app_protocols_dic, count, src_adr, dst_adr, len_pycap, len_medium, hex_frame):
        self.hexa_frame = super().set_hex_frame(hex_frame)

        self.eth_type_dic = eth_type_dic
        self.protocols_dic = protocols_dic
        self.app_protocols_dic = app_protocols_dic

        self.frame_number = count
        self.src_mac = src_adr
        self.dst_mac = dst_adr
        self.len_frame_pcap = len_pycap
        self.len_frame_medium = len_medium
        self.frame_type = "ETHERNET II"
        
        self.set_ether_type(hex_frame)

        self.cr_ether_type_dic = {
            #nezistujes ip_adresy
            "LLDP": self.cr_lldp_etcp_ipv6,
            "ECTP": self.cr_lldp_etcp_ipv6,
            "Ipv6": self.cr_lldp_etcp_ipv6,
            #ip na inom mieste
            "ARP": self.cr_arp,
            #ipv4
            "Ipv4": self.cr_ipv4,
        }
        self.arp_dic = {
            "0002": "reply",
            "0001": "request"
        }
        self.set_raw_frame(hex_frame)
        self.create_frame(self.cr_ether_type_dic, hex_frame)
    
    def convert_hex_dec(self, hex):
        return str(int(hex, base=16))

    def set_ether_type(self, hex_frame):
        #13-14B
        try:
            self.ether_type = self.eth_type_dic[hex_frame[24:28]]
        except KeyError:
            self.ether_type = False
    
    def create_frame(self, dic, hex_frame):
        if(self.ether_type != False):
            dic[self.ether_type](hex_frame)

    def cr_lldp_etcp_ipv6(self, hex_frame):
        pass

    def set_ip_arp(self, hex_frame):
        #ARP IP ADRESY rozdiel oproti klasik 
        #src_ip = 29 - 32B 56b po 64b
        #dst_ip = 39 - 42B 76b po 84b
        self.src_ip = self.convert_hex_dec(hex_frame[56:58]) + "." + self.convert_hex_dec(hex_frame[58:60]) + "."+ self.convert_hex_dec(hex_frame[60:62]) + "." + self.convert_hex_dec(hex_frame[62:64]) 
        self.dst_ip = self.convert_hex_dec(hex_frame[76:78]) + "." + self.convert_hex_dec(hex_frame[78:80]) + "." + self.convert_hex_dec(hex_frame[80:82]) + "." + self.convert_hex_dec(hex_frame[82:84])

    def cr_arp(self, hex_frame):
        self.set_ip_arp(hex_frame)
        self.set_op_code_arp(self.arp_dic)

    def set_app_protocol(self):

        if str(self.dst_port) in self.app_protocols_dic.keys():
            self.app_protocol = self.app_protocols_dic[str(self.dst_port)]

        elif str(self.src_port) in self.app_protocols_dic.keys():
            self.app_protocol = self.app_protocols_dic[str(self.src_port)]

        else:
            self.app_protocol = False

    def set_port(self, hex_frame):
        #35-36B
        if self.protocol == "TCP" or self.protocol == "UDP" :
            self.src_port = int(self.convert_hex_dec(hex_frame[68:72]))
            self.dst_port = int(self.convert_hex_dec(hex_frame[72:76]))
            self.set_app_protocol()
            return

        self.src_port = False
        self.dst_port = False
        self.app_protocol = False
        
    def set_ip_ipv4(self, hex_frame):
        #27-30
        self.src_ip = self.convert_hex_dec(hex_frame[52:54]) + "." + self.convert_hex_dec(hex_frame[54:56]) + "."+ self.convert_hex_dec(hex_frame[56:58]) + "." + self.convert_hex_dec(hex_frame[58:60]) 
        self.dst_ip = self.convert_hex_dec(hex_frame[60:62]) + "." + self.convert_hex_dec(hex_frame[62:64]) + "." + self.convert_hex_dec(hex_frame[64:66]) + "." + self.convert_hex_dec(hex_frame[66:68])


    def set_via_argument(self, type):
        self.icmp_type = type

    def set_type(self, hex ,dic):
        try:
            self.icmp_type = dic[hex[68:70]]

        except KeyError:
            self.icmp_type = "partial"
            raise KeyError
        
    def set_icmp_atr(self, hex_frame):
        #ak je fragment vacsi ako 0 potom
        #19-20B id
        #21-22B flags
        #
        self.id = int(hex_frame[36:40], 16)
        flags = bin(int(hex_frame[40:44], 16))[2:].zfill(16)
        
        self.icmp_flags = flags[0:3]
        frag_offset = flags[3:len(flags)]
        self.frag_offset = int(frag_offset, 2)

        m_f = flags[2]

        if m_f == "1":
            self.m_f = True
        else:
            self.m_f = False

    def set_protocol(self, hex_frame):
        #24B
        try:    
            self.protocol = self.protocols_dic[hex_frame[46:48]]
        except KeyError:
            self.protocol = False
        
        if self.protocol == "ICMP":
            self.set_icmp_atr(hex_frame)
    #4a tcp comm
    def set_flags(self, hex_frame):
        flags = hex_frame[92:96]
        
        try: 
            binary = bin(int(flags, base=16))
        except ValueError:
            return 

        l = len(binary)
        
        self.flags = binary[l - 5: l]  

    def cr_ipv4(self, hex_frame):
        self.set_protocol(hex_frame)
        self.set_ip_ipv4(hex_frame)
        self.set_port(hex_frame)
        self.set_flags(hex_frame)

    #21-22B
    def set_op_code_arp(self, arp_dic):
        try:
            self.opcode = arp_dic[self.raw_hex[40:44]]
        except KeyError:
            pass
            
    #4b udp comm
    def set_op_code_udp(self, udp_dic):
        self.opcode = udp_dic[self.raw_hex[84:88]]
    def set_raw_frame(self, raw_frame):
        self.raw_hex = raw_frame
    # def __str__(self):
    #     return f"num: {self.frame_number}\nsrc_adr: {self.src_mac} ndst_adr: {self.dst_mac}\nframe_len_pcap: {self.len_frame_pcap}\nframe_len_medium: {self.len_frame_medium}\nframe_type: {self.frame_type}\nprotocol: {self.ether_type}\n src_ip: {self.src_ip} \n dst_ip: {self.dst_ip} \nprotocol: {self.protocol} \n src_port: {self.src_port} \ndst_port: {self.dst_port} \n app_protocol: {self.app_protocol} \nhex_frame : \n{self.hexa_frame}\n"
    


    