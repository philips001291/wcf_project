using System;
using System.Linq;
using SensorData.Data;
using SensorData.Models;
using SensorClient.Simulation;

namespace SensorClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Attempting to initialize the database and ensure sensors exist...");

            try
            {
                EnsureSensorsExist();   // Inserts 10 sensors if the table is empty

                Console.WriteLine("Sensors are ready. Starting measurement simulation...");

                StartSimulation();      // Starts a simulator thread for each sensor

                Console.WriteLine("Simulation is running. Press Enter to exit.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.ReadLine();
        }

        /// Checks whether sensors exist in the database.
        /// If not, creates 10 default sensors: Sensor1 ... Sensor10.
        private static void EnsureSensorsExist()
        {
            using (var context = new SensorDbContext())
            {
                if (context.Sensors.Any())
                {
                    Console.WriteLine("Sensors already exist in the database.");
                    return;
                }

                for (int i = 1; i <= 10; i++)
                {
                    var sensor = new Sensor
                    {
                        Name = $"Sensor{i}"
                    };

                    context.Sensors.Add(sensor);
                }

                context.SaveChanges();
                Console.WriteLine("Inserted 10 sensors into the database.");
            }
        }

        /// Reads all sensors from the database and creates a SensorSimulator instance for each one.
        /// Each simulator runs on a background thread and sends temperature readings to the WCF service.

        private static void StartSimulation()
        {
            using (var context = new SensorDbContext())
            {
                var sensors = context.Sensors
                                     .OrderBy(s => s.Id)
                                     .ToList();

                foreach (var sensor in sensors)
                {
                    var simulator = new SensorSimulator(sensor.Id, sensor.Name);
                    simulator.Start();
                }

                Console.WriteLine($"Started {sensors.Count} sensor simulators.");
            }
        }
    }
}
