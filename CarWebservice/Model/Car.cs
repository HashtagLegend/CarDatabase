using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWebservice.Model
{
    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Id { get; set; }
        public string Year { get; set; }
        

        public Car()
        {
            
        }

        public Car(string brand, string model, string year)
        {
            Brand = brand;
            Model = model;
            Year = year;
        }

        public override string ToString()
        {
            return $"{nameof(Brand)}: {Brand}, {nameof(Model)}: {Model}, {nameof(Id)}: {Id}, {nameof(Year)}: {Year}";
        }
    }
}
