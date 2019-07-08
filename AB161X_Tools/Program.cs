using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AB161X_Tools
{
    class Program
    {
        static SerialPort _sp = null;

        static void Main(string[] args)
        {
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
                        read_flash(args[0], args);
                        break;

                    case "write_flash":
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
            Console.WriteLine("Usage: ab161x_tool com3 read_flash");
            Console.ReadKey();
        }

        static string ToHexString(byte[] bytes) // 0xae00cf => "AE00CF "
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

        static void read_flash(string com, string[] args)
        {
            AB161X_Tools ab_tools = new AB161X_Tools(_sp);

            if (!ab_tools.connect_chip())
            {
                Console.WriteLine("connect fail");
                return;
            }

            byte[] a = null;

            ab_tools.read_flash(args[2], "01", out a);

            Console.Write(ToHexString(a));

            Console.WriteLine("read_flash");
            _sp.Close();
        }
    }
}
