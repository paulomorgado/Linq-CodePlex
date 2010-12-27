//-----------------------------------------------------------------------
// <copyright file="CircularBuffer.cs"
//            project="PauloMorgado.Linq"
//            assembly="PauloMorgado.Linq"
//            solution="PauloMorgado.Linq"
//            company="Paulo Morgado">
//     Copyright (c) Paulo Morgado. All rights reserved.
// </copyright>
// <author>Paulo Morgado</author>
// <summary>Circularly buffers items in a fixed length buffer.</summary>
//-----------------------------------------------------------------------

namespace PauloMorgado.Linq
{
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Circularly buffers items in a fixed length buffer.
    /// </summary>
    /// <typeparam name="T">The type of the stored items.</typeparam>
    internal class CircularBuffer<T> : IEnumerable<T>
    {
        /// <summary>
        /// The buffer.
        /// </summary>
        private readonly T[] buffer;

        /// <summary>
        /// The position of the next inserted item.
        /// </summary>
        private int cursor;

        /// <summary>
        /// The number of stored items.
        /// </summary>
        private int count;

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularBuffer&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public CircularBuffer(int capacity)
        {
            Contract.Requires(capacity > 0);

            this.buffer = new T[capacity];
        }

        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(T item)
        {
            this.buffer[this.cursor] = item;

            if (this.count < this.buffer.Length)
            {
                this.count++;
            }

            if (++this.cursor >= this.buffer.Length)
            {
                this.cursor = 0;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1"></see> that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            int capacity = this.buffer.Length;

            for (int i = (this.cursor - this.count + capacity) % capacity, c = this.count; c > 0; c--, i = (i + 1) % capacity)
            {
                System.Diagnostics.Debug.WriteLine("[{0}]={1}", i, this.buffer[i]);

                yield return this.buffer[i];
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Contract checking invariant method.
        /// </summary>
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.buffer.Length > 0);
            Contract.Invariant(this.buffer != null);
            Contract.Invariant(this.count >= 0);
            Contract.Invariant(this.count <= this.buffer.Length);
            Contract.Invariant(this.cursor >= 0);
            Contract.Invariant(this.cursor < this.buffer.Length);
        }
    }
}
