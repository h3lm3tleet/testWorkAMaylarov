using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TestWorkCarsPassengers;

namespace CarsAndPassengers
{

    class Program
    {
        static void Main(string[] args)
        {
            var players = new ConcurrentQueue<Player>();
            var cars = new List<Car>();
            var random = new Random();

            for (int i = 0; i < 1000; i++)
            {
                var player = new Player
                {
                    Nickname = $"Player {i}",
                    Coordinate = new Coordinates
                    {
                        X = random.Next(0, 100),
                        Y = random.Next(0, 100)
                    }
                };
                players.Enqueue(player);
            }

            for (int i = 0; i < 200; i++)
            {
                cars.Add(new Car
                {
                    Name = $"Car {i}",
                    Coordinate = new Coordinates
                    {
                        X = random.Next(0, 100),
                        Y = random.Next(0, 100)
                    }
                }
                );
            }

            Parallel.ForEach(cars, car =>
            {
                if (players.TryDequeue(out var driver))
                {
                    car.Driver = driver;
                    driver.Coordinate = car.Coordinate;
                }

                while (car.Passengers.Count < 3 && players.TryDequeue(out var passenger))
                {
                    car.Passengers.Add(passenger);
                    passenger.Coordinate = car.Coordinate;
                }
            }
            );

            for (int i = 0; i < 5; i++)
            {
                int carIndex = random.Next(0, cars.Count);
                Console.WriteLine($"Car: {cars[carIndex].Name}");
                Console.WriteLine($"Driver: {cars[carIndex].Driver.Nickname}");
                Console.WriteLine("Passengers:");
                foreach (var passenger in cars[carIndex].Passengers)
                {
                    Console.WriteLine(passenger.Nickname);
                }
                Console.WriteLine();
            }

            int randomCarIndex = random.Next(0, cars.Count);
            var randomCarCoordinate = cars[randomCarIndex].Coordinate;

            Console.WriteLine($"Players within 15 radius of the car {randomCarIndex}:");
            foreach (var player in players)
            {
                var distance = player.Coordinate.Distance(randomCarCoordinate);
                if (distance <= 15)
                {
                    Console.WriteLine($"{player.Nickname}: {distance}");
                }
            }

            Console.ReadKey();
        }
    }
}
