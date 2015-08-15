﻿using System;
using System.Collections.Generic;
using System.Linq;

using static AirBreather.Common.Utilities.EnumerableUtility;

namespace AirBreather.Common.Collections
{
    public sealed class ReadOnlyListWrapper<T> : IReadOnlyList<T>, IList<T>
    {
        private readonly IReadOnlyList<T> wrappedList;

        public ReadOnlyListWrapper(IReadOnlyList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            this.wrappedList = list;
        }

        public bool IsReadOnly => true;
        public int Count => this.wrappedList.Count;

        public int IndexOf(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (Equals(this.wrappedList[i], item))
                {
                    return i;
                }
            }

            return -1;
        }

        public T this[int index] => this.wrappedList[index];

        T IList<T>.this[int index]
        {
            get { return this[index]; }
            set { throw new NotSupportedException(); }
        }

        public bool Contains(T item) => this.wrappedList.Contains(item);
        public void CopyTo(T[] array, int arrayIndex) => this.AsEnumerable().CopyTo(array, arrayIndex);
        public IEnumerator<T> GetEnumerator() => this.wrappedList.GetEnumerator();
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => this.wrappedList.GetEnumerator();

        public void Add(T item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Remove(T item)
        {
            throw new NotSupportedException();
        }

        public void Insert(int index, T item)
        {
            throw new NotSupportedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotSupportedException();
        }
    }
}