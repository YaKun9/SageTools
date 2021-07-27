using System;
using SageTools.Extension;
using Xunit;

namespace Ezreal.Tools.Test
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
            Convert.ToInt32("asdfasdfasd");
        }
    }
}
