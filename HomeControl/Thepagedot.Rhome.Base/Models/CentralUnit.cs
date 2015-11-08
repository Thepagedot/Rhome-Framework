using Newtonsoft.Json;

namespace Thepagedot.Rhome.Base.Models
{
    public class CentralUnit
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public CentralUnitBrand Brand { get; set; }

        [JsonConstructor]
        protected CentralUnit(string name, string address, CentralUnitBrand brand)
        {
            Name = name;
            Address = address;
            Brand = brand;
        }
    }

    public enum CentralUnitBrand
    {
        HomeMatic,
        PhilipsHue
    }
}
