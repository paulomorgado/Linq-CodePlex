//-----------------------------------------------------------------------
// <copyright file="PredicateEqualityComparer.cs"
//            project="PauloMorgado.Linq"
//            assembly="PauloMorgado.Linq"
//            solution="PauloMorgado.Linq"
//            company="Paulo Morgado">
//     Copyright (c) Paulo Morgado. All rights reserved.
// </copyright>
// <author>Paulo Morgado</author>
// <summary>EqualityComparer{T} that uses a predicate to compare objects.</summary>
//-----------------------------------------------------------------------

namespace PauloMorgado.Linq
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// <see cref="EqualityComparer{T}" /> that uses a predicate to compare objects.
    /// </summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    public class PredicateEqualityComparer<T> : EqualityComparer<T>
    {
        /// <summary>
        /// The empty hash function.
        /// </summary>
        /// <remarks>
        /// Always returns the same value to force the call to <see cref="IEqualityComparer{T}" />.Equals.
        /// </remarks>
        private static readonly Func<T, int> emptyHashFunc = obj => 0;

        /// <summary>
        /// The comparison predicate.
        /// </summary>
        private readonly Func<T, T, bool> predicate;

        /// <summary>
        /// The hash function.
        /// </summary>
        private readonly Func<T, int> hashFunc;

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateEqualityComparer{T}"/> class.
        /// </summary>
        /// <param name="predicate">The comparison predicate.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="predicate"/> is <see langword="null"/>.
        /// </exception>
        public PredicateEqualityComparer(Func<T, T, bool> predicate)
            : this(predicate, emptyHashFunc)
        {
            Contract.Requires<ArgumentNullException>(predicate != null, "predicate");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateEqualityComparer{T}"/> class.
        /// </summary>
        /// <param name="predicate">The comparison predicate.</param>
        /// <param name="hashFunc">The hash function.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="predicate"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="hashFunc"/> is <see langword="null"/>.
        /// </exception>
        public PredicateEqualityComparer(Func<T, T, bool> predicate, Func<T, int> hashFunc)
            : base()
        {
            Contract.Requires<ArgumentNullException>(predicate != null, "predicate");
            Contract.Requires<ArgumentNullException>(hashFunc != null, "hashFunc");

            this.predicate = predicate;
            this.hashFunc = hashFunc;
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
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        /// <exception cref="T:System.ArgumentNullException">The type of obj is a reference type and obj is null.</exception>
        [Pure]
        public override int GetHashCode(T obj)
        {
            Contract.Requires<ArgumentNullException>(obj != null, "obj");

            return this.hashFunc(obj);
        }

        /// <summary>
        /// Contract checking invariant method.
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.predicate != null);
            Contract.Invariant(this.hashFunc != null);
        }
    }
}
