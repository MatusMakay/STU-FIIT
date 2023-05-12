using PacketDotNet;
using PSIP_Project.src.service.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.CompilerServices;
using PSIP_Project.src.service.SysLog;
using System.Security.Cryptography;
using PcapDotNet.Packets.Igmp;

namespace PSIP_Project.src.service.filters
{
    class FilterPacketService
    {
        ClassifyPacketService classifyPacketService;
        SysLogService syslogService;
        Dictionary<int, List<Dictionary<string, string>>> ackIn = new Dictionary<int, List<Dictionary<string, string>>>();
        Dictionary<int, List<Dictionary<string, string>>> ackOut = new Dictionary<int, List<Dictionary<string, string>>>();
        List<int> idInterfaces;

        private Dictionary<int, bool> isAckClearIn = new Dictionary<int, bool>();
        private Dictionary<int, bool> isAckClearOut = new Dictionary<int, bool>();

        public FilterPacketService(ClassifyPacketService classifyPacketService, SysLogService syslogService, List<int> idInterfaces)
        {
            this.classifyPacketService = classifyPacketService;
            this.idInterfaces = idInterfaces;
            this.syslogService = syslogService; 
            setup();
        }

        private List<Dictionary<string, string>> setupList()
        {
            List<Dictionary<string, string>> newList = new List<Dictionary<string, string>>();
            for(int i = 0; i < 500; i++)
            {
                newList.Add(null);
            }

            return newList;
        }
        private void setup()
        {
            
            this.ackIn.Add(idInterfaces[0], setupList());
            this.ackOut.Add(idInterfaces[0], setupList());
            this.isAckClearIn.Add(idInterfaces[0], true);
            this.isAckClearOut.Add(idInterfaces[0], true);

            this.ackIn.Add(idInterfaces[1], setupList());
            this.ackOut.Add(idInterfaces[1], setupList());
            this.isAckClearIn.Add(idInterfaces[1], true);
            this.isAckClearOut.Add(idInterfaces[1], true);


        }
        public void addAck(int idInterface, string method, string direction, string protocol, string srcMac, string srcIp, string dstMac, string dstIp, string srcPort, string dstPort, int seqNum)
        {
            if (seqNum == 0)
            {
                seqNum = 0;
            }
            if(direction == "in")
            {
                this.isAckClearIn[idInterface] = false;
                this.ackIn[idInterface].Insert(seqNum, new Dictionary<string, string>()
                    {
                        { "seqNum", seqNum+"" },
                        { "method", method },
                        { "protocol", protocol },
                        { "srcMac", srcMac},
                        { "srcIp", srcIp },
                        { "dstMac", dstMac},
                        { "dstIp", dstIp },
                        { "srcPort", srcPort},
                        { "dstPort", dstPort }
                    }
                );
                this.syslogService.port = "eth1";

                this.syslogService.typeOfMessage(4);
            }
            else
            {
                this.isAckClearOut[idInterface] = false;
                this.ackOut[idInterface].Insert(seqNum, new Dictionary<string, string>()
                     {
                        { "seqNum", seqNum+"" },
                        { "method", method },
                        { "protocol", protocol },
                        { "srcMac", srcMac},
                        { "srcIp", srcIp },
                        { "dstMac", dstMac},
                        { "dstIp", dstIp },
                        { "srcPort", srcPort},
                        { "dstPort", dstPort }
                    }
                );
                this.syslogService.port = "eth2";

                this.syslogService.typeOfMessage(4);
            }
        }
        public void removeAck(List<(int, int, string)> removeRecords) 
        {
            List<Dictionary<string, string>> ackRemove;
            
            foreach((int, int, string) record in removeRecords)
            {
                if (record.Item3 == "in")
                {
                    ackRemove = ackIn[record.Item1];
                }
                else
                {
                    ackRemove = ackOut[record.Item1];
                }
                ackRemove.RemoveAt(record.Item2);
            }
        }
        private bool resolveAck(List<Dictionary<string, string>> ackList, string protocol, string srcIp, string srcMac, string srcPort,  string dstMac, string dstIp , string dstPort) {

            bool permit = false;

            List< Dictionary<string, string> > notNull = new List<Dictionary<string, string>>();
            
            foreach (Dictionary<string, string> record in ackList)
            {
                if (record != null)
                {
                    notNull.Add(record);
                }
            }

            foreach (Dictionary<string, string> record in notNull)
            {
                if(record != null)
                {
                    if ((record["srcIp"] == "any" || record["srcIp"] == srcIp || record["srcMac"] == srcMac || record["srcMac"] == "any" || record["srcPort"] == srcPort || record["srcPort"] == "any") && 
                        (record["dstIp"]  == "any" ||  record["dstIp"] == dstIp || record["dstMac"] == dstMac || record["dstMac"] == "any" || record["dstPort"] == dstPort || record["dstPort"] == "any") && 
                        (record["protocol"] == "any" || protocol == record["protocol"]) )
                    {
                        if (record["method"] == "permit")
                        {
                            permit = true;
                            break;
                        }
                        else
                        {
                            permit = false;
                            break;
                        }
                    }
                    else if ((record["srcIp"] == "any" && record["srcMac"] == "any" && record["srcPort"] == "any" && record["dstIp"] == "any" && record["dstMac"] == "any" && record["dstPort"] == "any") && record["protocol"] == "any" && record["method"] == "permit")
                    {
                        permit = true;
                        break;
                    }
                }
            }
            return permit;
        }
        public (bool, string) filterIn(Packet packet, int senderID)
        {
            EthernetPacket ethPacket = packet.Extract<EthernetPacket>();
            IPPacket ipPacket = packet.Extract<IPPacket>();

            string protocol = classifyPacketService.classifyPacket(packet);
            if (this.isAckClearIn[senderID])
            {
                return (true, protocol);
            }
            // add geting ack for specific interface IN
           
            string srcMAC = ethPacket.SourceHardwareAddress.ToString();
            string IPsrc = "";
            string srcPort = "";
            string dstMAC = ethPacket.DestinationHardwareAddress.ToString();
            string IPdst = "";
            string portDst = "";
            //ignore packets which are from

            if (ipPacket != null)
            {
                IPsrc = ipPacket.SourceAddress.ToString();
                IPdst = ipPacket.DestinationAddress.ToString();

                if (protocol == "UDP")
                {
                    UdpPacket udpPacket = packet.Extract<UdpPacket>();
                    srcPort = udpPacket.SourcePort.ToString();
                    portDst = udpPacket.SourcePort.ToString();
                }
                else if(protocol == "TCP")
                {
                    TcpPacket tcpPacket = packet.Extract<TcpPacket>();
                    srcPort = tcpPacket.SourcePort.ToString();
                    portDst = tcpPacket.SourcePort.ToString();
                }
            }

            List<Dictionary<string, string>> ackIn = this.ackIn[senderID];

            bool permit = resolveAck(ackIn, protocol, IPsrc, srcMAC, srcPort, dstMAC, IPdst, portDst);
            
            return (permit, protocol);
        }

