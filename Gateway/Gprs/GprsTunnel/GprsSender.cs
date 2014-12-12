using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using CFlat.Utility;

namespace WallTraversal.Gateway.Gprs.GprsTunnel
{
    public class GprsSender
    {
        private SerialPort serialPort { get; set; }
        private byte[] packetBytes { get; set; }
        public GprsSender()
        {
            string comPort = ConfigParser.getValueFromCFlatConfig("GprsBridgeComPort");
            Logger.debug("GprsSender: get configured port : {0}", comPort);
            serialPort = new SerialPort(comPort);
            serialPort.BaudRate = 115200;
            serialPort.DataBits = 8;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;
            packetBytes = new byte[240];
            packetBytes[239] = 0xAA;
        }

        public void start()
        {
            serialPort.Open();
        }

        public void stop()
        {
            serialPort.Close();
        }

        //must end with 0xaa
        public void send(string sendString)
        {
            byte[] sendBytes = System.Text.Encoding.Default.GetBytes(sendString);
            int copyLength = sendBytes.Length > 239 ? 239 : sendBytes.Length;
            for (int i = 0; i < copyLength; i++)
                packetBytes[i] = sendBytes[i];
            for (int i = copyLength; i < 239; i++)
                packetBytes[i] = (byte)' ';
            serialPort.Write(packetBytes, 0, packetBytes.Length);
            System.Threading.Thread.Sleep(1000);
        }

        public byte makeSum(byte[] sourceData, int start, int end)
        {
            if ((start > end) || (end >= sourceData.Length))
                throw new Exception("start/end error.");

            int temp = sourceData[start];
            for (int i = start + 1; i <= end; i++)
            {
                temp = temp + sourceData[i];
            }
            return (byte)temp;
        }
    }
}
