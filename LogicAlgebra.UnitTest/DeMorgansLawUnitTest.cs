using LogicAlgebra.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LogicAlgebra.UnitTest
{
    [TestClass]
    public class DeMorgansLawUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var original = new OrFunction(
                new IBooleanFunction[]
                {
                    new AndFunction(
                        new IBooleanFunction[]
                        {
                            new NotFunction(new InputFunction(3)),
                            new InputFunction(2)
                        }),
                    new AndFunction(
                        new IBooleanFunction[]
                        {
                            new NotFunction(new InputFunction(2)),
                            new InputFunction(1)
                        }),
                    new AndFunction(
                        new IBooleanFunction[]
                        {
                            new InputFunction(3),
                            new NotFunction(new InputFunction(1))
                        })

                });
            
            //var andOnly = new And
        }
    }
}
