from Frame import Frame

class IeeeFrame(Frame):
    
    def __init__(self, frame_type_dic, sap_dic, pid_dic, count, src_adr, dst_adr, len_pycap, len_medium, hex_frame, hex_code):
        self.hexa_frame = super().set_hex_frame(hex_frame)
        
        self.frame_type_dic = frame_type_dic
        self.sap_dic = sap_dic
        self.pid_dic = pid_dic
        
        self.frame_number = count
        self.src_mac = src_adr
        self.dst_mac = dst_adr
        self.len_frame_pcap = len_pycap
        self.len_frame_medium = len_medium
        
        self.set_frame_type(hex_code, hex_frame)

    def set_pid(self, hex_frame):
        # ak nebude na tomto, skus 90:94
        try:
            self.pid = self.pid_dic[hex_frame[40:44]]
        except KeyError:
            self.pid = False

    def set_sap(self, hex_frame):
        try:
            self.sap = self.sap_dic[hex_frame[28:32]]
        except KeyError:
            self.sap = False

    def set_frame_type(self, hex_code, hex_frame):
        self.frame_type = "IEEE 802.3 "

        try:
            self.frame_type += self.frame_type_dic[hex_code]

        except KeyError:
            self.frame_type += "LLC"
            self.set_sap(hex_frame)
        
        if self.frame_type == "IEEE 802.3 LLC & SNAP":
            self.set_pid(hex_frame)
        
    