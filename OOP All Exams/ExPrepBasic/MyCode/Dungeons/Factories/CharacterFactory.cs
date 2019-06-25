using DungeonsAndCodeWizards.Characters;
using DungeonsAndCodeWizards.Characters.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsAndCodeWizards.Factories
{
    public class CharacterFactory
    {
        public Character CreateCharacter(string faction, string type, string name)
        {
            if (!Enum.TryParse<Faction>(faction, out var parsedFaction))
            {
                throw new ArgumentException($"Invalid faction \"{faction}\"!");
            }

            if (type == "Warrior")
            {
                Warrior warrior = new Warrior(name, parsedFaction);
                return warrior;
            }
            else if (type == "Cleric")
            {
                Cleric cleric = new Cleric(name, parsedFaction);
                return cleric;
            }
            else
            {
                throw new ArgumentException($"Invalid character type \"{type}\"!");
            }
        }
    }
}
