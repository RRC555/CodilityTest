using NUnit.Framework;
using System.Diagnostics;

namespace CodilityTest.Configs
{
    /// <summary>
    /// Class for NUnit configration and holding the logs
    /// </summary>
    public class Logger
    {
        [SetUpFixture]
        public class Output
        {
            [OneTimeSetUp]
            public void StartTest()
            {
                Trace.Listeners.Add(new ConsoleTraceListener());
            }

            [OneTimeTearDown]
            public void EndTest()
            {
                Trace.Flush();
            }
        }
    }
}
