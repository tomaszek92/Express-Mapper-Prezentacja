using System;

namespace ExpressMapperTutorial.Models.Test3
{
    public class DestCarItem : IEquatable<DestCarItem>
    {
        public int ID { get; set; }
        public string Description { get; set; }

        public bool Equals(DestCarItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ID == other.ID && string.Equals(Description, other.Description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DestCarItem) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (ID*397) ^ Description.GetHashCode();
            }
        }
    }
}