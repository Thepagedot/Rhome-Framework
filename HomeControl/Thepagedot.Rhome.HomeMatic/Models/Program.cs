using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thepagedot.Rhome.HomeMatic.Services;

namespace Thepagedot.Rhome.HomeMatic.Models
{
    public class Program
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string TimeStamp { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Visible { get; set; }
        public bool Operate { get; set; }

        private HomeMaticXmlApi _HomeMaticXmlApi;

        public Program(int id, bool active, string timeStamp, string name, string description, bool visible, bool operate, HomeMaticXmlApi homeMaticXmlApi)
        {
            Id = id;
            Active = active;
            TimeStamp = timeStamp;
            Name = name;
            Description = description;
            Visible = visible;
            Operate = operate;

            _HomeMaticXmlApi = homeMaticXmlApi;
        }

        public async Task RunAsync()
        {
            await _HomeMaticXmlApi.RunProgramAsync(Id);
        }
    }
}
