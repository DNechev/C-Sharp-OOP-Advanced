using System;

namespace AnimalCentre.IO
{
    public class Reader : IReader

    {
    public string readData()
    {
        return Console.ReadLine();
    }
    }
}