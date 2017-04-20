
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System;
namespace ProduceManager
{
    public class COM_ProtConnect : IDisposable
    {
        public bool IsPortOpen
        {
            set;
            get;
        }
        public string ErroMsg { get; set; }
        public string SendMsg
        {
            get
            {
                return _SendMsg;
            }
            set
            {
                _SendMsg = value;
            }
        }
        private string _SendMsg = "S";
        public event invokeDelegate OnPortDataReceived
        {
            add
            {
                _onPortDataReceived = value;
            }
            remove
            {
                _onPortDataReceived = null;
            }
        }
        private event invokeDelegate _onPortDataReceived;
        public delegate void invokeDelegate(string recVale);
        private System.IO.Ports.SerialPort Balance_Port
        {
            get
            {
                if (_Balance_Port == null)
                {
                    Init_BalancePort();
                }
                return _Balance_Port;
            }
        }
        private System.IO.Ports.SerialPort _Balance_Port = null;

        private System.IO.Ports.SerialPort CodeCard_Port
        {
            get
            {
                if (_CodeCard_Port == null)
                {
                    Init_CodeCardPort();
                }
                return _CodeCard_Port;
            }
        }
        private System.IO.Ports.SerialPort _CodeCard_Port = null;
        private string _InputData = string.Empty;
        private List<byte> _CodeCard = new List<byte>();
        public int DobSeed
        {
            set { _DobSeed = value; }
            get { return _DobSeed; }
        }
        private int _DobSeed = 2;
        public COM_ProtConnect()
        {
        }
        public void Init_BalancePort()
        {
            Init_BalancePort(2400, "COM1");
        }
        public void Init_BalancePort(string comName)
        {
            Init_BalancePort(2400, comName);
        }
        public void Init_BalancePort(int baudRate, string comName)
        {
            Init_SerialPort(ref _Balance_Port, Balance_DataReceived, baudRate, comName);
        }
        public void Init_BalancePort(
          int baudRate,
          string comName,
          System.IO.Ports.Parity parity,//指定奇偶校验位
          System.IO.Ports.StopBits stopBits,//停止位
          int dataBits)//数据位数
        {
            Init_SerialPort(ref _Balance_Port, Balance_DataReceived, baudRate, comName, parity, stopBits, dataBits);
        }
        public void Init_CodeCardPort()
        {
            Init_CodeCardPort(9600, "COM1");
        }
        public void Init_CodeCardPort(string comName)
        {
            Init_CodeCardPort(9600, comName);
        }
        public void Init_CodeCardPort(int baudRate, string comName)
        {
            Init_SerialPort(ref _CodeCard_Port, CodeCard_DataReceived, baudRate, comName);
        }

        public void Init_SerialPort(
          ref System.IO.Ports.SerialPort serPort,
          System.IO.Ports.SerialDataReceivedEventHandler recFunction,
          int baudRate,
          string comName)
        {
            Init_SerialPort(ref serPort, recFunction, baudRate, comName, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One, 8);
        }
        public void Init_SerialPort(
          ref System.IO.Ports.SerialPort serPort,
          System.IO.Ports.SerialDataReceivedEventHandler recFunction,
          int baudRate,
          string comName,
          System.IO.Ports.Parity parity,
          System.IO.Ports.StopBits stopBits,
          int dataBits)
        {
            serPort = new System.IO.Ports.SerialPort();
            serPort.BaudRate = baudRate;
            serPort.PortName = comName;
            serPort.Parity = parity;
            serPort.StopBits = stopBits;
            serPort.DataBits = dataBits;
            try
            {
                serPort.Open();
                serPort.DataReceived += recFunction;
                ErroMsg = string.Empty;
                IsPortOpen = true;
            }
            catch
            {
                ErroMsg = "端口 " + serPort.PortName + " 不能打开!";
            }
        }
        private void Balance_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] readBuffer = new byte[Balance_Port.BytesToRead];
            Balance_Port.BaseStream.Read(readBuffer, 0, readBuffer.Length);
            Balance_DataReceived(readBuffer);
        }
        private void CodeCard_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] readBuffer = new byte[_CodeCard_Port.BytesToRead];
            _CodeCard_Port.BaseStream.Read(readBuffer, 0, readBuffer.Length);
            CodeCard_DataReceived(readBuffer);
        }
        private void Balance_DataReceived(byte[] readBuffer)
        {
            for (int i = 0; i < readBuffer.Length; i++)
            {
                if (readBuffer[i] > 128)
                {
                    readBuffer[i] = ((byte)(readBuffer[i] - 128));
                }
            }
            _InputData += Encoding.ASCII.GetString(readBuffer).Trim();
            if ((_onPortDataReceived != null) && (readBuffer[readBuffer.Length - 1] == 10))
            {
                byte[] _allIpts = Encoding.ASCII.GetBytes(_InputData);
                for (int i = 0; i < _allIpts.Length; i++)
                {
                    if ((_allIpts[i] >= 48 && _allIpts[i] <= 57) || _allIpts[i] == 46)
                    {
                    }
                    else
                    {
                        _allIpts[i] = 32;
                    }
                }
                string AllInputData = Encoding.ASCII.GetString(_allIpts).Trim();
                _InputData = string.Empty;
                if (AllInputData.Equals(string.Empty)) return;

                try
                {
                    AllInputData = System.Text.RegularExpressions.Regex.Replace(AllInputData, @"[^0-9+-.]", string.Empty);
                    decimal dData = 0;
                    decimal.TryParse(AllInputData, out dData);
                    AllInputData = Math.Round(dData, _DobSeed, MidpointRounding.AwayFromZero).ToString();
                    if (null == CApplication.CurrForm || !CApplication.CurrForm.InvokeRequired)
                    {
                        _onPortDataReceived(AllInputData);
                    }
                    else
                    {
                        CApplication.CurrForm.Invoke(new Action<string>(_onPortDataReceived), AllInputData);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), AllInputData);
                }
            }
        }
        private void CodeCard_DataReceived(byte[] readBuffer)
        {
            _CodeCard.AddRange(readBuffer);
            if (_CodeCard.Count < 10) return;
            string AllInputData = string.Empty;
            foreach (byte _cbyte in _CodeCard)
            {
                AllInputData += string.Format("{0:00}", _cbyte);
            }
            _CodeCard.Clear();
            _onPortDataReceived(AllInputData);
        }
        public void Close()
        {
            if (Balance_Port != null && Balance_Port.IsOpen) Balance_Port.Close();
            if (_CodeCard_Port != null && _CodeCard_Port.IsOpen) _CodeCard_Port.Close();
        }
        public void port_Send()
        {
            if (Balance_Port != null)
            {
                Balance_Port.WriteLine(SendMsg);
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            Close();
            if (_Balance_Port != null)
            {
                _Balance_Port.Dispose();
                _Balance_Port = null;
            }
            if (_CodeCard_Port != null)
            {
                _CodeCard_Port.Dispose();
                _CodeCard_Port = null;
            }
        }

        #endregion
    }

}
