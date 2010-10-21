
namespace PauloMorgado.Linq.UnitTests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PauloMorgado.Linq.UnitTests.Utils;


    /// <summary>
    ///This is a test class for EnumerableTest and is intended
    ///to contain all EnumerableTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Enumerable_SkipLast_Test
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SkipLast_WithNullSource_ThrowsException()
        {
            int count = 0;
            System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.TestTools.UnitTesting.GenericParameterHelper> source = null;
            System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.TestTools.UnitTesting.GenericParameterHelper> actual;

            actual = PauloMorgado.Linq.Enumerable.SkipLast<Microsoft.VisualStudio.TestTools.UnitTesting.GenericParameterHelper>(source, count);
        }

        [TestMethod]
        public void SkipLast_WithNegativeCount_ReturnsAllElements()
        {
            int count = -1;
            System.Collections.Generic.IEnumerable<int> range = System.Linq.Enumerable.Range(0, 5);
            System.Collections.Generic.IEnumerable<int> source = range;
            System.Collections.Generic.IEnumerable<int> actual;
            System.Collections.Generic.IEnumerable<int> expected = range;

            actual = source.SkipLast(count);

            CollectionAssert.AreEqual(expected.AsCollection(), actual.AsCollection());
        }

        [TestMethod]
        public void SkipLast_WithZeroCount_ReturnsAllElements()
        {
            int count = 0;
            System.Collections.Generic.IEnumerable<int> range = System.Linq.Enumerable.Range(0, 5);
            System.Collections.Generic.IEnumerable<int> source = range;
            System.Collections.Generic.IEnumerable<int> actual;
            System.Collections.Generic.IEnumerable<int> expected = range;

            actual = source.SkipLast(count);

            CollectionAssert.AreEqual(expected.AsCollection(), actual.AsCollection());
        }

        [TestMethod]
        public void SkipLast_WithCountGreaterThanSize_ReturnsEmptyEnumerable()
        {
            int count = 15;
            System.Collections.Generic.IEnumerable<int> source = System.Linq.Enumerable.Range(0, 10);
            System.Collections.Generic.IEnumerable<int> actual;

            actual = source.SkipLast(count);

            Assert.AreEqual(0, actual.Count(), "Sequence not empty.");
        }

        [TestMethod]
        public void SkipLast_WithListSourceAndCountGreaterThanSize_ReturnsEmptyEnumerable()
        {
            int count = 15;
            System.Collections.Generic.IEnumerable<int> source = System.Linq.Enumerable.Range(0, 10).ToList();
            System.Collections.Generic.IEnumerable<int> actual;

            actual = source.SkipLast(count);

            Assert.AreEqual(0, actual.Count(), "Sequence not empty.");
        }

        [TestMethod]
        public void SkipLast_WithPositiveCount_ReturnsEnumerableSkippingLastCountElements()
        {
            int count = 10;
            System.Collections.Generic.IEnumerable<int> range = System.Linq.Enumerable.Range(0, 25);
            System.Collections.Generic.IEnumerable<int> source = range;
            System.Collections.Generic.IEnumerable<int> actual;
            System.Collections.Generic.IEnumerable<int> expected = System.Linq.Enumerable.Range(0, 15);

            actual = source.SkipLast(count);

            CollectionAssert.AreEqual(expected.AsCollection(), actual.AsCollection());
        }

        [TestMethod]
        public void SkipLast_WithListSourceAndPositiveCount_ReturnsEnumerableSkippingLastCountElements()
        {
            int count = 10;
            System.Collections.Generic.IEnumerable<int> range = System.Linq.Enumerable.Range(0, 25);
            System.Collections.Generic.IEnumerable<int> source = range.ToList();
            System.Collections.Generic.IEnumerable<int> actual;
            System.Collections.Generic.IEnumerable<int> expected = System.Linq.Enumerable.Range(0, 15);

            actual = source.SkipLast(count);

            CollectionAssert.AreEqual(expected.AsCollection(), actual.AsCollection());
        }
    }
}
