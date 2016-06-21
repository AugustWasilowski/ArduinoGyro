using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Assets.Scripts
{
    public class ArduinoJob : ThreadedJob
    {
        public Quaternion Data;

        SerialPort SerialPort { get; set; }

        public ArduinoJob(string port, int baudRate)
        {
            Debug.Log(string.Format("Opening Port: {0} at {1} baud.", port, baudRate));
            SerialPort = new SerialPort(port, baudRate);
            Debug.Log("Serial Port Set.");
            SerialPort.Open();
            Debug.Log("Serial Port Open.");
            SerialPort.ErrorReceived += SerialPort_ErrorReceived;
            SerialPort.DataReceived += SerialPortOnDataReceived;
            SerialPort.Disposed += SerialPort_Disposed;
        }

        void SerialPort_Disposed(object sender, EventArgs e)
        {

        }

        private void SerialPortOnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }

        void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            Debug.LogError("Bad thing: " + e.EventType.ToString());
        }   

        protected override void ThreadFunction()
        {
            if (SerialPort.IsOpen)
            {
                Data = GetRotation(SerialPort.ReadLine());
            }
        }

        protected override void OnFinished()
        {
            IsDone = false;
            this.Start();
        }

        private Quaternion GetRotation(string data)
        {
            // This is the crappiest way to get the data out. I couldn't get a JSON parser to work because I'm dumb, so please forgive this. 
            Debug.Log(data);
            var startIndex = data.IndexOf("w") + 2;

            data = data.Substring(startIndex, data.LastIndexOf("}") - startIndex);

            startIndex = data.IndexOf(":") + 1;
            var endIndex = data.IndexOf(",");
            var w = data.Substring(startIndex, endIndex - startIndex);

            startIndex = data.IndexOf("x") + 3;
            endIndex = data.IndexOf("y") - 3;
            var x = data.Substring(startIndex, endIndex - startIndex);

            startIndex = data.IndexOf("y") + 3;
            endIndex = data.IndexOf("z") - 3;
            var y = data.Substring(startIndex, endIndex - startIndex);

            startIndex = data.IndexOf("z") + 3;
            endIndex = data.LastIndexOf("}");
            var z = data.Substring(startIndex, endIndex - startIndex);

            return new Quaternion(float.Parse(w), float.Parse(y), float.Parse(x), float.Parse(z));
            
        }
    }
}
