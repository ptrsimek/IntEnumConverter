namespace Converter
{
    using System;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    public static class IntEnumConverter<T> where T : Enum
    {
        private static readonly Func<int, T> ConvertFuncIntToT = CreateConverterIntToT();
        
        private static readonly Func<T, int> ConvertFuncTToInt = CreateConverterTToInt();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Convert(int e)
        {
            var result = ConvertFuncIntToT(e);
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Convert(T e)
        {
            var result = ConvertFuncTToInt(e);
            return result;
        }

        private static Func<int, T> CreateConverterIntToT()
        {
            var m = new DynamicMethod(string.Empty, typeof(T), new[] { typeof(int) }, true);

            var il = m.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ret);

            var result = (Func<int, T>)m.CreateDelegate(typeof(Func<int, T>));
            return result;
        }
        
        private static Func<T, int> CreateConverterTToInt()
        {
            var m = new DynamicMethod(string.Empty, typeof(int), new[] { typeof(T) }, true);
            
            var il = m.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ret);
            
            var result = (Func<T, int>)m.CreateDelegate(typeof(Func<T, int>));
            return result;
        }
    }
}