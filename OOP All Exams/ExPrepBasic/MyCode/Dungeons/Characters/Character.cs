using DungeonsAndCodeWizards.Bags;
using DungeonsAndCodeWizards.Characters.Enums;
using DungeonsAndCodeWizards.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsAndCodeWizards.Characters
{
    public abstract class Character
    {
        private string name;
        private double baseHealth;
        private double health;
        private double baseArmor;
        private double armor;
        private double abilityPoints;

        public Character(string name, double health, double armor, double abilityPoints, Bag bag, Faction faction)
        {
            this.Name = name;

            this.BaseHealth = health;
            this.Health = health;

            this.BaseArmor = armor;
            this.Armor = armor;

            this.AbilityPoints = abilityPoints;
            this.Bag = bag;
            this.Faction = faction;
        }

        protected virtual double RestHealthMultiplier => 0.2;

        public bool IsAlive { get; set; } = true;

        public Bag Bag { get; }
        public Faction Faction { get; protected set; }

        public double AbilityPoints
        {
            get { return abilityPoints; }
            private set { abilityPoints = value; }
        }

        public double Armor
        {
            get { return armor; }
            set
            {
                if (value > this.BaseArmor)
                {
                    value = this.BaseArmor;
                }
                else if (value < 0)
                {
                    value = 0;
                }
                armor = value;
            }
        }

        public double BaseArmor
        {
            get { return baseArmor; }
            private set { baseArmor = value; }
        }

        public double Health
        {
            get { return health; }
            set
            {
                if (value > this.BaseHealth)
                {
                    value = this.BaseHealth;
                }
                else if (value < 0)
                {
                    value = 0;
                }
                health = value;
            }
        }

        public double BaseHealth
        {
            get { return baseHealth; }
            private set
            {
                baseHealth = value;
            }
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or whitespace!");
                }
                name = value;
            }
        }

        public void TakeDamage(double hitPoints)
        {
            if (this.IsAlive)
            {
                double initialArmorDamage = this.Armor - hitPoints;
                if (initialArmorDamage <= 0)
                {
                    this.Armor = 0;
                    this.Health = this.Health - Math.Abs(initialArmorDamage);
                    if (this.Health <= 0)
                    {
                        this.IsAlive = false;
                    }
                }
                else
                {
                    this.Armor = this.Armor - hitPoints;
                }
            }
            else
            {
                throw new InvalidOperationException("Must be alive to perform this action!");
            }
        }

        public void Rest()
        {
            if (this.IsAlive)
            {
                this.Health += this.BaseHealth * this.RestHealthMultiplier;
            }
            else
            {
                throw new InvalidOperationException("Must be alive to perform this action!");
            }
        }

        public void UseItem(Item item)
        {
            if (this.IsAlive)
            {
                item.AffectCharacter(this);
            }
            else
            {
                throw new InvalidOperationException("Must be alive to perform this action!");
            }
        }

        public void UseItemOn(Item item, Character character)
        {
            if (this.IsAlive && character.IsAlive)
            {
                item.AffectCharacter(character);
            }
            else
            {
                throw new InvalidOperationException("Must be alive to perform this action!");
            }
        }

        public void GiveCharacterItem(Item item, Character character)
        {
            if (this.IsAlive && character.IsAlive)
            {
                character.Bag.AddItem(item);
            }
            else
            {
                throw new InvalidOperationException("Must be alive to perform this action!");
            }
        }

        public void ReceiveItem(Item item)
        {
            if (this.IsAlive)
            {
                this.Bag.AddItem(item);
            }
            else
            {
                throw new InvalidOperationException("Must be alive to perform this action!");
            }
        }
    }
}
