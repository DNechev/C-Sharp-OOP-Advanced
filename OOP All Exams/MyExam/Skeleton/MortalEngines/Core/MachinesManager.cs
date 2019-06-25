namespace MortalEngines.Core
{
    using Contracts;
    using MortalEngines.Common;
    using MortalEngines.Entities;
    using MortalEngines.Entities.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class MachinesManager : IMachinesManager
    {
        private readonly List<IPilot> pilots;
        private readonly MachineFactory machineFactory;
        private readonly List<ITank> tanks;
        private readonly List<IFighter> fighters;
        private readonly List<IMachine> machines;

        public MachinesManager()
        {
            this.pilots = new List<IPilot>();
            this.machineFactory = new MachineFactory();
            this.machines = new List<IMachine>();
            this.tanks = new List<ITank>();
            this.fighters = new List<IFighter>();
        }

        //ok
        public string HirePilot(string name)
        {
            var pilotToAdd = new Pilot(name);

            if (this.pilots.Any(p => p.Name == name))
            {
                return string.Format(OutputMessages.PilotExists, pilotToAdd.Name);
            }
            else
            {
                this.pilots.Add(pilotToAdd);
                return string.Format(OutputMessages.PilotHired, pilotToAdd.Name);
            }
        }

        //ok
        public string ManufactureTank(string name, double attackPoints, double defensePoints)
        {
            var tank = this.machineFactory.CreateMachine("Tank", name, attackPoints, defensePoints);
            if (this.machines.Any(t => t.Name == name))
            {
                return string.Format(OutputMessages.MachineExists, tank.Name);
            }
            else
            {
                this.machines.Add(tank);
                return string.Format(OutputMessages.TankManufactured, tank.Name, tank.AttackPoints, tank.DefensePoints);
            }
        }

        //ok
        public string ManufactureFighter(string name, double attackPoints, double defensePoints)
        {
            var fighter = (IFighter)this.machineFactory.CreateMachine("Fighter", name, attackPoints, defensePoints);
            if (this.machines.Any(f => f.Name == name))
            {
                return string.Format(OutputMessages.MachineExists, fighter.Name);
            }
            else
            {
                this.machines.Add(fighter);

                string aggressive = "OFF";
                if (fighter.AggressiveMode == true)
                {
                    aggressive = "ON";
                }

                return string.Format(OutputMessages.FighterManufactured, fighter.Name, fighter.AttackPoints, fighter.DefensePoints, aggressive);
            }
        }

        //ok
        public string EngageMachine(string selectedPilotName, string selectedMachineName)
        {
            if (!this.pilots.Any(p => p.Name == selectedPilotName))
            {
                return string.Format(OutputMessages.PilotNotFound, selectedPilotName);
            }
            else if (!this.machines.Any(m => m.Name == selectedMachineName))
            {
                return string.Format(OutputMessages.MachineNotFound, selectedMachineName);
            }
            var pilot = this.pilots.FirstOrDefault(p => p.Name == selectedPilotName);
            var machine = this.machines.FirstOrDefault(m => m.Name == selectedMachineName);

            if (machine.Pilot != null)
            {
                return string.Format(OutputMessages.MachineHasPilotAlready, machine.Name);
            }
            else if (machine.Pilot == null)
            {
                pilot.AddMachine(machine);
                machine.Pilot = pilot;
                return string.Format(OutputMessages.MachineEngaged, pilot.Name, machine.Name);
            }
            return string.Empty;
        }

        //ok
        public string AttackMachines(string attackingMachineName, string defendingMachineName)
        {
            if (!this.machines.Any(m => m.Name == attackingMachineName))
            {
                return string.Format(OutputMessages.MachineNotFound, attackingMachineName);
            }
            var attacking = this.machines.FirstOrDefault(m => m.Name == attackingMachineName);

            if (!this.machines.Any(m => m.Name == defendingMachineName))
            {
                return string.Format(OutputMessages.PilotNotFound, defendingMachineName);
            }
            var defending = this.machines.FirstOrDefault(m => m.Name == defendingMachineName);

            if (attacking.HealthPoints == 0)
            {
                return string.Format(OutputMessages.DeadMachineCannotAttack, attacking.Name);
            }
            if (defending.HealthPoints == 0)
            {
                return string.Format(OutputMessages.DeadMachineCannotAttack, defending.Name);
            }

            attacking.Attack(defending);

            return string.Format(OutputMessages.AttackSuccessful, defending.Name, attacking.Name, defending.HealthPoints);
        }

        public string PilotReport(string pilotReporting)
        {
            string result = this.pilots.FirstOrDefault(p => p.Name == pilotReporting).Report();
            return result;
        }

        public string MachineReport(string machineName)
        {
            string result = this.machines.FirstOrDefault(m => m.Name == machineName).ToString();
            return result;
        }

        public string ToggleFighterAggressiveMode(string fighterName)
        {
            if (!this.machines.Any(m => m.Name == fighterName))
            {
                return string.Format(OutputMessages.MachineNotFound, fighterName);
            }
            var fighter = (IFighter)this.machines.FirstOrDefault(f => f.Name == fighterName);

            fighter.ToggleAggressiveMode();
            return string.Format(OutputMessages.FighterOperationSuccessful, fighter.Name);
        }

        public string ToggleTankDefenseMode(string tankName)
        {
            if (!this.machines.Any(m => m.Name == tankName))
            {
                return string.Format(OutputMessages.MachineNotFound, tankName);
            }
            var tank = (ITank)this.machines.FirstOrDefault(f => f.Name == tankName);

            tank.ToggleDefenseMode();
            return string.Format(OutputMessages.TankOperationSuccessful, tank.Name);
        }
    }
}