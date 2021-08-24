using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pathological_section_diagnosis_assistant_software
{
    public class ImageURL
    {
        public long Id { get; set; }
        public long PatientId { get; set; }
        public string URL { get; set; }
        public string Disease { get; set; }
        public virtual Patient Patient { get; set; }

    }
}
