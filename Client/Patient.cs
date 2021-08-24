using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathological_section_diagnosis_assistant_software
{
    public class Patient
    {
        public Patient()
        {
            this.ImageURLs = ImageURLs;
        }
        public long Id { get; set; }
        public string P_Number { get; set; }
        public string P_First_Name { get; set; }
        public string P_Last_Name { get; set; }

        public List<ImageURL> ImageURLs { get; set; }


    }
}
