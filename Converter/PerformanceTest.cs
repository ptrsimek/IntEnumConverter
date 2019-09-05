namespace Converter
{
    using System;

    using BenchmarkDotNet.Attributes;

    [RyuJitX64Job, RyuJitX86Job]
    public class PerformanceTest
    {
        private const int TestsCount = 10000;

        private readonly TestEnum enumValue;

        public PerformanceTest()
        {
            this.enumValue = DateTime.Now.Year > 0 ? TestEnum.B : TestEnum.C;
        }
        
        [Benchmark]
        public void EnumToIntConverter()
        {
            var sum = 0;
            var e = this.enumValue;
            
            for (var i = 0; i < TestsCount; i++)
            {
                sum += ConvertToInt(e);
            }
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
        }
        
        private static int ConvertToInt<T>(T val) where T : Enum
        {
            var result = IntEnumConverter<T>.Convert(val);
            return result;
        }
        
        private static T ConvertToEnum<T>(int val) where T : Enum
        {
            var result = IntEnumConverter<T>.Convert(val);
            return result;
        }
    }
}