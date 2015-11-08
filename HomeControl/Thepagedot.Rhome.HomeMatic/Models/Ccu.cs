using Newtonsoft.Json;
using Thepagedot.Rhome.Base.Models;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class Ccu : CentralUnit
    {
        [JsonConstructor]
        public Ccu(string name, string address) : base (name, address, CentralUnitBrand.HomeMatic)
        {
        }
    }
}
