using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pathological_section_diagnosis_assistant_software
{
    public partial class History : Form
    {
        #region 数据库连接
        private readonly DB_model db = new DB_model();
        #endregion
        public string patient_full_name;
        List<ImageURL> imageURLs = new List<ImageURL>();
        Patient patient = new Patient();
        public History(List<ImageURL> URLs, long PatientId, string patient_full_name)
        {
            InitializeComponent();
            this.imageURLs = URLs;
            this.patient_full_name = patient_full_name;
            patient = db.Patients.FirstOrDefault(s => s.Id == PatientId);
            for(int i=0;i<imageURLs.Count;i++)
            {
                imageURLs[i].URL = "http://127.0.0.1:8000" + imageURLs[i].URL.Substring(1);
                imageList1.Images.Add(new Bitmap((new System.Net.WebClient()).OpenRead(imageURLs[i].URL)));
                listView1.Items.Add(imageURLs[i].Disease);
                listView1.Items[i].ImageIndex = i;
                listView1.Items[i].Name = imageURLs[i].Disease;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Upload upload = new Upload(patient_full_name);
            upload.PatientId = patient.Id;
            upload.PatientNumber = patient.P_Number;
            this.Hide();
            upload.label2.Text += patient_full_name;
            upload.ShowDialog();
            this.Dispose();
        }
    }
}
