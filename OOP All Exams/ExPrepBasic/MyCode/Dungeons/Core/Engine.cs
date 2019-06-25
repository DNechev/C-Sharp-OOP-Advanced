using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonsAndCodeWizards.Core
{
    public class Engine
    {
        private DungeonMaster DungeonMaster;

        public Engine()
        {
            this.DungeonMaster = new DungeonMaster();
        }

        public void RunEngine()
        {
            string input = Console.ReadLine();

            while (!string.IsNullOrEmpty(input))
            {
                string[] args = input.Split().ToArray();
                string command = args[0];
                string[] tokkens = args.Skip(1).ToArray();
                string result = string.Empty;

                try
                {
                    if (command == "JoinParty")
                    {
                        result = this.DungeonMaster.JoinParty(tokkens);
                        Console.WriteLine(result);
                    }
                    else if (command == "AddItemToPool")
                    {
                        result = this.DungeonMaster.AddItemToPool(tokkens);
                        Console.WriteLine(result);
                    }
                    else if (command == "PickUpItem")
                    {
                        result = this.DungeonMaster.PickUpItem(tokkens);
                        Console.WriteLine(result);
                    }
                    else if (command == "UseItem")
                    {
                        result = this.DungeonMaster.UseItem(tokkens);
                        Console.WriteLine(result);
                    }
                    else if (command == "UseItemOn")
                    {
                        result = this.DungeonMaster.UseItemOn(tokkens);
                        Console.WriteLine(result);
                    }
                    else if (command == "GiveCharacterItem")
                    {
                        result = this.DungeonMaster.GiveCharacterItem(tokkens);
                        Console.WriteLine(result);
                    }
                    else if (command == "GetStats")
                    {
                        result = this.DungeonMaster.GetStats();
                        Console.WriteLine(result);
                    }
                    else if (command == "Attack")
                    {
                        result = this.DungeonMaster.Attack(tokkens);
                        Console.WriteLine(result);
                    }
                    else if (command == "Heal")
                    {
                        result = this.DungeonMaster.Heal(tokkens);
                        Console.WriteLine(result);
                    }
                    else if (command == "EndTurn")
                    {
                        result = this.DungeonMaster.EndTurn();
                        Console.WriteLine(result);
                    }
                    else if (command == "IsGameOver")
                    {
                        this.DungeonMaster.IsGameOver();
                    }

                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine("Invalid Operation: " +  ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Parameter Error: " + ex.Message);

                }

                if (DungeonMaster.IsGameOver())
                {
                    break;
                }

                input = Console.ReadLine();
            }

            Console.WriteLine("Final stats: ");
            string finalStat = DungeonMaster.GetStats();
            Console.WriteLine(finalStat);
        }
    }
}
