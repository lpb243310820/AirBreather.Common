﻿using System;

using AirBreather.Logging;

namespace AirBreather
{
    // my own IDisposable pattern, part 1 of 2
    // almost everything of mine that needs to be IDisposable will be this.
    // no finalizer; unmanaged resources must have a managed wrapper,
    // see UnmanagedResourceContainer<T>.
    public abstract class DisposableObject : IDisposable
    {
        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            if (this.IsDisposed)
            {
                Log.For(this).Verbose("Skipping dispose of an already disposed object.");
                return;
            }

            this.DisposeCore();
            this.IsDisposed = true;
        }

        protected abstract void DisposeCore();

        protected void ThrowIfDisposed()
        {
            if (this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().Name).Log(this.GetType());
            }
        }
    }
}
