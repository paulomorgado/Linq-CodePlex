namespace PauloMorgado.Linq.UnitTests.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Collections;

    public static class Enumerable
    {
        public static ICollection AsCollection(this IEnumerable source)
        {
            return new EnumerableAsCollection(source);
        }

        private class EnumerableAsCollection : ICollection
        {
            private readonly IEnumerable source;

            public EnumerableAsCollection(IEnumerable source)
            {
                this.source = source;
            }

            #region IEnumerable Members

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.source.GetEnumerator();
            }

            #endregion

            #region ICollection Members

            public int Count
            {
                get { return 0; }
            }

            public void CopyTo(Array array, int index)
            {
                throw new NotImplementedException();
            }

            public bool IsSynchronized
            {
                get { throw new NotImplementedException(); }
            }

            public object SyncRoot
            {
                get { throw new NotImplementedException(); }
            }

            #endregion
        }
    }
}
