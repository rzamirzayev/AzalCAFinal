﻿namespace Services.Cities
{
    public class CitiesGetAllDto
    {
        public int CityId { get; set; }
        public required string  CityName { get; set; }
        public required string CountryName { get; set; }
    }
}
