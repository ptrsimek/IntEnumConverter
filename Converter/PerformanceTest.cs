namespace Converter
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection.Emit;

    using BenchmarkDotNet.Attributes;

    [RyuJitX64Job]
    public class PerformanceTest
    {
        private const int TestsCount = 10000;

        private readonly TestEnum enumValue;

        private static readonly Func<TestEnum, int> ConverterDynamic = CreateConverterDynamic<TestEnum>();
        
        private static readonly Func<TestEnum, int> ConverterExpession = CreateConverterDynamic<TestEnum>();

        public PerformanceTest()
        {
            this.enumValue = DateTime.Now.Year > 0 ? TestEnum.B : TestEnum.C;
        }
        
        [Benchmark]
        public void EnumToIntDynamic()
        {
            var sum = 0;
            var e = this.enumValue;
            var c = ConverterDynamic;
            
            for (var i = 0; i < TestsCount; i++)
            {
                sum += c(e);
            }
            
            GC.KeepAlive(sum);
        }
        
        [Benchmark]
        public void EnumToIntExpression()
        {
            var sum = 0;
            var e = this.enumValue;
            var c = ConverterExpession;
            
            for (var i = 0; i < TestsCount; i++)
            {
                sum += c(e);
            }
            
            GC.KeepAlive(sum);
        }

        private static Func<T, int> CreateConverterDynamic<T>()
        {
            var m = new DynamicMethod(string.Empty, typeof(T), new[] { typeof(T) }, typeof(PerformanceTest), false);
            
            var il = m.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ret);
            
            var result = (Func<T, int>)m.CreateDelegate(typeof(Func<T, int>));
            return result;
        }

        private static Func<T, int> CreateConverterExpression<T>()
        {
            var arg = Expression.Parameter(typeof(T), "arg");
            var f = Expression.Lambda<Func<T, int>>(Expression.Convert(arg, typeof(int)), arg);
            var result = f.Compile();

            return result;
        }

        //[Benchmark]
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
        
        private static int ConvertToInt<T>(T val) where T : Enum
        {
            var result = IntEnumConverter<T>.Convert(val);
            return result;
        }
        
        // private static T ConvertToEnum<T>(int val) where T : Enum
        // {
        //     var result = IntEnumConverter<T>.Convert(val);
        //     return result;
        // }
    }
}