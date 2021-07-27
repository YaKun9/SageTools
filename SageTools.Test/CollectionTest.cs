using System.Collections.Generic;
using System.Linq;
using SageTools.Extension;
using Xunit;

namespace Ezreal.Tools.Test
{
    public class CollectionTest
    {
        [Fact]
        public void WhereIf()
        {
            var list = new List<Stuent>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(new Stuent()
                {
                    Age = 18 + i * 2,
                    Name = $"张{i.ArabToChinese()}丰"
                });
            }
            Assert.Equal(9, list.WhereIf(x => x.Age > 18, true).Count());

            
        }


        class Stuent
        {
            public int Age { get; set; }

            public string Name { get; set; }
        }
    }


}
