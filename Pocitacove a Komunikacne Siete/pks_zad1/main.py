from importlib.resources import path
from scapy.all import rdpcap, raw
from binascii import hexlify

from os import listdir
from os.path import isfile, join
import sys

from FrameCreator import FrameCreator
from FrameWriter import FrameWriter

from utility import pars_mac_and_len_and_type, create_eth_dics, create_list_of_senders

eth_path = "./protocols/ethernet2.json"
ieee_path = "./protocols/ieee.json"


count = 1
# read data from output/ dir and create dictionary for IEEE and ETHERNET2

eth_dics, ieee_dics = create_eth_dics(eth_path, ieee_path)

creator = FrameCreator(eth_dics, ieee_dics)
writer = FrameWriter()


list_packets = []

file_name = ""

def main(path_pcap, yaml_path):
    count = 1
    ipv4_packets = []

    for packet in packets:
        hex_packet = hexlify(raw(packet)).decode('utf-8')

        #parsne mac adresy, dlzku ramca, typ ramca a returne values
        src_adr, dst_adr, frame_len_pcap, frame_len_medium, type_frame = pars_mac_and_len_and_type(hex_packet, count)

        #IEE, ETHERNET2
        frame = creator.search_eth_type_dic(type_frame, count, src_adr, dst_adr,frame_len_pcap, frame_len_medium, hex_packet)
        if "protocol" in frame.__dict__ and frame.protocol:
            ipv4_packets.append(frame)



        list_packets.append(frame)
        count += 1

    

    max_send_packets_by, ipv4_senders = create_list_of_senders(ipv4_packets)

    writer.init_ul_1_2_3(yaml_path ,path_pcap)
    writer.add_frames_in_dir_ul_1_2_3(list_packets)
    writer.add_senders_in_dir(max_send_packets_by, ipv4_senders)
    writer.write_in_yaml_ul_1_2_3()


while(True):
    path_pcap = input("write name of pcap: ")

    if path_pcap == "exit":
        exit("Thanks for using")    
    try:
        packets = rdpcap("vzorky/"+path_pcap)
    except FileNotFoundError:
        continue

    yaml_path = "output/" + path_pcap[0: len(path_pcap)-4] + "yaml"
    main(path_pcap, yaml_path)
    print("Run succesfull")



