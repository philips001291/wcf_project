using System;
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
                // Ensure SensorDbContext implements IDisposable
                using (var context = new SensorDbContext())
                {
                    var sensor = new Sensor
                    {
                        Name = "TestSensor1"
                    };

                    // Use the Sensors property directly
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
