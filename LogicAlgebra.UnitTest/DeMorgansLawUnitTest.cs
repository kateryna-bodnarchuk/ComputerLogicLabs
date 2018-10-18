using LogicAlgebra.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LogicAlgebra.UnitTest
{
    [TestClass]
    public class DeMorgansLawUnitTest
    {
        [TestMethod]
        public void TestTryConvertNotOfOrToAndOfNot()
        {
            var original = new NotFunction(
                new OrFunction(
                    new InputFunction(0),
                    new InputFunction(1)
                )
            );
            var expected = new AndFunction(
                new NotFunction(new InputFunction(0)),
                new NotFunction(new InputFunction(1))
            );
            IBooleanFunction result;
            Assert.IsTrue(DeMorgansLaw.TryConvertNotOfOrToAndOfNot(original, out result));
            Assert.IsNotNull(result);
            Assert.IsTrue(BooleanFunctionEqualityComparer.Instance.Equals(result, expected));
        }

        [TestMethod]
        public void TestTryConvertNotOfAndToOrOfNot()
        {
            var original = new NotFunction(
                new AndFunction(
                    new InputFunction(0),
                    new InputFunction(1)
                )
            );
            var expected = new OrFunction(
                new NotFunction(new InputFunction(0)),
                new NotFunction(new InputFunction(1))
            );
            IBooleanFunction result;
            Assert.IsTrue(DeMorgansLaw.TryConvertNotOfAndToOrOfNot(original, out result));
            Assert.IsNotNull(result);
            Assert.IsTrue(BooleanFunctionEqualityComparer.Instance.Equals(result, expected));
        }
    }
}
