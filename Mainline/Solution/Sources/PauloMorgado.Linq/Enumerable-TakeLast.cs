//-----------------------------------------------------------------------
// <copyright file="Enumerable-TakeLast.cs"
//            project="PauloMorgado.Linq"
//            assembly="PauloMorgado.Linq"
//            solution="PauloMorgado.Linq"
//            company="Paulo Morgado">
//     Copyright (c) Paulo Morgado. All rights reserved.
// </copyright>
// <author>Paulo Morgado</author>
// <summary>Returns a specified number of contiguous elements from the end of a sequence.</summary>
//-----------------------------------------------------------------------

namespace PauloMorgado.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <remarks>
    /// Returns a specified number of contiguous elements from the end of a sequence.
    /// </remarks>
    public static partial class Enumerable
    {
        /// <summary>
        /// Returns a specified number of contiguous elements from the end of a sequence.	
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence to return elements from.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <returns>An <see cref="IEnumerable&lt;T&gt;"/> that contains the specified number of elements from the end of the input sequence.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source"/> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TSource> TakeLast<TSource>(this IEnumerable<TSource> source, int count)
        {
            Contract.Requires<ArgumentNullException>(source != null, "source");
            Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);

            if (count <= 0)
            {
                return System.Linq.Enumerable.Empty<TSource>();
            }

            var list = source as IList<TSource>;

            if (list != null)
            {
                return TakeLastListIterator<TSource>(list, count);
            }

            return TakeLastIterator<TSource>(source, count);
        }

        /// <summary>
        /// Returns a specified number of contiguous elements from the end of a list.	
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the <paramref name="list"/>.</typeparam>
        /// <param name="list">A list to return elements from.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <returns>An <see cref="IEnumerable&lt;T&gt;"/> that contains the specified number of elements from the end of the input list.</returns>
        private static IEnumerable<TSource> TakeLastListIterator<TSource>(IList<TSource> list, int count)
        {
            Contract.Assert(list != null);
            Contract.Assert(count > 0);

            int listCount = list.Count;

            for (int idx = listCount - ((count < listCount) ? count : listCount); idx < listCount; idx++)
            {
                yield return list[idx];
            }
        }

        /// <summary>
        /// Returns a specified number of contiguous elements from the end of a sequence.	
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence to return elements from.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <returns>An <see cref="IEnumerable&lt;T&gt;"/> that contains the specified number of elements from the end of the input sequence.</returns>
        private static IEnumerable<TSource> TakeLastIterator<TSource>(IEnumerable<TSource> source, int count)
        {
            Contract.Assert(source != null);
            Contract.Assert(count > 0);

            var sourceEnumerator = source.GetEnumerator();
            var buffer = new TSource[count];
            var numOfItems = 0;
            int idx;

            for (idx = 0; (idx < count) && sourceEnumerator.MoveNext(); idx++, numOfItems++)
            {
                buffer[idx] = sourceEnumerator.Current;
            }

            if (numOfItems < count)
            {
                for (idx = 0; idx < numOfItems; idx++)
                {
                    yield return buffer[idx];
                }

                yield break;
            }

            for (idx = 0; sourceEnumerator.MoveNext(); idx = (idx + 1) % count)
            {
                System.Diagnostics.Debug.WriteLine("3. end={0}", idx);

                buffer[idx] = sourceEnumerator.Current;
            }

            for (; numOfItems > 0; idx = (idx + 1) % count, numOfItems--)
            {
                yield return buffer[idx];
            }
        }
    }
}