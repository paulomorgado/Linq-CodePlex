namespace PauloMorgado.Linq.UnitTests.Utils
{
    using System.Collections;

    internal partial class EnumerableAssert
    {
        private class ObjectComparer : IComparer
        {
            int IComparer.Compare(object x, object y)
            {
                if (!object.Equals(x, y))
                {
                    return -1;
                }

                return 0;
            }
        }
    }
}
