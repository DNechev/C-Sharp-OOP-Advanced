namespace TheTankGame.Tests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Text;
    using TheTankGame.Entities.Miscellaneous;
    using TheTankGame.Entities.Parts;
    using TheTankGame.Entities.Parts.Contracts;
    using TheTankGame.Entities.Vehicles;

    [TestFixture]
    public class BaseVehicleTests
    {
        [Test]
        public void TestVanguard()
        {
            Vanguard vehicle = new Vanguard("Kolio", 80, 3M, 1, 1, 1, new VehicleAssembler());
            ArsenalPart arsenalPart = new ArsenalPart("Ars1", 10, 3M, 10);
            EndurancePart endurancePart = new EndurancePart("End1", 10, 3M, 10);
            ShellPart shellPart = new ShellPart("Shell1", 10, 3M, 10);

            vehicle.AddArsenalPart(arsenalPart);
            vehicle.AddEndurancePart(endurancePart);
            vehicle.AddShellPart(shellPart);

            int expectedAttack = 11;
            int expectedEndurance = 11;
            int expectedDefense = 11;
            int expectedWeight = 110;
            decimal totalPrice = 12M;
            List<IPart> orderedParts = new List<IPart>();
            orderedParts.Add(arsenalPart);
            orderedParts.Add(shellPart);
            orderedParts.Add(endurancePart);

            StringBuilder result = new StringBuilder();

            result.AppendLine($"{vehicle.GetType().Name} - {vehicle.Model}");
            result.AppendLine($"Total Weight: {vehicle.TotalWeight:F3}");
            result.AppendLine($"Total Price: {vehicle.TotalPrice:F3}");
            result.AppendLine($"Attack: {vehicle.TotalAttack}");
            result.AppendLine($"Defense: {vehicle.TotalDefense}");
            result.AppendLine($"HitPoints: {vehicle.TotalHitPoints}");
            result.Append("Parts: ");
            result.Append("Ars1, End1, Shell1");

            string expectedToString = result.ToString();
            string actualToString = vehicle.ToString();

            Assert.AreEqual(expectedToString, actualToString);
            Assert.AreEqual(expectedAttack, vehicle.TotalAttack);
            Assert.AreEqual(expectedEndurance, vehicle.TotalHitPoints);
            Assert.AreEqual(expectedDefense, vehicle.TotalDefense);
            Assert.AreEqual(expectedWeight, vehicle.TotalWeight);
            Assert.AreEqual(totalPrice, vehicle.TotalPrice);
        }

        [Test]
        public void TestRevenger()
        {
            Revenger vehicle = new Revenger("Kolio", 80, 3M, 1, 1, 1, new VehicleAssembler());
            ArsenalPart arsenalPart = new ArsenalPart("Ars1", 10, 3M, 10);
            EndurancePart endurancePart = new EndurancePart("End1", 10, 3M, 10);
            ShellPart shellPart = new ShellPart("Shell1", 10, 3M, 10);

            vehicle.AddArsenalPart(arsenalPart);
            vehicle.AddEndurancePart(endurancePart);
            vehicle.AddShellPart(shellPart);

            int expectedAttack = 11;
            int expectedEndurance = 11;
            int expectedDefense = 11;
            int expectedWeight = 110;
            decimal totalPrice = 12M;

            Assert.AreEqual(expectedAttack, vehicle.TotalAttack);
            Assert.AreEqual(expectedEndurance, vehicle.TotalHitPoints);
            Assert.AreEqual(expectedDefense, vehicle.TotalDefense);
            Assert.AreEqual(expectedWeight, vehicle.TotalWeight);
            Assert.AreEqual(totalPrice, vehicle.TotalPrice);
        }
    }
}