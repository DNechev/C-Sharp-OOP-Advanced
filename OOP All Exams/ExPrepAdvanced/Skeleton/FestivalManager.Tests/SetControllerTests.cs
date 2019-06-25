// Use this file for your unit tests.
// When you are ready to submit, REMOVE all using statements to your project (entities/controllers/etc)
namespace FestivalManager.Tests
{

    using NUnit.Framework;
    using System;



    using FestivalManager.Core.Controllers;
    using FestivalManager.Core.Controllers.Contracts;
    using FestivalManager.Entities;
    using FestivalManager.Entities.Contracts;
    using FestivalManager.Entities.Instruments;
    using FestivalManager.Entities.Sets;
    using System.Collections.Generic;

    [TestFixture]
    public class SetControllerTests
    {
        [Test]
        public void TestResultAfterSetsPlay()
        {
            ISet set = new Short("Set1");
            IPerformer gosho = new Performer("Gosho", 20);
            gosho.AddInstrument(new Guitar());
            set.AddPerformer(gosho);
            ISong song = new Song("Song1", new TimeSpan(0, 1, 2));
            set.AddSong(song);
            IStage stage = new Stage();
            stage.AddSet(set);
            ISetController setController = new SetController(stage);

            string actualResult = setController.PerformSets();
            string expectedResult = "1. Set1:\r\n-- 1. Song1 (01:02)\r\n-- Set Successful";

            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void TestResultWhenSetsCantBePlayed()
        {
            ISet set = new Short("Set1");
            IPerformer gosho = new Performer("Gosho", 20);
            gosho.AddInstrument(new Guitar());
            set.AddPerformer(gosho);
            IStage stage = new Stage();
            stage.AddSet(set);
            ISetController setController = new SetController(stage);

            string actualResult = setController.PerformSets();
            string expectedResult = "1. Set1:\r\n-- Did not perform";

            Assert.AreEqual(actualResult, expectedResult);
        }

        [Test]
        public void TestIfInstrumentsWearDownAfterUse()
        {
            ISet set = new Short("Set1");
            IPerformer gosho = new Performer("Gosho", 20);
            var instrument = new Guitar();
            gosho.AddInstrument(instrument);
            set.AddPerformer(gosho);
            ISong song = new Song("Song1", new TimeSpan(0, 1, 2));
            set.AddSong(song);
            IStage stage = new Stage();
            stage.AddSet(set);
            ISetController setController = new SetController(stage);

            setController.PerformSets();
            setController.PerformSets();

            bool expected = true;
            bool actual = instrument.IsBroken;

            Assert.AreEqual(actual, expected);
        }
    }
}