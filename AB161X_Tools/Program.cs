using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace AB161X_Tools_Console
{
    class Program
    {
        static SerialPort _sp = null;

        static void Main(string[] args)
        {
            Console.WriteLine("AB_161X Tools V0.1");

            if (args.Length < 2)
            {
                Console.WriteLine("para is too few!!!");
                Paras_Help();
                return;
            }

            if (args[0].StartsWith("com"))
            {
                _sp = new SerialPort();
                _sp.BaudRate = 115200; //波特率 
                _sp.DataBits = 8;
                _sp.StopBits = StopBits.One;
                _sp.Parity = Parity.None;
                _sp.ReadTimeout = 500;
                _sp.DtrEnable = false; //IO0=HIGH
                _sp.RtsEnable = false; //EN =HIGH

                try
                {
                    _sp.PortName = args[0];
                    _sp.Open();
                }
                catch(Exception e)
                {
                    Console.WriteLine("con not open {0}!!!", args[0]);
                    return;
                }

                switch (args[1])
                {
                    case "read_flash":
                        read_flash(args);
                        break;

                    case "write_flash":
                        write_flash(args);
                        break;

                    case "erase_flash":
                        erase_flash();
                        break;

                    default:
                        Paras_Help();
                        break;
                }
            }
            else
            {
                Console.WriteLine("please input vail paras!!!");
                Paras_Help();
                return;
            }
        }

        static void Paras_Help()
        {
            Console.WriteLine("Usage: ab161x_tool comXX [read_flash|write_flash|erase_flash]");
            Console.ReadKey();
        }

        public static string ToHexString(byte[] bytes) // 0xae00cf => "AE00CF "
        {
            string hexString = string.Empty;

            if (bytes != null)

            {

                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)

                {

                    strB.Append(bytes[i].ToString("X2"));

                }

                hexString = strB.ToString();

            }
            return hexString;
        }

        static void erase_flash()
        {
            AB161X_Tools ab_tools = new AB161X_Tools(_sp);

            if (!ab_tools.connect_chip())
            {
                Console.WriteLine("connect fail");
                return;
            }
            ab_tools.erase_flash();

            Console.WriteLine("Erase OK!!");
        }

        static void write_flash(string[] args)
        {
            byte[] write_buff = new byte[256];
            byte[] calcu_buff = new byte[0x1000];

            //AB1611_Tools com3 write_flash test.bin
            if (args.Length != 3)
            {
                Console.WriteLine("Paras is too few!!!");
                Console.WriteLine("Use: AB161X_Tools.exe comXX write_flash <addr> <file name>");
                return;
            }

            if (!File.Exists(args[2]))
            {
                Console.WriteLine("Error: File {0} do not exist!!!");
                return;
            }

            AB161X_Tools ab_tools = new AB161X_Tools(_sp);

            if (!ab_tools.connect_chip())
            {
                Console.WriteLine("connect fail");
                return;
            }

            long bak_len = 0x1000;
            Console.Write("Backup Calibration Data ... ... ");

            for (long i = 0x1000; i < 0x1000 + bak_len; i += 0x100)
            {
                ab_tools.read_flash(i, AB161X_Tools.Flash_Length.LEN_256B, write_buff);
                write_buff.CopyTo(calcu_buff, i - 0x1000);
            }
            Console.WriteLine("OK");

            Thread.Sleep(100);
            Console.Write("Erase flash ... ... ");

            if (ab_tools.erase_flash() != 0)
            {
                Console.WriteLine(" Fail!!!");
                return;
            }

            Thread.Sleep(2000);
            Console.WriteLine("OK");

            FileStream fs = new FileStream(args[2], FileMode.Open);
            long file_length = fs.Length;

            for (long i = 0; i < file_length; i += 0x100)
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
                    Console.Write("Write flash fail");
                    break;
                }

                if (i % 0x1000 == 0)
                {
                    Console.Write("\rWrite flash:{0}% ... ", i * 100 / file_length);
                }
            }

            Console.Write("\rWrite:{0} bytes Done",  file_length);
        }

        static void read_flash(string[] args)
        {
            if(args.Length != 5)
            {
                Console.WriteLine("Paras is too few!!!");
                Console.WriteLine("Use: AB161X_Tools.exe comXX read_flash <addr> <len> <file name>");
                return;
            }

            AB161X_Tools ab_tools = new AB161X_Tools(_sp);

            if (!ab_tools.connect_chip())
            {
                Console.WriteLine("connect fail");
                return;
            }

            byte[] a = new byte[256];

            if (File.Exists(args[4]))
            {
                File.Delete(args[4]);
            }

            FileStream fs = new FileStream(args[4], FileMode.Create);

            long addr = long.Parse(args[2], System.Globalization.NumberStyles.HexNumber);
            long length = long.Parse(args[3], System.Globalization.NumberStyles.HexNumber);

            Console.WriteLine("Will frome addr {1} read {0} bytes  to file {2}", length.ToString("x4"), addr.ToString("x8"), args[4]);

            long i = 0;
            for (; i < length; i += 0x100)
            {
                if (ab_tools.read_flash(addr, AB161X_Tools.Flash_Length.LEN_256B, a) != 256)
                {
                    Console.WriteLine("Read flash Fail");
                    break;
                }

                addr   += 0x100;
                fs.Write(a, 0, 0x100);

                if (i % 0x1000 == 0)
                {
                    Console.Write("\rHave read:{0}% ... ", i*100 / length);
                }
            }

            if (length - i < 0x100)
            {
                Console.Write("\rRead flash Compect!!!");
                fs.Flush();
                fs.Close();
            }
            _sp.Close();
        }
    }
}
