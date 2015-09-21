namespace Thepagedot.Rhome.Base.Models
{
    public abstract class CentralUnit
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public CentralUnitBrand Brand { get; set; }

        protected CentralUnit(string name, string address, CentralUnitBrand brand)
        {
            Name = name;
            Address = address;
            Brand = brand;
        }
    }

    public enum CentralUnitBrand
    {
        HomeMatic
    }
}