        public bool filterOut(Packet packet, int senderID)
        {
            if (this.isAckClearOut[senderID])
            {
                return true;
            }

            EthernetPacket ethPacket = packet.Extract<EthernetPacket>();
            IPPacket ipPacket = packet.Extract<IPPacket>();

            string protocol = classifyPacketService.classifyPacket(packet);
            string srcMAC = ethPacket.SourceHardwareAddress.ToString();
            string dstMAC = ethPacket.DestinationHardwareAddress.ToString();

            string IPsrc = "";
            string srcPort = "";
            string IPdst = "";
            string portDst = "";
            //ignore packets which are from

            if (ipPacket != null)
            {
                IPsrc = ipPacket.SourceAddress.ToString();
                IPdst = ipPacket.DestinationAddress.ToString();

                if (protocol == "UDP")
                {
                    UdpPacket udpPacket = packet.Extract<UdpPacket>();
                    srcPort = udpPacket.SourcePort.ToString();
                    portDst = udpPacket.DestinationPort.ToString();
                }
                else if (protocol == "TCP")
                {
                    TcpPacket tcpPacket = packet.Extract<TcpPacket>();
                    srcPort = tcpPacket.SourcePort.ToString();
                    portDst = tcpPacket.DestinationPort.ToString();
                }
            }

            List<Dictionary<string, string>> ackOut = this.ackOut[senderID];

            bool permit = resolveAck(ackOut, protocol, IPsrc, srcMAC, srcPort, dstMAC, IPdst, portDst);

            return permit;
        }

        public void clearAllAck()
        {
            foreach(int key in this.isAckClearIn.Keys.ToArray())
            {
                this.isAckClearIn[key] = true;
                this.isAckClearOut[key] = true;
            }
            ackIn.Clear();
            ackOut.Clear();
        }
    }
}
