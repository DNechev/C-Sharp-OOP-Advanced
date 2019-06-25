namespace TheTankGame.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using IO.Contracts;

    public class Engine : IEngine
    {
        private bool isRunning;
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICommandInterpreter commandInterpreter;

        public Engine(
            IReader reader, 
            IWriter writer, 
            ICommandInterpreter commandInterpreter)
        {
            this.reader = reader;
            this.writer = writer;
            this.commandInterpreter = commandInterpreter;

            this.isRunning = true;
        }

        public void Run()
        {
            while(isRunning)
            {
                string input = this.reader.ReadLine();
                List<string> arguments = input.Split(" ").ToList();
                string output = this.commandInterpreter.ProcessInput(arguments);

                if (input == "Terminate")
                {
                    isRunning = false;
                }

                this.writer.WriteLine(output);
            }
        }
    }
}