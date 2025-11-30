using SensorData.Models;
using System.Data.Entity;
using System;

public class SensorDbContext : IDisposable
{
    public DbSet<Sensor> Sensors { get; set; }
    public void SaveChanges() { /* implementation */ }
    public void Dispose() { /* implementation */ }
}
