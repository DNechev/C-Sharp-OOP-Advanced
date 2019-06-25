using MortalEngines.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MortalEngines.Core
{
    public class CommandParser
    {
        private readonly IMachinesManager machineManager;

        public CommandParser(IMachinesManager MachManager)
        {
            this.machineManager = MachManager;
        }

        public string Parse(IList<string> arguments)
        {
            string command = arguments[0];

            string[] args = arguments.Skip(1).ToArray();
            string result = string.Empty;


            //esult = (string)this.machineManager
            //  .GetType()
            //  .GetMethods()
            //  .FirstOrDefault(m => m.Name == command)
            //  .Invoke(this.machineManager, new object[] { commandArguments });
            //

            if (command == "HirePilot")
            {
                result = this.machineManager.HirePilot(args[0]);
            }
            else if (command == "PilotReport")
            {
                result = this.machineManager.PilotReport(args[0]);
            }
            else if (command == "ManufactureTank")
            {
                result = this.machineManager.ManufactureTank(args[0], double.Parse(args[1]), double.Parse(args[2]));
            }
            else if (command == "ManufactureFighter")
            {
                result = this.machineManager.ManufactureFighter(args[0], double.Parse(args[1]), double.Parse(args[2]));
            }
            else if (command == "MachineReport")
            {
                result = this.machineManager.MachineReport(args[0]);
            }
            else if (command == "AggressiveMode")
            {
                result = this.machineManager.ToggleFighterAggressiveMode(args[0]);
            }
            else if (command == "DefenseMode")
            {
                result = this.machineManager.ToggleTankDefenseMode(args[0]);
            }
            else if (command == "Engage")
            {
                result = this.machineManager.EngageMachine(args[0], args[1]);
            }
            else if (command == "Attack")
            {
                result = this.machineManager.AttackMachines(args[0], args[1]);
            }

            return result;
        }
    }
}
