using System;

namespace TailwindBlazorElectron.Model
{
	public class Address
	{
		public Guid Id { get; set; }
		public int ZipCode { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public string HouseNumber { get; set; }
		public string Flattened => $"{Street} {HouseNumber}, {ZipCode} {City}";
	}
}
