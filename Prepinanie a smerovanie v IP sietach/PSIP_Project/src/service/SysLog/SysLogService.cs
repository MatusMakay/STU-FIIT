using PacketDotNet;
using PSIP_Project.src.service.traffic;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PSIP_Project.src.service.SysLog
{
    class SysLogService
    {
        SendPacketService sendPacketService;
        IPAddress ipOriginator;
        IPAddress ipCollector;

        PhysicalAddress originatorMac;
        PhysicalAddress collectorMac;
        // 
        ushort collectorPort = 514;
        ushort originatorPort = 10325;

        public bool isSetupPropely = false;


        public string port;
        public string macAdressDevice;

        public SysLogService(SendPacketService sendPacketService)
        {
            this.sendPacketService= sendPacketService;
        }
        public void typeOfMessage(int messageID)
        {
            switch (messageID)
            {

                case 0:
                    //2 facility
                    createMessage("<19> 1 " + string.Format("{0:yyyy-MM-ddTHH:mm:ss.FFFZ}", DateTime.UtcNow) + " " + this.ipOriginator + " Spojenie bolo prerusene na porte: " + this.port + ". Bolo odpojene zariadenie s MAC: " + this.macAdressDevice);
                    break;

                case 1:
                    //2 facility
                    createMessage("<18> 1 " + string.Format("{0:yyyy-MM-ddTHH:mm:ss.FFFZ}", DateTime.UtcNow) + " " + this.ipOriginator + " Vymena kablov");
                    break;

                case 2:
                    //3 facility
                    createMessage("<27> 1 " + string.Format("{0:yyyy-MM-ddTHH:mm:ss.FFFZ}", DateTime.UtcNow) + " " + this.ipOriginator + " Syslog komunikacia zacala IP originator: " +this.ipOriginator + ". IP collectora: " + this.ipCollector);
                    break;

                case 3:
                    //4 facility
                    createMessage("<35> 1 " + string.Format("{0:yyyy-MM-ddTHH:mm:ss.FFFZ}", DateTime.UtcNow) + " " + this.ipOriginator + " Zariadenie s MAC: " + this.macAdressDevice + ". Bolo pripojene na port: " + this.port);
                    break;

                case 4:
                    //1 facility
                    createMessage("<10> 1 " + string.Format("{0:yyyy-MM-ddTHH:mm:ss.FFFZ}", DateTime.UtcNow) + " " + this.ipOriginator + " Pridanie filtra na port: " + this.port);
                    break;
            }
        }

        private bool arpResolveMac()
        {
            //ktory device budem potrebovat

            (PhysicalAddress, PhysicalAddress) tmp = sendPacketService.resolveARP(this.ipCollector);

            this.collectorMac = tmp.Item1;
            this.originatorMac= tmp.Item2;
            
            if(collectorMac == null)
            {
                //todo error handling
                return false;
            }
            
            typeOfMessage(2);
            return true;


        }

        public void setup(string ipOriginator, string ipCollector) 
        {
            bool ipOrig = IPAddress.TryParse(ipOriginator, out this.ipOriginator);
            bool ipColl = IPAddress.TryParse(ipCollector, out this.ipCollector);

            if (!ipOrig)
            {
                //todo error handling
            }
            else if (!ipColl)
            {
                //todo error handling
            }
            else
            {
                bool find = arpResolveMac();
                if (find)
                {
                    this.isSetupPropely = true;
                }
            }

        }

        private void createMessage(string sysLogMessage)
        {

            // TODO aka bude fyzicka adresa source?
            // najskor arp => zistit MAC syslog servera
            if (this.isSetupPropely)
            {
                EthernetPacket ethPacket = new EthernetPacket(this.originatorMac, this.collectorMac, EthernetType.IPv4);

                var ipv4 = new IPv4Packet(this.ipOriginator, this.ipCollector);
                ethPacket.PayloadPacket = ipv4;

                var udp = new UdpPacket(originatorPort, this.collectorPort);

                byte[] bytesSysLog = Encoding.ASCII.GetBytes(sysLogMessage);

                udp.PayloadData = bytesSysLog;
                ipv4.PayloadPacket = udp;

                sendPacketService.sendSyslogMessage(originatorMac, ethPacket);
            }
            else
            {
                //todo error handling
            }

        }

        

        
    }
}
