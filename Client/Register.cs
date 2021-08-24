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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        #region 数据库连接
        private readonly DB_model db = new DB_model();
        #endregion

        #region back
        private void button2_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Dispose();
        }
        #endregion

        ////#region 用户名
        ////private void txtRecharge_Enter(object sender, EventArgs e)
        ////{
        ////    if (textBox1.Text == "no less than 5 characters")
        ////    {
        ////        //true_username_length = 1;
        ////        textBox1.Text = "";
        ////        textBox1.ForeColor = Color.Black;
        ////    }
        ////}
        ////private void txtRecharge_Leave(object sender, EventArgs e)
        ////{           
        ////    if (textBox1.Text == "")
        ////    {
        ////        true_username_length = 0;
        ////        textBox1.Text = "no less than 5 characters";
        ////        //文本框内字体的颜色（灰色）
        ////        textBox1.ForeColor = Color.Gray;
        ////    }
        ////}
        ////#endregion

        ////#region 密码
        ////private void passRecharge_Enter(object sender, EventArgs e)
        ////{
        ////    if (textBox2.Text == "no less than 6 characters")
        ////    {
        ////        //true_pass_length = 1;
        ////        textBox2.Text = "";
        ////        textBox2.ForeColor = Color.Black;
        ////        textBox2.PasswordChar = '*';
        ////    }
        ////}
        ////private void passRecharge_Leave(object sender, EventArgs e)
        ////{
        ////    true_pass_length = 0;
        ////    if (textBox2.Text == "")
        ////    {
                
        ////        textBox2.PasswordChar = new char();
        ////        textBox2.Text = "no less than 6 characters";
        ////        //文本框内字体的颜色（灰色）
        ////        textBox2.ForeColor = Color.Gray;
        ////    }
        ////}
        ////#endregion

        ////#region 重复密码
        ////private void repassRecharge_Enter(object sender, EventArgs e)
        ////{
        ////    if (textBox3.Text == "should be same with password")
        ////    {
        ////        //true_repass_length = 1;
        ////        textBox3.Text = "";
        ////        textBox3.ForeColor = Color.Black;
        ////        textBox3.PasswordChar = '*';
        ////    }
        ////}
        ////private void repassRecharge_Leave(object sender, EventArgs e)
        ////{
        ////    true_repass_length = 0;
        ////    if (textBox3.Text == "")
        ////    {
                
        ////        textBox3.PasswordChar = new char();
        ////        textBox3.Text = "should be same with password";
        ////        //文本框内字体的颜色（灰色）
        ////        textBox3.ForeColor = Color.Gray;
        ////    }
        ////}
        ////#endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }

        #region 输入
        //用户名
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Replace(" ", string.Empty);
            if (textBox1.Text.Length < 5)
            {
                label5.Text = "Username should be no less than 5 chars";
                label5.ForeColor = Color.Red;
            }
            else
            {
                label5.Text = "";
            }
        }

        //密码
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Replace(" ", string.Empty);
            if (textBox2.Text.Length < 6)
            {
                label6.Text = "Password should be no less than 6 chars";
                label6.ForeColor = Color.Red;
            }
            else
            {
                label6.Text = "";
            }
        }

        //重复输入
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Replace(" ", string.Empty);
            if (textBox3.Text != textBox2.Text)
            {
                label7.Text = "Re-Password should be same with Password";
                label7.ForeColor = Color.Red;
            }
            else
            {
                label7.Text = "";
            }
        }
        #endregion

        #region 执行注册
        private void button1_Click(object sender, EventArgs e)
        {
           if(textBox1.Text.Length<5)
           {
                if(textBox1.Text.Length<=0)
                {
                    DialogResult result = MessageBox.Show("Please input Username", "Hint", MessageBoxButtons.OK);
                }
                else
                {
                    DialogResult result = MessageBox.Show("Username should be no less than 5 chars", "Hint", MessageBoxButtons.OK);
                }
           }
           else
           {
                if(textBox2.Text.Length<6)
                {
                    if(textBox2.Text.Length<=0)
                    {
                        DialogResult result = MessageBox.Show("Please input Password", "Hint", MessageBoxButtons.OK);
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Password should be no less than 6 chars", "Hint", MessageBoxButtons.OK);
                    }
                }
                else
                {
                    if (textBox2.Text!=textBox3.Text)
                    {
                        DialogResult result = MessageBox.Show("Repassword should be same with Password", "Hint", MessageBoxButtons.OK);
                    }
                    else
                    {
                        User temp_user = db.Users.FirstOrDefault(s => s.Username == textBox1.Text);
                        if(temp_user!=null)
                        {
                            DialogResult result = MessageBox.Show("This Username has been registered, please try another one", "Hint", MessageBoxButtons.OK);

                        }
                        else
                        {
                            User user = new User()
                            {
                                Username = textBox1.Text,
                                Password = Encrption.MD5Hash(textBox2.Text),
                            };
                            db.Users.Add(user);
                            db.SaveChanges();
                            DialogResult result = MessageBox.Show("Register successfully", "Hint", MessageBoxButtons.OK);
                            Login login = new Login();
                            string username_jump = textBox1.Text;
                            this.Hide();
                            login.textBox1.Text = username_jump;
                            login.ShowDialog();
                            this.Dispose();
                        }
                        
                    }
                }
           }

            
        }
        #endregion
    }
}
