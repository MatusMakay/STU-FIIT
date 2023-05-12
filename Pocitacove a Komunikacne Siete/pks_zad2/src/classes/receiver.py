import os
import threading
import time
from exc.switch import SwitchRoles 

from classes.socket import Socket

from exc.comunication_exceed import ComunicationExceed
from exc.damaged_packet import DamagedPacket
from exc.communication_end import CommunicationEnd

class Receiver(Socket):

    def __init__(self, bool_flag):
        super().__init__(bool_flag)
        self.who = "receiver"

    def keep_alive(self, init_flags):
        """
        Sends a keep alive packets on individual socket and port with receiver.
        When Sender didn't respond in five second ComunicationExceed is raised.
        """
        count = 0
        while(True):
            self.iskeepalive=True
            # if comunication end flag[False]
            if not init_flags[0]:
                return 

            try: 
                packet, receiver_adress_port = super().wait_for_packet(self.keep_alive_socket, 1024, 6.0)
                count = 0

            except ComunicationExceed:
                count += 1 
                if count == 4:
                    print("Keep-alive: other side didn't answered!\nComunication Ends!\n")
                    self.iskeepalive = False
                    return
                continue
            

            flag = super().unpack_init_packet(packet)

            if flag != "k":
                raise ComunicationExceed

            time.sleep(3)

            header = super().create_header("a", 1, 0)
            packet = super().pack_packet(header, "".encode())

            self.keep_alive_socket.sendto(packet, receiver_adress_port)

    def print_statistic(self, count_good, count_bad ,full_path, size_pckt, size_last_pckt):
        print(f"""Num. packets received: {count_good-1}\nNum. damaged packets received: {count_bad}\nSize: {os.path.getsize(full_path)}\nSize packets: {size_pckt}\nSize last packet: {size_last_pckt}\n""")

    def print_received_message(self, full_path):
        with open(full_path, "r") as f:
            message = f.read()
        
        print(f"\nReceived message: {message} ")

    def send_acknowledgment(self, conf):
        header = super().create_header("ac", 1, 0)
        packet = super().pack_packet(header, "".encode())

        self.my_socket.sendto(packet, conf)

    def listen_for_incomming_packets(self):
        # 1. Get packet with file_name
        # 2. open file with received name in mode ab
        # 3. in while loop: a) check if data are okey, b) apend in file
        # 4. close file
        # 5. print statistics about received packets
        # checksum 
        try:
            #   can raise ComunicationExceed
            packet, conf = super().wait_for_packet(self.my_socket, 1024, 90.0)
            act_directory = os.getcwd() + "/src/"
            #   can raise DamagedPacket
            file_name, init_flags, buffer_size = super().unpack_init_file_packet(packet)
            
            if init_flags == "sw":
               self.other_conf = conf            
               raise SwitchRoles(conf)
            if init_flags == "ex":
                raise CommunicationEnd

            if init_flags != "s1" and init_flags != "s0" and init_flags != "f1" and init_flags != "f0":
                print("Didn't received init file packet!\nSend it again!")
                header = super().create_header("fa", 1, 0)
                packet = super().pack_packet(header, "".encode())
                
                data, conf = super().wait_for_packet(self.my_socket, 1000, 6.0)
                
                self.my_socket.sendto(conf, packet)

                self.listen_for_incomming_packets()
                return
            
        except ComunicationExceed:
            if not self.iskeepalive:
                print("Keep alive is not responding!")
                raise ComunicationExceed 

            ipt = input("Sender exceeded time to send init message packet!\nDo you want to stay connected?\nY/N\n")
            if ipt == "Y" or ipt == "y":
                print("Waiting on init file packet...")
                self.listen_for_incomming_packets()
                return
            else:
                print("Receiver isn't listening for init packets!")
                raise ComunicationExceed

        full_path = act_directory  + "classes/files/received/" + file_name
        print("Full path to a file: " + full_path)

        #   clean file first
        with open(full_path, "w") as f:
            f.write("")

        pckt_idx = []

        if init_flags == "f1" or init_flags == "s1" or init_flags == "f0" or init_flags == "s0":
            
            with open(full_path, "ab") as f:
                
                print("Open file!")
                
                flags = "1"
                count_good = 1
                count_damaged = 0
                

                while flags == "1" and self.iskeepalive:
                    print(count_good ,": Writing in file!")

                    count_good+=1

                    try:
                        #   can raise ComunicationExceed
                        # packet, conf = super().wait_for_packet(self.my_socket, buffer_size)
                        packet, conf = super().wait_for_packet(self.my_socket, buffer_size, 15.0)#self.my_socket.recvfrom(buffer_size)

                        #   can raise DamagedPacket
                        flags, pckt_num ,chunk = super().unpack_data_packet(packet)
                        
                        pckt_idx.append(pckt_num)                    

                    except ComunicationExceed:
                        print(f"Timer timed out! Waiting on packet number: {count_good-1} again!")

                        if self.iskeepalive:
                            count_good-=1
                            continue
                        print("Keep alive is not responding!")
                        raise ComunicationExceed
                        

                    except DamagedPacket:
                        print(f"Packet number: {count_good-1} was received damaged!\n")
                        count_damaged+=1
                        count_good-=1
                        flags = "1"
                        continue

                    self.send_acknowledgment(conf)
                    
                    if count_good == 2:
                        size_pckt = len(chunk)

                    if pckt_idx.count(pckt_num) == 1:
                        f.write(chunk)
            
            print("Close File\n")
        
        if not self.iskeepalive:
            print("Keep alive is not responding!")
            raise ComunicationExceed 

        size_last_pckt = len(chunk)
        self.print_statistic(count_good, count_damaged, full_path, size_pckt, size_last_pckt)
        if init_flags == "s1" or init_flags == "s0":
            self.print_received_message(full_path)

        print("Listening for incomming packets!")

        self.listen_for_incomming_packets()
        
    def run_keep_alive(self, flag): 
        print("run: ", flag[0])
        threading.Thread(target=self.keep_alive, args=(flag,)).start()


    def answer_on_start_com(self, sender_adress_port, flag):
        header = super().create_header("a", 0, 0)
        packet = super().pack_packet(header, "".encode())

        self.my_socket.sendto(packet, sender_adress_port)
        
        self.run_keep_alive(flag)

        print("\nReceiver: Comunication Established!")
        print("Listening for incoming packets...\n")

        self.listen_for_incomming_packets()

    def listen_start_com(self, flag):

        #data, sender_adress_port = self.my_socket.recvfrom(1200)
        data, sender_adress_port = super().wait_for_packet(self.my_socket, 1200, 3600.0)
        flags = super().unpack_init_packet(data)

        if flags == "i" or flags == 'i':
            self.answer_on_start_com(sender_adress_port, flag)
        else:
            print("Didn't received init comunication packet")
            self.listen_start_com(flag)

    def start(self, flag):
        flags_keep_alive = [True, False]
        try:
            if not flag:
                self.listen_start_com(flags_keep_alive)
            else:
                print("Listenning for incomming packets...")
                #self.run_keep_alive(flags_keep_alive)
                self.listen_for_incomming_packets()

            print("Receiver is listening for init packets!")
            self.start(False)
        except CommunicationEnd:
            print("Communication was succesfully ended!")
            flags_keep_alive[0] = False
            print("Waiting ....")
            time.sleep(15)
            print("Receiver is listening for init packets...")
            self.start(False)
        
        except ComunicationExceed:
            print("Comunication ENDED!")
            print("Receiver is listening for init packets!")
            self.my_socket.settimeout(None)
            self.start(False)

