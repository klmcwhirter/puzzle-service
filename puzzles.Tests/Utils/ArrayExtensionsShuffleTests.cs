using System.Linq;
using Xunit;
using puzzles.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace puzzles.Tests
{
    public partial class ArrayExtensionsTests
    {
        public class ArrayParameter
        {
            public object[] Array { get; set; }
        }

        static readonly ArrayParameter IntArray = new ArrayParameter { Array = new object[] { 1, 3, 4, 9 } };

        static readonly ArrayParameter StringArray = new ArrayParameter { Array = new object[] { "hello", "world", "this", "array" } };

        public static IEnumerable<object> ArraysMemberDataSource()
        {
            yield return new object[] { IntArray };
            yield return new object[] { StringArray };
        }

        [Theory]
        [MemberData(nameof(ArraysMemberDataSource))]
        public void ShuffleIntArrayShouldShuffle(object orig)
        {
            var param = orig as ArrayParameter;
            var origArr = (object[])param.Array;
            //Given
            var arr = (object[])origArr.Clone();

            //When
            arr.Shuffle();

            //Then
            var diff = false;
            for (int i = 0; i < origArr.Length; i++)
            {
                if (((IComparable)origArr[i]).CompareTo(((IComparable)arr[i])) != 0)
                {
                    diff = true;
                }
            }
            var origStr = origArr.ToMessageString();
            var arrStr = arr.ToMessageString();

            Assert.True(diff, $"arr should have been shuffled\norigArr = {origStr}\narr = {arrStr}");
        }
    }
}
