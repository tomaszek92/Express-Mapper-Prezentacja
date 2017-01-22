using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpressMapperTutorial.Models.Test3
{
    public class SourceCar : IRandomCreateable, IHandWrittenMapperable<DestCar>
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public float Engine { get; set; }
        public int MaxSpeed { get; set; }
        public List<SourceCarItem> Equipment { get; set; }
        public SourceCarCategory Category { get; set; }

        public void FillWithRandomValues()
        {
            Random rand = new Random();
            Id = rand.Next();
            Brand = "Toyota";
            Price = (decimal) (rand.NextDouble()*1000000);
            Engine = rand.Next(5);
            MaxSpeed = rand.Next(100, 250);

            Array enumValues = Enum.GetValues(typeof(SourceCarCategory));
            Category = (SourceCarCategory) enumValues.GetValue(rand.Next(enumValues.Length));

            Equipment = new List<SourceCarItem>();
            for (int i = 0; i < 5; i++)
            {
                Equipment.Add(new SourceCarItem
                {
                    Id = rand.Next(),
                    Description = "coś"
                });
            }
        }

        public DestCar Map()
        {
            return new DestCar
            {
                Engine = this.Engine,
                ID = this.Id,
                Mark = this.Brand,
                Price = this.Price,
                MaxSpeedInMetersPerSec = (int) Math.Round(this.MaxSpeed*10/3.6),
                Equipment = this.Equipment
                    .Select(x => new DestCarItem {ID = x.Id, Description = x.Description})
                    .ToList(),
                Cat = Map(this.Category)
            };
        }

        public DestCarCategory Map(SourceCarCategory sourceCarCategory)
        {
            switch (sourceCarCategory)
            {
                case SourceCarCategory.A:
                    return DestCarCategory.Mini;
                case SourceCarCategory.B:
                    return DestCarCategory.Miejskie;
                case SourceCarCategory.C:
                    return DestCarCategory.Kompaktowe;
                case SourceCarCategory.D:
                    return DestCarCategory.Średnie;
                case SourceCarCategory.E:
                    return DestCarCategory.Wyższe;
                case SourceCarCategory.F:
                    return DestCarCategory.Luksusowe;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sourceCarCategory), sourceCarCategory, null);
            }
        }
    }
}