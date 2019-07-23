using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using AB161X_Tools_Console;

namespace AB161X_Tools_Form
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        string binFile = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            baud_cBox.Items.AddRange(new string[] {"115200","2000000" });
            baud_cBox.Text = "115200";

            new Thread(new ThreadStart(ScanThread))
            {
                IsBackground = true
            }.Start();
        }

        public void ScanThread()
        {
            int f = 0, c = 0;
            string[] serial = SerialPort.GetPortNames();//获取串口名称数组

            sp_cBox.Items.AddRange(serial);

            f = c = serial.Length;

            while (true)
            {
                serial = SerialPort.GetPortNames();//获取串口名称数组
                c = serial.Length;

                if (f != c) //串口数量有变化
                {
                    sp_cBox.Items.Clear();
                    sp_cBox.Items.AddRange(serial);
                }
                f = c;
                Thread.Sleep(500);
            }
        }

        private void file_select_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//多个文件
            dialog.Title = "请选择丝印文件";
            dialog.Filter = "BIN文件(*.bin)|*.bin";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {

                binFile = dialog.FileName;//文件路径及文件名
                binFile_tBox.Text = dialog.SafeFileName;//仅有文件名
                binFile_tBox.BackColor = Color.LightGreen;
            }
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(write_flash))
            {
                IsBackground = true
            }.Start();
        }

        void log_string(string s)
        {
            log_tBox.AppendText(s);
            log_tBox.CreateControl();
        }

        void write_flash()
        {
            SerialPort _sp = new SerialPort();
            _sp.BaudRate = 115200; //波特率 
            _sp.DataBits = 8;
            _sp.StopBits = StopBits.One;
            _sp.Parity = Parity.None;
            _sp.ReadTimeout = 500;
            _sp.DtrEnable = false; //IO0=HIGH
            _sp.RtsEnable = false; //EN =HIGH

            log_tBox.Clear();

            try
            {
                _sp.PortName = sp_cBox.Text;
                _sp.Open();
            }
            catch (Exception e)
            {
                log_string("con not open :" + sp_cBox.Text  + "!!!\r\n");
                return;
            }

            byte[] write_buff = new byte[256];
            byte[] calcu_buff = new byte[0x1000];
            long i = 0;

            if (!File.Exists(binFile))
            {
                log_string("Error: File " + binFile + " do not exist!!!\r\n");
                return;
            }

            AB161X_Tools ab_tools = new AB161X_Tools(_sp);

            log_string("Connect Chip ... ...");
            if (!ab_tools.connect_chip())
            {
                log_string("Fail\r\n");
                return;
            }

            log_string("OK\r\n");
            log_tBox.BackColor = Color.SkyBlue;

            long bak_len = 0x1000;

            log_string("Backup Calibration Data ... ... ");
            for (i = 0x1000; i < 0x1000 + bak_len; i += 0x100)
            {
                ab_tools.read_flash(i, AB161X_Tools.Flash_Length.LEN_256B, write_buff);
                write_buff.CopyTo(calcu_buff, i - 0x1000);
            }
            log_string("OK\r\n");

            Thread.Sleep(100);

            log_string("Erase flash ... ... ");
            if (ab_tools.erase_flash() != 0)
            {
                Console.WriteLine(" Fail!!!");
                return;
            }

            Thread.Sleep(2000);
            log_string("OK\r\n");

            FileStream fs = new FileStream(binFile, FileMode.Open);
            long file_length = fs.Length;

            log_string("Write Flash ... ...\r\n");
            
            for (i = 0; i < file_length; i += 0x100)
            {
                if ((i >= 0x1000) && (i < 0x1000 + bak_len))
                {
                    Array.Copy(calcu_buff, i - 0x1000, write_buff, 0, 0x100);
                    ab_tools.write_flash(i, 0, write_buff);

                    fs.Read(write_buff, 0, 0x100);
                    continue;
                }

                int read_len = fs.Read(write_buff, 0, 0x100);

                if (ab_tools.write_flash(i, 0, write_buff) != 0) //写数据出错
                {
                    log_string("Write flash fail\r\n");
                    break;
                }

                if (i % 0x1000 == 0)
                {
                    download_pBar.Value = (int)(i * 100 / file_length);
                }
            }

            if (file_length - i < 0x100)
            {
                log_string("Write:" + file_length.ToString() + " bytes Done!!!\r\n");
                download_pBar.Value = 100;
                log_tBox.BackColor = Color.LawnGreen;
            }
            else
            {
                log_tBox.BackColor= Color.Red;
                log_string("Write Fail\r\n");
            }

            fs.Close();
            _sp.Close();
        }
    }
}
