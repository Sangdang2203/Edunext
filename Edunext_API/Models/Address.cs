namespace Edunext_API.Models
{
    public class Address
    {
        public string Name { get; set; }

        public string DivisionType { get; set; }

        public int Code { get; set; }

        public int? PhoneCode { get; set; }

        public IEnumerable<Address>? Districts { get; set; }

        public IEnumerable<Address>? Wards { get; set; }
    }
}
