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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        #region 数据库连接
        private readonly DB_model db = new DB_model();
        #endregion

        #region 登录
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length<=0)
            {
                DialogResult result = MessageBox.Show("Please enter Username", "Hint", MessageBoxButtons.OK);
            }
            else
            {
                if(textBox1.Text.Length < 5)
                {
                    DialogResult result = MessageBox.Show("Username should be no more than 5 chars", "Hint", MessageBoxButtons.OK);
                }
                else
                {
                    if (textBox2.Text.Length <= 0)
                    {
                        DialogResult result = MessageBox.Show("Please enter Password", "Hint", MessageBoxButtons.OK);
                    }
                    else
                    {
                        if (textBox2.Text.Length < 6)
                        {
                            DialogResult result = MessageBox.Show("Password should be no more than 6 chars", "Hint", MessageBoxButtons.OK);
                        }
                        else
                        {
                            User user = db.Users.FirstOrDefault(s => s.Username == textBox1.Text);
                            if(user==null)
                            {
                                DialogResult result = MessageBox.Show("User doesn't exit", "Hint", MessageBoxButtons.OK);
                            }
                            else
                            {
                                if(user.Password!= Encrption.MD5Hash(textBox2.Text))
                                {
                                    DialogResult result = MessageBox.Show("Incorrect Password!", "Hint", MessageBoxButtons.OK);
                                }
                                else
                                {
                                    PatientInfo navigation = new PatientInfo();
                                    this.Hide();
                                    navigation.ShowDialog();
                                    this.Dispose();
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 注册
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register register = new Register();
            this.Hide();
            register.ShowDialog();
            this.Dispose();
        }
        #endregion

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Replace(" ", string.Empty);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.Text = textBox2.Text.Replace(" ", string.Empty);
        }
    }
}
