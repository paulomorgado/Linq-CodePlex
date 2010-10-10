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
        public static IEnumerable<TSource> TakeLast<TSource>(IEnumerable<TSource> source, int count)
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
                    return list;
                }

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
            Contract.Assert(count < list.Count);

            int listCount = list.Count;

            for (int i = listCount - count; i < listCount; i++)
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
        private static IEnumerable<TSource> TakeLastIterator<TSource>(IEnumerable<TSource> source, int count)
        {
            Contract.Assert(source != null);
            Contract.Assert(count > 0);

            var buffer = new TSource[count];
            var end = -1;
            var numOfItems = 0;

            foreach (var item in source)
            {
                if (numOfItems < count)
                {
                    end++;
                    numOfItems++;
                }
                else
                {
                    end = (end + 1) % count;
                }

                buffer[end] = item;
            }

            for (int i = numOfItems - 1; i >= 0; i--)
            {
                yield return buffer[i % count];
            }
        }
    }
}