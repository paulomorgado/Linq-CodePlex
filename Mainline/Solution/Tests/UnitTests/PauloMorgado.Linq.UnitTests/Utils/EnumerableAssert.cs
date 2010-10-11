namespace PauloMorgado.Linq.UnitTests.Utils
{
    using System.Diagnostics.Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Globalization;
    using System.Collections;

    internal partial class EnumerableAssert
    {
        public static void AreEqual(IEnumerable expected, IEnumerable actual)
        {
            AreEqual(expected, actual, string.Empty, null);
        }

        public static void AreEqual(IEnumerable expected, IEnumerable actual, string message, params object[] parameters)
        {
            string reason = string.Empty;

            if (!AreEnumerablesEqual(expected, actual, new ObjectComparer(), ref reason))
            {
                HandleFail("EnumerableAssert.AreEqual", string.Format("{0}({1})", message, reason), parameters);
            }
        }

        private static bool AreEnumerablesEqual(IEnumerable expected, IEnumerable actual, IComparer comparer, ref string reason)
        {
            Contract.Assert(expected != null);
            Contract.Assert(actual != null);
            Contract.Assert(comparer != null);

            if (!object.ReferenceEquals(expected, actual))
            {
                if ((expected == null) || (actual == null))
                {
                    return false;
                }

                IEnumerator expectedEnumerator = expected.GetEnumerator();
                IEnumerator actualEnumerator = actual.GetEnumerator();

                for (int i = 0; expectedEnumerator.MoveNext() && actualEnumerator.MoveNext(); i++)
                {
                    if (0 != comparer.Compare(expectedEnumerator.Current, actualEnumerator.Current))
                    {
                        reason = string.Format("Element at index {0} do not match.", i);

                        return false;
                    }
                }

                reason = "Both Enumerable contain same elements.";

                return true;
            }

            reason = "Both Enumerable references point to the same Enumerable object.";

            return true;
        }

        internal static void HandleFail(string assertionName, string message, params object[] parameters)
        {
            string str = string.Empty;

            if (!string.IsNullOrEmpty(message))
            {
                if (parameters == null)
                {
                    str = ReplaceNulls(message);
                }
                else
                {
                    str = string.Format(CultureInfo.CurrentCulture, ReplaceNulls(message), parameters);
                }
            }

            throw new AssertFailedException(string.Format("{0} failed. {1}", assertionName, str));
        }

        internal static string ReplaceNulls(object input)
        {
            if (input == null)
            {
                return "(null)";
            }

            string str = input.ToString();

            if (str == null)
            {
                return "(object)";
            }

            return Assert.ReplaceNullChars(str);
        }
    }
}
