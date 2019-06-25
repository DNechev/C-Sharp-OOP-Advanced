namespace TheTankGame.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;

    public class CommandInterpreter : ICommandInterpreter
    {
        private readonly IManager tankManager;

        public CommandInterpreter(IManager tankManager)
        {
            this.tankManager = tankManager;
        }

        public string ProcessInput(IList<string> inputParameters)
        {
            string command = inputParameters[0];
            inputParameters.RemoveAt(0);

            string result = (string)this.tankManager
                .GetType()
                .GetMethods()
                .FirstOrDefault(m => m.Name.Contains(command))
                .Invoke(this.tankManager, new object[] { inputParameters });

            return result;
        }
    }
}