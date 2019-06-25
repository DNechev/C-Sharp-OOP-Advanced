using MortalEngines.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Entities.Machines
{
    public class Tank : BaseMachine, ITank
    {
        private const double InitialHealth = 100;
        private bool defenseMode;

        public Tank(string name, double attackPoints, double defensePoints ) 
            : base(name, attackPoints - 40, defensePoints + 30, InitialHealth)
        {
            this.DefenseMode = true;
        }

        public bool DefenseMode
        {
            get { return defenseMode; }
            private set { defenseMode = value; }
        }

        public void ToggleDefenseMode()
        {
            if (DefenseMode == true)
            {
                this.DefenseMode = false;
                this.AttackPoints = this.AttackPoints + 40;
                this.DefensePoints = this.DefensePoints - 30;
            }
            else
            {
                this.DefenseMode = true;
                this.AttackPoints = this.AttackPoints - 40;
                this.DefensePoints = this.DefensePoints + 30;
            }
        }

        public override string ToString()
        {
            string toggle = string.Empty;
            if (this.DefenseMode == true)
            {
                toggle = "ON";
            }
            else
            {
                toggle = "OFF";
            }
            return base.ToString() + Environment.NewLine + $" *Defense: {toggle}";
        }
    }
}
