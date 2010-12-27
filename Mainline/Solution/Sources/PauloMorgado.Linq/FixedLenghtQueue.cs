//-----------------------------------------------------------------------
// <copyright file="FixedLenghtQueue.cs"
//            project="PauloMorgado.Linq"
//            assembly="PauloMorgado.Linq"
//            solution="PauloMorgado.Linq"
//            company="Paulo Morgado">
//     Copyright (c) Paulo Morgado. All rights reserved.
// </copyright>
// <author>Paulo Morgado</author>
// <summary>A fixed length queue.</summary>
//-----------------------------------------------------------------------

namespace PauloMorgado.Linq
{
    using System.Diagnostics.Contracts;

    /// <summary>
    /// A fixed length queue.
    /// </summary>
    /// <typeparam name="T">The tyoe of the queued items.</typeparam>
    internal class FixedLenghtQueue<T>
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
        /// Initializes a new instance of the <see cref="FixedLenghtQueue&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public FixedLenghtQueue(int capacity)
        {
            Contract.Requires(capacity > 0);

            this.buffer = new T[capacity];
        }

        /// <summary>
        /// Pushes an item (<paramref name="push"/>) into the queue a and pops (<paramref name="pop"/>) the item at end of the queue, if the queue is full.
        /// </summary>
        /// <param name="push">The item to push.</param>
        /// <param name="pop">The item at end of the queue, if the queue is full; otherwise the default value of <typeparamref name="T"/>.</param>
        /// <returns><see langword="true"/> if an item was popped; otherwise <see langword="false"/>.</returns>
        public bool PushPop(T push, out T pop)
        {
            bool result;

            if (this.count < this.buffer.Length)
            {
                pop = default(T);

                this.buffer[this.cursor] = push;

                this.count++;
                this.cursor++;

                result = false;
            }
            else
            {
                pop = this.buffer[this.cursor];
                this.buffer[this.cursor] = push;

                this.cursor++;

                result = true;
            }

            if (this.cursor == this.buffer.Length)
            {
                this.cursor = 0;
            }

            return result;
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
            Contract.Invariant(this.cursor <= this.count);
            Contract.Invariant(this.cursor < this.buffer.Length);
        }
    }
}
