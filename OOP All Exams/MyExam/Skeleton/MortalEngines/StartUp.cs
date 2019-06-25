using MortalEngines.Core;
using MortalEngines.Core.Contracts;
using MortalEngines.IO;
using MortalEngines.IO.Contracts;

namespace MortalEngines
{
    public class StartUp
    {
        public static void Main()
        {
            MyReader reader = new MyReader();
            IWriter writer = new Writer();

            IMachinesManager machineManager = new MachinesManager();
            CommandParser commandParser = new CommandParser(machineManager);

            IEngine engine = new Engine(reader, writer, commandParser);
            engine.Run();
        }
    }
}