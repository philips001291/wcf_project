using System;
using System.Threading;
using System.ServiceModel;
using SensorClient.Utils;
using SensorClient.SensorServiceReference;

namespace SensorClient.Simulation
{
    public class SensorSimulator
    {
        private readonly int _sensorId;
        private readonly string _sensorName;
        private readonly Thread _thread;
        private bool _running = true;

        private readonly ISensorService _serviceProxy;

        public SensorSimulator(int sensorId, string sensorName)
        {
            _sensorId = sensorId;
            _sensorName = sensorName;

            _thread = new Thread(Run)
            {
                IsBackground = true
            };

            var binding = new BasicHttpBinding();
            var endpoint = new EndpointAddress("http://localhost:8733/SensorService/");
            var factory = new ChannelFactory<ISensorService>(binding, endpoint);

            _serviceProxy = factory.CreateChannel();
        }

        public void Start()
        {
            _thread.Start();
        }

        public void Stop()
        {
            _running = false;
        }

        private void Run()
        {
            while (_running)
            {
                try
                {
                    double temperature = RandomGenerator.Next(18, 31);
                    var timestamp = DateTime.Now;

                    // Send measurement to the WCF service
                    _serviceProxy.SubmitReading(_sensorId, temperature, timestamp);

                    Console.WriteLine($"[{timestamp:HH:mm:ss}] {_sensorName} -> {temperature} °C");

                    int delayMs = RandomGenerator.Next(1000, 10001);
                    Thread.Sleep(delayMs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in {_sensorName}: {ex.Message}");
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
