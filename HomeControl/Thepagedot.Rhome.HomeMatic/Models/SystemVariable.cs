﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class SystemVariable
    {
        public int IseId { get; set; }
        public string Name { get; set; }
        public dynamic Value { get; set; }
        public List<string> ValueList { get; }
        public double? Min { get; set; }
        public double? Max { get; set; }
        public string Unit { get; set; }
        public int Type { get; set; }
        public int SubType { get; set; }
        public bool Visible { get; set; }
        public string TimeStamp { get; set; }
        public string ValueName0 { get; set; }
        public string ValueName1 { get; set; }

        public string ValueString
        {
            get
            {
                try
                {
                    switch (Type)
                    {
                        default:
                            return Value.ToString();
                        case 2:
                            if ((bool)Value)
                                return ValueName1;
                            else
                                return ValueName0;
                        case 16:
                            return ValueList.ElementAt(Convert.ToInt32((string)Value));
                    }
                }
                catch (Exception ex)
                {
                    return Value.ToString();
                }
            }
        }

        public SystemVariable(int iseId, string name, string value, string valueList, string min, string max, string unit, int type, int subtype, bool visible, string timeStamp, string valueName0, string valueName1)
        {
            // Set properties
            IseId = iseId;
            Name = name;
            Unit = unit;
            Type = type;
            SubType = subtype;
            Visible = visible;
            TimeStamp = timeStamp;
            ValueName0 = valueName0;
            ValueName1 = valueName1;

            // Transform Value List
            if (valueList != String.Empty)
            {
                var splitted = valueList.Split(';');
                ValueList = splitted.ToList();
            }

            // Transform min and max
            if (min != String.Empty)
                Min = Convert.ToDouble(min);
            else
                Min = null;
            if (max != String.Empty)
                Max = Convert.ToDouble(max);
            else
                Max = null;

            // Transform value
            try
            {
                switch (type)
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