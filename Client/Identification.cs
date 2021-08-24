using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pathological_section_diagnosis_assistant_software
{
    public partial class Identification : Form
    {
        public Identification(string img_path, string server_image_path, string patient_full_name)
        {
            InitializeComponent();
            this.pictureBox1.Load(img_path);
            this.server_image_path = server_image_path;
            this.patient_full_name = patient_full_name;
        }

        #region 数据库连接
        private readonly DB_model db = new DB_model();
        #endregion

        public string server_image_path;

        public long PatientId;
        public string PatientNumber;
        public long ImageId;
        public string patient_full_name;

        private void button1_Click(object sender, EventArgs e)
        {
            JObject result = null;
            ImageURL imageURL = db.ImageURLs.FirstOrDefault(s => s.Id == ImageId);
#if DEBUG
            string url = "http://127.0.0.1:8000/api/get_model_result?image_path=" + this.server_image_path;
#else
            string url = "http://116.62.169.180:8000/api/get_model_result?image_path=" + this.server_image_path;
#endif

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            result = JsonConvert.DeserializeObject<JObject>(retString);
            if(result["data"].ToString() == "lung_n")
            {
                label1.Text = "Result: This lung tissue is normal.";
                imageURL.Disease = "normal lung tissue";
            }
            if (result["data"].ToString() == "lung_aca")
            {
                label1.Text = "Result: This lung tissue is lung adenocarcinoma.";
                imageURL.Disease = "lung adenocarcinomae";
            }
            if (result["data"].ToString() == "lung_scc")
            {
                label1.Text = "Result: This lung tissue is lung squamous cell carcinoma.";
                imageURL.Disease = "lung squamous cell carcinoma";
            }
            try
            {
                db.Entry(imageURL).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                db.Entry(imageURL).State = EntityState.Unchanged;         
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Upload upload = new Upload(patient_full_name);
            upload.PatientId = PatientId;
            upload.PatientNumber = PatientNumber;
            Patient patient = db.Patients.FirstOrDefault(s => s.Id == PatientId);
            upload.label2.Text += patient_full_name;
            this.Hide();
            upload.ShowDialog();
            this.Dispose();
        }

        private void Identification_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
