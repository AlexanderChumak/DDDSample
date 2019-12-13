using System;

namespace Uklon.Ordering.Domain
{
    public abstract class DomainEntity<TKey>
    {
        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        public TKey Id { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as DomainEntity<TKey>);
        }

        public virtual bool Equals(DomainEntity<TKey> other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                       otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        public static bool operator ==(DomainEntity<TKey> left, DomainEntity<TKey> right)
        {
            return object.Equals(left, null) ? object.Equals(right, null) : left.Equals(right);
        }

        public static bool operator !=(DomainEntity<TKey> x, DomainEntity<TKey> y)
        {
            return !(x == y);
        }

        private int? _requestedHashCode;

        public override int GetHashCode()
        {
            if (!IsTransient(this))
            {
                if (!_requestedHashCode.HasValue)
                {
                    _requestedHashCode = Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
                }

                return _requestedHashCode.Value;
            }
            else
            {
                return GetHashCode();
            }
        }

        private static bool IsTransient(DomainEntity<TKey> obj)
        {
            return obj != null && Equals(obj.Id, default(TKey));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }
    }
}
