using PacketDotNet;
using PcapDotNet.Packets.Ethernet;
using PSIP_Project.src.model;
using PSIP_Project.src.service.model;
using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PSIP_Project.src.service.traffic
{
    internal class SendPacketService
    {

        public ILiveDevice eth1;
        public ILiveDevice eth2;

        DataModel model;
        public SwitchService switchService;

        public SendPacketService(DataModel m, ILiveDevice eth1, ILiveDevice eth2) 
        {
            model = m;
            this.eth1 = eth1;
            this.eth2 = eth2;
        }

        public (PhysicalAddress, PhysicalAddress) resolveARP(IPAddress dstIp)
        {
            var arpEth1 = (LibPcapLiveDevice)eth1;
            var arpEth2 = (LibPcapLiveDevice)eth2;

            var arperEth1 = new ARP(arpEth1);
            var arperEth2 = new ARP(arpEth2);

            for(int i = 0; i < 5; i++) 
            {
                PhysicalAddress resolvedMacEth1 = arperEth1.Resolve(dstIp);
                PhysicalAddress resolvedMacEth2 = arperEth2.Resolve(dstIp);

                if(resolvedMacEth1 != null || resolvedMacEth2 != null)
                {
                    return resolvedMacEth1 == null ? (resolvedMacEth2, this.eth2.MacAddress) : (resolvedMacEth1, this.eth1.MacAddress);
                }
            }

            return (null, null);
        }

        public void sendPacket(Packet p, string sendTo, string protocol)
        {
            try
            {
                if (sendTo == "eth2")
                {
                    this.eth2.SendPacket(p);
                    
                    model.updateOutEth(protocol, "eth2");
                }

                else
                {
                    this.eth1.SendPacket(p);
                    
                    model.updateOutEth(protocol, "eth1");
                }
            }
            catch(PcapException)
            {
               
            }
        }
        public void checkConnectivity(byte[] bytes, string sendTo)
        {
            try
            {
                if (sendTo == "eth2")
                {
                    this.eth2.SendPacket(bytes);
                }

                else
                {
                    this.eth1.SendPacket(bytes);
                }
            }
            catch (PcapException)
            {
                switchService.cableDisconected(sendTo);
                model.cableDisconnect(sendTo);
            }
        }

        public void sendSyslogMessage(PhysicalAddress interfaceAdress, EthernetPacket ethPacket)
        {
            if(this.eth1.MacAddress == interfaceAdress)
            {
                this.eth1.SendPacket(ethPacket);
            }
            else
            {
                this.eth2.SendPacket(ethPacket);
            }
        }
    }
}
