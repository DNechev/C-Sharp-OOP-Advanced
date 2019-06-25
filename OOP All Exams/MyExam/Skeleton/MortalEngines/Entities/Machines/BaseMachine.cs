using MortalEngines.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MortalEngines.Entities
{
    public abstract class BaseMachine : IMachine
    {
        private string name;
        private IPilot pilot;
        private double attackPoints;
        private double defensePoints;
        private double healthPoints;
        private readonly IList<string> targets;

        protected BaseMachine(string name, double attackPoints, double defensePoints, double healthPoints)
        {
            this.Name = name;
            this.AttackPoints = attackPoints;
            this.DefensePoints = defensePoints;
            this.HealthPoints = healthPoints;
            this.targets = new List<string>();
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Machine name cannot be null or empty.");
                }
                name = value;
            }
        }

        public IPilot Pilot
        {
            get { return pilot; }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException("Pilot cannot be null.");
                }
                pilot = value;
            }
        }

        public IList<string> Targets => this.targets.ToList().AsReadOnly();

        public double AttackPoints { get => attackPoints; protected set => attackPoints = value; }
        public double DefensePoints { get => defensePoints; protected set => defensePoints = value; }
        public double HealthPoints { get => healthPoints; set => healthPoints = value; }

        public void Attack(IMachine target)
        {
            if (target == null)
            {
                throw new NullReferenceException("Target cannot be null");
            }

            double damage = this.AttackPoints - target.DefensePoints;

            target.HealthPoints = target.HealthPoints - damage;

            if (target.HealthPoints < 0)
            {
                target.HealthPoints = 0;
            }

            this.targets.Add(target.Name);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"- {this.Name}")
                .AppendLine($" *Type: {this.GetType().Name}")
                .AppendLine($" *Health: {this.HealthPoints:f2}")
                .AppendLine($" *Attack: {this.AttackPoints:f2}")
                .AppendLine($" *Defense: {this.DefensePoints:f2}");

            if (this.targets.Count == 0)
            {
                sb.AppendLine(" *Targets: None");
            }
            else
            {
                string targetsToString = string.Join(",", Targets);
                sb.AppendLine(" *Targets: " + targetsToString);
            }

            string output = sb.ToString().Trim();

            return output;
        }

    }
}