using System;
using System.Globalization;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class Datapoint
    {
		public DatapointType Type { get; set; }
		public int IseId { get; set; }
		public int ChannelIseId { get; set; }
		public string Value { get; set; }
        public string ValueUnit { get; set; }

        public Datapoint(string type, int iseId, int channelIseId, string value, string valueUnit)
        {
            this.IseId = iseId;
            this.ChannelIseId = channelIseId;
            this.Value = value;
            this.ValueUnit = valueUnit;

			// Try to parse type string to DatapointType.
			// If this fails, datapointType will remain the default value UNKNOWN.
			DatapointType datapointType;
			Enum.TryParse<DatapointType>(type, out datapointType);
			Type = datapointType;
        }

		public override string ToString()
		{
			return ToFormattedString(null);
		}

		public string ToFormattedString(string name = null)
		{
			switch (Type)
			{
				default:
					name = name ?? Type.ToString();
					return String.Format("{0}: {1}{2}", name, Value, ValueUnit);
				case DatapointType.HUMIDITY:
					name = name ?? "Humidity";
					return String.Format("{0}: {1}{2}", name, Value, ValueUnit);
				case DatapointType.TEMPERATURE:
					name = name ?? "Temperature";
                    return String.Format("{0}: {1:N2}{2}", name, double.Parse(Value, CultureInfo.InvariantCulture), ValueUnit);
			}
		}
    }

	public enum DatapointType
	{
		UNKNOWN,
		ERROR,
		HUMIDITY,
		LEVEL,
		LOWBAT,
		SETPOINT,
        SET_TEMPERATURE,
		STATE,
		STOP,
		TEMPERATURE
	}
}
