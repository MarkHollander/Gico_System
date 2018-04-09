using System;
using System.Reflection;
using Gico.CQRS.Model.Interfaces;
using Gico.Domains;

namespace Gico.OrderDomains
{
    public class Address : BaseDomain,IVersioned
    {
        public Address(int version)
        {
            Version = version;
        }

        public string CustomerId { get; set; }
        public string ClientId { get; set; }
        public string CountryId  { get; set; }
        public string  ProvinceId { get; set; }
        public string DistrictId { get; set; }
        public string WardId { get; set; }
        public string StreetId { get; set; }
        public string CountryName { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string WardName { get; set; }
        public string StreetName { get; set; }
        public string Detail { get; set; }
        public string Description { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public int Version { get; }
    }
}