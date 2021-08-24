using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace Pathological_section_diagnosis_assistant_software
{
    public partial class Upload : Form
    {
        public Upload(string patient_full_name)
        {
            InitializeComponent();
            this.patient_full_name = patient_full_name;
        }
        public string patient_full_name;
        #region 数据库连接
        private readonly DB_model db = new DB_model();
        #endregion

        public long PatientId;
        public string PatientNumber;
        public string file_route;
        public string[] img_type = { "png", "jpg", "jpeg", "bmp", "ico" };

        #region select and show file
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult file_select_dialog = openFileDialog.ShowDialog();
            if (file_select_dialog == DialogResult.OK)
            {
                string file_type = Path.GetExtension(openFileDialog.SafeFileName).ToLower().Substring(1);
                if (file_type == "gif" || file_type == "GIF")
                {
                    DialogResult result = MessageBox.Show("Not support gif picture", "Hint", MessageBoxButtons.OK);
                }
                else
                {
                    if (!img_type.Contains(file_type))
                    {
                        DialogResult result = MessageBox.Show("Please select picture(png, jpg, jpeg, bmp, ico)", "Hint", MessageBoxButtons.OK);
                    }
                    else
                    {
                        if (openFileDialog.FileNames.Length > 1)
                        {
                            DialogResult result = MessageBox.Show("Please choose one picture", "Hint", MessageBoxButtons.OK);
                        }
                        else
                        {
                            file_route = openFileDialog.FileName;
                            Console.WriteLine(file_route);
                            this.pictureBox1.Load(file_route);
                        }
                    }
                }
                
            }

        }
        #endregion

        #region upl
        private void button2_Click(object sender, EventArgs e)
        {
            WebClient micro_service = new WebClient();
            micro_service.Headers.Add("Content-Type", "application/form-data");//注意头部必须是form-data
            Patient patient = db.Patients.FirstOrDefault(s => s.Id == PatientId);
            byte[] responseArray = null;

#if DEBUG
            try
            {
                responseArray = micro_service.UploadFile("http://127.0.0.1:8000/api/get_file", "POST", file_route);
                string get_json_string = System.Text.Encoding.GetEncoding("UTF-8").GetString(responseArray);
                JObject result = JsonConvert.DeserializeObject<JObject>(get_json_string);
                string server_file_path = result["data"].ToString();

                ImageURL imageURL = new ImageURL()
                {
                    Patient = patient,
                    URL = server_file_path,
                    PatientId = PatientId
                };
                db.ImageURLs.Add(imageURL);
                db.SaveChanges();
                Console.WriteLine(server_file_path);

                ImageURL imageURL_jump = db.ImageURLs.FirstOrDefault(s => s.PatientId == PatientId && s.URL == server_file_path);


                Identification identification = new Identification("http://127.0.0.1:8000" + server_file_path.Substring(1), server_file_path, patient_full_name);
                identification.PatientId = PatientId;
                identification.PatientNumber = PatientNumber;
                identification.ImageId = imageURL_jump.Id;
                this.Hide();
                identification.ShowDialog();
                this.Dispose();
            }
            catch(Exception ex)
            {
                DialogResult result2 = MessageBox.Show("Please upload a picture", "Hint", MessageBoxButtons.OK);
            }
            




#else
            byte[] responseArray = micro_service.UploadFile("http://116.62.169.180:8000/api/get_file", "POST", file_route);
            string get_json_string = System.Text.Encoding.GetEncoding("UTF-8").GetString(responseArray);
            JObject result = JsonConvert.DeserializeObject<JObject>(get_json_string);
            string server_file_path = result["data"].ToString();

            ImageURL imageURL = new ImageURL()
            {
                Patient = patient,
                URL = server_file_path,
                PatientId = PatientId
            };
            db.ImageURLs.Add(imageURL);
            db.SaveChanges();
            Console.WriteLine(server_file_path);

            Identification identification = new Identification("http://116.62.169.180:8000" + server_file_path.Substring(1), server_file_path);
            identification.PatientId = PatientId;
            identification.PatientNumber = PatientNumber;
            this.Hide();           
            identification.ShowDialog();
            this.Dispose();

#endif
        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            PatientInfo patientInfo = new PatientInfo();
            this.Hide();
            patientInfo.ShowDialog();
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<ImageURL> imageURLs = db.ImageURLs.Where(s => s.PatientId == PatientId).ToList();
            History history = new History(imageURLs, PatientId,patient_full_name);
            this.Hide();
            history.ShowDialog();
            this.Dispose();
        }
    }
}
