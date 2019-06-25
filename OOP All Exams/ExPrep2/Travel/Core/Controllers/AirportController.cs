﻿namespace Travel.Core.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Contracts;
	using Entities;
	using Entities.Contracts;
	using Entities.Factories;
	using Entities.Factories.Contracts;
    using Travel.Entities.Airplanes;
    using Travel.Entities.Items;
    using Travel.Entities.Items.Contracts;

    public class AirportController : IAirportController
	{
		private const int BagValueConfiscationThreshold = 3000;

		private IAirport airport;
		private IAirplaneFactory airplaneFactory;
		private IItemFactory itemFactory;

		public AirportController(IAirport airport)
		{
			this.airport = airport;
			this.airplaneFactory = new AirplaneFactory();
			this.itemFactory = new ItemFactory();
		}

        //ok
		public string RegisterPassenger(string username)
		{
			if (this.airport.GetPassenger(username) != null)
			{
				throw new InvalidOperationException($"Passenger {username} already registered!");
			}

			var passenger = new Passenger(username);

			this.airport.AddPassenger(passenger);

			return $"Registered {passenger.Username}";
		}

        //ok
		public string RegisterBag(string username, IEnumerable<string> bagItems)
		{
			var passenger = this.airport.GetPassenger(username);

            var items = new List<IItem>();

            foreach (var itemType in bagItems)
            {
                var item = this.itemFactory.CreateItem(itemType);
                items.Add(item);
            }

            var bag = new Bag(passenger, items);

			passenger.Bags.Add(bag);

			return $"Registered bag with {string.Join(", ", bagItems)} for {username}";
		}

        //ok
		public string RegisterTrip(string source, string destination, string planeType)
		{
			var airplane = this.airplaneFactory.CreateAirplane(planeType);

			var trip = new Trip(source, destination, airplane);

			this.airport.AddTrip(trip);

			return $"Registered trip {trip.Id}";
		}

        //
		public string CheckIn(string username, string tripId, IEnumerable<int> bagIndicies)
		{
			var passenger = this.airport.GetPassenger(username);
            var trip = this.airport.Trips.FirstOrDefault(t => t.Id == tripId);

			var checkedIn = trip.Airplane.Passengers.Any(p => p.Username == username);
			if (checkedIn)
			{
				throw new InvalidOperationException($"{username} is already checked in!");
			}

			var confiscatedBags = CheckInBags(passenger, bagIndicies);
			trip.Airplane.AddPassenger(passenger);

			return
				$"Checked in {passenger.Username} with {bagIndicies.Count() - confiscatedBags}/{bagIndicies.Count()} checked in bags";
		}

		private int CheckInBags(IPassenger passenger, IEnumerable<int> bagsToCheckIn)
		{
			var bags = passenger.Bags;

			var confiscatedBagCount = 0;
			foreach (var i in bagsToCheckIn)
			{
				var currentBag = bags[i];
				bags.RemoveAt(i);

				if (ShouldConfiscate(currentBag))
				{
					airport.AddConfiscatedBag(currentBag);
					confiscatedBagCount++;
				}
				else
				{
					this.airport.AddCheckedBag(currentBag);
				}
			}
			return confiscatedBagCount;
		}

		private static bool ShouldConfiscate(IBag bag)
		{
			var luggageValue = bag.Items.Sum(v => v.Value);

			var shouldConfiscate = luggageValue > BagValueConfiscationThreshold;
			return shouldConfiscate;
		}
	}
}