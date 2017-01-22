using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressMapperTutorial.Models.Test3
{
    public class DestCar : IEquatable<DestCar>
    {
        public int ID { get; set; }
        public string Mark { get; set; }
        public decimal Price { get; set; }
        public float Engine { get; set; }
        public int MaxSpeedInMetersPerSec { get; set; }
        public List<DestCarItem> Equipment { get; set; }
        public DestCarCategory Cat { get; set; }

        public bool Equals(DestCar other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ID == other.ID &&
                   string.Equals(Mark, other.Mark) &&
                   Price == other.Price &&
                   Engine.Equals(other.Engine) &&
                   MaxSpeedInMetersPerSec == other.MaxSpeedInMetersPerSec &&
                   Equipment.SequenceEqual(other.Equipment) &&
                   Cat == other.Cat;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DestCar) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = ID;
                hashCode = (hashCode*397) ^ Mark.GetHashCode();
                hashCode = (hashCode*397) ^ Price.GetHashCode();
                hashCode = (hashCode*397) ^ Engine.GetHashCode();
                hashCode = (hashCode*397) ^ MaxSpeedInMetersPerSec;
                hashCode = (hashCode*397) ^ Equipment.GetHashCode();
                hashCode = (hashCode*397) ^ (int) Cat;
                return hashCode;
            }
        }
    }
}