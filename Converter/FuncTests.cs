namespace Converter
{
    using System;

    using Xunit;

    public class FuncTests
    {
        [Fact]
        public void Test_ConvertToInt_Byte()
        {
            var val = ConvertToInt(TestEnumByte.C);
            Assert.Equal(130, val);
        }
        
        [Fact]
        public void Test_ConvertToInt_Short()
        {
            var val = ConvertToInt(TestEnumShort.B);
            Assert.Equal(10020, val);
        }
        
        [Fact]
        public void Test_ConvertToInt()
        {
            var val = ConvertToInt(TestEnumInt.B);
            Assert.Equal(100020, val);
        }
        
        [Fact]
        public void Test_ConvertToInt_NotValidEnumItem()
        {
            var val = ConvertToInt((TestEnumInt)25);
            Assert.Equal(25, val);
        }
        
        [Fact]
        public void Test_ConvertToEnum()
        {
            var val = ConvertToEnum<TestEnumInt>(100020);
            Assert.Equal(TestEnumInt.B, val);
        }
        
        [Fact]
        public void Test_ConvertToEnum_NotValidEnumItem()
        {
            var val = ConvertToEnum<TestEnumInt>(25);
            Assert.Equal((TestEnumInt)25, val);
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