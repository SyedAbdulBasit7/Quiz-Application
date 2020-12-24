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
    public partial class CategoryForm : Form
    {
        private int user_id;
        public CategoryForm(int getId)
        {
            InitializeComponent();
            ResetForm();
            user_id = getId;
        }

        private void select_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                quiz_category ded = (from s in db.quiz_categories
                                     where s.category_name == select_category.Text
                                     select s).FirstOrDefault();
                if (ded == null)
                {
                    Console.WriteLine("Null Value");
                }
                else
                {
                    if (txt_result_id.Text == "")
                    {
                        MessageBox.Show("Please enter Result ID");
                    }
                    else
                    {
                        AttemptQuiz aq = new AttemptQuiz(select_category.Text, ded.category_id, user_id, txt_result_id.Text);
                        aq.Show();
                        this.Hide();
                    }
                   
                }

            }
            catch (Exception ex)
            { }
        }
        private void ResetForm()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var scd = db.quiz_categories.Select(c => new { c.category_name });
            select_category.DataSource = scd.ToList();
            select_category.DisplayMember = "category_name";
        }
    }
}
