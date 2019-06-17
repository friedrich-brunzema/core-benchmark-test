using NUnit.Framework;
using sciex.Testing.Utilities;
using System;
using System.Threading;

namespace Tests
{
    public class Tests
    {
        private BaselineComHelper _helper;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _helper = new BaselineComHelper();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [Category("Performance")]
        public void Test1()
        {
            Assert.That(Benchmark(() =>
            {
                var s = string.Empty;
                for (var i = 1; i < 10000; i++)
                    s = s + i;
                Thread.Sleep(500);
                Console.WriteLine("end");
            }), Is.True);
        }

        private bool Benchmark(Action action, int runs = 5, int warmup = 0, double threshold = 0.3)
        {
            var time = SimpleBenchmarking.BenchmarkTime(action, runs, warmup);

            var baselineMedian =
                _helper.GetBaselineAndStoreMeasurement("Grp5", "Unique-Test5", "ut5", "Median",
                    time.Median);

            var pass = ((baselineMedian - time.Median) / baselineMedian) > (threshold * -1);
            return pass;
        }
    }

}