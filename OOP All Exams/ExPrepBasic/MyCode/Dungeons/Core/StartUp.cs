﻿using DungeonsAndCodeWizards.Bags;
using DungeonsAndCodeWizards.Core;
using System;
using System.Linq;

namespace DungeonsAndCodeWizards
{
    public class StartUp
    {
        // DO NOT rename this file's namespace or class name.
        // However, you ARE allowed to use your own namespaces (or no namespaces at all if you prefer) in other classes.
        public static void Main(string[] args)
        {
            Engine engine = new Engine();

            engine.RunEngine();
        }
    }
}