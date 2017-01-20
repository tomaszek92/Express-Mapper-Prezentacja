using System;

namespace ExpressMapperTutorial.Models.Test1
{
    public class PersonEf : IRandomCreateable, IHandWrittenMapperable<PersonViewModel>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public decimal Money { get; set; }
        public Gender Gender { get; set; }
        public bool HasDog { get; set; }

        public void FillWithRandomValues()
        {
            Random rand = new Random();
            Id = rand.Next();
            Gender = rand.Next(2) % 2 == 0 ? Gender.Man : Gender.Female;
            Age = rand.Next(100);
            FirstName = "FirstName" + rand.Next();
            LastName = "LastName" + rand.Next();
            HasDog = rand.Next(2) % 2 == 0;
            Money = (decimal) (rand.NextDouble() * 1000);
        }

        public PersonViewModel Map()
        {
            return new PersonViewModel
            {
                Age = this.Age,
                Gender = this.Gender,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Money = this.Money,
                Id = this.Id,
                HasDog = this.HasDog
            };
        }
    }
}