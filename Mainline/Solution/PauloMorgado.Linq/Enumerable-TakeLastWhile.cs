//-----------------------------------------------------------------------
// <copyright file="Enumerable-TakeLastWhile.cs"
//            project="PauloMorgado.Linq"
//            assembly="PauloMorgado.Linq"
//            solution="PauloMorgado.Linq"
//            company="Paulo Morgado">
//     Copyright (c) Paulo Morgado. All rights reserved.
// </copyright>
// <author>Paulo Morgado</author>
// <summary>Returns the elements from the end of a sequence as long as the specified condition is true.</summary>
//-----------------------------------------------------------------------

namespace PauloMorgado.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <remarks>
    /// Returns the elements from the end of a sequence as long as the specified condition is true.
    /// </remarks>
    public static partial class Enumerable
    {
        /// <summary>
        /// Returns the elements from the end of a sequence as long as the specified condition is true.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence to return elements from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An <see cref="IEnumerable&lt;T&gt;"/> that contains elements from the input sequence that occur after the last element at which the test no longer passes.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="predicate"/> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TSource> TakeLastWhile<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            Contract.Requires<ArgumentNullException>(source != null, "source");
            Contract.Requires<ArgumentNullException>(predicate != null, "predicate");
            Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);

            return TakeLastWhileIterator<TSource>(source, predicate);
        }

        /// <summary>
        /// Returns the elements from the end of a sequence as long as the specified condition is true.
        /// The elements index is used in the logic of the predicate function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence to return elements from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An <see cref="IEnumerable&lt;T&gt;"/> that contains elements from the input sequence that occur after the last element at which the test no longer passes.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="predicate"/> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TSource> TakeLastWhile<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            Contract.Requires<ArgumentNullException>(source != null, "source");
            Contract.Requires<ArgumentNullException>(predicate != null, "predicate");
            Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);

            return TakeLastWhileIterator<TSource>(source, predicate);
        }

        /// <summary>
        /// Returns the elements from the end of a sequence as long as the specified condition is true.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence to return elements from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An <see cref="IEnumerable&lt;T&gt;"/> that contains elements from the input sequence that occur after the last element at which the test no longer passes.</returns>
        private static IEnumerable<TSource> TakeLastWhileIterator<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            Contract.Assert(source != null);
            Contract.Assert(predicate != null);

            var buffer = new List<TSource>();

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    buffer.Add(item);
                }
                else
                {
                    buffer.Clear();
                }
            }

            foreach (var item in buffer)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Returns the elements from the end of a sequence as long as the specified condition is true.
        /// The elements index is used in the logic of the predicate function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence to return elements from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An <see cref="IEnumerable&lt;T&gt;"/> that contains elements from the input sequence that occur after the last element at which the test no longer passes.</returns>
        private static IEnumerable<TSource> TakeLastWhileIterator<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            Contract.Assert(source != null);
            Contract.Assert(predicate != null);

            var buffer = new List<TSource>();
            var index = 0;

            foreach (var item in source)
            {
                if (predicate(item, index++))
                {
                    buffer.Add(item);
                }
                else
                {
                    buffer.Clear();
                }
            }

            foreach (var item in buffer)
            {
                yield return item;
            }
        }
    }
}
