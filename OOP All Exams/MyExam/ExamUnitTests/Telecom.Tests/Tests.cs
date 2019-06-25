namespace Telecom.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test()
        {
            Phone phone = new Phone("apple", "iphone");

            phone.AddContact("Pesho", "0000000000");
            string name = "Pesho";
            string number = "0000000000";

            int expected = 1;
            Assert.AreEqual(expected, 1);
            Assert.Throws<InvalidOperationException>(() => phone.AddContact("Pesho", "0000000000"));
            string expectedResult = $"Calling {name} - {number}...";
            string actual = phone.Call(name);

            Assert.AreEqual(expectedResult, actual);

            Assert.Throws<InvalidOperationException>(() => phone.Call("Gosho"));

            Assert.Throws<ArgumentException>(() => new Phone(null, "iphone"));
            Assert.Throws<ArgumentException>(() => new Phone("apple", null));

        }
    }
}