using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment_1
{
    public class LLQueue
    {

        public object value;
        public LLQueue next;
        public LLQueue(object v, LLQueue n)
        {
            value = v;
            next = n;
        }
    }
    /// <summary>
    /// A queue internally implemented as a linked list of objects
    /// </summary>
    public class LinkedListQueue : Queue
    {


        LLQueue firstLLQ;
        LLQueue lastLLQ;
        /// <summary>
        /// Add object to end of queue
        /// </summary>
        /// <param name="o">object to add</param>
        public override void Enqueue(object o)
        {
            if (IsEmpty)
            {
                LLQueue tailLLQ = new LLQueue(o, firstLLQ);
            }
        }

        /// <summary>
        /// Remove object from beginning of queue.
        /// </summary>
        /// <returns>Object at beginning of queue</returns>
        public override object Dequeue()
        {
            // Remove this line when you implement this mehtod
            throw new NotImplementedException();
        }

        /// <summary>
        /// The number of elements in the queue.
        /// </summary>
        public override int Count
        {
            get
            {
                // Remove this line when you fill this method in.
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// True if the queue is full and enqueuing of new elements is forbidden.
        /// Note: LinkedListQueues can be grown to arbitrary length, and so can
        /// never fill.
        /// </summary>
        public override bool IsFull
        {
            get
            {
                // Remove this line when you fill this method in.
                throw new NotImplementedException();
            }
        }
    }
}

