﻿using SoftUniRestaurant.Core;
using SoftUniRestaurant.Models.Foods;
using System;
using System.Linq;

namespace SoftUniRestaurant
{
    public class StartUp
    {
        public static void Main()
        {
            Engine engine = new Engine();

            engine.Run();
        }
    }
}
