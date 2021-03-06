﻿//-----------------------------------------------------------------------
// <copyright file="Enumerable-Distinct.cs"
//            project="PauloMorgado.Linq"
//            assembly="PauloMorgado.Linq"
//            solution="PauloMorgado.Linq"
//            company="Paulo Morgado">
//     Copyright (c) Paulo Morgado. All rights reserved.
// </copyright>
// <author>Paulo Morgado</author>
// <summary>Returns distinct elements from a sequence.</summary>
//-----------------------------------------------------------------------

namespace PauloMorgado.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;

    /// <content>
    /// Returns distinct elements from a sequence.
    /// </content>
    public static partial class Enumerable
    {
        /// <summary>
        /// Returns distinct elements from a sequence by using the <paramref name="predicate" /> to determine if two elements are the same.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <param name="predicate">A predicate to determine if two elements are the same.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains distinct elements from the source sequence.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="predicate"/> is <see langword="null" />.
        /// </exception>
        internal static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, bool> predicate)
        {
            Contract.Requires<ArgumentNullException>(source != null, "source");
            Contract.Requires<ArgumentNullException>(predicate != null, "predicate");
            Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);

            return source.Distinct(new PredicateEqualityComparer<TSource>(predicate));
        }

        /// <summary>
        /// Returns distinct elements from a sequence by using the <paramref name="selector" /> to select the comparison key.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <param name="selector">The selector.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> that contains distinct elements from the source sequence.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            Contract.Requires<ArgumentNullException>(source != null, "source");
            Contract.Requires<ArgumentNullException>(selector != null, "selector");
            Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);

            return source.Distinct(new SelectorEqualityComparer<TSource, TKey>(selector));
        }

        /// <summary>
        /// Returns distinct elements from a sequence by using the <paramref name="selector" /> to select the comparison key and the sppecified <paramref name="comparer" /> to compare the values of the keys.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <param name="selector">The selector.</param>
        /// <param name="comparer">The comparer.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> that contains distinct elements from the source sequence.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IEqualityComparer<TKey> comparer)
        {
            Contract.Requires<ArgumentNullException>(source != null, "source");
            Contract.Requires<ArgumentNullException>(selector != null, "selector");
            Contract.Requires<ArgumentNullException>(comparer != null, "comparer");
            Contract.Ensures(Contract.Result<IEnumerable<TSource>>() != null);

            return source.Distinct(new SelectorEqualityComparer<TSource, TKey>(selector, comparer));
        }
    }
}