using CosmosX.Core.Contracts;
using CosmosX.IO.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CosmosX.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private ICommandParser commandParser;
        private bool isRunning;

        public Engine(IReader reader, IWriter writer, ICommandParser commandParser)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandParser = commandParser;
            this.isRunning = true;
        }

        public void Run()
        {
            while(isRunning)
            {
                string input = this.reader.ReadLine();
                List<string> arguments = input.Split(' ').ToList();
                string output = this.commandParser.Parse(arguments);

                if (input == "Exit")
                {
                    isRunning = false;
                }

                this.writer.WriteLine(output);
            }
        }
    }
}