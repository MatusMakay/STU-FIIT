import time
import os
import threading
import random

from ctypes import *
import zlib


from classes.socket import Socket
from exc.comunication_exceed import ComunicationExceed
from exc.communication_end import CommunicationEnd
from exc.switch import SwitchRoles

from utils.utils import input_adr_port, input_sender_menu, input_same_length_sending_packet, input_damage_packets

class Sender(Socket):
    
    def __init__(self, flag):
        super().__init__(flag)
        self.who = "sender"

    

    def keep_alive(self, receiver_adress_port, flag):
        """
        Sends a keep alive packets on individual socket and port with receiver.
        When receiver didn't respond in five second ComunicationExceed is raised.
        """
        count = 0
        while(True):
            
            self.iskeepalive = True

            if not flag[0]:
                return

            header = super().create_header("k", 1, 0)
            packet = super().pack_packet(header, "".encode())

            self.keep_alive_socket.sendto(packet, receiver_adress_port)

            time.sleep(3)
            try:
                packet, conf = super().wait_for_packet(self.keep_alive_socket, 1024, 6.0)

                flags = super().unpack_init_packet(packet)

                count = 0
            except ComunicationExceed:
                count += 1

                if count == 4:
                    print("Keep-alive other side is not responding!\nComunication ENDS!\n")
                    self.iskeepalive = False
                    return
                continue

            if flags != "a":
                raise ComunicationExceed

