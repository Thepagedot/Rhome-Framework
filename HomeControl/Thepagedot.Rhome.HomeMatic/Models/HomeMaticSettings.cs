namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class HomeMaticSettings
    {
        public string Address { get; set; }

        public HomeMaticSettings(string address)
        {
            Address = address;
        }
    }
}
