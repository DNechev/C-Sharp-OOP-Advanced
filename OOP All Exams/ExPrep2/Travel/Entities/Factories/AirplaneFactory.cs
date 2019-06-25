namespace Travel.Entities.Factories
{
    using Contracts;
    using Airplanes.Contracts;
    using Travel.Entities.Airplanes;
    using System;
    using System.Reflection;
    using System.Linq;

    public class AirplaneFactory : IAirplaneFactory
    {
        public IAirplane CreateAirplane(string type)
        {
            Type planeType = Assembly.GetCallingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == type);

            var airplane = (IAirplane)Activator.CreateInstance(planeType);

            return airplane;
        }
    }
}