#   Comunication Handling methods
    def check_inputs(self):
        """
        TODO skontroluj ci su vsetky data ziskane s input_sender_menu v poriadku
        """
        pass
            
    def sender_menu(self, receiver_adress_port):
        """
        Sets class atributes, which are taken from user
        """
        
        end = input("Do you want to continue in communication?\nY/N\n")

        if end == "n" or end =="N":
            print("Program ends!")
            self.flag_keep_alive[0] = False
            header = super().create_header("ex", 1, 0)
            packet = super().pack_packet(header, "".encode())
            self.my_socket.sendto(packet, receiver_adress_port)
            exit(5)

        inputs = input_sender_menu(os.getcwd()+"/src/classes")

        if self.iskeepalive: 
            flags = inputs[0]

            if flags == '10':
                self.__dict__["type_mess"] =  "File"
                self.__dict__["fragment"] =  False
                self.__dict__["file_name"] = inputs[1]
            elif flags == "11":
                self.__dict__["type_mess"] =  "File"
                self.__dict__["fragment"] =  True
                self.__dict__["file_name"] = inputs[1]
                self.__dict__["size_packet"] = inputs[2]
            elif flags == "00":
                self.__dict__["type_mess"] =  "Text"
                self.__dict__["fragment"] =  False
            elif flags == "01":
                self.__dict__["type_mess"] =  "Text"
                self.__dict__["fragment"] =  True
                self.__dict__["size_packet"] =  inputs[1]
        else:
            raise ComunicationExceed

    def find_full_path(self, file_name, dir_path):
        """
        Find file by names in specific directory and return full path
        """

        find = False
        # Iterate directory
        for path in os.listdir(dir_path):
            # check if current path is a file
            

            if os.path.isfile(os.path.join(dir_path, path)):
                if path == file_name:
                    return dir_path + "/" + path

        if find == False:
            raise FileNotFoundError

    def pick_random_idx(self, len_limit, idx_limit):
        idx_list = []

        print("Indexes of damaged packets will be:", end=" ")
        while(len(idx_list) != len_limit):
            ran = random.randint(0, idx_limit - 1)
            try:
                idx_list.index(ran)
            except ValueError:
                print(ran+1, end=", ")
                idx_list.append(ran)

        print("")
        idx_list.sort()
        return idx_list

    def divide_round_up(self, file_size, buffer_size):
        return int(file_size / buffer_size) + (file_size % buffer_size > 0)

    def create_init_header(self, buffer_size, checksum, num_pckt_to_send):
        if self.type_mess == "File":
            if num_pckt_to_send == 1:
                header = super().create_header("f0", buffer_size, checksum)
            else:
                header = super().create_header("f1", buffer_size, checksum)
        else:
            if num_pckt_to_send == 1:
                header = super().create_header("s0", buffer_size, checksum)
            else:
                    header = super().create_header("s1", buffer_size, checksum)

        return header

    def handle_damaged_packets(self, num_pckt_to_send):
        num_dmg_packets = input_damage_packets(num_pckt_to_send)
        index_dmg_packets = []
        if num_dmg_packets > 0:
            index_dmg_packets = []
            index_dmg_packets = self.pick_random_idx(num_dmg_packets, num_pckt_to_send)

        return num_dmg_packets, index_dmg_packets

    def calc_num_packets_to_send(self, file_name, buffer_size):
        directory = os.getcwd() + "/src/classes/files/send"
        full_path = self.find_full_path(file_name, directory)
        print(f"Full path to file: {full_path}\n")
        file_size = os.path.getsize(full_path)
        #   zaokruhlenie nahor
        num_pckt_to_send = self.divide_round_up(file_size, buffer_size)

        return full_path, num_pckt_to_send

    def print_statistic(self, count_good, count_bad ,full_path, size_pckt, size_last_pckt):
        print(f"""Num. packets received: {count_good-1}\nNum. damaged packets received: {count_bad}\nSize: {os.path.getsize(full_path)}\nSize packets: {size_pckt}\nSize last packet: {size_last_pckt}\n""")


    def send_file(self, receiver_adress_port, flags ,file_name, buffer_size):
        """
        TODO ADD LISTENING ON DAMAGE PACKETS AND SEND THEM AGAIN;
        Open file read and send data chunks with size of buffer_size to receiver
        """
        try:
            full_path ,num_pckt_to_send  = self.calc_num_packets_to_send(file_name, buffer_size)

            if num_pckt_to_send != 1:
                flags = "1"
        
            print(f"Packets to send: {num_pckt_to_send}")
            num_dmg_packets, index_dmg_packets = self.handle_damaged_packets(num_pckt_to_send)
            
            #send first packet with file_name
            checksum = zlib.crc32(file_name.encode())
            header = ""
            #informujem receivera paketom ci poslem len jeden paket s celymi datami alebo budem spravu fragmentovat
            header = self.create_init_header(buffer_size, checksum, num_pckt_to_send)
            packet = super().pack_packet(header, file_name.encode())
            self.my_socket.sendto(packet, receiver_adress_port)
            
            middle_pckt = self.divide_round_up(num_pckt_to_send, 2)

            with open(full_path, "rb") as f:

                count_pckt = 1
                count_bad = 0
                move = 0
                chunk = f.read(buffer_size)

                while chunk and self.iskeepalive:
                    print(f"{count_pckt}: Sended packet")
                    
                    if count_pckt == 1:
                        len_pckt = len(chunk)

                    #damage packet 
                    if num_dmg_packets > 0 and move != num_dmg_packets and count_pckt-1 == index_dmg_packets[move]:
                        move+=1
                        fake_data = b"fake data"
                        checksum = zlib.crc32(fake_data)
                    else:
                        checksum = zlib.crc32(chunk)

                    len_data = len(chunk)
                    
                    #   Rozhoduje ci posiela posledny paket alebo este bude posielat dalsie
                    if count_pckt < num_pckt_to_send:
                        header = super().create_header(flags, count_pckt, checksum)
                    else:
                        len_last_pckt = len(chunk)
                        header = super().create_header("0", count_pckt, checksum)

                    packet = super().pack_packet(header, chunk)

                    self.my_socket.sendto(packet, receiver_adress_port)

                    try: 
                        data, conf = super().wait_for_packet(self.my_socket, 1200, 5.0)
                        ack_flags = super().unpack_acknowledgment_packet(data)
                        
                        if ack_flags == "fa":
                            print("Sending init file again! ")
                            self.send_file(receiver_adress_port, flags, file_name, buffer_size)
                            
                            return 

                        if ack_flags != "ac":
                            raise ComunicationExceed


                    except ComunicationExceed:
                        if not self.iskeepalive:
                            print("Keep alive is not responding!")
                            raise ComunicationExceed

                        print(f"Acknowledgedment didn't received for packet: {count_pckt}\nSending again!\n")
                        count_bad += 1
                        continue
                    

                    if count_pckt != 1 and count_pckt == middle_pckt :   
                        buffer_size = input_same_length_sending_packet(buffer_size)

                    chunk = f.read(buffer_size)
                    count_pckt += 1

                print("Stopped sending File")
                print("")

                if not self.iskeepalive:
                    print("Keep alive is not responding!")
                    raise ComunicationExceed  

                self.print_statistic(count_pckt, count_bad, full_path, len_pckt, len_last_pckt )

        except FileNotFoundError:
            print("File Does Not Exist! \n")
            path = input("Write file name again\n")
            self.send_file(receiver_adress_port, flags, path, buffer_size)

    def communication(self, receiver_adress_port):
        """
        Send data to receiver. If fragment is set then the data will be split.
        Len of whole packet = len_header-cacl with c_function + len(data)
        """
        try: 
            # can raire CommunicationEnd 
            self.sender_menu(receiver_adress_port)

            if  self.type_mess == "File":
                if self.fragment:
                    self.send_file(receiver_adress_port, "1" ,self.file_name, self.size_packet)
                
                else:
                    self.send_file(receiver_adress_port, "0" ,self.file_name, 1460)
            else:
                if self.fragment:
                    self.send_file(receiver_adress_port, "1" ,"message.txt", self.size_packet)
                else:
                    self.send_file(receiver_adress_port, "0" ,"message.txt", 1460)
            
            super().switch_roles(True, receiver_adress_port[1])
            self.communication(receiver_adress_port)
        except CommunicationEnd:
            header = super().create_header("ex", 1, 0)
            packet = super().pack_packet(header, "".encode())
            self.my_socket.sendto(packet, receiver_adress_port)
            raise CommunicationEnd

    def run_keep_alive(self, flag, port):
        keep_alive = (super().RECEIVER_IP, port + 1)
        threading.Thread(target=self.keep_alive, args=(keep_alive, flag)).start()

