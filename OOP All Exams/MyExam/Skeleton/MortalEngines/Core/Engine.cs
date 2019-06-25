using MortalEngines.Core.Contracts;
using MortalEngines.IO;
using MortalEngines.IO.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MortalEngines.Core
{
    public class Engine : IEngine
    {
        private MyReader reader;
        private IWriter writer;
        private CommandParser commandParser;
        private bool isRunning;

        public Engine(MyReader reader, IWriter writer, CommandParser commandParser)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandParser = commandParser;
            this.isRunning = true;
        }

        public void Run()
        {
            while (isRunning)
            {
                string input = this.reader.Read();
                List<string> arguments = input.Split(' ').ToList();
                string output = this.commandParser.Parse(arguments);

                if (input == "Quit")
                {
                    isRunning = false;
                    break;
                }

                this.writer.Write(output);
            }
        }
    }
}
