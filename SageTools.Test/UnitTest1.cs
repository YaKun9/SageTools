using System;
using System.Collections.Generic;
using System.Linq;
using SageTools.Extension;
using Xunit;

namespace SageTools.Test
{
    public class UnitTest1
    {
        /// <summary>
        /// 阿拉伯数字转中文
        /// </summary>
        [Fact]
        public void ArabToChinese()
        {
            var num1 = 0;
            var num2 = 9;
            var num3 = -1;
            var num4 = 10;

            Assert.True(num1.ArabToChinese().Length > 0);
            Assert.True(num2.ArabToChinese().Length > 0);
            Assert.Throws<ArgumentOutOfRangeException>(() => num3.ArabToChinese());
            Assert.Throws<ArgumentOutOfRangeException>(() => num4.ArabToChinese());

        }


        [Fact]
        public void Test()
        {
            var list = new List<string>();
            for (int i = 0; i < 100000000; i++)
            {
                list.Add(SageTools.Utils.ShortIdGen.NextId());
            }
            var list2 = list.Distinct().ToList();

            Assert.True(list.Count==list2.Count);
        }
    }
}
