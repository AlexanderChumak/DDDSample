﻿using System.Collections.Generic;
using System.Linq;

namespace Uklon.Ordering.Domain
{
    public abstract class ValueObject<T>
        where T : ValueObject<T>
    {
        protected abstract IEnumerable<object> GetAttributesToIncludeInEqualityCheck();

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            var valueObject = (ValueObject<T>)obj;

            bool sequenceEqual = GetAttributesToIncludeInEqualityCheck()
                .SequenceEqual(valueObject.GetAttributesToIncludeInEqualityCheck());
            return sequenceEqual;
        }

        // override for performance optimizations
        public virtual bool Equals(T other)
        {
            return Equals((object)other);
        }

        public override int GetHashCode()
        {
            return GetAttributesToIncludeInEqualityCheck()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals((T)b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
        {
            return !(a == b);
        }
    }
}
