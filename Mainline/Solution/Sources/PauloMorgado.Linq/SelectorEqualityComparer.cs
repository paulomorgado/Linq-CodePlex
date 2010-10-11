//-----------------------------------------------------------------------
// <copyright file="SelectorEqualityComparer.cs"
//            project="PauloMorgado.Linq"
//            assembly="PauloMorgado.Linq"
//            solution="PauloMorgado.Linq"
//            company="Paulo Morgado">
//     Copyright (c) Paulo Morgado. All rights reserved.
// </copyright>
// <author>Paulo Morgado</author>
// <summary>EqualityComparer&lt;T&gt; that uses a selector to get the key to compare objects.</summary>
//-----------------------------------------------------------------------

namespace PauloMorgado.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// <see cref="EqualityComparer&lt;T&gt;" /> that uses a selector to get the key to compare objects.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public class SelectorEqualityComparer<TSource, TKey> : EqualityComparer<TSource>
    {
        /// <summary>
        /// The key selector.
        /// </summary>
        private IEqualityComparer<TKey> comparer;

        /// <summary>
        /// The key comparer.
        /// </summary>
        private Func<TSource, TKey> selector;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorEqualityComparer&lt;TSource, Tkey&gt;"/> class.
        /// </summary>
        /// <param name="selector">The key selector.</param>
        public SelectorEqualityComparer(Func<TSource, TKey> selector)
            : this(selector, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectorEqualityComparer&lt;TSource, Tkey&gt;"/> class.
        /// </summary>
        /// <param name="selector">The key selector.</param>
        /// <param name="comparer">The key comparer.</param>
        public SelectorEqualityComparer(Func<TSource, TKey> selector, IEqualityComparer<TKey> comparer)
            : base()
        {
            Contract.Requires<ArgumentNullException>(selector != null, "selector");
            Contract.Ensures(this.comparer != null);

            this.selector = selector;
            this.comparer = comparer ?? EqualityComparer<TKey>.Default;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="SelectorEqualityComparer&lt;TSource, Tkey&gt;"/> class from being created.
        /// </summary>
        private SelectorEqualityComparer()
            : base()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type <typeparamref name="TSource"/> to compare.</param>
        /// <param name="y">The second object of type <typeparamref name="TSource"/> to compare.</param>
        /// <returns><see langword="true" /> if the specified objects are equal; otherwise <see langword="false" />.</returns>
        [Pure]
        public override bool Equals(TSource x, TSource y)
        {
            TKey xKey = this.selector(x);
            TKey yKey = this.selector(y);

            return this.comparer.Equals(xKey, yKey);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// The type of <paramref name="obj"/> is a reference and <paramref name="obj"/> is <see langword="null" />.
        /// </exception>
        [Pure]
        public override int GetHashCode(TSource obj)
        {
            Contract.Requires<ArgumentNullException>(obj != null, "obj");

            TKey key = this.selector(obj);

            return this.comparer.GetHashCode(key);
        }

        /// <summary>
        /// Contract checking invarinat method.
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.selector != null);
            Contract.Invariant(this.comparer != null);
        }
    }
}
