using MortalEngines.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Entities.Machines
{
    public class Fighter : BaseMachine, IFighter
    {
        private const double InitialHealth = 200;
        private bool aggressiveMode;

        public Fighter(string name, double attackPoints, double defensePoints) 
            : base(name, attackPoints + 50, defensePoints - 25, InitialHealth)
        {
            this.AggressiveMode = true;
        }

        public bool AggressiveMode
        {
            get { return aggressiveMode; }
            private set { aggressiveMode = value; }
        }

        public void ToggleAggressiveMode()
        {
            if (AggressiveMode == true)
            {
                this.AggressiveMode = false;
                this.AttackPoints = this.AttackPoints - 50;
                this.DefensePoints = this.DefensePoints + 25;
            }
            else
            {
                this.AggressiveMode = true;
                this.AttackPoints = this.AttackPoints + 50;
                this.DefensePoints = this.DefensePoints - 25;
            }
        }

        public override string ToString()
        {
            string toggle = string.Empty;
            if (this.AggressiveMode == true)
            {
                toggle = "ON";
            }
            else
            {
                toggle = "OFF";
            }
            return base.ToString() + Environment.NewLine + $" *Aggressive: {toggle}";
        }
    }
}
