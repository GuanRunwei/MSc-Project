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
    public partial class PatientInfo : Form
    {
        public PatientInfo()
        {
            InitializeComponent();
        }

        #region 数据库连接
        private readonly DB_model db = new DB_model();
        #endregion

        #region 提交+跳转
        private void button1_Click(object sender, EventArgs e)
        {
            string patient_full_name = "";
            if (textBox3.Text.Length == 0)
            {
                DialogResult result = MessageBox.Show("Please enter Patent Number", "Hint", MessageBoxButtons.OK);
            }
            else
            {
                if (textBox1.Text.Length == 0)
                {
                    DialogResult result = MessageBox.Show("Please enter First Name", "Hint", MessageBoxButtons.OK);
                }
                else
                {
                    if (textBox2.Text.Length == 0)
                    {
                        DialogResult result = MessageBox.Show("Please enter Last Name", "Hint", MessageBoxButtons.OK);
                    }
                    else
                    {
                        Patient old_patient = db.Patients.FirstOrDefault(s => s.P_Number == textBox3.Text);
                        if (old_patient == null)
                        {
                            Patient patient = new Patient()
                            {
                                P_First_Name = Encrption.MD5Hash(textBox1.Text),
                                P_Last_Name = Encrption.MD5Hash(textBox2.Text),
                                P_Number = textBox3.Text
                            };
                            db.Patients.Add(patient);
                            db.SaveChanges();
                            DialogResult result2 = MessageBox.Show("Register Patient Info successfully", "Hint", MessageBoxButtons.OK);
                            Patient trans = db.Patients.FirstOrDefault(s => s.P_Number == textBox3.Text);
                            Upload upload = new Upload(textBox1.Text + " " + textBox2.Text);
                            upload.PatientId = trans.Id;
                            upload.PatientNumber = trans.P_Number;
                            patient_full_name = textBox1.Text + " " + textBox2.Text;
                            this.Hide();
                            upload.label2.Text += patient_full_name;
                            upload.ShowDialog();
                            this.Dispose();
                        }
                        else
                        {
                            DialogResult result2 = MessageBox.Show("The patient has been retrieved", "Hint", MessageBoxButtons.OK);
                            Upload upload = new Upload(textBox1.Text + " " + textBox2.Text);
                            upload.PatientId = old_patient.Id;
                            upload.PatientNumber = old_patient.P_Number;
                            patient_full_name = textBox1.Text + " " + textBox2.Text;
                            this.Hide();
                            upload.label2.Text += patient_full_name;
                            upload.ShowDialog();
                            this.Dispose();
                        }
                }
            }           
            }         
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Dispose();
        }
    }
}
