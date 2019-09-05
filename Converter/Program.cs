namespace Converter
{
    using System;

    using BenchmarkDotNet.Running;

    public static class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run<PerformanceTest>();
        }
    }
}