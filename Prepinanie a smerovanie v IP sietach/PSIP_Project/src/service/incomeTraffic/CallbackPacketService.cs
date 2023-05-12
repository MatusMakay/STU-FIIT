using PacketDotNet;
using SharpPcap;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace PSIP_Project.src.service
{
    /**
     * 
     * Handle: 
     * 1. register eventListner on Packet arrival
     * 2. analyzing Packets
     * 3. update 
     * 4. ...
     * **/
    internal class CallbackPacketService
    {
        public List<(Packet, int)> packetQueue;
        private object queueLock;

        public CallbackPacketService(List<(Packet, int)> packetQueue, object queueLock)
        {
            this.packetQueue = packetQueue;
            this.queueLock = queueLock;
        }
        private Packet parsePacket(PacketCapture p)
        {
            var rawPacket = p.GetPacket();
            return Packet.ParsePacket(rawPacket.LinkLayerType, rawPacket.Data);
        }

        public void onPacketArrival(object sender, PacketCapture packet)
        {
            Packet sharpPacket = parsePacket(packet);
            EthernetPacket ethPacket = sharpPacket.Extract<EthernetPacket>();

            if(ethPacket != null )
            {
                PhysicalAddress srcMAC = ethPacket.SourceHardwareAddress;

                if (srcMAC.ToString() != "00133B9BCC22" && srcMAC.ToString() != "089798876074")
                {
                    lock (queueLock)
                    {
                        packetQueue.Add((sharpPacket, sender.GetHashCode()));
                    }
                }
            }
            
            
        }

    }
}
