
using System;
using System.Linq;
namespace FestivalManager.Core
{
	using System.Reflection;
	using Contracts;
	using Controllers;
	using Controllers.Contracts;
	using IO.Contracts;

	class Engine : IEngine
	{
	    private IReader _reader;
	    private IWriter _writer;
        private IFestivalController _festivalCоntroller;
        private ISetController _setCоntroller;

        public Engine(IReader reader, IWriter writer, IFestivalController festivalController,
            ISetController setController)
        {
            this._reader = reader;
            this._writer = writer;
            this._festivalCоntroller = festivalController;
            this._setCоntroller = setController;
        }

		public void Run()
		{
			while (true)
			{
				var input = _reader.ReadLine();

                if (input == "END")
                {
                    var report = this._festivalCоntroller.ProduceReport();
                    this._writer.WriteLine("Results:");
                    this._writer.WriteLine(report);
                    break;
                }
                try
				{

					var result = this.ProcessCommand(input);
					this._writer.WriteLine(result);
				}
				catch (Exception ex) // in case we run out of memory
				{
					this._writer.WriteLine("ERROR: " + ex.Message);
				}
			}


		}

		public string ProcessCommand(string input)
		{
			var tokens = input.Split(" ".ToCharArray().First());

			var purvoto = tokens.First();
			var parametri = tokens.Skip(1).ToArray();

			if (purvoto == "LetsRock")
			{
				var setovete = this._setCоntroller.PerformSets();
				return setovete;
			}

			var festivalcontrolfunction = this._festivalCоntroller.GetType()
				.GetMethods()
				.FirstOrDefault(x => x.Name == purvoto);

			string result;

			try
			{
				result = (string)festivalcontrolfunction.Invoke(this._festivalCоntroller, new object[] { parametri });
			}
			catch (TargetInvocationException up)
			{
				throw up.InnerException;
			}

			return result;
		}
	}
}