using System;
using System.Threading;
using SensorClient.Utils;
using SensorData.Data;
using SensorData.Models;

namespace SensorClient.Simulation
{
    public class SensorSimulator
    {
        private readonly int _sensorId;
        private readonly string _sensorName;
        private readonly Thread _thread;
        private bool _running = true;

        public SensorSimulator(int sensorId, string sensorName)
        {
            _sensorId = sensorId;
            _sensorName = sensorName;
            _thread = new Thread(Run)
            {
                IsBackground = true
            };
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
                    // Generišemo random temperaturu (recimo 18–30 stepeni)
                    double temperature = RandomGenerator.Next(18, 31);

                    using (var context = new SensorDbContext())
                    {
                        var reading = new TemperatureReading
                        {
                            SensorId = _sensorId,
                            Temperature = temperature,
                            Timestamp = DateTime.Now
                        };

                        context.TemperatureReadings.Add(reading);
                        context.SaveChanges();
                    }

                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {_sensorName} -> {temperature} °C");

                    // Random pauza 1–10 sekundi
                    int delayMs = RandomGenerator.Next(1000, 10001);
                    Thread.Sleep(delayMs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Greška u senzoru {_sensorName}: {ex.Message}");
                    // mala pauza da se ne vrti u krug sa greškom
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
