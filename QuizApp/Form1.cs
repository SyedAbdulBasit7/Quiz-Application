using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizApp
{
    public partial class Form1 : Form
    {
        private int user_id;
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_signup_Click(object sender, EventArgs e)
        {
            SignUp sp = new SignUp();
            sp.Show();
            this.Hide();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txt_name.Text == "" || txt_password.Text == "")
            {
                errorProvider1.SetError(txt_name, "Name is Missing");
                errorProvider1.SetError(txt_password, "Password is Missing");
            }
            else
            {
                try
                {
                    if (IsvalidUser(txt_name.Text,txt_password.Text))
                    {
                        errorProvider1.Clear();
                        if (txt_name.Text == "Admin")
                        {
                            AdminPanel ap = new AdminPanel();
                            ap.Show();
                            this.Hide();
                        }
                        else
                        {
                            CategoryForm cf = new CategoryForm(user_id);
                            cf.Show();
                            this.Hide();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (!IsvalidUser(txt_name.Text, txt_password.Text))
                {
                    MessageBox.Show("InValid UserName OR Password");
                }
            }
        }
        public bool IsvalidUser(string userName, string password)
        {
            DataClasses1DataContext context = new DataClasses1DataContext();
            var q = from p in context.Users
                    where p.username == userName
                    && p.password == password
                    select p;
            User ded = (from s in context.Users
                                 where s.username == userName
                                 select s).FirstOrDefault();
            user_id = ded.user_id;
            if (q.Any())
                return true;
            else
                return false;
        }
    }
}
