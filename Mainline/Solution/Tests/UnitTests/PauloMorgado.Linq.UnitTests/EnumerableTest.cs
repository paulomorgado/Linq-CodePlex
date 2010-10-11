namespace PauloMorgado.Linq.UnitTests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;


    /// <summary>
    ///This is a test class for EnumerableTest and is intended
    ///to contain all EnumerableTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EnumerableTest
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
        public void TakeLast_WithNullSource_ThrowsException()
        {
            int count = 0;
            System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.TestTools.UnitTesting.GenericParameterHelper> source = null;
            System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.TestTools.UnitTesting.GenericParameterHelper> actual;

            actual = PauloMorgado.Linq.Enumerable.TakeLast<Microsoft.VisualStudio.TestTools.UnitTesting.GenericParameterHelper>(source, count);
        }

        [TestMethod]
        public void TakeLast_WithNegativeCount_ReturnsEmptyEnumerable()
        {
            int count = -1;
            System.Collections.Generic.IEnumerable<int> source = System.Linq.Enumerable.Range(0, 25);
            System.Collections.Generic.IEnumerable<int> actual;

            actual = PauloMorgado.Linq.Enumerable.TakeLast<int>(source, count);

            Assert.AreEqual(0, actual.Count(), "Expected an empty Enumerable.");
        }

        [TestMethod]
        public void TakeLast_WithZeroCount_ReturnsEmptyEnumerable()
        {
            int count = 0;
            System.Collections.Generic.IEnumerable<int> source = System.Linq.Enumerable.Range(0, 25);
            System.Collections.Generic.IEnumerable<int> actual;

            actual = PauloMorgado.Linq.Enumerable.TakeLast<int>(source, count);

            Assert.AreEqual(0, actual.Count(), "Expected an empty Enumerable.");
        }

        [TestMethod]
        public void TakeLast_WithCountGreaterThanSize_ReturnsEnumerableWithAllElements()
        {
            int count = 10;
            System.Collections.Generic.IEnumerable<int> source = System.Linq.Enumerable.Range(0, 5);
            System.Collections.Generic.IEnumerable<int> actual;
            System.Collections.Generic.IEnumerable<int> expected = source;

            actual = PauloMorgado.Linq.Enumerable.TakeLast<int>(source, count);

            Assert.AreEqual(0, actual.Except(expected).Count(), "Unexpected items in Enumerable.");
        }

        [TestMethod]
        public void TakeLast_WithListSourceAndCountGreaterThanSize_ReturnsEnumerableWithAllElements()
        {
            int count = 10;
            System.Collections.Generic.IEnumerable<int> source = System.Linq.Enumerable.Range(0, 5).ToList();
            System.Collections.Generic.IEnumerable<int> actual;
            System.Collections.Generic.IEnumerable<int> expected = source;

            actual = PauloMorgado.Linq.Enumerable.TakeLast<int>(source, count);

            Assert.AreEqual(0, actual.Except(expected).Count(), "Unexpected items in Enumerable.");
        }

        [TestMethod]
        public void TakeLast_WithPositiveCount_ReturnsEnumerableWithLastCountElements()
        {
            int count = 10;
            System.Collections.Generic.IEnumerable<int> source = System.Linq.Enumerable.Range(0, 25);
            System.Collections.Generic.IEnumerable<int> actual;
            System.Collections.Generic.IEnumerable<int> expected = System.Linq.Enumerable.Range(15, 25);

            actual = PauloMorgado.Linq.Enumerable.TakeLast<int>(source, count);

            Assert.AreEqual(0, actual.Except(expected).Count(), "Unexpected items in Enumerable.");
        }

        [TestMethod]
        public void TakeLast_WithListSourceAndPositiveCount_ReturnsEnumerableWithLastCountElements()
        {
            int count = 10;
            System.Collections.Generic.IEnumerable<int> source = System.Linq.Enumerable.Range(0, 25).ToList();
            System.Collections.Generic.IEnumerable<int> actual;
            System.Collections.Generic.IEnumerable<int> expected = System.Linq.Enumerable.Range(15, 25);

            actual = PauloMorgado.Linq.Enumerable.TakeLast<int>(source, count);

            Assert.AreEqual(0, actual.Except(expected).Count(), "Unexpected items in Enumerable.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TakeLastWhile_WithNullSource_ThrowsException()
        {
            System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.TestTools.UnitTesting.GenericParameterHelper> source = null;
            System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.TestTools.UnitTesting.GenericParameterHelper> actual;

            actual = PauloMorgado.Linq.Enumerable.TakeLastWhile<Microsoft.VisualStudio.TestTools.UnitTesting.GenericParameterHelper>(source, (Func<Microsoft.VisualStudio.TestTools.UnitTesting.GenericParameterHelper, bool>)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TakeLastWhile_WithNullPredicate_ThrowsException()
        {
            System.Collections.Generic.IEnumerable<int> source = System.Linq.Enumerable.Range(0, 25);
            System.Collections.Generic.IEnumerable<int> actual;

            actual = PauloMorgado.Linq.Enumerable.TakeLastWhile<int>(source, (Func<int, bool>)null);
        }

        [TestMethod]
        public void TakeLastWhile_WithPredicateThatMatchesLastElements_ReturnsLastSelectedElements()
        {
            System.Collections.Generic.IEnumerable<int> source = System.Linq.Enumerable.Range(0, 25);
            System.Collections.Generic.IEnumerable<int> actual;
            System.Collections.Generic.IEnumerable<int> expected = new int[] { 20, 21, 22, 23, 24 };

            actual = PauloMorgado.Linq.Enumerable.TakeLastWhile<int>(source, e => e % 10 < 5);

            Assert.AreEqual(0, actual.Except(expected).Count(), "Unexpected items in Enumerable.");
        }

        [TestMethod]
        public void TakeLastWhile_WithPredicateThatDoesntMatchLastElements_ReturnsEmptyEnumerable()
        {
            System.Collections.Generic.IEnumerable<int> source = System.Linq.Enumerable.Range(0, 25);
            System.Collections.Generic.IEnumerable<int> actual;

            actual = PauloMorgado.Linq.Enumerable.TakeLastWhile<int>(source, e => e % 10 >= 5);

            Assert.AreEqual(0, actual.Count(), "Expected an empty Enumerable.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TakeLastWhileWithIndex_WithNullSource_ThrowsException()
        {
            System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.TestTools.UnitTesting.GenericParameterHelper> source = null;
            System.Collections.Generic.IEnumerable<Microsoft.VisualStudio.TestTools.UnitTesting.GenericParameterHelper> actual;

            actual = PauloMorgado.Linq.Enumerable.TakeLastWhile<Microsoft.VisualStudio.TestTools.UnitTesting.GenericParameterHelper>(source, (Func<Microsoft.VisualStudio.TestTools.UnitTesting.GenericParameterHelper, int, bool>)null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TakeLastWhileWithIndex_WithNullPredicate_ThrowsException()
        {
            System.Collections.Generic.IEnumerable<int> source = System.Linq.Enumerable.Range(0, 25);
            System.Collections.Generic.IEnumerable<int> actual;

            actual = PauloMorgado.Linq.Enumerable.TakeLastWhile<int>(source, (Func<int, int, bool>)null);
        }

        [TestMethod]
        public void TakeLastWhileWithIndex_WithPredicateThatMatchesLastElements_ReturnsLastSelectedElements()
        {
            System.Collections.Generic.IEnumerable<int> source = System.Linq.Enumerable.Range(0, 25);
            System.Collections.Generic.IEnumerable<int> actual;
            System.Collections.Generic.IEnumerable<int> expected = new int[] { 20, 21, 22, 23, 24 };

            actual = PauloMorgado.Linq.Enumerable.TakeLastWhile<int>(source, (e, i) => i % 10 < 5);

            Assert.AreEqual(0, actual.Except(expected).Count(), "Unexpected items in Enumerable.");
        }

        [TestMethod]
        public void TakeLastWhileWithIndex_WithPredicateThatDoesntMatchLastElements_ReturnsEmptyEnumerable()
        {
            System.Collections.Generic.IEnumerable<int> source = System.Linq.Enumerable.Range(0, 25);
            System.Collections.Generic.IEnumerable<int> actual;

            actual = PauloMorgado.Linq.Enumerable.TakeLastWhile<int>(source, (e, i) => i % 10 > 5);

            Assert.AreEqual(0, actual.Count(), "Expected an empty Enumerable.");
        }
    }
}
