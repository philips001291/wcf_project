using System;
using SensorData.Data;
using SensorData.Models;

namespace SensorClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pokušavam da kreiram bazu i upišem senzora...");

            try
            {
                using (var context = new SensorDbContext())
                {
                    // Ensure Sensors property is initialized in SensorDbContext
                    if (context.Sensors == null)
                    {
                        throw new InvalidOperationException("Sensors DbSet is not initialized. Check your SensorDbContext implementation.");
                    }

                    var sensor = new Sensor
                    {
                        Name = "TestSensor1"
                    };

                    context.Sensors.Add(sensor);
                    context.SaveChanges();
                }

                Console.WriteLine("Uspešno! Baza i tabela su kreirane, senzor je upisan.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Greška: " + ex.Message);
            }

            Console.WriteLine("Pritisni Enter za izlaz...");
            Console.ReadLine();
        }
    }
}
