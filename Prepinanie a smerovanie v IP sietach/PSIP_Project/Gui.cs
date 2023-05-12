using PSIP_Project.src.controller;
using PSIP_Project.src.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSIP_Project
{
    public partial class Gui : Form
    {
        public delegate void AddSomeToTable(string s);
        public delegate void AddRecordToMac((string, string, int) newRecord);
        public delegate void UpdateMACALL(List<(string, string, int)> newMacTABLE);

        private IGuiInterface Icontroller;

        public Gui()
        {
            this.Icontroller = (IGuiInterface)(new GuiController(this));
            InitializeComponent();
          
        }

         private void changeTimer(object sender, EventArgs e)
        {
            string newTime = this.timerValue.Text;

            Icontroller.changeTimer(int.Parse(newTime));
        }

        private void clearMacTable(object sender, EventArgs e)
        {
            Icontroller.clearMacTable();
        }

        private void Start_Button_Click(object sender, EventArgs e)
        {
            this.Start_Button.Enabled = false;
            this.Stop_Button.Enabled = true;

            this.Icontroller.startListenning();

        }

        public void updateMACOneRecord((string, string, int) newRecord)
        {
            if (this.MAC_Table.InvokeRequired)
            {
                var d = new AddRecordToMac(this.updateMACOneRecord);
                this.MAC_Table.Invoke(d, new object[] { newRecord });
            }
            else
            {
                this.MAC_Table.Rows.Add(newRecord.Item1, newRecord.Item2, newRecord.Item3);
            }
        }

        public void updateMACAll(List<(string, string, int)> newMacTABLE)
        {

            if (this.MAC_Table.InvokeRequired)
            {
                var d = new UpdateMACALL(this.updateMACAll);
                this.MAC_Table.Invoke(d, new object[] { newMacTABLE });
            }
            else
            {
                this.MAC_Table.Rows.Clear();

                foreach((string, string, int) record in newMacTABLE)
                {
                    this.MAC_Table.Rows.Add(record.Item1, record.Item2, record.Item3);
                }

            }
        }

        private void NET_Stats_Enter(object sender, EventArgs e)
        {

        }

        public void updateEth(string eth, List<string> protocols, List<int> counts)
        {
            if (eth == "eth1")
            {
                if (this.eth1.InvokeRequired)
                {
                    var d = new UpdateEth(this.updateEth);
                    this.eth1.Invoke(d, new object[] { eth, protocols, counts });
                }
                else
                {
                    string[] tmpProtocols = protocols.ToArray();
                    int[] tmpCounts= counts.ToArray();

                    int index = 0;
                    foreach (string protocol in tmpProtocols)
                    {
                        var element = this.eth1.Controls.Find(protocol, true);
                        element[0].Text = $"{tmpCounts[index]}";

                        index++;
                    }
                }
            }
            else if (eth == "eth2")
            {
                if (this.eth2.InvokeRequired)
                {
                    var d = new UpdateEth(this.updateEth);
                    this.eth2.Invoke(d, new object[] { eth, protocols, counts });
                }
                else
                {
                    string[] tmpProtocols = protocols.ToArray();
                    int[] tmpCounts = counts.ToArray();

                    int index = 0;
                    foreach (string protocol in tmpProtocols)
                    {
                        var element = this.eth2.Controls.Find(protocol, true);
                        element[0].Text = $"{tmpCounts[index]}";

                        index++;
                    }
                }
            }
        }

        private void Stop_Button_Click(object sender, EventArgs e)
        {
            Icontroller.stopBtn();

            this.Start_Button.Enabled = true;
            this.Stop_Button.Enabled = false;

        }

        private void resetEht1(object sender, EventArgs e)
        {
            foreach (TextBox box in this.eth1.Controls) 
            { 
                box.Clear();
            }

            Icontroller.resetEth1Btn();
        }

        private void resetEth2(object sender, EventArgs e)
        {
            foreach (TextBox box in this.eth2.Controls)
            {
                box.Clear();
            }

            Icontroller.resetEth2Btn();
        }

        private void refreshInterfaceList(object sender, EventArgs e)
        {
            List<string> deviceListNames = this.Icontroller.refreshInterfaceList();

            this.DeviceList.Items.Clear();

            foreach(string s in deviceListNames)
            {
                this.DeviceList.Items.Add(s, false);
            }
        }
        private void resetCheckItems()
        {
            this.DeviceList.ClearSelected();
        }

        private void confirmEthDevices(object sender, EventArgs e)
        {
            List<string> confirmedDevName = new List<string>();

            foreach(string s in this.DeviceList.CheckedItems)
            {
                confirmedDevName.Add(s);
            }

            this.InterfaceName1.Text = confirmedDevName[0];

            this.InterfaceName2.Text = confirmedDevName[1];

            Icontroller.confirmInterfaces(confirmedDevName);
            this.resetCheckItems();
        }

        private void syslogStart_Click(object sender, EventArgs e)
        {
            this.Icontroller.syslogSetup(this.originatorIp.Text, this.collectorIp.Text);
        }

        private void addRow(object sender, EventArgs e)
        {
            this.Filters.Rows.Insert(0, "1-100" ,"eth1/eth2", "permit/deny", "in/out", "HTTP,...", "ffffffffffff", "ffffffffffff", "x.x.x.x", "x.x.x.x", "x", "x");
        }

        private void AddAckRule_Click(object sender, EventArgs e)
        {
            //this doesnt have to be true
            DataGridViewRow row =  this.Filters.Rows[0];
            DataGridViewCellCollection cells = row.Cells;

            string port = cells["PORTACK"].Value.ToString();
            string method = cells["Method"].Value.ToString();
            string direction = cells["Direction"].Value.ToString();
            string protocol = cells["Protocol"].Value.ToString();
            string srcMac = cells["SRCMAC"].Value.ToString();
            string srcIp = cells["SRCIP"].Value.ToString();
            string dstMac = cells["DSTMAC"].Value.ToString();
            string dstIp = cells["DSTIP"].Value.ToString();
            string srcPort = cells["SRCPORT"].Value.ToString();
            string dstPort = cells["DSTPORT"].Value.ToString();
            
            int seqNum = int.Parse(cells["SeqNum"].Value.ToString());
            //seqnum -1 posielaj
            Icontroller.addAck(port, method, direction, protocol, srcMac, srcIp, dstMac, dstIp, srcPort, dstPort, seqNum - 1);
                
        }   

        private void DeleteAck_Click(object sender, EventArgs e)
        {
            List<( string, int, string)> removeRecord = new List<(string, int, string)>();

            int index = 0;
            foreach (DataGridViewRow row in this.Filters.Rows)
            {
                // Get the value of the checkbox cell for the current row
                DataGridViewCheckBoxCell checkboxCell = row.Cells["checkbox"] as DataGridViewCheckBoxCell;
                bool isChecked = (bool)checkboxCell.Selected;

                // If the checkbox is checked, do something
                if (isChecked)
                {
                    DataGridViewCellCollection cells = row.Cells;

                    int seqNum = int.Parse(cells["SeqNum"].Value.ToString());
                    string port = cells["PORTACK"].Value.ToString();
                    string direction = cells["Direction"].Value.ToString();

                    (string, int, string) record = (port, seqNum-1, direction);

                    removeRecord.Add(record);
                    this.Filters.Rows.RemoveAt(index);
                }
                index++;
            }
            Icontroller.removeAck(removeRecord);
        }

        private void ClearAck_Click(object sender, EventArgs e)
        {
            Icontroller.removeAllAck();
            this.Filters.Rows.Clear();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

      

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {

        }

        private void ETH_IN1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void MAC_Table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }


      

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void label44_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label44_Click_2(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Filters_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
    }
}
