using System;
using System.Collections.Generic;
using System.Linq;
using StorageMaster.Core;

namespace StorageMaster.Core
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] command = Console.ReadLine().Split(' ').ToArray();
            StorageMaster sm = new StorageMaster();

            while (command[0] != "END")
            {
                try
                {
                    if (command[0] == "AddProduct")
                    {
                        string type = command[1];
                        double price = double.Parse(command[2]);
                        var result = sm.AddProduct(type, price);
                        Console.WriteLine(result);

                    }
                    else if (command[0] == "RegisterStorage")
                    {
                        string type = command[1];
                        string name = command[2];
                        var result = sm.RegisterStorage(type, name);
                        Console.WriteLine(result);
                    }
                    else if (command[0] == "SelectVehicle")
                    {
                        string storageName = command[1];
                        int garageSlot = int.Parse(command[2]);
                        var result = sm.SelectVehicle(storageName, garageSlot);
                        Console.WriteLine(result);
                    }
                    else if (command[0] == "LoadVehicle")
                    {
                        int lenght = command.Length;
                        List<string> products = new List<string>();

                        for (int i = 1; i < command.Length; i++)
                        {
                            products.Add(command[i]);
                        }
                        IEnumerable<string> p = products.AsEnumerable();
                        var result = sm.LoadVehicle(p);
                        Console.WriteLine(result);
                    }
                    else if (command[0] == "SendVehicleTo")
                    {
                        string sourceName = command[1];
                        int sourceSlot = int.Parse(command[2]);
                        string destinationName = command[3];

                        var result = sm.SendVehicleTo(sourceName, sourceSlot, destinationName);
                        Console.WriteLine(result);
                    }
                    else if (command[0] == "UnloadVehicle")
                    {
                        string storageName = command[1];
                        int sourceSlot = int.Parse(command[2]);

                        var result = sm.UnloadVehicle(storageName, sourceSlot);
                        Console.WriteLine(result);
                    }
                    else if (command[0] == "GetStorageStatus")
                    {
                        string storageName = command[1];
                        var result = sm.GetStorageStatus(storageName);
                        Console.WriteLine(result);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                command = Console.ReadLine().Split(' ').ToArray();
            }

            string summary = sm.GetSummary();
            Console.WriteLine(summary);
        }
    }
}
