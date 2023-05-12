using PacketDotNet;
using PSIP_Project.src.model;
using PSIP_Project.src.service.filters;
using PSIP_Project.src.service.SysLog;
using PSIP_Project.src.service.traffic;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace PSIP_Project.src.service
{
    class SwitchService
    {
        DataModel model;
        
        FilterPacketService filterPacketService;
        SendPacketService sendPacketService;
        SysLogService sysLogService;

        object macLock = new object();

        Dictionary<PhysicalAddress, string> macTABLE = new Dictionary<PhysicalAddress, string>();
        Dictionary<int, string> idToInterface = new Dictionary<int, string>();
        List<int> interfaceIDs;

        public SwitchService(DataModel model, FilterPacketService filterPacketService, SendPacketService sendPacketService, SysLogService sysLogService ,List<int> idInterface) 
        { 
            this.filterPacketService = filterPacketService;
            this.sendPacketService = sendPacketService;
            this.sysLogService = sysLogService;
            this.model = model;

            setIdInterfaceDictionary(idInterface);
        }

        private void setIdInterfaceDictionary(List<int> interfaceIDs)
        {
            this.interfaceIDs = interfaceIDs;

            idToInterface.Add(interfaceIDs[0], "eth1");
            if(interfaceIDs.Count > 1)
            {
                idToInterface.Add(interfaceIDs[1], "eth2");
            }
        }


        public void switchPacket(Packet packet, int senderID, string protocol)
        {
            if(protocol != "OTHERS")
            {
                bool newDevice = false;
                EthernetPacket ethPacket = packet.Extract<EthernetPacket>();

                PhysicalAddress srcMAC = ethPacket.SourceHardwareAddress;
                PhysicalAddress dstMAC = ethPacket.DestinationHardwareAddress;

                //ignore packets which are from
                IPPacket ipPacket = packet.Extract<IPPacket>();

                if(ipPacket != null)
                {
                    IPAddress srcIP = ipPacket.SourceAddress;
                }

                bool isSrcMACInTable = macTABLE.ContainsKey(srcMAC);

                if (isSrcMACInTable)
                {
                    // todo check if cable ports was changed
                    if (macTABLE[srcMAC] != idToInterface[senderID])
                    {
                        model.cableSwitch();
                        this.cableSwitched();

                        sysLogService.typeOfMessage(1);
                    }
                }
                else
                {
                    string port = idToInterface[senderID];
                    lock (macLock)
                    {
                        this.macTABLE.Add(srcMAC, port);
                        model.updateMacTable(srcMAC.ToString(), port);
                    }

                    newDevice = true;
                    

                }

                bool isDstMACInTable = macTABLE.ContainsKey(dstMAC);
                
                if (isDstMACInTable)
                {
                    bool passedACKOUT = filterPacketService.filterOut(packet, senderID == this.interfaceIDs[0] ? this.interfaceIDs[1] : senderID);

                    if (passedACKOUT)
                    {
                        sendPacketService.sendPacket(packet, senderID == this.interfaceIDs[0] ? "eth2" : "eth1", protocol);
                    }
                }
                else
                {

                    bool passedACKOUT = filterPacketService.filterOut(packet, senderID == this.interfaceIDs[0] ? this.interfaceIDs[1] : senderID);

                    if (passedACKOUT)
                    {
                        string sendTo = senderID == this.interfaceIDs[0] ? "eth2" : "eth1";

                        sendPacketService.sendPacket(packet, sendTo, protocol);
                    }
                    
                }
                if (newDevice)
                {
                    if (sysLogService.isSetupPropely)
                    {
                        sysLogService.port = idToInterface[senderID];
                        sysLogService.macAdressDevice = srcMAC.ToString();
                        sysLogService.typeOfMessage(3);
                    }
                }
                
            }

        }

        public void cableDisconected(string disconectedPort)
        {
            lock (this.macLock)
            {
                foreach (PhysicalAddress key in this.macTABLE.Keys.ToList())
                {
                    if (this.macTABLE[key] == disconectedPort)
                    {
                        if (this.sysLogService.isSetupPropely)
                        {
                            this.sysLogService.port = disconectedPort;
                            this.sysLogService.macAdressDevice = key.ToString();
                            this.sysLogService.typeOfMessage(0);
                        }
                        this.macTABLE.Remove(key);
                    }
                }

            }
            
        }

        public void removeMacAfterMinusOne(List<string> deleteMac) 
        {
            lock(this.macLock)
            {
                foreach (PhysicalAddress key in this.macTABLE.Keys.ToList())
                {

                    if (deleteMac.Contains(key.ToString()))
                    {
                        this.macTABLE.Remove(key);
                    }

                }
            }
            
        }



        public void cableSwitched()
        {
            lock (macLock)
            {
                foreach (PhysicalAddress key in this.macTABLE.Keys.ToList())
                {
                    if (this.macTABLE[key] == "eth1")
                    {
                        this.macTABLE[key] = "eth2";
                    }
                    else
                    {
                        this.macTABLE[key] = "eth1";
                    }
                }
            }
            if (this.sysLogService.isSetupPropely)
            {
                this.sysLogService.typeOfMessage(1);
            }
        }

        public void clearMacTable()
        {
            lock (macLock)
            {
                this.macTABLE.Clear();
            }
        }
    }
}