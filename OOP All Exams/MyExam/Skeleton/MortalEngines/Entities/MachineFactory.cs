using MortalEngines.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MortalEngines.Entities
{
    public class MachineFactory
    {
        public IMachine CreateMachine(string machineType, string name, double attackPoints, double defensePoints)
        {
            Type type = Assembly.GetCallingAssembly()
                 .GetTypes()
                 .FirstOrDefault(t => t.Name == machineType);

            var machine = (IMachine)Activator.CreateInstance(type, name, attackPoints, defensePoints);

            return machine;
        }
    }
}
