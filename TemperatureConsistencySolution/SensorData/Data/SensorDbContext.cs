using System.Data.Entity;
using SensorData.Models;

namespace SensorData.Data
{
    public class SensorDbContext : DbContext
    {
        // Konstruktor koji koristi connection string iz App.config-a
        public SensorDbContext()
            : base("name=SensorDbContext")
        {
        }

        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<TemperatureReading> TemperatureReadings { get; set; }
    }
}
