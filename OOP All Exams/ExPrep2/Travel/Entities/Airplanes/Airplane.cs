﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Entities.Airplanes.Contracts;
using Travel.Entities.Contracts;

namespace Travel.Entities.Airplanes
{
    public abstract class Airplane : IAirplane
    {
        private List<IPassenger> passengers;
        private List<IBag> baggageCompartment;
        public int BaggageCompartments { get; }
        public int Seats { get; }

        protected Airplane(int seats, int baggageCompartments)
        {
            this.Seats = seats;
            this.BaggageCompartments = baggageCompartments;
            this.passengers = new List<IPassenger>();
            this.baggageCompartment = new List<IBag>();
        }

        //todo - add fields if needed

        public IReadOnlyCollection<IBag> BaggageCompartment => this.baggageCompartment.AsReadOnly();
        public IReadOnlyCollection<IPassenger> Passengers => this.passengers.AsReadOnly();
        public bool IsOverbooked => this.Passengers.Count > this.Seats;

        public void AddPassenger(IPassenger passenger)
        {
            this.passengers.Add(passenger);
        }

        public IPassenger RemovePassenger(int seat)
        {
            //Posible bug
            var removedPassenger = this.passengers[seat];
            this.passengers.RemoveAt(seat);
            return removedPassenger;
        }

        public IEnumerable<IBag> EjectPassengerBags(IPassenger passenger)
        {
            //List could be empty
            var removedBags = this.baggageCompartment.Where(x => x.Owner.Username == passenger.Username).ToList();
            this.baggageCompartment.RemoveAll(x => x.Owner.Username == passenger.Username);
            return removedBags;
        }

        public void LoadBag(IBag bag)
        {
            if (this.BaggageCompartments < this.baggageCompartment.Count)
            {
                throw new InvalidOperationException($"No more bag room in {this.GetType().Name}!");
            }
            else
            {
                this.baggageCompartment.Add(bag);
            }
        }
    }
}