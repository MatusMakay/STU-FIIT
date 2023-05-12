using PSIP_Project.src.service.traffic;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacketDotNet;

namespace PSIP_Project.src.service.checkConnectivity
{
    class ConnectivityService
    {
        SendPacketService sendPacketService;
        List<string> activeDevices;
        Timer objTimer;

        public int timer = 8000;
        public ConnectivityService(SendPacketService sendPacketService, List<string> activeDevices) 
        { 
            this.sendPacketService = sendPacketService;
            this.activeDevices = activeDevices;
        }

        private byte[] GetRandomPacket()
        {
            byte[] packet = new byte[200];
            Random rand = new Random();
            rand.NextBytes(packet);
            return packet;
        }

        //possibly not working 
        public int changeTimer(int newTime)
        {
            int oldTimer = this.timer;
            
            this.stopCheckingConnectivity();

            this.timer = newTime;
            this.startCheckingConnectivity();

            return oldTimer;
        }

        public void startCheckingConnectivity()
        {
            objTimer = new Timer(checkInterfaces, null, 0, timer);
        }

        public void stopCheckingConnectivity()
        {
            objTimer.Dispose();
            objTimer = null;
        }
        private void checkInterfaces(Object o)
        {
            foreach(string eth in activeDevices)
            {
                byte[] bytes = GetRandomPacket();

                this.sendPacketService.checkConnectivity(bytes, eth);
            }
        }

    }
}
