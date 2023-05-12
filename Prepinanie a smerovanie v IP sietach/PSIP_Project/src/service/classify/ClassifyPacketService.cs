using PacketDotNet;
using PSIP_Project.src.controller;
using PSIP_Project.src.model;
using PSIP_Project.src.service.traffic;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSIP_Project.src.service.model
{
    /**
     * 
     * 
     * TREBA DOROBIT A HTTPS
     */
    class ClassifyPacketService
    {
        private DataModel model;

        private SendPacketService sendPacket;

        public ClassifyPacketService(GuiController controller, DataModel d, SendPacketService service)
        {
            this.model = d;
            this.sendPacket = service;
        }

        /**
         * classifyPacket return type of packet as:
         * tcp
         * udp
         * icmp
         * arp
         * **/
        public string classifyPacket(Packet packet)
        {


            string type = null;

            if (packet.GetType().Name == "EthernetPacket")
            {


                //IpV4
                if (packet.PayloadPacket is IPv4Packet ipPacket /*spytat sa ci tcp, upd moze ist cez ipv6 myslim ze hej ale len tak*/)
                {
                    // TCP tcppacket.port => zistis http alebo zistis https
                    if (ipPacket.PayloadPacket is TcpPacket tcpPacket)
                    {

                        type = "TCP";

                        if (tcpPacket.SourcePort == 80 || tcpPacket.DestinationPort == 80)
                        {
                            type = "HTTP";
                        }
                        else if (tcpPacket.SourcePort == 443)
                        {
                            type = "HTTPS";
                        }

                    }
                    // UDP
                    else if (ipPacket.PayloadPacket is UdpPacket udpPacket)
                    {
                        type = "UDP";
                    }
                    // ICMP
                    else if (ipPacket.PayloadPacket is IcmpV4Packet icmpPacket)
                    {
                        type = "ICMP";
                    }
                }
                else
                {
                    ArpPacket arpPacket = null;
                    if (packet != null && packet.PayloadPacket != null)
                    {
                        arpPacket = packet.PayloadPacket.Extract<ArpPacket>();
                    }

                    if (arpPacket != null)
                    {
                        type = "ARP";
                    }

                }
                if (type == null)
                    return "OTHERS";

                else return type;
            }
            return type;
        }
        public void packetIncomeEth1(Packet packet)
        {
            // 1. modify, create data 
            // 2. update model
            if (packet.GetType().Name == "EthernetPacket")
            {
                string protocol = classifyPacket(packet);

                if (protocol != "OTHERS")
                {
                    model.updateInEth(protocol, "eth1");
                    sendPacket.sendPacket(packet, "eth2", protocol);

                }
                else
                {
                    model.updateInEth("TOTAL", "eth1");
                    sendPacket.sendPacket(packet, "eth2", "TOTAL");
                }

            }

        }
    }
}
