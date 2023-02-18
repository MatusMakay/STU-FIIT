import socket
import struct
import zlib

from exc.comunication_exceed import ComunicationExceed
from exc.damaged_packet import DamagedPacket
from exc.switch import SwitchRoles

from utils.utils import wait_for_packet_init


class Socket:
    """
    Handles operation: creating headers, waiting for answers, unpack packet
    Holds const: LEN_HEADER, LEN_WINDOW, RECEIVER_ADRESS_PORT, SENDER_ADRESS_PORT  
    """
    #       CONST

    #   rec = "127.0.0.2"
    #   sen = "127.0.0.1"
    # RECEIVER_IP = "1.1.1.3"
    # SENDER_IP = "1.1.1.2"

    RECEIVER_IP = "127.0.0.2"
    SENDER_IP =  "127.0.0.1"

    # 46B => other headers
    # LEN_HEADER = len_my_header + len_ip_udp_eth_checksum
    #   TODO HLAVICKA MA NAKONIEC DLZKU 12
    LEN_HEADER_ALL = 12 + 46
    LEN_HEADER_MY = 12

    def __init__(self, type, ):
        self.type = type
        if type == "s":
            ip = self.SENDER_IP
        if type == "r":
            ip = self.RECEIVER_IP
            
        self.ip = ip
        self.port = ""
        # data receiving, sending socket
        self.conf = ()
        self.iskeepalive = True


    def set_my_ip_port(self):
        self.conf = self.my_socket.getsockname()
        self.ip = self.conf[0]
        self.port = self.conf[1]
        
             
    def print_info(self, flag, obj):
        #receiver 
        if flag == "r":
            print(f"{obj.ip}/{obj.port}")
            print(f"Receiver is ready and listening on:\nIP: {obj.ip}\nPORT:{obj.port}")

        elif flag == "ssw":
            print(f"{obj.ip}/{obj.port}")
            print(f"Receiver is ready and listening on:\nIP: {obj.ip}\nPORT:{obj.port}")
 
        elif flag == "rsw":
            print(f"{obj.ip}/{obj.port}")
            print(f"Sender is ready on:\nIP: {obj.ip}\nPORT:{obj.port}")

        elif flag == "s":
            print(f"{obj.ip}/{obj.port}")
            print(f"Sender is ready on:\nIP: {obj.ip}\nPORT:{obj.port}")

    def set_port(self, port):
        if self.type == "r":
            self.RECEIVER_PORT = port
        else:
             self.SENDER_PORT = port
        self.port = port
        self.conf = (self.ip, self.port)

    

    def set_my_sockets(self, sockets):
        self.my_socket = sockets[0]
        self.keep_alive_socket = sockets[1]
    
    def get_sockets(self):
        return (self.my_socket, self.keep_alive_socket)

    def get_conf(self):
        return self.conf

    def switch_roles(self, isSender, dest_port):
        ipt = input("Do you want a switch roles?\nY/N\n")
        ip = self.my_socket.getsockname()[0]
        
        if ipt == "y" or ipt == "Y" and isSender:
            header = self.create_header("sw", 0, 0)
            packet = self.pack_packet(header, "".encode())
            
            if ip == self.SENDER_IP:
                self.my_socket.sendto(packet, (self.RECEIVER_IP, dest_port))
            else: 
                self.my_socket.sendto(packet, (self.SENDER_IP, dest_port))
            raise SwitchRoles(1234)


