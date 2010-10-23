//-----------------------------------------------------------------------
// <copyright file="PredicateEqualityComparer.cs"
//            project="PauloMorgado.Linq"
//            assembly="PauloMorgado.Linq"
//            solution="PauloMorgado.Linq"
//            company="Paulo Morgado">
//     Copyright (c) Paulo Morgado. All rights reserved.
// </copyright>
// <author>Paulo Morgado</author>
// <summary>EqualityComparer&lt;T&gt; that uses a predicate to compare objects.</summary>
//-----------------------------------------------------------------------

namespace PauloMorgado.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// <see cref="EqualityComparer&lt;T&gt;" /> that uses a predicate to compare objects.
    /// </summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    public class PredicateEqualityComparer<T> : EqualityComparer<T>
    {
        /// <summary>
        /// The predicate.
        /// </summary>
        private Func<T, T, bool> predicate;

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateEqualityComparer&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="predicate"/> is <see langword="null" />.
        /// </exception>
        public PredicateEqualityComparer(Func<T, T, bool> predicate)
            : base()
        {
            Contract.Requires<ArgumentNullException>(predicate != null, "predicate");

            this.predicate = predicate;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="PredicateEqualityComparer&lt;T&gt;"/> class from being created.
        /// </summary>
        private PredicateEqualityComparer()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type <typeparamref name="T"/> to compare.</param>
        /// <param name="y">The second object of type <typeparamref name="T"/> to compare.</param>
        /// <returns><see langword="true" /> if the specified objects are equal; otherwise <see langword="false" />.</returns>
        [Pure]
        public override bool Equals(T x, T y)
        {
            if (x != null)
            {
                return ((y != null) && this.predicate(x, y));
            }

            if (y != null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object for which to get a hash code.</param>
        /// <returns>
        /// Always return the same value to force the call to <see cref="IEqualityComparer&lt;T&gt;" />.Equals.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// The type of <paramref name="obj"/> is a reference and <paramref name="obj"/> is <see langword="null"/>.
        /// </exception>
        [Pure]
        public override int GetHashCode(T obj)
        {
            Contract.Requires<ArgumentNullException>(obj != null, "obj");

            return 0;
        }

        /// <summary>
        /// Contract checking invarinat method.
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.predicate != null);
        }
    }
}
