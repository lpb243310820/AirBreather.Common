﻿using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace AirBreather.Random
{
    public static class CryptographicRandomGenerator
    {
        // RNGCryptoServiceProvider claims to be thread-safe and carries no state.
        private static readonly Lazy<RNGCryptoServiceProvider> SingletonProvider = new Lazy<RNGCryptoServiceProvider>();

        public static byte[] GetBuffer(int length) => FillBuffer(length == 0 ? Array.Empty<byte>() : new byte[length]);

        public static byte[] FillBuffer(byte[] data)
        {
            SingletonProvider.Value.GetBytes(data);
            return data;
        }

        public static byte[] FillBuffer(byte[] data, int offset, int count)
        {
            if (offset == 0 && count == data?.Length)
            {
                // In this common case, we can avoid the allocation and byte copy that the 3-
                // parameter version of this method does.
                SingletonProvider.Value.GetBytes(data);
            }
            else
            {
                SingletonProvider.Value.GetBytes(data, offset, count);
            }

            return data;
        }

        // Offer quick-and-easy ways of generating random value types, because this is often going
        // to be used just to get a random seed for a PRNG. Allocate a new array each time because
        // these are expected to be used infrequently.
        public static byte NextByte() => Next<byte>();
        public static short NextInt16() => Next<short>();
        public static int NextInt32() => Next<int>();
        public static long NextInt64() => Next<long>();

        public static sbyte NextSByte() => Next<sbyte>();
        public static ushort NextUInt16() => Next<ushort>();
        public static uint NextUInt32() => Next<uint>();
        public static ulong NextUInt64() => Next<ulong>();

        public static double NextDouble() => NextUInt64() / (double)ulong.MaxValue;
        public static float NextSingle() => (float)NextDouble();

        private static T Next<T>()
        {
            byte[] buf = ByteArrayPool.Instance.Rent(Unsafe.SizeOf<T>());
            try
            {
                FillBuffer(buf);
                return Unsafe.As<byte, T>(ref buf[0]);
            }
            finally
            {
                ByteArrayPool.Instance.Return(buf);
            }
        }
    }
}

