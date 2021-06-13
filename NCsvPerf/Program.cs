﻿using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using Knapcode.NCsvPerf.CsvReadable.TestCases;

namespace Knapcode.NCsvPerf
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IConfig config = null;
#if DEBUG
            config = new DebugInProcessConfig();
#else
            config = DefaultConfig.Instance;
#endif
            config = config.WithOption(ConfigOptions.DisableOptimizationsValidator, value: true);
            BenchmarkRunner.Run<PackageAssetsSuite>(config);
        }
    }
}
