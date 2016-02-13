namespace Thepagedot.Rhome.HomeMatic.Misc
{
    public static class RoomNameResolver
    {
        public static string Resolve(string name)
        {
            if (!name[0].Equals('$'))
                return name;

            switch (name)
            {
                case "${roomBedroom}": return "Bedroom";
                case "${roomGarage}": return "Garage";
                case "${roomHWR}": return "HWR";
                case "${roomKitchen}": return "Kitchen";
                case "${roomLivingRoom}": return "Living Room";
                case "${roomOffice}": return "Office";
                case "${roomBathroom}": return "Bathroom";
            }

            return name;
        }
    }
}
