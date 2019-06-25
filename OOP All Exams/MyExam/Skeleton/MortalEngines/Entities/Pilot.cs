using MortalEngines.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MortalEngines.Entities
{
    public class Pilot : IPilot
    {
        private string name;
        private readonly IList<IMachine> machines;

        public Pilot(string name)
        {
            this.Name = name;
            this.machines = new List<IMachine>();
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Pilot name cannot be null or empty string.");
                }
                name = value;
            }
        }
        public IList<IMachine> Machines => this.machines.ToList().AsReadOnly();

        public void AddMachine(IMachine machine)
        {
            if (machine == null)
            {
                throw new NullReferenceException("Null machine cannot be added to the pilot.");
            }
            this.machines.Add(machine);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.Name} - {this.machines.Count} machines");

            foreach (var machine in Machines)
            {
                sb.AppendLine($"- {machine.Name}")
                    .AppendLine($" *Type: {machine.GetType().Name}")
                    .AppendLine($" *Health: {machine.HealthPoints}")
                    .AppendLine($" *Attack: {machine.AttackPoints}")
                    .AppendLine($" *Defense: {machine.DefensePoints}")
                    .AppendLine($" *Targets: {string.Join(",", machine.Targets)}");            
            }

            string output = sb.ToString().Trim();

            return output;
        }
    }
}
