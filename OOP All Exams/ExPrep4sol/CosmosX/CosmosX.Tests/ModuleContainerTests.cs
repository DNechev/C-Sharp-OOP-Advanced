namespace CosmosX.Tests
{
    using CosmosX.Entities.Containers;
    using CosmosX.Entities.Modules.Absorbing;
    using CosmosX.Entities.Modules.Energy;
    using CosmosX.Entities.Modules.Energy.Contracts;
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class ModuleContainerTests
    {
        [Test]
        public void Test()
        {
            int moduleCapacity = 1;
            var cryoMod = new CryogenRod(1, 1000);

            ModuleContainer testContainer = new ModuleContainer(moduleCapacity);

            testContainer.AddEnergyModule(cryoMod);

            Assert.AreEqual(testContainer.ModulesByInput.Count, 1);
            Assert.AreEqual(testContainer.TotalEnergyOutput, 1000);
            Assert.AreEqual(testContainer.TotalHeatAbsorbing, 0);

        }

        [Test]
        public void TestEx()
        {
            int moduleCapacity = 1;
            CryogenRod cryoMod = null;

            ModuleContainer testContainer = new ModuleContainer(moduleCapacity);

            Assert.Throws<ArgumentException>(() => testContainer.AddEnergyModule(cryoMod));
        }

        [Test]
        public void TestHeatModule()
        {
            int moduleCapacity = 1;
            var cdSystem = new CooldownSystem(1, 1000);

            ModuleContainer testContainer = new ModuleContainer(moduleCapacity);

            testContainer.AddAbsorbingModule(cdSystem);

            Assert.AreEqual(testContainer.ModulesByInput.Count, 1);
            Assert.AreEqual(testContainer.TotalEnergyOutput, 0);
            Assert.AreEqual(testContainer.TotalHeatAbsorbing, 1000);
        }



        [Test]
        public void TestFullContainer()
        {
            int moduleCapacity = 1;
            var cdSystem = new CooldownSystem(2, 1000);
            var cryoMod = new CryogenRod(1, 1000);

            ModuleContainer testContainer = new ModuleContainer(moduleCapacity);

            testContainer.AddEnergyModule(cryoMod);
            testContainer.AddAbsorbingModule(cdSystem);

            Assert.AreEqual(testContainer.ModulesByInput.Count, 1);
            Assert.AreEqual(testContainer.TotalEnergyOutput, 0);
            Assert.AreEqual(testContainer.TotalHeatAbsorbing, 1000);
        }
    }
}