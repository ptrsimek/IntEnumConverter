namespace Converter
{
    using System;

    using Xunit;

    public class Tests
    {
        [Fact]
        public void Test_ConvertToInt()
        {
            var val = ConvertToInt(TestEnum.B);
            Assert.Equal(20, val);
        }
        
        [Fact]
        public void Test_ConvertToInt_NotValidEnumItem()
        {
            var val = ConvertToInt((TestEnum)25);
            Assert.Equal(25, val);
        }
        
        [Fact]
        public void Test_ConvertToEnum()
        {
            var val = ConvertToEnum<TestEnum>(20);
            Assert.Equal(TestEnum.B, val);
        }
        
        [Fact]
        public void Test_ConvertToEnum_NotValidEnumItem()
        {
            var val = ConvertToEnum<TestEnum>(25);
            Assert.Equal((TestEnum)25, val);
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

        private enum TestEnum
        {
            A = 10,
            B = 20,
            C = 30
        }
    }
}