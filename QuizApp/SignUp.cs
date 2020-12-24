using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;

namespace QuizApp
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        public void RegExp(string re, TextBox tb, ErrorProvider er)
        {
            Regex regex = new Regex(re);
            if (regex.IsMatch(tb.Text)){
                er.Clear();
            }
            else
            {
                er.SetError(tb, "Invalid Input");
            }
        }

        private void btn_register_user_Click(object sender, EventArgs e)
        {
            if (txt_name.Text == "" || txt_password.Text == "" ||
              txt_email.Text == "" || txt_confirmPass.Text == "")
            {
                RegExp(@"^[a-zA-Z0-9]{8,}$", txt_name, errorProvider1);
                RegExp(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", txt_password, errorProvider1);
                RegExp(@"^[\w_\.]+@[a-z]{5,7}\.[a-z]{2,3}$", txt_email, errorProvider1);
            }
            else
            {
                if (txt_password.Text == txt_confirmPass.Text)
                {
                    try
                    {
                        DataClasses1DataContext db = new DataClasses1DataContext();
                        User newUser = new User()
                        {
                            username = txt_name.Text,
                            password = txt_password.Text,
                            email = txt_email.Text,
                        };
                        db.Users.InsertOnSubmit(newUser);
                        db.SubmitChanges();
                        txt_name.Text = "";
                        txt_password.Text = "";
                        txt_confirmPass.Text = "";
                        txt_email.Text = "";
                        MessageBox.Show("User Added Suucessfully!");
                        Form1 f1 = new Form1();
                        f1.Show();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Password Not Same");
                }
            }
        }
    }
}
