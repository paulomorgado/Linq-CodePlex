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
    public class Enumerable_TakeLastWhile_Tests
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

            actual = source.TakeLastWhile((Func<int, bool>)null);
        }

        [TestMethod]
        public void TakeLastWhile_WithPredicateThatMatchesLastElements_ReturnsLastSelectedElements()
        {
            Func<int, bool> predicate = e => e % 10 < 5;
            System.Collections.Generic.IEnumerable<int> range = System.Linq.Enumerable.Range(0, 25);
            System.Collections.Generic.IEnumerable<int> source = range;
            System.Collections.Generic.IEnumerable<int> actual;
            System.Collections.Generic.IEnumerable<int> expected = System.Linq.Enumerable.Range(20, 5);

            actual = source.TakeLastWhile(predicate);

            CollectionAssert.AreEqual(expected.AsCollection(), actual.AsCollection());
        }

        [TestMethod]
        public void TakeLastWhile_WithPredicateThatDoesntMatchLastElements_ReturnsEmptyEnumerable()
        {
            Func<int, bool> predicate = e => e % 10 >= 5;
            System.Collections.Generic.IEnumerable<int> range = System.Linq.Enumerable.Range(0, 25);
            System.Collections.Generic.IEnumerable<int> source = range;
            System.Collections.Generic.IEnumerable<int> actual;

            actual = source.TakeLastWhile(predicate);

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

            actual = source.TakeLastWhile((Func<int, int, bool>)null);
        }

        [TestMethod]
        public void TakeLastWhileWithIndex_WithPredicateThatMatchesLastElements_ReturnsLastSelectedElements()
        {
            Func<int, int, bool> predicate = (e, i) => i % 10 < 5;
            System.Collections.Generic.IEnumerable<int> range = System.Linq.Enumerable.Range(0, 25);
            System.Collections.Generic.IEnumerable<int> source = range;
            System.Collections.Generic.IEnumerable<int> actual;
            System.Collections.Generic.IEnumerable<int> expected = System.Linq.Enumerable.Range(20, 5);

            actual = source.TakeLastWhile(predicate);

            CollectionAssert.AreEqual(expected.AsCollection(), actual.AsCollection());
        }

        [TestMethod]
        public void TakeLastWhileWithIndex_WithPredicateThatDoesntMatchLastElements_ReturnsEmptyEnumerable()
        {
            Func<int, int, bool> predicate = (e, i) => i % 10 > 5;
            System.Collections.Generic.IEnumerable<int> range = System.Linq.Enumerable.Range(0, 25);
            System.Collections.Generic.IEnumerable<int> source = range;
            System.Collections.Generic.IEnumerable<int> actual;

            actual = source.TakeLastWhile(predicate);

            Assert.AreEqual(0, actual.Count(), "Expected an empty Enumerable.");
        }
    }
}
