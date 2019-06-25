using DungeonsAndCodeWizards.Characters;
using DungeonsAndCodeWizards.Characters.Contracts;
using DungeonsAndCodeWizards.Factories;
using DungeonsAndCodeWizards.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonsAndCodeWizards.Core
{
    public class DungeonMaster
    {
        private List<Character> charParty;
        private CharacterFactory charFactory;
        private Stack<Item> itemsPool;
        private ItemFactory itemFactory;
        int lastSurvivor = 0;

        public DungeonMaster()
        {
            this.charParty = new List<Character>();
            this.charFactory = new CharacterFactory();
            this.itemsPool = new Stack<Item>();
            this.itemFactory = new ItemFactory();
        }

        public string JoinParty(string[] args)
        {
            string faction = args[0];
            string type = args[1];
            string name = args[2];

            Character character = this.charFactory.CreateCharacter(faction, type, name);
            this.charParty.Add(character);
            string result = $"{character.Name} joined the party!";
            return result;
        }

        public string AddItemToPool(string[] args)
        {
            string name = args[0];
            Item item = this.itemFactory.createItem(name);
            this.itemsPool.Push(item);
            string result = $"{item.GetType().Name} added to pool.";
            return result;
        }

        public string PickUpItem(string[] args)
        {
            string charName = args[0];

            if (!charParty.Any(c => c.Name == charName))
            {
                throw new ArgumentException($"Character {charName} not found!");
            }
            if (!this.itemsPool.Any())
            {
                throw new InvalidOperationException("No items left in pool!");
            }

            var character = this.charParty.FirstOrDefault(c => c.Name == charName);

            string itemName = this.itemsPool.Peek().GetType().Name;

            character.ReceiveItem(itemsPool.Pop());
            string result = $"{charName} picked up {itemName}!";
            return result;
        }

        public string UseItem(string[] args)
        {
            string charName = args[0];
            string itemName = args[1];

            if (!charParty.Any(c => c.Name == charName))
            {
                throw new ArgumentException($"Character {charName} not found!");
            }

            var character = this.charParty.FirstOrDefault(c => c.Name == charName);

            Item item = character.Bag.GetItem(itemName);

            string result = $"{charName} used {itemName}.";
            return result;
        }

        public string UseItemOn(string[] args)
        {
            string giverName = args[0];
            string receiverName = args[1];
            string itemName = args[2];

            if (!charParty.Any(c => c.Name == giverName))
            {
                throw new ArgumentException($"Character {giverName} not found!");
            }
            if (!charParty.Any(c => c.Name == receiverName))
            {
                throw new ArgumentException($"Character {receiverName} not found!");
            }
            var giver = this.charParty.FirstOrDefault(c => c.Name == giverName);
            var receiver = this.charParty.FirstOrDefault(c => c.Name == receiverName);

            Item item = giver.Bag.GetItem(itemName);
            item.AffectCharacter(receiver);
            string result = $"{giverName} used {itemName} on {receiverName}.";
            return result;
        }

        public string GiveCharacterItem(string[] args)
        {
            string giverName = args[0];
            string receiverName = args[1];
            string itemName = args[2];

            if (!charParty.Any(c => c.Name == giverName))
            {
                throw new ArgumentException($"Character {giverName} not found!");
            }
            if (!charParty.Any(c => c.Name == receiverName))
            {
                throw new ArgumentException($"Character {receiverName} not found!");
            }
            var giver = this.charParty.FirstOrDefault(c => c.Name == giverName);
            var receiver = this.charParty.FirstOrDefault(c => c.Name == receiverName);

            Item item = giver.Bag.GetItem(itemName);
            receiver.ReceiveItem(item);
            string result = $"{giverName} gave {receiverName} {itemName}.";
            return result;
        }

        public string GetStats()
        {
            var sortedParty = charParty.OrderByDescending(s => s.IsAlive).ThenByDescending(s => s.Health);
            StringBuilder sb = new StringBuilder();
            foreach (var ch in sortedParty)
            {
                string status = "";
                if (ch.IsAlive)
                {
                    status = "Alive";
                }
                else
                {
                    status = "Dead";
                }
                sb.AppendLine($"{ch.Name} - HP: {ch.Health}/{ch.BaseHealth}, AP: {ch.Armor}/{ch.BaseArmor}, Status: {status}");
            }
            return sb.ToString().TrimEnd();
        }

        public string Attack(string[] args)
        {
            string attacker = args[0];
            string defender = args[1];

            if (!charParty.Any(c => c.Name == attacker))
            {
                throw new ArgumentException($"Character {attacker} not found!");
            }
            if (!charParty.Any(c => c.Name == defender))
            {
                throw new ArgumentException($"Character {defender} not found!");
            }

            var attackingChar = this.charParty.FirstOrDefault(c => c.Name == attacker);
            var defendingChar = this.charParty.FirstOrDefault(c => c.Name == defender);

            if (!(attackingChar is IAttackable attackable))
            {
                throw new ArgumentException($"{attacker} cannot attack!");
            }

            attackable.Attack(defendingChar);

            string result = "";
            string attMsg = $"{attacker} attacks {defender} for {attackingChar.AbilityPoints} hit points! {defender} has {defendingChar.Health}/{defendingChar.BaseHealth} HP and {defendingChar.Armor}/{defendingChar.BaseArmor} AP left!";
            string dead = $"{defender} is dead!";

            if (!defendingChar.IsAlive)
            {
                result = attMsg + Environment.NewLine + dead;
            }
            else
            {
                result = attMsg;
            }
            return result;
        }

        public string Heal(string[] args)
        {
            string healer = args[0];
            string healed = args[1];

            if (!charParty.Any(c => c.Name == healer))
            {
                throw new ArgumentException($"Character {healer} not found!");
            }
            if (!charParty.Any(c => c.Name == healed))
            {
                throw new ArgumentException($"Character {healed} not found!");
            }

            var healingChar = this.charParty.FirstOrDefault(c => c.Name == healer);
            var healedChar = this.charParty.FirstOrDefault(c => c.Name == healed);

            if (!(healingChar is IHealable healable))
            {
                throw new ArgumentException($"{healer} cannot heal!");
            }

            healable.Heal(healedChar);

            string result = $"{healer} heals {healed} for {healingChar.AbilityPoints}! {healedChar.Name} has {healedChar.Health} health now!";
            return result;
        }

        public string EndTurn()
        {
            int counter = 0;
            StringBuilder sb = new StringBuilder();

            foreach (var hero in charParty.Where(c => c.IsAlive))
            {
                double healthPriorRest = hero.Health;
                hero.Rest();
                sb.AppendLine($"{hero.Name} rests ({healthPriorRest} => {hero.Health})");
                counter++;
            }

            if (counter == 0 || counter == 1)
            {
                lastSurvivor++;
            }

            return sb.ToString().TrimEnd();
        }

        public bool IsGameOver()
        {
            bool result = false;
            int survivors = charParty.Where(p => p.IsAlive).Count();

            if (survivors <= 1 && lastSurvivor > 1)
            {
                result = true;
            }

            return result;
        }
    }
}
