using System;

namespace SensorData.Models
{
    public class TemperatureReading
    {
        public int Id { get; set; }
        public int SensorId { get; set; }
        public double Temperature { get; set; }
        public DateTime Timestamp { get; set; }
    }

}
