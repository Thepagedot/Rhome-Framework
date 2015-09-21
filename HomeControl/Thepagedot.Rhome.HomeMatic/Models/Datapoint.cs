namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class Datapoint
    {
		public string Type { get; set; }
		public int IseId { get; set; }
		public int ChannelIseId { get; set; }
		public string Value { get; set; }
        public string ValueUnit { get; set; }

        public Datapoint(string type, int iseId, int channelIseId, string value, string valueUnit)
        {
            this.Type = type;
            this.IseId = iseId;
            this.ChannelIseId = channelIseId;
            this.Value = value;
            this.ValueUnit = valueUnit;
        }
    }
}
