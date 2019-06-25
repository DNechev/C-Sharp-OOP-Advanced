using System;
using System.Linq;

namespace AnimalCentre
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string[] commands = Console.ReadLine().Split(" ").ToArray();
            AnimalCentre centre = new AnimalCentre();
            string result = " ";

            while (commands[0] != "End")
            {
                try
                {
                    if (commands[0] == "RegisterAnimal")
                    {
                        string type = commands[1];
                        string name = commands[2];
                        int energy = int.Parse(commands[3]);
                        int happiness = int.Parse(commands[4]);
                        int procedureTime = int.Parse(commands[5]);

                        result = centre.RegisterAnimal(type, name, energy, happiness, procedureTime);

                    }
                    else if (commands[0] == "Chip")
                    {
                        string name = commands[1];
                        int time = int.Parse(commands[2]);
                        result = centre.Chip(name, time);
                    }
                    else if (commands[0] == "Vaccinate")
                    {
                        string name = commands[1];
                        int time = int.Parse(commands[2]);
                        result = centre.Vaccinate(name, time);
                    }
                    else if (commands[0] == "Fitness")
                    {
                        string name = commands[1];
                        int time = int.Parse(commands[2]);
                        result = centre.Fitness(name, time);
                    }
                    else if (commands[0] == "Play")
                    {
                        string name = commands[1];
                        int time = int.Parse(commands[2]);
                        result = centre.Play(name, time);
                    }
                    else if (commands[0] == "DentalCare")
                    {
                        string name = commands[1];
                        int time = int.Parse(commands[2]);
                        result = centre.DentalCare(name, time);
                    }
                    else if (commands[0] == "NailTrim")
                    {
                        string name = commands[1];
                        int time = int.Parse(commands[2]);
                        result = centre.NailTrim(name, time);
                    }
                    else if (commands[0] == "Adopt")
                    {
                        string name = commands[1];
                        string owner = commands[2];
                        result = centre.Adopt(name, owner);
                        centre.Adopt(name, owner);
                    }
                    else
                    {
                        string type = commands[1];
                        centre.History(type);
                    }
                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
                commands = Console.ReadLine().Split(" ").ToArray();
            }
        }
    }
}
