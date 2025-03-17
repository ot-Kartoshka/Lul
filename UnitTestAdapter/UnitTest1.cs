using System;
using System.IO;
using System.Reflection.Emit;
using NUnit.Framework;
using RefactoringGuru.DesignPatterns.Adapter.Conceptual;

namespace UnitTestAdapter
{ 
    [TestFixture]
    public class AdapterUnitTests
    {
        [Test]
        public void Adapter_ReturnsCorrectString()
        {
            Adaptee adaptee = new Adaptee();
            ITarget adapter = new Adapter(adaptee);
            string expected = "This is 'Specific request.'";

            string actual = adapter.GetRequest();

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Program_ExitCode_Success()
        {
            using (StringWriter stdout = new StringWriter())
            using (StringWriter stderr = new StringWriter())
            {
                Console.SetOut(stdout);
                Console.SetError(stderr);

                int exitCode = adapter.Main(Array.Empty<string>());

                Assert.That(exitCode, Is.EqualTo(0));
                Assert.That(stdout.ToString(), Does.Contain("Adaptee interface is incompatible with the client."));
                Assert.That(stdout.ToString(), Does.Contain("This is 'Specific request.'"));
                Assert.That(stderr.ToString(), Is.Empty);
            }
        }

        [Test]
        public void Adapter_NullAdaptee_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Adapter(null));
        }

        [Test]
        public void Program_ExitCode_Exception()
        {
            using (StringWriter stdout = new StringWriter())
            using (StringWriter stderr = new StringWriter())
            {
                Console.SetOut(stdout);
                Console.SetError(stderr);

                int exitCode = adapter.Main(null);

                Assert.That(exitCode, Is.EqualTo(1));
                Assert.That(stderr.ToString(), Does.Contain("Error"));
            }
        }

        [Test]
        public void Adaptee_ReturnsSpecificRequest()
        {
            Adaptee adaptee = new Adaptee();
            string expected = "Specific request.";

            string actual = adaptee.GetSpecificRequest();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
