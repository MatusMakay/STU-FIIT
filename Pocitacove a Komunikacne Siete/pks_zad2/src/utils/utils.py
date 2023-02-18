from exc.comunication_exceed import ComunicationExceed
import os
import socket


def create_sockets(obj):
    conf_com = obj.get_conf()
    
    port_keepalive = conf_com[1] + 1
    conf_keepalive = (conf_com[0], port_keepalive)

    com_socket = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)
    com_socket.bind(conf_com)


    socket_keepalive = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)
    socket_keepalive.bind(conf_keepalive)


    return [com_socket, socket_keepalive]



def input_adr_port():
    print("Write: IP/PORT \nexample: 12.12.12.12/25")
    
    inp = input()
    
    tmp = inp.split('/') 
    
    try:
        return tmp[0], int(tmp[1])
    except IndexError:
        raise IndexError 


def input_sender_menu(path):
    """
    Returns: [flags, file_name/message, [size_of_fragment]]
    """
    flags = input("Do you want to send File or String?\nFile=1, String=0\n")
    
    #SEND FILE
    if flags == '1':
        file_name = ""

        file_name += input("Write a name of the file you want to send?\n")
        flags += input("Fragment?\nYes=1, No=0\n")
        
        if flags =="10":
            return [flags, file_name]
        
        fragment = 0

        while fragment == 0 or fragment > 1460:
            fragment = int(input("Write size of packet in B?\n"))


        return [flags, file_name, fragment] 

    #SEND STRING
    else: 
        message = input("Write your message:\n")
        flags += input("Fragment?\nYes=1, No=0\n")

        path+="/files/send/message.txt"

        with open(path, "w") as f:
            f.write(message)

        if flags == "00":
            return [flags]

        fragment = int(input("Write size of packet in B?\n"))

        return [flags, fragment] 

def wait_for_packet_init(socket, buffer_size,timeout):
        """
        Now use only on starting communication, or when you should check if comunication is still alive
        Sender will use it
        """
        try:
            socket.settimeout(timeout)
            packet, sender_adress_port = socket.recvfrom(buffer_size) 
            socket.settimeout(None)
            return packet, sender_adress_port
            
        except socket.timeout:
            socket.setttimeout(None)
            raise ComunicationExceed("Toto zachytavam KeyError-om :'( ")


def input_same_length_sending_packet(buffer_size):
    state = input("Do you want to send a packet with same length?\nY/N\n")
    
    if state == "N" or state == "n":
        buffer_size = int(input("Write new packet length in B\n"))

    return buffer_size

def input_damage_packets(num_packets):
    flag = input("Do you want send some damaged packets?\nY/N\n")

    if flag == "Y" or flag == "y":
        num = int(input(f"How many?\nAll packets={num_packets}\n"))
        return num
    else:
        return 0
