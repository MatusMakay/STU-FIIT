using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PSIP_Project.src.controller
{
    internal interface IGuiInterface
    {
        // define operations with buttons
        void startListenning();
        void stopBtn();
        void resetEth1Btn();
        void resetEth2Btn();

        void clearMacTable();

        void changeTimer(int newTimer);

        void confirmInterfaces(List<string> interfaceName);
        List<string> refreshInterfaceList();

        void syslogSetup(string ipSource, string ipDest);

        void addAck(string port, string method, string direction, string protocol, string srcMac, string srcIp, string dstMac, string dstIp, string srcPort, string dstPort, int rowNumber);

        void removeAck(List<(string, int, string)> removeAck);

        void removeAllAck();
    }
}
