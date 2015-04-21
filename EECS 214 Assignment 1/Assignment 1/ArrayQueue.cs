using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment_1
{
    /// <summary>
    /// A queue internally implemented as an array
    /// </summary>
    public class ArrayQueue : Queue
    {
        object[] data = new object[100];
        int head = 0;
        int tail = 0;
        /// <summary>
        /// Add object to end of queue
        /// </summary>
        /// <param name="o">object to add</param>
        public override void Enqueue(object o)
        {
            if (!IsFull)
            {
                data[tail] = o;
                tail = (tail + 1) % data.Length;
            }
            else throw new QueueFullException();
        }

        /// <summary>
        /// Remove object from beginning of queue.
        /// </summary>
        /// <returns>Object at beginning of queue</returns>
        public override object Dequeue()
        {
            if (!IsEmpty)
            {
                object item = data[head];
                head = (head + 1) % data.Length;
                return item;
            }
            else throw new QueueEmptyException();
        }

        /// <summary>
        /// The number of elements in the queue.
        /// </summary>
        public override int Count
        {
            get {
                if (tail >= head) return tail - head;
                else return (100 - head) + tail;
            }
        }

        /// <summary>
        /// True if the queue is full and enqueuing of new elements is forbidden.
        /// </summary>
        public override bool IsFull
        {
            get {
                return Count >= 100;
            }
        }
    }
}
