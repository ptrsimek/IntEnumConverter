namespace Converter
{
    using System;
    using System.Runtime.CompilerServices;

    using BenchmarkDotNet.Attributes;

    [LegacyJitX64Job, RyuJitX64Job]
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
            
            // let the compiler think that the variable is in use
            GC.KeepAlive(sum);
        }
        
        [Benchmark]
        public void EnumToIntUnsafe()
        {
            var sum = 0;
            var e = this.enumValue;
            
            for (var i = 0; i < TestsCount; i++)
            {
                sum += ConvertToIntUnsafe(e);
            }
            
            // let the compiler think that the variable is in use
            GC.KeepAlive(sum);
        }

        [Benchmark(Baseline = true)]
        public void EnumToIntCast()
        {
            var sum = 0;
            var e = this.enumValue;
            
            for (var i = 0; i < TestsCount; i++)
            {
                sum += (int)e;
            }
            
            // let the compiler think that the variable is in use
            GC.KeepAlive(sum);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int ConvertToInt<T>(T val) where T : Enum
        {
            var result = IntEnumConverter<T>.Convert(val);
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int ConvertToIntUnsafe<T>(T val) where T : Enum
        {
            var result = IntEnumConverterUnsafe.Convert(val);
            return result;
        }
    }
}