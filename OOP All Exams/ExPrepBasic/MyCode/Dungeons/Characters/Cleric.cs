using System;
using System.Collections.Generic;
using System.Text;
using DungeonsAndCodeWizards.Bags;
using DungeonsAndCodeWizards.Characters.Contracts;
using DungeonsAndCodeWizards.Characters.Enums;

namespace DungeonsAndCodeWizards.Characters
{
    public class Cleric : Character, IHealable
    {
        private const double clericHealth = 50;
        private const double clericArmor = 25;
        private const double clericAbility = 40;

        public Cleric(string name,Faction faction)
            : base(name, clericHealth, clericArmor, clericAbility, new Backpack(), faction)
        {
        }

        protected override double RestHealthMultiplier => 0.5;

        public void Heal(Character character)
        {
            if (this.IsAlive && character.IsAlive)
            {
                if (this.Faction != character.Faction)
                {
                    throw new InvalidOperationException("Cannot heal enemy character!");
                }
                else
                {
                    character.Health += this.AbilityPoints;
                }
            }
            else
            {
                throw new InvalidOperationException("Must be alive to perform this action!");
            }
        }
    }
}
