using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Forms.VisualStyles;

namespace PSIP_Project
{
    partial class Gui
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Start_Button = new System.Windows.Forms.Button();
            this.Stop_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ETH_IN1 = new System.Windows.Forms.TextBox();
            this.IP_IN1 = new System.Windows.Forms.TextBox();
            this.ARP_IN1 = new System.Windows.Forms.TextBox();
            this.TCP_IN1 = new System.Windows.Forms.TextBox();
            this.UDP_IN1 = new System.Windows.Forms.TextBox();
            this.ICMP_IN1 = new System.Windows.Forms.TextBox();
            this.HTTP_IN1 = new System.Windows.Forms.TextBox();
            this.HTTPS_IN1 = new System.Windows.Forms.TextBox();
            this.TOTAL_IN1 = new System.Windows.Forms.TextBox();
            this.TOTAL_OUT1 = new System.Windows.Forms.TextBox();
            this.HTTPS_OUT1 = new System.Windows.Forms.TextBox();
            this.HTTP_OUT1 = new System.Windows.Forms.TextBox();
            this.ICMP_OUT1 = new System.Windows.Forms.TextBox();
            this.UDP_OUT1 = new System.Windows.Forms.TextBox();
            this.TCP_OUT1 = new System.Windows.Forms.TextBox();
            this.ARP_OUT1 = new System.Windows.Forms.TextBox();
            this.IP_OUT1 = new System.Windows.Forms.TextBox();
            this.ETH_OUT1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.eth1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.RESET_ETH1 = new System.Windows.Forms.Button();
            this.RESET_ETH2 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.eth2 = new System.Windows.Forms.GroupBox();
            this.TCP_OUT2 = new System.Windows.Forms.TextBox();
            this.ETH_IN2 = new System.Windows.Forms.TextBox();
            this.TOTAL_OUT2 = new System.Windows.Forms.TextBox();
            this.IP_IN2 = new System.Windows.Forms.TextBox();
            this.HTTPS_OUT2 = new System.Windows.Forms.TextBox();
            this.ARP_IN2 = new System.Windows.Forms.TextBox();
            this.HTTP_OUT2 = new System.Windows.Forms.TextBox();
            this.TCP_IN2 = new System.Windows.Forms.TextBox();
            this.ICMP_OUT2 = new System.Windows.Forms.TextBox();
            this.UDP_IN2 = new System.Windows.Forms.TextBox();
            this.UDP_OUT2 = new System.Windows.Forms.TextBox();
            this.ICMP_IN2 = new System.Windows.Forms.TextBox();
            this.HTTP_IN2 = new System.Windows.Forms.TextBox();
            this.ARP_OUT2 = new System.Windows.Forms.TextBox();
            this.HTTPS_IN2 = new System.Windows.Forms.TextBox();
            this.IP_OUT2 = new System.Windows.Forms.TextBox();
            this.TOTAL_IN2 = new System.Windows.Forms.TextBox();
            this.ETH_OUT2 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.timerValue = new System.Windows.Forms.TextBox();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.AddAck = new System.Windows.Forms.Button();
            this.DeleteAck = new System.Windows.Forms.Button();
            this.ClearAck = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.originatorIp = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.collectorIp = new System.Windows.Forms.TextBox();
            this.syslogStart = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.InterfaceName2 = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.InterfaceName1 = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.DeviceList = new System.Windows.Forms.CheckedListBox();
            this.MAC_Table = new System.Windows.Forms.DataGridView();
            this.MAC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PORT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIMER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.confirmTimer = new System.Windows.Forms.Button();
            this.Filters = new System.Windows.Forms.DataGridView();
            this.SeqNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PORTACK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Method = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Direction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Protocol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SRCMAC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SRCIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DSTMAC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DSTIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SRCPORT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DSTPORT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AddAckRule = new System.Windows.Forms.Button();
            this.eth1.SuspendLayout();
            this.eth2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MAC_Table)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Filters)).BeginInit();
            this.SuspendLayout();
            // 
            // Start_Button
            // 
            this.Start_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Start_Button.Location = new System.Drawing.Point(681, 57);
            this.Start_Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Start_Button.Name = "Start_Button";
            this.Start_Button.Size = new System.Drawing.Size(105, 39);
            this.Start_Button.TabIndex = 0;
            this.Start_Button.Text = "START";
            this.Start_Button.UseVisualStyleBackColor = true;
            this.Start_Button.Click += new System.EventHandler(this.Start_Button_Click);
            // 
            // Stop_Button
            // 
            this.Stop_Button.Enabled = false;
            this.Stop_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Stop_Button.Location = new System.Drawing.Point(792, 57);
            this.Stop_Button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Stop_Button.Name = "Stop_Button";
            this.Stop_Button.Size = new System.Drawing.Size(103, 39);
            this.Stop_Button.TabIndex = 1;
            this.Stop_Button.Text = "STOP";
            this.Stop_Button.UseVisualStyleBackColor = true;
            this.Stop_Button.Click += new System.EventHandler(this.Stop_Button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(714, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "MAC Table";
            // 
            // ETH_IN1
            // 
            this.ETH_IN1.Location = new System.Drawing.Point(5, 21);
            this.ETH_IN1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ETH_IN1.Name = "ETH_IN1";
            this.ETH_IN1.Size = new System.Drawing.Size(103, 22);
            this.ETH_IN1.TabIndex = 8;
            this.ETH_IN1.TextChanged += new System.EventHandler(this.ETH_IN1_TextChanged);
            // 
            // IP_IN1
            // 
            this.IP_IN1.Location = new System.Drawing.Point(5, 49);
            this.IP_IN1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.IP_IN1.Name = "IP_IN1";
            this.IP_IN1.Size = new System.Drawing.Size(103, 22);
            this.IP_IN1.TabIndex = 9;
            // 
            // ARP_IN1
            // 
            this.ARP_IN1.Location = new System.Drawing.Point(5, 78);
            this.ARP_IN1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ARP_IN1.Name = "ARP_IN1";
            this.ARP_IN1.Size = new System.Drawing.Size(103, 22);
            this.ARP_IN1.TabIndex = 10;
            // 
            // TCP_IN1
            // 
            this.TCP_IN1.Location = new System.Drawing.Point(5, 105);
            this.TCP_IN1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TCP_IN1.Name = "TCP_IN1";
            this.TCP_IN1.Size = new System.Drawing.Size(103, 22);
            this.TCP_IN1.TabIndex = 11;
            // 
            // UDP_IN1
            // 
            this.UDP_IN1.Location = new System.Drawing.Point(5, 133);
            this.UDP_IN1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UDP_IN1.Name = "UDP_IN1";
            this.UDP_IN1.Size = new System.Drawing.Size(103, 22);
            this.UDP_IN1.TabIndex = 12;
            // 
            // ICMP_IN1
            // 
            this.ICMP_IN1.Location = new System.Drawing.Point(5, 161);
            this.ICMP_IN1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ICMP_IN1.Name = "ICMP_IN1";
            this.ICMP_IN1.Size = new System.Drawing.Size(103, 22);
            this.ICMP_IN1.TabIndex = 13;
            // 
            // HTTP_IN1
            // 
            this.HTTP_IN1.Location = new System.Drawing.Point(5, 190);
            this.HTTP_IN1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HTTP_IN1.Name = "HTTP_IN1";
            this.HTTP_IN1.Size = new System.Drawing.Size(103, 22);
            this.HTTP_IN1.TabIndex = 14;
            // 
            // HTTPS_IN1
            // 
            this.HTTPS_IN1.Location = new System.Drawing.Point(5, 217);
            this.HTTPS_IN1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HTTPS_IN1.Name = "HTTPS_IN1";
            this.HTTPS_IN1.Size = new System.Drawing.Size(103, 22);
            this.HTTPS_IN1.TabIndex = 15;
            // 
            // TOTAL_IN1
            // 
            this.TOTAL_IN1.Location = new System.Drawing.Point(5, 245);
            this.TOTAL_IN1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TOTAL_IN1.Name = "TOTAL_IN1";
            this.TOTAL_IN1.Size = new System.Drawing.Size(103, 22);
            this.TOTAL_IN1.TabIndex = 16;
            // 
            // TOTAL_OUT1
            // 
            this.TOTAL_OUT1.Location = new System.Drawing.Point(160, 245);
            this.TOTAL_OUT1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TOTAL_OUT1.Name = "TOTAL_OUT1";
            this.TOTAL_OUT1.Size = new System.Drawing.Size(103, 22);
            this.TOTAL_OUT1.TabIndex = 25;
            // 
            // HTTPS_OUT1
            // 
            this.HTTPS_OUT1.Location = new System.Drawing.Point(160, 217);
            this.HTTPS_OUT1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HTTPS_OUT1.Name = "HTTPS_OUT1";
            this.HTTPS_OUT1.Size = new System.Drawing.Size(103, 22);
            this.HTTPS_OUT1.TabIndex = 24;
            this.HTTPS_OUT1.TextChanged += new System.EventHandler(this.textBox11_TextChanged);
            // 
            // HTTP_OUT1
            // 
            this.HTTP_OUT1.Location = new System.Drawing.Point(160, 190);
            this.HTTP_OUT1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HTTP_OUT1.Name = "HTTP_OUT1";
            this.HTTP_OUT1.Size = new System.Drawing.Size(103, 22);
            this.HTTP_OUT1.TabIndex = 23;
            // 
            // ICMP_OUT1
            // 
            this.ICMP_OUT1.Location = new System.Drawing.Point(160, 161);
            this.ICMP_OUT1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ICMP_OUT1.Name = "ICMP_OUT1";
            this.ICMP_OUT1.Size = new System.Drawing.Size(103, 22);
            this.ICMP_OUT1.TabIndex = 22;
            // 
            // UDP_OUT1
            // 
            this.UDP_OUT1.Location = new System.Drawing.Point(160, 133);
            this.UDP_OUT1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UDP_OUT1.Name = "UDP_OUT1";
            this.UDP_OUT1.Size = new System.Drawing.Size(103, 22);
            this.UDP_OUT1.TabIndex = 21;
            // 
            // TCP_OUT1
            // 
            this.TCP_OUT1.Location = new System.Drawing.Point(160, 105);
            this.TCP_OUT1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TCP_OUT1.Name = "TCP_OUT1";
            this.TCP_OUT1.Size = new System.Drawing.Size(103, 22);
            this.TCP_OUT1.TabIndex = 20;
            // 
            // ARP_OUT1
            // 
            this.ARP_OUT1.Location = new System.Drawing.Point(160, 78);
            this.ARP_OUT1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ARP_OUT1.Name = "ARP_OUT1";
            this.ARP_OUT1.Size = new System.Drawing.Size(103, 22);
            this.ARP_OUT1.TabIndex = 19;
            // 
            // IP_OUT1
            // 
            this.IP_OUT1.Location = new System.Drawing.Point(160, 49);
            this.IP_OUT1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.IP_OUT1.Name = "IP_OUT1";
            this.IP_OUT1.Size = new System.Drawing.Size(103, 22);
            this.IP_OUT1.TabIndex = 18;
            // 
            // ETH_OUT1
            // 
            this.ETH_OUT1.Location = new System.Drawing.Point(160, 21);
            this.ETH_OUT1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ETH_OUT1.Name = "ETH_OUT1";
            this.ETH_OUT1.Size = new System.Drawing.Size(103, 22);
            this.ETH_OUT1.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(43, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 19);
            this.label2.TabIndex = 26;
            this.label2.Text = "ETHERNET II";
            // 
            // eth1
            // 
            this.eth1.AccessibleName = "";
            this.eth1.Controls.Add(this.TCP_OUT1);
            this.eth1.Controls.Add(this.ETH_IN1);
            this.eth1.Controls.Add(this.TOTAL_OUT1);
            this.eth1.Controls.Add(this.IP_IN1);
            this.eth1.Controls.Add(this.HTTPS_OUT1);
            this.eth1.Controls.Add(this.ARP_IN1);
            this.eth1.Controls.Add(this.HTTP_OUT1);
            this.eth1.Controls.Add(this.TCP_IN1);
            this.eth1.Controls.Add(this.ICMP_OUT1);
            this.eth1.Controls.Add(this.UDP_IN1);
            this.eth1.Controls.Add(this.UDP_OUT1);
            this.eth1.Controls.Add(this.ICMP_IN1);
            this.eth1.Controls.Add(this.HTTP_IN1);
            this.eth1.Controls.Add(this.ARP_OUT1);
            this.eth1.Controls.Add(this.HTTPS_IN1);
            this.eth1.Controls.Add(this.IP_OUT1);
            this.eth1.Controls.Add(this.TOTAL_IN1);
            this.eth1.Controls.Add(this.ETH_OUT1);
            this.eth1.Location = new System.Drawing.Point(160, 185);
            this.eth1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.eth1.Name = "eth1";
            this.eth1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.eth1.Size = new System.Drawing.Size(268, 282);
            this.eth1.TabIndex = 27;
            this.eth1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(69, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 19);
            this.label3.TabIndex = 28;
            this.label3.Text = "IP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(61, 263);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 19);
            this.label4.TabIndex = 29;
            this.label4.Text = "ARP";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(61, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 19);
            this.label5.TabIndex = 30;
            this.label5.Text = "TCP";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(61, 319);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 19);
            this.label6.TabIndex = 31;
            this.label6.Text = "UDP";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(61, 347);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 19);
            this.label7.TabIndex = 32;
            this.label7.Text = "ICMP";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label8.Location = new System.Drawing.Point(61, 375);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 19);
            this.label8.TabIndex = 33;
            this.label8.Text = "HTTP";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label9.Location = new System.Drawing.Point(61, 403);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 19);
            this.label9.TabIndex = 34;
            this.label9.Text = "HTTPS";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label10.Location = new System.Drawing.Point(64, 431);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 19);
            this.label10.TabIndex = 35;
            this.label10.Text = "Total";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label11.Location = new System.Drawing.Point(200, 163);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 19);
            this.label11.TabIndex = 36;
            this.label11.Text = "IN";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label12.Location = new System.Drawing.Point(349, 163);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 19);
            this.label12.TabIndex = 37;
            this.label12.Text = "OUT";
            // 
            // RESET_ETH1
            // 
            this.RESET_ETH1.Location = new System.Drawing.Point(251, 473);
            this.RESET_ETH1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RESET_ETH1.Name = "RESET_ETH1";
            this.RESET_ETH1.Size = new System.Drawing.Size(79, 39);
            this.RESET_ETH1.TabIndex = 38;
            this.RESET_ETH1.Text = "Reset";
            this.RESET_ETH1.UseVisualStyleBackColor = true;
            this.RESET_ETH1.Click += new System.EventHandler(this.resetEht1);
            // 
            // RESET_ETH2
            // 
            this.RESET_ETH2.Location = new System.Drawing.Point(1616, 473);
            this.RESET_ETH2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RESET_ETH2.Name = "RESET_ETH2";
            this.RESET_ETH2.Size = new System.Drawing.Size(79, 39);
            this.RESET_ETH2.TabIndex = 51;
            this.RESET_ETH2.Text = "Reset";
            this.RESET_ETH2.UseVisualStyleBackColor = true;
            this.RESET_ETH2.Click += new System.EventHandler(this.resetEth2);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label13.Location = new System.Drawing.Point(1715, 163);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 19);
            this.label13.TabIndex = 50;
            this.label13.Text = "OUT";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label14.Location = new System.Drawing.Point(1565, 163);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(24, 19);
            this.label14.TabIndex = 49;
            this.label14.Text = "IN";
            this.label14.Click += new System.EventHandler(this.label14_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label15.Location = new System.Drawing.Point(1429, 431);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 19);
            this.label15.TabIndex = 48;
            this.label15.Text = "Total";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label16.Location = new System.Drawing.Point(1427, 403);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 19);
            this.label16.TabIndex = 47;
            this.label16.Text = "HTTPS";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label17.Location = new System.Drawing.Point(1427, 375);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(45, 19);
            this.label17.TabIndex = 46;
            this.label17.Text = "HTTP";
            this.label17.Click += new System.EventHandler(this.label17_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label18.Location = new System.Drawing.Point(1427, 347);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(44, 19);
            this.label18.TabIndex = 45;
            this.label18.Text = "ICMP";
            this.label18.Click += new System.EventHandler(this.label18_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label19.Location = new System.Drawing.Point(1427, 319);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(38, 19);
            this.label19.TabIndex = 44;
            this.label19.Text = "UDP";
            this.label19.Click += new System.EventHandler(this.label19_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label20.Location = new System.Drawing.Point(1427, 291);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(35, 19);
            this.label20.TabIndex = 43;
            this.label20.Text = "TCP";
            this.label20.Click += new System.EventHandler(this.label20_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label21.Location = new System.Drawing.Point(1427, 263);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(37, 19);
            this.label21.TabIndex = 42;
            this.label21.Text = "ARP";
            this.label21.Click += new System.EventHandler(this.label21_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label22.Location = new System.Drawing.Point(1435, 235);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(22, 19);
            this.label22.TabIndex = 41;
            this.label22.Text = "IP";
            this.label22.Click += new System.EventHandler(this.label22_Click);
            // 
            // eth2
            // 
            this.eth2.Controls.Add(this.TCP_OUT2);
            this.eth2.Controls.Add(this.ETH_IN2);
            this.eth2.Controls.Add(this.TOTAL_OUT2);
            this.eth2.Controls.Add(this.IP_IN2);
            this.eth2.Controls.Add(this.HTTPS_OUT2);
            this.eth2.Controls.Add(this.ARP_IN2);
            this.eth2.Controls.Add(this.HTTP_OUT2);
            this.eth2.Controls.Add(this.TCP_IN2);
            this.eth2.Controls.Add(this.ICMP_OUT2);
            this.eth2.Controls.Add(this.UDP_IN2);
            this.eth2.Controls.Add(this.UDP_OUT2);
            this.eth2.Controls.Add(this.ICMP_IN2);
            this.eth2.Controls.Add(this.HTTP_IN2);
            this.eth2.Controls.Add(this.ARP_OUT2);
            this.eth2.Controls.Add(this.HTTPS_IN2);
            this.eth2.Controls.Add(this.IP_OUT2);
            this.eth2.Controls.Add(this.TOTAL_IN2);
            this.eth2.Controls.Add(this.ETH_OUT2);
            this.eth2.Location = new System.Drawing.Point(1525, 185);
            this.eth2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.eth2.Name = "eth2";
            this.eth2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.eth2.Size = new System.Drawing.Size(268, 282);
            this.eth2.TabIndex = 40;
            this.eth2.TabStop = false;
            this.eth2.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // TCP_OUT2
            // 
            this.TCP_OUT2.Location = new System.Drawing.Point(160, 105);
            this.TCP_OUT2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TCP_OUT2.Name = "TCP_OUT2";
            this.TCP_OUT2.Size = new System.Drawing.Size(103, 22);
            this.TCP_OUT2.TabIndex = 20;
            // 
            // ETH_IN2
            // 
            this.ETH_IN2.Location = new System.Drawing.Point(5, 21);
            this.ETH_IN2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ETH_IN2.Name = "ETH_IN2";
            this.ETH_IN2.Size = new System.Drawing.Size(103, 22);
            this.ETH_IN2.TabIndex = 8;
            // 
            // TOTAL_OUT2
            // 
            this.TOTAL_OUT2.Location = new System.Drawing.Point(160, 245);
            this.TOTAL_OUT2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TOTAL_OUT2.Name = "TOTAL_OUT2";
            this.TOTAL_OUT2.Size = new System.Drawing.Size(103, 22);
            this.TOTAL_OUT2.TabIndex = 25;
            // 
            // IP_IN2
            // 
            this.IP_IN2.Location = new System.Drawing.Point(5, 49);
            this.IP_IN2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.IP_IN2.Name = "IP_IN2";
            this.IP_IN2.Size = new System.Drawing.Size(103, 22);
            this.IP_IN2.TabIndex = 9;
            // 
            // HTTPS_OUT2
            // 
            this.HTTPS_OUT2.Location = new System.Drawing.Point(160, 217);
            this.HTTPS_OUT2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HTTPS_OUT2.Name = "HTTPS_OUT2";
            this.HTTPS_OUT2.Size = new System.Drawing.Size(103, 22);
            this.HTTPS_OUT2.TabIndex = 24;
            // 
            // ARP_IN2
            // 
            this.ARP_IN2.Location = new System.Drawing.Point(5, 78);
            this.ARP_IN2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ARP_IN2.Name = "ARP_IN2";
            this.ARP_IN2.Size = new System.Drawing.Size(103, 22);
            this.ARP_IN2.TabIndex = 10;
            // 
            // HTTP_OUT2
            // 
            this.HTTP_OUT2.Location = new System.Drawing.Point(160, 190);
            this.HTTP_OUT2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HTTP_OUT2.Name = "HTTP_OUT2";
            this.HTTP_OUT2.Size = new System.Drawing.Size(103, 22);
            this.HTTP_OUT2.TabIndex = 23;
            // 
            // TCP_IN2
            // 
            this.TCP_IN2.Location = new System.Drawing.Point(5, 105);
            this.TCP_IN2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TCP_IN2.Name = "TCP_IN2";
            this.TCP_IN2.Size = new System.Drawing.Size(103, 22);
            this.TCP_IN2.TabIndex = 11;
            // 
            // ICMP_OUT2
            // 
            this.ICMP_OUT2.Location = new System.Drawing.Point(160, 161);
            this.ICMP_OUT2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ICMP_OUT2.Name = "ICMP_OUT2";
            this.ICMP_OUT2.Size = new System.Drawing.Size(103, 22);
            this.ICMP_OUT2.TabIndex = 22;
            // 
            // UDP_IN2
            // 
            this.UDP_IN2.Location = new System.Drawing.Point(5, 133);
            this.UDP_IN2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UDP_IN2.Name = "UDP_IN2";
            this.UDP_IN2.Size = new System.Drawing.Size(103, 22);
            this.UDP_IN2.TabIndex = 12;
            // 
            // UDP_OUT2
            // 
            this.UDP_OUT2.Location = new System.Drawing.Point(160, 133);
            this.UDP_OUT2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UDP_OUT2.Name = "UDP_OUT2";
            this.UDP_OUT2.Size = new System.Drawing.Size(103, 22);
            this.UDP_OUT2.TabIndex = 21;
            // 
            // ICMP_IN2
            // 
            this.ICMP_IN2.Location = new System.Drawing.Point(5, 161);
            this.ICMP_IN2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ICMP_IN2.Name = "ICMP_IN2";
            this.ICMP_IN2.Size = new System.Drawing.Size(103, 22);
            this.ICMP_IN2.TabIndex = 13;
            // 
            // HTTP_IN2
            // 
            this.HTTP_IN2.Location = new System.Drawing.Point(5, 190);
            this.HTTP_IN2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HTTP_IN2.Name = "HTTP_IN2";
            this.HTTP_IN2.Size = new System.Drawing.Size(103, 22);
            this.HTTP_IN2.TabIndex = 14;
            // 
            // ARP_OUT2
            // 
            this.ARP_OUT2.Location = new System.Drawing.Point(160, 78);
            this.ARP_OUT2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ARP_OUT2.Name = "ARP_OUT2";
            this.ARP_OUT2.Size = new System.Drawing.Size(103, 22);
            this.ARP_OUT2.TabIndex = 19;
            // 
            // HTTPS_IN2
            // 
            this.HTTPS_IN2.Location = new System.Drawing.Point(5, 217);
            this.HTTPS_IN2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HTTPS_IN2.Name = "HTTPS_IN2";
            this.HTTPS_IN2.Size = new System.Drawing.Size(103, 22);
            this.HTTPS_IN2.TabIndex = 15;
            // 
            // IP_OUT2
            // 
            this.IP_OUT2.Location = new System.Drawing.Point(160, 49);
            this.IP_OUT2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.IP_OUT2.Name = "IP_OUT2";
            this.IP_OUT2.Size = new System.Drawing.Size(103, 22);
            this.IP_OUT2.TabIndex = 18;
            this.IP_OUT2.TextChanged += new System.EventHandler(this.textBox20_TextChanged);
            // 
            // TOTAL_IN2
            // 
            this.TOTAL_IN2.Location = new System.Drawing.Point(5, 245);
            this.TOTAL_IN2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TOTAL_IN2.Name = "TOTAL_IN2";
            this.TOTAL_IN2.Size = new System.Drawing.Size(103, 22);
            this.TOTAL_IN2.TabIndex = 16;
            this.TOTAL_IN2.TextChanged += new System.EventHandler(this.textBox21_TextChanged);
            // 
            // ETH_OUT2
            // 
            this.ETH_OUT2.Location = new System.Drawing.Point(160, 21);
            this.ETH_OUT2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ETH_OUT2.Name = "ETH_OUT2";
            this.ETH_OUT2.Size = new System.Drawing.Size(103, 22);
            this.ETH_OUT2.TabIndex = 17;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label23.Location = new System.Drawing.Point(1408, 207);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(89, 19);
            this.label23.TabIndex = 39;
            this.label23.Text = "ETHERNET II";
            this.label23.Click += new System.EventHandler(this.label23_Click);
            // 
            // timerValue
            // 
            this.timerValue.Location = new System.Drawing.Point(998, 451);
            this.timerValue.Name = "timerValue";
            this.timerValue.Size = new System.Drawing.Size(64, 22);
            this.timerValue.TabIndex = 56;
            this.timerValue.Text = "10";
            this.timerValue.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 757);
            this.splitter1.TabIndex = 57;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.BackColor = System.Drawing.Color.MidnightBlue;
            this.splitter2.Location = new System.Drawing.Point(4, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(4, 757);
            this.splitter2.TabIndex = 58;
            this.splitter2.TabStop = false;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label24.Location = new System.Drawing.Point(940, 452);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(52, 19);
            this.label24.TabIndex = 59;
            this.label24.Text = "Timer ";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label25.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label25.Location = new System.Drawing.Point(748, 512);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(82, 29);
            this.label25.TabIndex = 61;
            this.label25.Text = "Filters";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(530, 443);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 39);
            this.button1.TabIndex = 63;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.clearMacTable);
            // 
            // AddAck
            // 
            this.AddAck.Location = new System.Drawing.Point(1842, 554);
            this.AddAck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddAck.Name = "AddAck";
            this.AddAck.Size = new System.Drawing.Size(79, 39);
            this.AddAck.TabIndex = 70;
            this.AddAck.Text = "AddRow";
            this.AddAck.UseVisualStyleBackColor = true;
            this.AddAck.Click += new System.EventHandler(this.addRow);
            // 
            // DeleteAck
            // 
            this.DeleteAck.Location = new System.Drawing.Point(1842, 640);
            this.DeleteAck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DeleteAck.Name = "DeleteAck";
            this.DeleteAck.Size = new System.Drawing.Size(79, 39);
            this.DeleteAck.TabIndex = 71;
            this.DeleteAck.Text = "Delete";
            this.DeleteAck.UseVisualStyleBackColor = true;
            this.DeleteAck.Click += new System.EventHandler(this.DeleteAck_Click);
            // 
            // ClearAck
            // 
            this.ClearAck.Location = new System.Drawing.Point(1842, 683);
            this.ClearAck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ClearAck.Name = "ClearAck";
            this.ClearAck.Size = new System.Drawing.Size(79, 39);
            this.ClearAck.TabIndex = 72;
            this.ClearAck.Text = "Clear";
            this.ClearAck.UseVisualStyleBackColor = true;
            this.ClearAck.Click += new System.EventHandler(this.ClearAck_Click);
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.button5.Location = new System.Drawing.Point(681, 100);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(214, 32);
            this.button5.TabIndex = 73;
            this.button5.Text = "REFRESH INT";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.refreshInterfaceList);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label26.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label26.Location = new System.Drawing.Point(202, 32);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(115, 29);
            this.label26.TabIndex = 74;
            this.label26.Text = "SYSLOG";
            this.label26.Click += new System.EventHandler(this.label26_Click);
            // 
            // originatorIp
            // 
            this.originatorIp.Location = new System.Drawing.Point(120, 93);
            this.originatorIp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.originatorIp.Name = "originatorIp";
            this.originatorIp.Size = new System.Drawing.Size(135, 22);
            this.originatorIp.TabIndex = 26;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label27.Location = new System.Drawing.Point(147, 70);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(96, 19);
            this.label27.TabIndex = 75;
            this.label27.Text = "Originator IP";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label28.Location = new System.Drawing.Point(283, 70);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(87, 19);
            this.label28.TabIndex = 77;
            this.label28.Text = "Collector IP";
            // 
            // collectorIp
            // 
            this.collectorIp.Location = new System.Drawing.Point(261, 93);
            this.collectorIp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.collectorIp.Name = "collectorIp";
            this.collectorIp.Size = new System.Drawing.Size(139, 22);
            this.collectorIp.TabIndex = 76;
            // 
            // syslogStart
            // 
            this.syslogStart.Location = new System.Drawing.Point(419, 85);
            this.syslogStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.syslogStart.Name = "syslogStart";
            this.syslogStart.Size = new System.Drawing.Size(95, 37);
            this.syslogStart.TabIndex = 78;
            this.syslogStart.Text = "Start";
            this.syslogStart.UseVisualStyleBackColor = true;
            this.syslogStart.Click += new System.EventHandler(this.syslogStart_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label29.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label29.Location = new System.Drawing.Point(1584, 40);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(123, 29);
            this.label29.TabIndex = 79;
            this.label29.Text = "Interfaces";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label30.Location = new System.Drawing.Point(1669, 77);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(80, 19);
            this.label30.TabIndex = 83;
            this.label30.Text = "Interface 2";
            // 
            // InterfaceName2
            // 
            this.InterfaceName2.Location = new System.Drawing.Point(1647, 100);
            this.InterfaceName2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.InterfaceName2.Name = "InterfaceName2";
            this.InterfaceName2.Size = new System.Drawing.Size(139, 22);
            this.InterfaceName2.TabIndex = 82;
            this.InterfaceName2.Text = "Devica Name";
            this.InterfaceName2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label31.Location = new System.Drawing.Point(1535, 77);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(80, 19);
            this.label31.TabIndex = 81;
            this.label31.Text = "Interface 1";
            this.label31.Click += new System.EventHandler(this.label31_Click);
            // 
            // InterfaceName1
            // 
            this.InterfaceName1.Location = new System.Drawing.Point(1506, 100);
            this.InterfaceName1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.InterfaceName1.Name = "InterfaceName1";
            this.InterfaceName1.Size = new System.Drawing.Size(135, 22);
            this.InterfaceName1.TabIndex = 80;
            this.InterfaceName1.Text = "Device Name";
            this.InterfaceName1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.InterfaceName1.TextChanged += new System.EventHandler(this.textBox15_TextChanged);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label35.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label35.Location = new System.Drawing.Point(226, 132);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(131, 29);
            this.label35.TabIndex = 87;
            this.label35.Text = "Interface 1";
            this.label35.Click += new System.EventHandler(this.label35_Click);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label36.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label36.Location = new System.Drawing.Point(1584, 134);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(131, 29);
            this.label36.TabIndex = 88;
            this.label36.Text = "Interface 2";
            this.label36.Click += new System.EventHandler(this.label36_Click);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label44.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label44.Location = new System.Drawing.Point(1175, 147);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(138, 29);
            this.label44.TabIndex = 96;
            this.label44.Text = "Device List";
            this.label44.Click += new System.EventHandler(this.label44_Click_2);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(1222, 443);
            this.button7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(79, 39);
            this.button7.TabIndex = 97;
            this.button7.Text = "Confirm";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.confirmEthDevices);
            // 
            // DeviceList
            // 
            this.DeviceList.BackColor = System.Drawing.Color.RoyalBlue;
            this.DeviceList.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.DeviceList.FormattingEnabled = true;
            this.DeviceList.Location = new System.Drawing.Point(1103, 180);
            this.DeviceList.Name = "DeviceList";
            this.DeviceList.Size = new System.Drawing.Size(297, 259);
            this.DeviceList.TabIndex = 98;
            // 
            // MAC_Table
            // 
            this.MAC_Table.BackgroundColor = System.Drawing.SystemColors.GrayText;
            this.MAC_Table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MAC_Table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MAC,
            this.PORT,
            this.TIMER});
            this.MAC_Table.Location = new System.Drawing.Point(530, 185);
            this.MAC_Table.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MAC_Table.Name = "MAC_Table";
            this.MAC_Table.RowHeadersWidth = 51;
            this.MAC_Table.RowTemplate.Height = 24;
            this.MAC_Table.Size = new System.Drawing.Size(532, 251);
            this.MAC_Table.TabIndex = 6;
            this.MAC_Table.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MAC_Table_CellContentClick);
            // 
            // MAC
            // 
            this.MAC.HeaderText = "MAC";
            this.MAC.MinimumWidth = 6;
            this.MAC.Name = "MAC";
            this.MAC.Width = 125;
            // 
            // PORT
            // 
            this.PORT.HeaderText = "PORT";
            this.PORT.MinimumWidth = 6;
            this.PORT.Name = "PORT";
            this.PORT.Width = 125;
            // 
            // TIMER
            // 
            this.TIMER.HeaderText = "TIMER";
            this.TIMER.MinimumWidth = 6;
            this.TIMER.Name = "TIMER";
            this.TIMER.Width = 125;
            // 
            // confirmTimer
            // 
            this.confirmTimer.Location = new System.Drawing.Point(998, 478);
            this.confirmTimer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.confirmTimer.Name = "confirmTimer";
            this.confirmTimer.Size = new System.Drawing.Size(64, 34);
            this.confirmTimer.TabIndex = 99;
            this.confirmTimer.Text = "Confirm";
            this.confirmTimer.UseVisualStyleBackColor = true;
            this.confirmTimer.Click += new System.EventHandler(this.changeTimer);
            // 
            // Filters
            // 
            this.Filters.BackgroundColor = System.Drawing.SystemColors.GrayText;
            this.Filters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Filters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SeqNum,
            this.PORTACK,
            this.Method,
            this.Direction,
            this.Protocol,
            this.SRCMAC,
            this.SRCIP,
            this.DSTMAC,
            this.DSTIP,
            this.SRCPORT,
            this.DSTPORT,
            this.checkbox});
            this.Filters.Location = new System.Drawing.Point(10, 554);
            this.Filters.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Filters.Name = "Filters";
            this.Filters.RowHeadersWidth = 51;
            this.Filters.RowTemplate.Height = 24;
            this.Filters.Size = new System.Drawing.Size(1826, 266);
            this.Filters.TabIndex = 100;
            this.Filters.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Filters_CellContentClick);
            // 
            // SeqNum
            // 
            this.SeqNum.HeaderText = "SeqNum";
            this.SeqNum.MinimumWidth = 6;
            this.SeqNum.Name = "SeqNum";
            this.SeqNum.Width = 125;
            // 
            // PORTACK
            // 
            this.PORTACK.HeaderText = "PORT";
            this.PORTACK.MinimumWidth = 6;
            this.PORTACK.Name = "PORTACK";
            this.PORTACK.Width = 125;
            // 
            // Method
            // 
            this.Method.HeaderText = "Method";
            this.Method.MinimumWidth = 6;
            this.Method.Name = "Method";
            this.Method.Width = 125;
            // 
            // Direction
            // 
            this.Direction.HeaderText = "Direction";
            this.Direction.MinimumWidth = 6;
            this.Direction.Name = "Direction";
            this.Direction.Width = 125;
            // 
            // Protocol
            // 
            this.Protocol.HeaderText = "Protocol";
            this.Protocol.MinimumWidth = 6;
            this.Protocol.Name = "Protocol";
            this.Protocol.Width = 125;
            // 
            // SRCMAC
            // 
            this.SRCMAC.HeaderText = "SRCMAC";
            this.SRCMAC.MinimumWidth = 6;
            this.SRCMAC.Name = "SRCMAC";
            this.SRCMAC.Width = 125;
            // 
            // SRCIP
            // 
            this.SRCIP.HeaderText = "SRCIP";
            this.SRCIP.MinimumWidth = 6;
            this.SRCIP.Name = "SRCIP";
            this.SRCIP.Width = 125;
            // 
            // DSTMAC
            // 
            this.DSTMAC.HeaderText = "DSTMAC";
            this.DSTMAC.MinimumWidth = 6;
            this.DSTMAC.Name = "DSTMAC";
            this.DSTMAC.Width = 125;
            // 
            // DSTIP
            // 
            this.DSTIP.HeaderText = "DSTIP";
            this.DSTIP.MinimumWidth = 6;
            this.DSTIP.Name = "DSTIP";
            this.DSTIP.Width = 125;
            // 
            // SRCPORT
            // 
            this.SRCPORT.HeaderText = "SRCPORT";
            this.SRCPORT.MinimumWidth = 6;
            this.SRCPORT.Name = "SRCPORT";
            this.SRCPORT.Width = 125;
            // 
            // DSTPORT
            // 
            this.DSTPORT.HeaderText = "DSTPORT";
            this.DSTPORT.MinimumWidth = 6;
            this.DSTPORT.Name = "DSTPORT";
            this.DSTPORT.Width = 125;
            // 
            // checkbox
            // 
            this.checkbox.HeaderText = "checkbox";
            this.checkbox.MinimumWidth = 6;
            this.checkbox.Name = "checkbox";
            this.checkbox.Width = 125;
            // 
            // AddAckRule
            // 
            this.AddAckRule.Location = new System.Drawing.Point(1842, 597);
            this.AddAckRule.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AddAckRule.Name = "AddAckRule";
            this.AddAckRule.Size = new System.Drawing.Size(79, 39);
            this.AddAckRule.TabIndex = 101;
            this.AddAckRule.Text = "AddAck";
            this.AddAckRule.UseVisualStyleBackColor = true;
            this.AddAckRule.Click += new System.EventHandler(this.AddAckRule_Click);
            // 
            // Gui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(1924, 757);
            this.Controls.Add(this.AddAckRule);
            this.Controls.Add(this.Filters);
            this.Controls.Add(this.confirmTimer);
            this.Controls.Add(this.DeviceList);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label44);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.InterfaceName2);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.InterfaceName1);
            this.Controls.Add(this.label29);
            this.Controls.Add(this.syslogStart);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.collectorIp);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.originatorIp);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.ClearAck);
            this.Controls.Add(this.DeleteAck);
            this.Controls.Add(this.AddAck);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.timerValue);
            this.Controls.Add(this.RESET_ETH2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.eth2);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.RESET_ETH1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.eth1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MAC_Table);
            this.Controls.Add(this.Stop_Button);
            this.Controls.Add(this.Start_Button);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Gui";
            this.Text = "Softverovy switch";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.eth1.ResumeLayout(false);
            this.eth1.PerformLayout();
            this.eth2.ResumeLayout(false);
            this.eth2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MAC_Table)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Filters)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Button Start_Button;
        private System.Windows.Forms.Button Stop_Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ETH_IN1;
        private System.Windows.Forms.TextBox IP_IN1;
        private System.Windows.Forms.TextBox ARP_IN1;
        private System.Windows.Forms.TextBox TCP_IN1;
        private System.Windows.Forms.TextBox UDP_IN1;
        private System.Windows.Forms.TextBox ICMP_IN1;
        private System.Windows.Forms.TextBox HTTP_IN1;
        private System.Windows.Forms.TextBox HTTPS_IN1;
        private System.Windows.Forms.TextBox TOTAL_IN1;
        private System.Windows.Forms.TextBox TOTAL_OUT1;
        private System.Windows.Forms.TextBox HTTPS_OUT1;
        private System.Windows.Forms.TextBox HTTP_OUT1;
        private System.Windows.Forms.TextBox ICMP_OUT1;
        private System.Windows.Forms.TextBox UDP_OUT1;
        private System.Windows.Forms.TextBox TCP_OUT1;
        private System.Windows.Forms.TextBox ARP_OUT1;
        private System.Windows.Forms.TextBox IP_OUT1;
        private System.Windows.Forms.TextBox ETH_OUT1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox eth1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button RESET_ETH1;
        private System.Windows.Forms.Button RESET_ETH2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.GroupBox eth2;
        private System.Windows.Forms.TextBox TCP_OUT2;
        private System.Windows.Forms.TextBox ETH_IN2;
        private System.Windows.Forms.TextBox TOTAL_OUT2;
        private System.Windows.Forms.TextBox IP_IN2;
        private System.Windows.Forms.TextBox HTTPS_OUT2;
        private System.Windows.Forms.TextBox ARP_IN2;
        private System.Windows.Forms.TextBox HTTP_OUT2;
        private System.Windows.Forms.TextBox TCP_IN2;
        private System.Windows.Forms.TextBox ICMP_OUT2;
        private System.Windows.Forms.TextBox UDP_IN2;
        private System.Windows.Forms.TextBox UDP_OUT2;
        private System.Windows.Forms.TextBox ICMP_IN2;
        private System.Windows.Forms.TextBox HTTP_IN2;
        private System.Windows.Forms.TextBox ARP_OUT2;
        private System.Windows.Forms.TextBox HTTPS_IN2;
        private System.Windows.Forms.TextBox IP_OUT2;
        private System.Windows.Forms.TextBox TOTAL_IN2;
        private System.Windows.Forms.TextBox ETH_OUT2;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox timerValue;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button AddAck;
        private System.Windows.Forms.Button DeleteAck;
        private System.Windows.Forms.Button ClearAck;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox originatorIp;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox collectorIp;
        private System.Windows.Forms.Button syslogStart;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox InterfaceName2;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox InterfaceName1;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.CheckedListBox DeviceList;
        private System.Windows.Forms.DataGridView MAC_Table;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAC;
        private System.Windows.Forms.DataGridViewTextBoxColumn PORT;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIMER;
        private System.Windows.Forms.Button confirmTimer;
        private System.Windows.Forms.DataGridView Filters;
        private System.Windows.Forms.Button AddAckRule;
        private System.Windows.Forms.DataGridViewTextBoxColumn SeqNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn PORTACK;
        private System.Windows.Forms.DataGridViewTextBoxColumn Method;
        private System.Windows.Forms.DataGridViewTextBoxColumn Direction;
        private System.Windows.Forms.DataGridViewTextBoxColumn Protocol;
        private System.Windows.Forms.DataGridViewTextBoxColumn SRCMAC;
        private System.Windows.Forms.DataGridViewTextBoxColumn SRCIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn DSTMAC;
        private System.Windows.Forms.DataGridViewTextBoxColumn DSTIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn SRCPORT;
        private System.Windows.Forms.DataGridViewTextBoxColumn DSTPORT;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkbox;
    }
}

