using System;
using SensorData.Data;
using SensorData.Models;
using SensorService.Contracts;

namespace SensorService.Services
{
    public class SensorService : ISensorService
    {
        public void SubmitReading(int sensorId, double value, DateTime timestamp)
        {
            // Stores a single temperature reading sent from the client
            using (var context = new SensorDbContext())
            {
                var reading = new TemperatureReading
                {
                    SensorId = sensorId,
                    Temperature = value,
                    Timestamp = timestamp
                };

                context.TemperatureReadings.Add(reading);
                context.SaveChanges();
            }
        }
    }
}
