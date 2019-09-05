namespace Converter
{
    using System;
    using System.Runtime.CompilerServices;

    using BenchmarkDotNet.Attributes;

    [RyuJitX64Job]
    public class PerformanceTest
    {
        private const int TestsCount = 10000;

        private readonly TestEnumInt enumValue;

        public PerformanceTest()
        {
            this.enumValue = DateTime.Now.Year > 0 ? TestEnumInt.B : TestEnumInt.C;
        }
        
        [Benchmark]
        public void EnumToIntDynamic()
        {
            var sum = 0;
            var e = this.enumValue;
            
            for (var i = 0; i < TestsCount; i++)
            {
                sum += ConvertToInt(e);
            }
            
            GC.KeepAlive(sum);
        }

        [Benchmark]
        public void EnumToIntCast()
        {
            var sum = 0;
            var e = this.enumValue;
            
            for (var i = 0; i < TestsCount; i++)
            {
                sum += (int)e;
            }
            
            GC.KeepAlive(sum);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int ConvertToInt<T>(T val) where T : Enum
        {
            var result = IntEnumConverter<T>.Convert(val);
            return result;
        }
    }
}