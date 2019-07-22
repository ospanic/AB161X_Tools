using System.IO.Ports;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AB161X_Tools_Console
{
    /*  | cmd_head | cmd_len | cmd | pars |
        | 2 Octs   | 2 oct   |     |      |


    */
    class AB161X_Cmds
    {
        public static string CMD_HEAD = "055A";

        public static string CMD_WRIT = "0707";
        public static string CMD_ERSE = "0307";
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
    }

    public class AB161X_Tools
    {
        SerialPort _sp = null;

        public enum Flash_Length
        {
            LEN_256B = 1,
            LEN_1KB  = 2, 
            LEN_2KB  = 3,
            LEN_4KB  = 4,
            LEN_6KB  = 5,
            LEN_8KB  = 6,
            LEN_12KB = 7,
            LEN_16KB = 8
        }

        public AB161X_Tools(SerialPort sp)
        {
            _sp = sp;
        }

        public void reset()
        {
            try
            {
                _sp.DtrEnable = false;
                _sp.RtsEnable = true;

                Thread.Sleep(10);

                _sp.RtsEnable = false;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        public bool connect_chip()
        {
            Console.Write("Connecting Chip ... ... ");

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
                    Console.WriteLine("OK");
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

        public int erase_flash()
        {
            string tmp = AB161X_Cmds.CMD_HEAD + "0200" + AB161X_Cmds.CMD_ERSE;

            byte[] write_buf = HexToByte(tmp);

            try
            {
                _sp.DiscardInBuffer();
                _sp.Write(write_buf, 0, write_buf.Length);
            }
            catch (Exception) { }

            return 0;
        }

        public int write_flash(long addr, Flash_Length len, byte[] data)
        {
            string addr_str = addr.ToString("X8");

            addr_str = addr_str.Substring(6, 2) + addr_str.Substring(4, 2) + addr_str.Substring(2, 2) + addr_str.Substring(0, 2);

            string tmp = AB161X_Cmds.CMD_HEAD + "0701" + AB161X_Cmds.CMD_WRIT + CRC8Calculate(data,data.Length).ToString("X2") + addr_str;

            byte[] write_buf = HexToByte(tmp);

            try
            {
                _sp.DiscardInBuffer();
                _sp.Write(write_buf, 0, write_buf.Length);
                _sp.Write(data, 0, data.Length);
            }
            catch (Exception) { }

            int have_read = 0;  //已经读出的数据
            int need_read = 11; //还需要读出的数据

            while (need_read - have_read > 0)
            {
                try
                {
                    have_read += _sp.Read(write_buf, have_read, need_read - have_read);
                }
                catch (Exception)
                {
                    return 0;
                }
            }

            return 0;
        }

        public int read_flash(long addr, Flash_Length len, byte[] data)
        {
            byte[] read_buff = new byte[10240];  //10K
            string len_str = null;
            string addr_str = addr.ToString("X8");

            addr_str = addr_str.Substring(6, 2) + addr_str.Substring(4, 2) + addr_str.Substring(2, 2) + addr_str.Substring(0, 2);

            switch (len)
            {
                case Flash_Length.LEN_256B:
                    len_str = "01";
                    break;

                case Flash_Length.LEN_1KB:
                    len_str = "02";
                    break;

                case Flash_Length.LEN_4KB:
                    len_str = "04";
                    break;

                case Flash_Length.LEN_8KB:
                    len_str = "06";
                    break;

                default:
                    len_str = "01";
                    break;
            }

            string tmp = AB161X_Cmds.CMD_HEAD + "0700" + AB161X_Cmds.CMD_READ + len_str + addr_str;

            byte[] write_buf = HexToByte(tmp);

            try
            {
                _sp.DiscardInBuffer();
                _sp.Write(write_buf, 0, write_buf.Length);
            }
            catch (Exception) { }

            int have_read = 0;  //已经读出的数据
            int need_read = 269;//还需要读出的数据

            while (need_read - have_read > 0)
            {
                try
                {
                    have_read += _sp.Read(read_buff, have_read, need_read - have_read);
                }
                catch (Exception)
                {
                    return 0;
                }
            }

            Array.Copy(read_buff, 13, data, 0, 256);

            return 256;
        }

        public byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        public byte CRC8Calculate(byte[] data, long length)
        {
            byte retCRCValue = 0x00;
            int pData =0;
            byte pDataBuf = 0;

            while (length > 0 )
            {
                length--;

                pDataBuf = data[pData];
                pData++;

                for ( int i = 0; i < 8; i++)
                {
                    if (((retCRCValue ^ (pDataBuf)) & 0x01) > 0)
                    {
                        retCRCValue ^= 0x18;
                        retCRCValue >>= 1;
                        retCRCValue |= 0x80;
                    }
                    else
                    {
                        retCRCValue >>= 1;
                    }
                    pDataBuf >>= 1;
                }
            }
            return retCRCValue;
        }
    }
}