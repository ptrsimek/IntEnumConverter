namespace Converter
{
    using System;
    using System.Runtime.CompilerServices;

    public static class IntEnumConverterUnsafe
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Convert<T>(int e) where T : Enum
        {
            var result = Unsafe.As<int, T>(ref e);
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Convert<T>(T e) where T : Enum
        {
            var result = Unsafe.As<T, int>(ref e);
            return result;
        }
    }
}