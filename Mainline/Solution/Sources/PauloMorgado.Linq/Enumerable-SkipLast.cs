//-----------------------------------------------------------------------
// <copyright file="Enumerable-SkipLast.cs"
//            project="PauloMorgado.Linq"
//            assembly="PauloMorgado.Linq"
//            solution="PauloMorgado.Linq"
//            company="Paulo Morgado">
//     Copyright (c) Paulo Morgado. All rights reserved.
// </copyright>
// <author>Paulo Morgado</author>
// <summary>Returns all but a specified number of contiguous elements from the end of a sequence.</summary>
//-----------------------------------------------------------------------

namespace PauloMorgado.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <remarks>
    /// Returns all but a specified number of contiguous elements from the end of a sequence.
    /// </remarks>
    public static partial class Enumerable
    {
        /// <summary>
        /// Returns all but a specified number of contiguous elements from the end of a sequence.	
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence to return elements from.</param>
        /// <param name="count">The number of elements to skip.</param>
        /// <returns>An <see cref="IEnumerable&lt;T&gt;"/> that contains all but the last specified number of elements from the end of the input sequence.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source"/> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TSource> SkipLast<TSource>(IEnumerable<TSource> source, int count)
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
                if (count >= list.Count)
                {
                    return System.Linq.Enumerable.Empty<TSource>();
                }

                return SkipLastListIterator<TSource>(list, count);
            }

            return SkipLastListIterator<TSource>(source, count);
        }

        /// <summary>
        /// Returns a specified number of contiguous elements from the end of a list.	
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the <paramref name="list"/>.</typeparam>
        /// <param name="list">A list to return elements from.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <returns>An <see cref="IEnumerable&lt;T&gt;"/> that contains the specified number of elements from the end of the input list.</returns>
        private static IEnumerable<TSource> SkipLastListIterator<TSource>(IList<TSource> list, int count)
        {
            Contract.Assert(list != null);
            Contract.Assert(count > 0);
            Contract.Assert(count < list.Count);

            int returnCount = list.Count - count;

            for (int i = 0; i < returnCount; i++)
            {
                yield return list[i];
            }
        }

        /// <summary>
        /// Returns a specified number of contiguous elements from the end of a sequence.	
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence to return elements from.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <returns>An <see cref="IEnumerable&lt;T&gt;"/> that contains the specified number of elements from the end of the input sequence.</returns>
        private static IEnumerable<TSource> SkipLastListIterator<TSource>(IEnumerable<TSource> source, int count)
        {
            Contract.Assert(source != null);
            Contract.Assert(count > 0);

            var buffer = new TSource[count];
            var end = -1;

            var sourceEnumerator = source.GetEnumerator();

            while ((++end < count) && sourceEnumerator.MoveNext())
            {
                buffer[end] = sourceEnumerator.Current;
            }

            while (sourceEnumerator.MoveNext())
            {
                end = (end + 1) % count;

                var yieldableItem = buffer[end];

                buffer[end] = sourceEnumerator.Current;

                yield return yieldableItem;
            }
        }
    }
}