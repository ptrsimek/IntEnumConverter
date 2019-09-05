namespace Converter
{
    using System;
    using System.Reflection.Emit;

    public static class IntEnumConverter<T> where T : Enum
    {
        private static readonly Func<int, T> ConvertFunc = CreateConverter();
        
        private static readonly Func<T, int> ConvertFuncX = CreateConverterX();

        public static T Convert(int e)
        {
            var result = ConvertFunc(e);
            return result;
        }
        
        public static int Convert(T e)
        {
            var result = ConvertFuncX(e);
            return result;
        }

        private static Func<int, T> CreateConverter()
        {
            var m = new DynamicMethod(string.Empty, typeof(T), new[] { typeof(int) }, true);

            var il = m.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ret);

            var result = (Func<int, T>)m.CreateDelegate(typeof(Func<int, T>));
            return result;
        }
        
        private static Func<T, int> CreateConverterX()
        {
            var m = new DynamicMethod(string.Empty, typeof(T), new[] { typeof(T) }, true);

            var il = m.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ret);

            var result = (Func<T, int>)m.CreateDelegate(typeof(Func<T, int>));
            return result;
        }
    }
}