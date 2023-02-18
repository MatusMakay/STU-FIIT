from IeeeFrame import IeeeFrame
from Ethernet2Frame import Ethernet2Frame

class FrameCreator:
    """Call search_eth_type_dic to access directory. Via directory creating classes IEEE or Ethernet2. """
    def __init__(self, eth_dics, ieee_dics):

        self.dic = {
            "IEEE 802.3": self.cr_ieee_frame,
            "ETHERNET II": self.cr_eth_frame,
        }
        #[ipv]
        self.eth_dics = eth_dics
        #[eth_type, sap, pid]
        self.ieee_dics = ieee_dics

        self.eth_l = 24
        self.eth_h = 28

        self.ieee_l = 28
        self.ieee_h = 30


    def search_eth_type_dic(self, type, count, src_adr, dst_adr, len_pycap, len_medium, hex_code):
        return self.dic[type](count, src_adr, dst_adr, len_pycap, len_medium, hex_code)

    def cr_eth_frame(self, count, src_adr, dst_adr, len_pycap, len_medium, hex_code):
        return Ethernet2Frame(self.eth_dics["ether_type"], self.eth_dics["protocols"], self.eth_dics["app_protocols"] ,count, src_adr, dst_adr, len_pycap, len_medium, hex_code)

    def cr_ieee_frame(self, count, src_adr, dst_adr, len_pycap, len_medium, hex_code):
        return IeeeFrame(self.ieee_dics["ether_type"], self.ieee_dics["sap"], self.ieee_dics["pid"], count, src_adr, dst_adr, len_pycap, len_medium, hex_code ,hex_code[self.ieee_l : self.ieee_h])


