using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Generator.CustomGenerator
{
    using System;
    using System.Collections;
    using Interface;

    /// <summary>
    /// Custom id generator.
    /// </summary>
    [Serializable]
    public class Generator : IGenerator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Generator"/> class.
        /// </summary>
        /// <param name="current">Current number.</param>
        public Generator(int current)
        {
            this.Current = current;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Generator"/> class.
        /// </summary>
        public Generator() : this(0)
        {
        }

        private volatile int current;

        /// <summary>
        /// Gets information about current number of the sequence.
        /// </summary>
        public int Current
        {
            get { return current; }
            private set { this.current = value; }
        }

        /// <summary>
        /// Gets information about current number of the sequence.
        /// </summary>
        object IEnumerator.Current => this.Current;

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
        public bool MoveNext()
        {
            if (int.MaxValue - this.Current < 1)
            {
                return false;
            }
            Current++;
            return true;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public void Reset()
        {
            this.Current = 0;
        }

        /// <summary>
        /// Method is using for closing and releasing unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Current = 0;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current generator instance.
        /// </summary>
        /// <returns>New object that is a copy of the current instance.</returns>
        public object Clone()
        {
            return new Generator(this.Current);
        }
    }
}