#   Start Com. methods
    def send_start_com_packet(self, receiver_adress_port, flag):
        
        header= super().create_header("i", 0, 0)

        packet = super().pack_packet(header, "".encode())

        ipt = input("Do you want to send init packet?\nY/N\n")
        

        if ipt == "Y" or ipt == "y":
            
            self.my_socket.sendto(packet, receiver_adress_port)

            packet, conf = super().wait_for_packet(self.my_socket, 64, 15.0)

            flags = super().unpack_init_packet(packet)


            if flags == 'a':
                print("\nSender: Comunication Established!")
                print("You can send packets now!\n")
                self.run_keep_alive(flag, receiver_adress_port[1])
                self.communication(receiver_adress_port)
            else:
                raise ComunicationExceed("Wrong packet")
            
        else:
            print("Program ends!")
            exit(1)
        
    def set_other_side(self, other_side):
        self.receiver_adress_port = other_side


    def start(self, flag, count):
        # if not flag:
            # print("\nWrite Adress and Port of endpoint:")
            # end_ip, end_port = input_adr_port()
        ip = self.my_socket.getsockname()[0]
        self.flag_keep_alive = [True, False]
        try:
            if not flag:
                self.receiver_port = int(input("Write receiver port. \n"))
                self.send_start_com_packet((super().RECEIVER_IP, self.receiver_port), self.flag_keep_alive)
            else:
                if ip == super().SENDER_IP:
                    self.communication(self.receiver_adress_port)
                else: 
                    self.communication(self.receiver_adress_port)

            super().switch_roles(True, self.receiver_port)
            self.start(True, count)

        except ComunicationExceed:
            print("End point didn't answer. Comunication ends")
            print("\nSender is ready to send init packet!")
            self.start(False, 0)
       
        # except SwitchRoles:
        #     flag_keep_alive[0] = False
        #     time.sleep(10)
        #     raise SwitchRoles


        except OSError as e:
            print(e)
            print("Something went wrong. Try again")
            self.start(False)
        
        except CommunicationEnd:
            print("Communication was succesfuly ended")
            flag_keep_alive[0] = False
            print("Waiting ...")
            time.sleep(15)
            self.start(False, count)