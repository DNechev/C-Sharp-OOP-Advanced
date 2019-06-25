namespace FestivalManager.Core.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Contracts;
    using Entities.Contracts;
    using FestivalManager.Entities;
    using FestivalManager.Entities.Factories;
    using FestivalManager.Entities.Factories.Contracts;

    public class FestivalController : IFestivalController
    {
        private const string TimeFormat = "mm\\:ss";

        private readonly IStage stage;
        private readonly IInstrumentFactory instrumentFactory;
        private readonly ISetFactory setFactory;

        public FestivalController(IStage stage)
        {
            this.stage = stage;
            this.instrumentFactory = new InstrumentFactory();
            this.setFactory = new SetFactory();
        }

        public string ProduceReport()
        {
            var sb = new StringBuilder();

            var totalFestivalLength = new TimeSpan(this.stage.Sets.Sum(s => s.ActualDuration.Ticks));

            sb.AppendLine($"Festival length: {FormatTimeSpan(totalFestivalLength)}");

            foreach (var set in this.stage.Sets)
            {
                sb.AppendLine($"--{set.Name} ({FormatTimeSpan(set.ActualDuration)}):");

                var performersOrderedDescendingByAge = set.Performers.OrderByDescending(p => p.Age);
                foreach (var performer in performersOrderedDescendingByAge)
                {
                    var instruments = string.Join(", ", performer.Instruments
                        .OrderByDescending(i => i.Wear));

                    sb.AppendLine($"---{performer.Name} ({instruments})");
                }

                if (!set.Songs.Any())
                {
                    sb.AppendLine("--No songs played");
                }
                else
                {
                    sb.AppendLine("--Songs played:");
                    foreach (var song in set.Songs)
                    {
                        sb.AppendLine($"----{song.Name} ({song.Duration.ToString(TimeFormat)})");
                    }
                }
            }

            return sb.ToString().TrimEnd('\r', '\n');
        }

        private static string FormatTimeSpan(TimeSpan timeSpan)
        {
            var formatted = string.Format("{0:D2}:{1:D2}", (int)timeSpan.TotalMinutes, timeSpan.Seconds);
            return formatted;
        }

        //ok
        public string RegisterSet(string[] args)
        {
            string name = args[0];
            string type = args[1];
            string result = string.Empty;

            var set = this.setFactory.CreateSet(name, type);
            if (set != null)
            {
                this.stage.AddSet(set);
                result = $"Registered {type} set";
            }
            return result;
        }

        //ok
        public string SignUpPerformer(string[] args)
        {
            var name = args[0];
            var age = int.Parse(args[1]);

            var instrumenti = args.Skip(2).ToArray();

            var instrumenti2 = instrumenti
                .Select(i => this.instrumentFactory.CreateInstrument(i))
                .ToArray();

            var performer = new Performer(name, age);

            foreach (var instrument in instrumenti2)
            {
                performer.AddInstrument(instrument);
            }

            this.stage.AddPerformer(performer);

            return $"Registered performer {performer.Name}";
        }

        //ok
        public string RegisterSong(string[] args)
        {
            string name = args[0];
            string[] duration = args[1].Split(":").ToArray();
            int minutes = int.Parse(duration[0]);
            int seconds = int.Parse(duration[1]);

            TimeSpan timeSpan = new TimeSpan(0, minutes, seconds);

            ISong song = new Song(name, timeSpan);

            string result = string.Empty;
            if (song != null)
            {
                this.stage.AddSong(song);
                result = $"Registered song {name} ({timeSpan:mm\\:ss})";
            }

            return result;
        }

        //ok
        public string AddPerformerToSet(string[] args)
        {
            string performerName = args[0];
            string setName = args[1];
            string result = string.Empty;

            if (this.stage.HasPerformer(performerName) == false)
            {
                throw new InvalidOperationException("Invalid performer provided");
            }
            if (this.stage.HasSet(setName) == false)
            {
                throw new InvalidOperationException("Invalid set provided");
            }


            var set = this.stage.GetSet(setName);
            var performer = this.stage.GetPerformer(performerName);
            set.AddPerformer(performer);

            result = $"Added {performerName} to {setName}";
            return result;

        }

        //ok
        public string RepairInstruments(string[] args)
        {
            var instrumentsToRepair = this.stage.Performers
                .SelectMany(p => p.Instruments)
                .Where(i => i.Wear < 100)
                .ToArray();

            foreach (var instrument in instrumentsToRepair)
            {
                instrument.Repair();
            }

            return $"Repaired {instrumentsToRepair.Length} instruments";
        }

        //ok
        public string AddSongToSet(string[] args)
        {
            string songName = args[0];
            string setName = args[1];
            string result = string.Empty;

            if (this.stage.HasSet(setName) == false)
            {
                throw new InvalidOperationException("Invalid set provided");
            }
            if (this.stage.HasSong(songName) == false)
            {
                throw new InvalidOperationException("Invalid song provided");
            }


            var song = this.stage.GetSong(songName);
            var set = this.stage.GetSet(setName);

            set.AddSong(song);
            result = $"Added {songName} ({song.Duration:mm\\:ss}) to {setName}";

            return result;
        }
    }
}