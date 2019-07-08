using System.IO.Ports;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AB161X_Tools
{
    /*  | cmd_head | cmd_len | cmd | pars |
        | 2 Octs   | 2 oct   |     |      |


    */
    class AB161X_Cmds
    {
        public static string CMD_HEAD = "055A";

        public static string CMD_WRIT = "0707";
        public byte[] CMD_ERSE = { 0x03, 0x07 };
        public static string CMD_READ = "0807";

        public byte[] CMD_UNK1 = { 0x01, 0x02 };
        public byte[] CMD_UNK2 = { 0x00, 0x02 };
        public byte[] CMD_UNK3 = { 0x00, 0x07 };
        public byte[] CMD_UNK4 = { 0x02, 0x07 };

        public byte[] LEN_256B = { 0x01 };
        public byte[] LEN_1KB = { 0x02 };
        public byte[] LEN_2KB = { 0x03 };
        public byte[] LEN_4KB = { 0x04 };
        public byte[] LEN_6KB = { 0x05 };
        public byte[] LEN_8KB = { 0x06 };
        public byte[] LEN_12KB = { 0x07 };
        public byte[] LEN_16KB = { 0x08 };

        void connect_init()
        {
            //41    OUT    05 5a 08 00  01 02 01 00  50 80 00 04
            //41    IN     05 5b 0c 00  01 02 00 00  50 80 00 04  01 00 00 00
            //41    OUT    05 5a 0c 00  00 02 01 00  50 80 00 04  01 00 00 00
            //41    IN     05 5b 07 00  00 02 00 00  00 00 00
            //41    OUT    05 5a 02 00  00 07
            //41    IN     05 5b 06 00  00 07 00 c8 60 13
        }



        void erase_flash()
        {

        }

        void write_flash(byte[] addr, byte[] len, byte[] data)
        {

        }
    }

    class AB161X_Tools
    {
        SerialPort _sp = null;

        public AB161X_Tools(SerialPort sp)
        {
            _sp = sp;
        }

        public bool connect_chip()
        {
            try
            {
                _sp.DtrEnable = true;
                _sp.RtsEnable = true;

                Thread.Sleep(10);

                _sp.RtsEnable = false;
                _sp.DtrEnable = false;

                Thread.Sleep(2);

                byte[] start_cmd = { 0x05, 0x5a, 0x02, 0x00, 0x00, 0x07 };

                _sp.DiscardInBuffer();
                _sp.Write(start_cmd, 0, start_cmd.Length);

                Thread.Sleep(10);

                byte[] tmp = new byte[32];
                int n = _sp.Read(tmp, 0, 12);

                if ((tmp[0] == 0x05) && (tmp[1] == 0x5b))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("{0},{1},{2},{3},{4}", n , tmp[0], tmp[1], tmp[2], tmp[3]);
                }
            } 
            catch(Exception e)
            {
                Console.Write(e.Message);
                return false;
            }
            return false;
        }

        public void read_flash(string addr, string len, out byte[] data)
        {
            string tmp = AB161X_Cmds.CMD_HEAD + "0700" + AB161X_Cmds.CMD_READ + len + addr;

            byte[] write_buf = HexToByte(tmp);
            byte[] out_bud = new byte[1024];

            try
            {
                _sp.DiscardInBuffer();
                _sp.Write(write_buf, 0, write_buf.Length);

            }
            catch (Exception) { }

            int readed = 0;
            int need_read = 269;

            while (need_read - readed > 0)
            {
                try
                {
                    readed += _sp.Read(out_bud, readed, need_read - readed);
                }
                catch (Exception)
                {

                }
            }
            data = out_bud;
        }

        public byte[] HexToByte(string hexString)
        {
            Console.WriteLine(hexString);
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }
}