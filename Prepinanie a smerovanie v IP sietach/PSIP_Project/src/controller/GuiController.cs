using PSIP_Project.src.model;
using PSIP_Project.src.service;
using PSIP_Project.src.service.checkConnectivity;
using PSIP_Project.src.service.filters;
using PSIP_Project.src.service.guitimer;
using PSIP_Project.src.service.incomeTraffic;
using PSIP_Project.src.service.model;
using PSIP_Project.src.service.queue;
using PSIP_Project.src.service.SysLog;
using PSIP_Project.src.service.traffic;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PSIP_Project.src.controller
{
    public class GuiController : IGuiInterface
    {

        private CallbackPacketService incommingPacketService;
        private ClassifyPacketService classifyPacketService;
        private HandleQueueService handleQueue;
        private SendPacketService sendPacketService;
        private AnalyzePacketService analyzePacketService;
        private SwitchService switchService;
        private FilterPacketService filterPacketService;
        private SysLogService sysLogService;
        private ConnectivityService connectivityService;
        private GuiTimer guiTimer;

        

        public Gui GUI;
        private InitDeviceService initDeviceService;
        private DataModel model;

        private Dictionary<string, string> map;
        Dictionary<string, int> nameToIntHash;

        public GuiController(Gui g) 
        {
            this.GUI = g;
            this.model = new DataModel(g);
            this.initDeviceService = new InitDeviceService(new GuiTimer(model));
        }

        private void createServices(List<int> idInterface, List<string> activeDev)
        {
            // new queue when devices are set

            List<ILiveDevice> activeDevices =  this.initDeviceService.getActiveDevices();

            this.sendPacketService = new SendPacketService(this.model, activeDevices[0], activeDevices[1]);
            this.classifyPacketService = new ClassifyPacketService(this, this.model, this.sendPacketService);

            this.sysLogService = new SysLogService(this.sendPacketService);
            this.filterPacketService = new FilterPacketService(this.classifyPacketService, sysLogService, idInterface);

            this.switchService = new SwitchService(this.model, this.filterPacketService, this.sendPacketService, this.sysLogService, idInterface);
            this.sendPacketService.switchService = this.switchService;
            this.model.switchService = this.switchService;

            this.analyzePacketService = new AnalyzePacketService(this.filterPacketService, this.switchService, this.model, idInterface[0]);

            this.handleQueue = new HandleQueueService(idInterface[0], this.analyzePacketService);

            this.connectivityService = new ConnectivityService(sendPacketService, activeDev);

            this.initDeviceService.connectivityService = this.connectivityService;
            this.initDeviceService.handleQueue = this.handleQueue;
            this.initDeviceService.incommingPacketService = new CallbackPacketService(this.handleQueue.packetQueue, this.handleQueue.queueLock);

            this.handleQueue.incommingPacketService = this.initDeviceService.incommingPacketService;
        }

        public void startListenning()
        {
            this.initDeviceService.startSniffing();   
        }
        public void stopBtn()
        {
            this.initDeviceService.stopSniffing();
        }
        public void resetEth1Btn() 
        {
            model.resetEth1();
        }
        public void resetEth2Btn()
        {
            model.resetEth2();   
        }

        public void confirmInterfaces(List<string> interfaceDescription)
        {
            List<string> interfacesNames = new List<string>();

            foreach(string description in interfaceDescription)
            {
                interfacesNames.Add(this.map[description]);
            }
            (List<int>, List<string>, Dictionary<string, int>) specDevices = this.initDeviceService.initDevices(interfacesNames);
          
            nameToIntHash = specDevices.Item3;

            createServices(specDevices.Item1, specDevices.Item2);
        }

        public List<string> refreshInterfaceList()
        {
            CaptureDeviceList devices = this.initDeviceService.getDeviceList();

            List<string> list = new List<string>();

           this.map = new Dictionary<string, string>();

            foreach(ICaptureDevice dev in devices)
            {
                string[] s = dev.ToString().Split('\n');
                string[] tmp = s[1].Split(':');

                list.Add(tmp[1]);
                map.Add(tmp[1], dev.Name);
            }

            return list;
        }


        public void clearMacTable()
        {
            this.switchService.clearMacTable();
            this.model.clearMacTable();
        }
        public void changeTimer(int newTime)
        {
            newTime = newTime * 1000;
            //int oldTime = this.connectivityService.changeTimer(newTime);
            this.model.timerChanged(newTime);
        }

        public void syslogSetup(string ipOriginator, string ipCollector)
        {
            this.sysLogService.setup(ipOriginator, ipCollector);
        }


        public void addAck(string port, string method, string direction, string protocol, string srcMac, string srcIp, string dstMac, string dstIp, string srcPort, string dstPort, int rowNumber)
        {
            this.filterPacketService.addAck(nameToIntHash[port], method, direction, protocol, srcMac, srcIp, dstMac, dstIp, srcPort, dstPort, rowNumber);
        }   

        public void removeAck(List<(string, int, string)> removeAck)
        {
            List<(int, int, string)> tmp = new List<(int, int, string)>();

            foreach((string, int, string) record in removeAck)
            {
                tmp.Add((nameToIntHash[record.Item1], record.Item2, record.Item3));
            }

            this.filterPacketService.removeAck(tmp);
        }

        public void removeAllAck()
        {
            filterPacketService.clearAllAck();
        }
    }
}
