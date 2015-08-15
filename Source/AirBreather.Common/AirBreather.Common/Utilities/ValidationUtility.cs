﻿using System;
using System.Globalization;

namespace AirBreather.Common.Utilities
{
    public static class ValidationUtility
    {
        public static T ValidateNotNull<T>(this T value, string paramName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }

            return value;
        }

        public static T ValidateInRange<T>(this T value, string paramName, T minInclusive, T maxExclusive) where T : IComparable<T>
        {
            if (value.CompareTo(minInclusive) < 0 || value.CompareTo(maxExclusive) >= 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, String.Format(CultureInfo.InvariantCulture, "Must be between [{0}, {1}).", minInclusive, maxExclusive));
            }

            return value;
        }

        public static T ValidateMin<T>(this T value, string paramName, T minInclusive) where T : IComparable<T>
        {
            if (value.CompareTo(minInclusive) < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, String.Format(CultureInfo.InvariantCulture, "Must be greater than or equal to {0}.", minInclusive));
            }

            return value;
        }
    }
}
