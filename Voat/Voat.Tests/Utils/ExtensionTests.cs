﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voat.Common;

namespace Voat.Tests.Utils
{
    [TestClass]
    public class ExtensionTests : BaseUnitTest
    {

        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void EnsureRangeTests()
        {
            Assert.AreEqual(10, 10.EnsureRange(0, 20));
            Assert.AreEqual(10, 10.EnsureRange(10, 20));
            Assert.AreEqual(15, 10.EnsureRange(15, 20));
            Assert.AreEqual(5, 10.EnsureRange(0, 5));
            Assert.AreEqual(-10, 10.EnsureRange(-20, -10));

            Assert.AreEqual(1.1, 0.9.EnsureRange(1.1, 1.9));
            Assert.AreEqual(1.2, 1.2.EnsureRange(1.1, 1.9));
            Assert.AreEqual(1.9, 2.7.EnsureRange(1.1, 1.9));

        }

        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void Null_IsEqual_1()
        {
            string before = null;
            bool result = before.IsEqual(null);
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void Null_IsEqual_2()
        {
            string before = null;
            bool result = before.IsEqual("");
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void CaseDifference_IsEqual()
        {
            string before = "lower";
            bool result = before.IsEqual("LOWER");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void ContentDifference_IsEqual()
        {
            string before = "lower";
            bool result = before.IsEqual("UPPER");
            Assert.AreEqual(false, result);
        }


        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void Null_TrimSafe()
        {
            string before = null;
            string expected = null;
            string result = before.TrimSafe();
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void Empty_TrimSafe()
        {
            string before = "";
            string expected = "";
            string result = before.TrimSafe();
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void Padded_TrimSafe()
        {
            string before = " x ";
            string expected = "x";
            string result = before.TrimSafe();
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void HasInterface_Simple()
        {
            var result = typeof(List<object>).HasInterface(typeof(IList));
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void HasInterface_Simple_False()
        {
            var result = typeof(string).HasInterface(typeof(IList));
            Assert.AreEqual(false, result);
        }
        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void HasInterface_Generic_Generic_Partial()
        {
            var result = typeof(HashSet<object>).HasInterface(typeof(ISet<>));
            Assert.AreEqual(true, result);
            
        }
        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void HasInterface_Generic_Generic_FullyQualified()
        {
            var result = typeof(HashSet<object>).HasInterface(typeof(ISet<object>));
            Assert.AreEqual(true, result);
        }


        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void IsDefault_String_1()
        {
            Assert.AreEqual(false, "".IsDefault());
        }


        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void IsDefault_String_2()
        {
            Assert.AreEqual(true, ((string)null).IsDefault());
        }

        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void IsDefault_Int_1()
        {
            Assert.AreEqual(false, 1.IsDefault());
        }

        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void IsDefault_Int_2()
        {
            Assert.AreEqual(true, 0.IsDefault());
        }

        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void IsDefault_Enum_1()
        {
            Assert.AreEqual(false, TestEnum.Value1.IsDefault());
        }

        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void IsDefault_Enum_2()
        {
            Assert.AreEqual(true, TestEnum.Value0.IsDefault());
        }
        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void IsDefault_Object_1()
        {
            TestObject t = new TestObject();
            Assert.AreEqual(false, t.IsDefault());
        }

        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void IsDefault_Object_2()
        {
            TestObject t = null;
            Assert.AreEqual(true, t.IsDefault());
        }



        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void Convert_Object_1()
        {
            TestObjectParent t = new TestObjectParent();
            TestObject x = t.Convert<TestObject, TestObjectParent>();
            Assert.IsNotNull(x);
        }

        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        //[ExpectedException(typeof(InvalidCastException))]
        public void Convert_Object_2()
        {
            Assert.Throws<InvalidCastException>(() => {
                TestObject t = new TestObject();
                TestObjectParent x = t.Convert<TestObjectParent, TestObject>();
            });
        }

        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void Convert_int_1()
        {
            object input = (int)17;
            int output = input.Convert<int, object>();
            Assert.AreEqual(17, output);
        }

        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void Convert_Complex_1()
        {
            var input = (object)new HashSet<int>();
            ISet<int> output = input.Convert<ISet<int>, object>();
            Assert.IsNotNull(output);
        }
        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void ToQueryString()
        {

            Assert.AreEqual("text=Hello%2C%20I%27m%20writing%20%23text%20in%20a%20url%21%3F", (new { text = "Hello, I'm writing #text in a url!?" }).ToQueryString());
            Assert.AreEqual("text=http%3A%2F%2Fwww.voat.co%2Fv%2Fall%2Fnew%3Fpage%3D3%26show%3Dall", (new { text = "http://www.voat.co/v/all/new?page=3&show=all" }).ToQueryString());

            Assert.AreEqual("id=4", (new { id = 4 }).ToQueryString());
            Assert.AreEqual("id=1&id2=2&id3=3&id4=four", (new { id = 1, id2 = 2, id3 = "3", id4 = "four" }).ToQueryString());
            Assert.AreEqual("id=4&name=joe", (new { id = 4, name = "joe" }).ToQueryString());

            Assert.AreEqual("id=4", (new { id = 4, name = (string)null }).ToQueryString());
            Assert.AreEqual("id=4&name=", (new { id = 4, name = (string)null }).ToQueryString(true));

        }

        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void TestEnumParsing()
        {



            Assert.AreEqual(Domain.Models.CommentSortAlgorithm.Top, Extensions.AssignIfValidEnumValue(-23223, Domain.Models.CommentSortAlgorithm.Top));
            Assert.AreEqual(Domain.Models.CommentSortAlgorithm.Top, Extensions.AssignIfValidEnumValue(324324, Domain.Models.CommentSortAlgorithm.Top));
            Assert.AreEqual(Domain.Models.CommentSortAlgorithm.New, Extensions.AssignIfValidEnumValue((int)Domain.Models.CommentSortAlgorithm.New, Domain.Models.CommentSortAlgorithm.Top));
            Assert.AreEqual(Domain.Models.CommentSortAlgorithm.Top, Extensions.AssignIfValidEnumValue(null, Domain.Models.CommentSortAlgorithm.Top));
            Assert.AreEqual(Domain.Models.CommentSortAlgorithm.Intensity, Extensions.AssignIfValidEnumValue((int)Domain.Models.CommentSortAlgorithm.Intensity, Domain.Models.CommentSortAlgorithm.Top));

            Assert.IsFalse(Extensions.IsValidEnumValue((Domain.Models.CommentSortAlgorithm?)777));
            Assert.IsFalse(Extensions.IsValidEnumValue((Domain.Models.CommentSortAlgorithm?)null));
            Assert.IsTrue(Extensions.IsValidEnumValue((Domain.Models.CommentSortAlgorithm?)2));

            Assert.IsFalse(Extensions.IsValidEnumValue((Domain.Models.CommentSortAlgorithm)777));
            Assert.IsTrue(Extensions.IsValidEnumValue((Domain.Models.CommentSortAlgorithm)2));

        }
        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        public void TestSafeEnum_Conversions()
        {
            var safeClass = new TestEnumClass();
            safeClass.CommentSort = Domain.Models.CommentSortAlgorithm.Top;

            Assert.AreEqual(Domain.Models.CommentSortAlgorithm.Top,  safeClass.CommentSort.Value);
            Assert.IsTrue(Domain.Models.CommentSortAlgorithm.Top == safeClass.CommentSort);
            Assert.IsTrue(safeClass.CommentSort == Domain.Models.CommentSortAlgorithm.Top);

            Domain.Models.CommentSortAlgorithm x = Domain.Models.CommentSortAlgorithm.New;
            x = safeClass.CommentSort;
            Assert.AreEqual(Domain.Models.CommentSortAlgorithm.Top, x);

            switch (safeClass.CommentSort.Value)
            {
                case Domain.Models.CommentSortAlgorithm.Top:
                    break;
                default:
                    Assert.Fail("This is a problem");
                    break;
            }

            safeClass.CommentSort = 4;
            Assert.AreEqual(Domain.Models.CommentSortAlgorithm.Bottom, safeClass.CommentSort.Value);

            safeClass.CommentSort = "New";
            Assert.AreEqual(Domain.Models.CommentSortAlgorithm.New, safeClass.CommentSort.Value);

            safeClass.CommentSort = "5";
            Assert.AreEqual(Domain.Models.CommentSortAlgorithm.Intensity, safeClass.CommentSort.Value);


        }

        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        //[ExpectedException(typeof(TypeInitializationException))]
        public void TestSafeEnum_ConstructionError()
        {
            Assert.Throws<TypeInitializationException>(() => {
                var s = new SomeStruct();
                SafeEnum<SomeStruct> x = new SafeEnum<SomeStruct>(s);
            });

        }


        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        //[ExpectedException(typeof(TypeInitializationException))]
        public void TestSafeEnum_ConstructionError2()
        {
            Assert.Throws<TypeInitializationException>(() => {
                SafeEnum<int> x = new SafeEnum<int>(45);
            });

        }

        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestSafeEnum_Errors()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                var safeClass = new TestEnumClass();
                safeClass.CommentSort = ((Domain.Models.CommentSortAlgorithm)(-203));
            });

        }
        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestSafeEnum_Errors_2()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                var safeClass = new TestEnumClass();
                safeClass.CommentSort = ((Domain.Models.CommentSortAlgorithm)(203));
            });

        }

        [TestMethod]
        [TestCategory("Utility"), TestCategory("Extentions")]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestSafeEnum_Errors_3()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => {
                var safeClass = new TestEnumClass();
                safeClass.CommentSort = 203;
            });

        }
    }

    public class TestEnumClass
    {
        public SafeEnum<Domain.Models.CommentSortAlgorithm> CommentSort { get; set; }
    }

    public class TestObject
    {

    }
    public class TestObjectParent : TestObject
    {

    }
    public struct SomeStruct : IConvertible
    {
        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
    }
    public enum TestEnum
    {
        Value0,
        Value1,
        Value2
    }
}
