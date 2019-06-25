using System;
using System.Collections.Generic;
using System.Text;
using DungeonsAndCodeWizards.Bags;
using DungeonsAndCodeWizards.Characters.Contracts;
using DungeonsAndCodeWizards.Characters.Enums;

namespace DungeonsAndCodeWizards.Characters
{
    public class Warrior : Character, IAttackable
    {
        private const double warriorHealth = 100;
        private const double warriorArmor = 50;
        private const double warriorAbilityPoints = 40;
        
        public Warrior(string name, Faction faction)
            : base(name, warriorHealth, warriorArmor, warriorAbilityPoints, new Satchel(), faction)
        {
        }

        public void Attack(Character character)
        {
            if (this.IsAlive && character.IsAlive)
            {
                if (this == character)
                {
                    throw new InvalidOperationException("Cannot attack self!");
                }
                if (this.Faction == character.Faction)
                {
                    throw new ArgumentException($"Friendly fire! Both characters are from {this.Faction} faction!");
                }

                character.TakeDamage(this.AbilityPoints);
            }
            else
            {
                throw new InvalidOperationException("Must be alive to perform this action!");
            }
        }
    }
}
