using PacketDotNet;
using PSIP_Project.src.model;
using PSIP_Project.src.service.filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSIP_Project.src.service.incomeTraffic
{
    class AnalyzePacketService
    {
        private FilterPacketService filterPacketService;
        private SwitchService switchService;
        private DataModel model;
        private int eth1ID;

        public AnalyzePacketService(FilterPacketService filterPacketService, SwitchService switchService, DataModel model, int eth1ID) 
        {
            this.switchService = switchService;
            this.filterPacketService = filterPacketService;
            this.model = model;
            this.eth1ID = eth1ID;
        }


        public void analyzeBeforeSwitch(Packet packet, int senderID)
        {
             //(passTest, protocol)
            (bool, string) result = filterPacketService.filterIn(packet, senderID);

            bool passedACKIN = result.Item1;
            string protocol = result.Item2;
            
            if (passedACKIN && protocol != null)
            {
                string eth = senderID == eth1ID ? "eth1" : "eth2";

                model.updateInEth(protocol, eth);

                switchService.switchPacket(packet, senderID, protocol);
            }
        }
    }
}
