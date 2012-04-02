using System;

namespace RoastPotato.Application.Tests
{
    public class ExtendedData
    {
        public string City { get; set; }
        public DateTime Created { get; set; }
    }

    public class SampleData
    {
        public string Address { get; set; }
        public int Id { get; set; }
        public ExtendedData Location { get; set; }
        public DateTime AddDate { get; set; }
        public decimal Value { get; set; }
    }
}