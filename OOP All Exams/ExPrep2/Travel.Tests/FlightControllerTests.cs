// REMOVE any "using" statements, which start with "Travel." BEFORE SUBMITTING
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;

namespace Travel.Tests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Text;
    using Travel.Core.Controllers;
    using Travel.Entities;
    using Travel.Entities.Airplanes;
    using Travel.Entities.Airplanes.Contracts;
    using Travel.Entities.Contracts;
    using Travel.Entities.Items;
    using Travel.Entities.Items.Contracts;

    [TestFixture]
    public class FlightControllerTests
    {
        [Test]
        public void TestSuccessfulTakeOff()
        {
            IAirport airport = new Airport();

            IAirplane airplane = new LightAirplane();
            IPassenger passenger = new Passenger("Pesho");
            IItem item = new Toothbrush();
            IItem item2 = new Jewelery();
            List<IItem> items = new List<IItem>();
            items.Add(item);
            items.Add(item2);
            IBag bag = new Bag(passenger, items);
            airplane.AddPassenger(passenger);
            ITrip trip = new Trip("Sofia", "London", airplane);

            airport.AddTrip(trip);

            FlightController flight = new FlightController(airport);

            var actualResult = flight.TakeOff();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SofiaLondon1:")
                .AppendLine("Successfully transported 1 passengers from Sofia to London.")
                .AppendLine("Confiscated bags: 0 (0 items) => $0");

            var expectedResult = sb.ToString().Trim();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestOverbooked()
        {
            IAirport airport = new Airport();

            IAirplane airplane = new LightAirplane();
            IPassenger passenger = new Passenger("Pesho");
            IPassenger passenger2 = new Passenger("Kolio");
            IPassenger passenger3 = new Passenger("Gosho");
            IPassenger passenger4 = new Passenger("Kiro");
            IPassenger passenger5 = new Passenger("Ganio");
            IPassenger passenger6 = new Passenger("Penka");
            IItem item = new Toothbrush();
            IItem item2 = new Jewelery();
            List<IItem> items = new List<IItem>();
            items.Add(item);
            items.Add(item2);
            IBag bag = new Bag(passenger, items);
            airplane.AddPassenger(passenger);
            airplane.AddPassenger(passenger2);
            airplane.AddPassenger(passenger3);
            airplane.AddPassenger(passenger4);
            airplane.AddPassenger(passenger5);
            airplane.AddPassenger(passenger6);
            ITrip trip = new Trip("Sofia", "London", airplane);

            airport.AddTrip(trip);

            FlightController flight = new FlightController(airport);

            var actualResult = flight.TakeOff();


            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SofiaLondon1:")
                .AppendLine("Overbooked! Ejected Kolio")
                .AppendLine("Confiscated 0 bags ($0)")
                .AppendLine("Successfully transported 5 passengers from Sofia to London.")
                .AppendLine("Confiscated bags: 0 (0 items) => $0");

            var expectedResult = sb.ToString().Trim();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestWithConfiscatedBags()
        {
            IAirport airport = new Airport();

            IAirplane airplane = new LightAirplane();
            IPassenger passenger = new Passenger("Pesho");
            IPassenger passenger2 = new Passenger("Kolio");
            IPassenger passenger3 = new Passenger("Gosho");
            IPassenger passenger4 = new Passenger("Kiro");
            IPassenger passenger5 = new Passenger("Ganio");
            IPassenger passenger6 = new Passenger("Penka");
            IItem item = new Toothbrush();
            IItem item2 = new Jewelery();
            IItem item3 = new Colombian();
            List<IItem> items = new List<IItem>();
            items.Add(item);
            items.Add(item2);
            items.Add(item3);
            IBag bag = new Bag(passenger, items);
            airplane.AddPassenger(passenger);
            airplane.AddPassenger(passenger2);
            airplane.AddPassenger(passenger3);
            airplane.AddPassenger(passenger4);
            airplane.AddPassenger(passenger5);
            airplane.AddPassenger(passenger6);
            ITrip trip = new Trip("Sofia", "London", airplane);

            airport.AddTrip(trip);
            airport.AddConfiscatedBag(bag);

            FlightController flight = new FlightController(airport);

            flight.TakeOff();

            var actualResult = trip.IsCompleted;
            Assert.IsTrue(actualResult);
        }
    }
}