#               PACKET UNPACKING

    def unpack_acknowledgment_packet(self, packet):
        udp_header_encoded = packet[0 : self.LEN_HEADER_MY]
        udp_header_decoded = struct.unpack("2s II", udp_header_encoded)
        #   return flags
        return udp_header_decoded[0].decode()


    def unpack_data_packet(self, packet):
        udp_header_encoded = packet[0: self.LEN_HEADER_MY]
        udp_header_decoded = struct.unpack("s II", udp_header_encoded)
        data = packet[self.LEN_HEADER_MY: ]
        
        checksum_get = udp_header_decoded[2]
        checksum_calc = zlib.crc32(data)

        pckt_num = udp_header_decoded[1]
        
        if checksum_get != checksum_calc:
            raise DamagedPacket
        
        # return flags and data
        return udp_header_decoded[0].decode(), pckt_num, data

    def unpack_init_file_packet(self, packet):
        
        udp_header_encoded = packet[0: self.LEN_HEADER_MY]
        udp_header_decoded = struct.unpack("2s II", udp_header_encoded)

        file_name = packet[ self.LEN_HEADER_MY :]

        checksum_get = udp_header_decoded[2]
        checksum_calc = zlib.crc32(file_name)
        
        if checksum_get != checksum_calc:
            raise DamagedPacket

        flags = udp_header_decoded[0]
        buffer_size = udp_header_decoded[1]

        #   return file_name, flags, and len of data which will comming in comunication
        return file_name.decode(), flags.decode(), buffer_size

    def unpack_init_packet(self, packet):
        
        udp_header = packet[0: self.LEN_HEADER_MY]

        udp_header_encoded = struct.unpack("s II", udp_header)

        flags = udp_header_encoded[0]

        return flags.decode()

#               HEADER CREATING

#        FLAGS: Init. com: a=accept, s=start
#        Data: 0 Data NonF, 1 Data F
#       


    def switch(self, case, pckt_num, checksum):
        """
        Input: Header data, 
        Output: Encoded Header 
        """

        udp_header = ""
        if case == "a":
            udp_header = struct.pack("cx II", str.encode("a"), pckt_num, checksum)
        elif case == "i":
            udp_header = struct.pack("cx II", str.encode("i"), pckt_num, checksum)
        elif case == "s0":
            udp_header = struct.pack("2s II", str.encode("s0"), pckt_num + self.LEN_HEADER_MY, checksum)
        elif case == "s1":
            udp_header = struct.pack("2s II", str.encode("s1"), pckt_num + self.LEN_HEADER_MY, checksum)
        elif case == "f0":
            udp_header = struct.pack("2s II", str.encode("f0"), pckt_num + self.LEN_HEADER_MY,  checksum)
        elif case == "f1":
            udp_header = struct.pack("2s II", str.encode("f1"), pckt_num + self.LEN_HEADER_MY , checksum)
        elif case == "0":
            udp_header = struct.pack("cx II", str.encode("0"), pckt_num, checksum)
        elif case == "1":
            udp_header = struct.pack("cx II", str.encode("1"), pckt_num, checksum)
        #   keep alive
        elif case == "k":
            udp_header = struct.pack("cx II", str.encode("k"), pckt_num, checksum)
        elif case == "ex":
            udp_header = struct.pack("2s II", str.encode("ex"), pckt_num, checksum)
        #   acknowledgment of data packet
        elif case == "ac":
            udp_header = struct.pack("2s II", str.encode("ac"), pckt_num, checksum)
        #   switch roles
        elif case == "sw":
            udp_header = struct.pack("2s II", str.encode("sw"), pckt_num, checksum)
        elif case == "fa":
            udp_header = struct.pack("2s II", str.encode("fa"), pckt_num, checksum)
        return udp_header

    def pack_packet(self, header, data):
        packet = header + data
        return packet
            

    def create_header(self, flags ,pckt_num , checksum):
        """
        Input: Flags, pckt_num. 
        Cases: a-accept, s-start, f-sending file, 00-nonf message, 01-f message, 10-nonf file, 11-f file, f10- file in one packet
        Output: Encoded Header
        """
        return self.switch(flags, pckt_num, checksum)
        
#               COMUNICATION HANDLING
    def wait_for_packet(self, socket, buffer_size, timeout):
        """
        Now use only on starting communication, or when you should check if comunication is still alive
        Sender will use it
        """
        try:
            packet, sender_adress_port =  wait_for_packet_init(socket, buffer_size, timeout)
            return packet, sender_adress_port
        except TypeError as t:
            if t.__str__() == "catching classes that do not inherit from BaseException is not allowed":
                raise ComunicationExceed("End point didn't answer!\nWrite IP adress and port again!")


