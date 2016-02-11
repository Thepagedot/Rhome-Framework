using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class SystemVariable
    {
        //TODO: Add value list
        //TODO: Add unit
        public int IseId { get; set; }
        public string Name { get; set; }
        public bool Visible { get; set; }
        public int TypeId { get; set; }
        public string ValueName0 { get; set; }
        public string ValueName1 { get; set; }
        public dynamic Value { get; set; }

        public SystemVariable(int iseId, string name, bool visible, int typeId, string valueName0, string valueName1, dynamic value)
        {
            IseId = iseId;
            Name = name;
            Visible = visible;
            TypeId = typeId;
            ValueName0 = valueName0;
            ValueName1 = valueName1;
            Value = value;

            try
            {
                switch (typeId)
                {
                    default:
                    // Alarm und Logikwert
                    case 2:
                        if (value == String.Empty)
                            value = "false";

                        Value = Convert.ToBoolean(value);
                        break;
                    // Number
                    case 4:
                        Value = Convert.ToDouble(value);
                        break;
                    // Value list
                    case 16:
                        // TODO: Implement Value list!
                        Value = value;
                        break;
                    // String
                    case 20:
                        Value = value;
                        break;
                }
            }
            catch (FormatException fex)
            {
                Value = value;
            }
        }
    }
}
