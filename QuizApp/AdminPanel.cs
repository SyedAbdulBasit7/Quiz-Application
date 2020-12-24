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
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
            ResetForm();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            string choices= txt_option1.Text+","+ txt_option2.Text+"," + txt_option3.Text + "," + txt_option4.Text+",";
            DataClasses1DataContext db = new DataClasses1DataContext();
            quiz_question addQuestion = new quiz_question()
            {
                question_id = txtq_id.Text,
                category_id = txt_c_id.Text,
                question= txt_question.Text,
                choices=choices,
                answer=txt_answer.Text,
            };
            db.quiz_questions.InsertOnSubmit(addQuestion);
            db.SubmitChanges();
            txtq_id.Text = "";
            txt_c_id.Text = "";
            txt_question.Text = "";
            txt_option1.Text = txt_option2.Text = txt_option3.Text = txt_option4.Text = "";
            txt_answer.Text = "";
            MessageBox.Show("Question Added Suucessfully!");
            ResetForm();
        }

        private void btn_category_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            quiz_category addCategory = new quiz_category()
            {
                category_id = txt_category_id.Text,
                category_name = txt_category.Text,
            };
            db.quiz_categories.InsertOnSubmit(addCategory);
            db.SubmitChanges();
            txt_category_id.Text = "";
            txt_category.Text = "";
            MessageBox.Show("Category Added Suucessfully!");
            ResetForm();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                quiz_question upd = (from s in db.quiz_questions
                                     where s.category_id == txt_update_categoryId.Text
                                     select s).FirstOrDefault();
                string choices = txt_c1update.Text + ',' + txt_c2update.Text + ',' + txt_c3update.Text + ',' + txt_c4update.Text + ',';
                upd.choices = choices;
                upd.answer = txt_ansupdate.Text;
                upd.category_id = txt_update_categoryId.Text;
                db.SubmitChanges();
                MessageBox.Show("Record Updated Successfully!");
                ResetForm();
                txt_c1update.Text = txt_c2update.Text =txt_c3update.Text =txt_c4update.Text ="";
                txt_ansupdate.Text = "";
                txt_update_categoryId.Text = "";
                comboUpdate_Category.Text = "";
                comboQuestion.Text = "";

            }
            catch (Exception ex)
            { }
        }

        private void comboUpdate_Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                quiz_category ded = (from s in db.quiz_categories
                                     where s.category_name == comboUpdate_Category.Text
                                 select s).FirstOrDefault();
                if (ded == null)
                {
                    Console.WriteLine("Null Value");
                }
                else
                {
                    txt_update_categoryId.Text = ded.category_id;

                }
              
            }
            catch (Exception ex)
            { }
        }
        private void comboQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                quiz_question ded = (from s in db.quiz_questions
                                     where s.question == comboQuestion.Text
                                     select s).FirstOrDefault();
                if (ded == null)
                {
                    Console.WriteLine("Null Value");
                }
                else
                {
                    Char[] myChars = { ',', ',', ',' ,','};
                    string myChoice = ded.choices;
                    string[] splitChoice = myChoice.Split(myChars);
                    txt_c1update.Text = splitChoice[0];
                    txt_c2update.Text = splitChoice[1];
                    txt_c3update.Text = splitChoice[2];
                    txt_c4update.Text = splitChoice[3];
                    txt_ansupdate.Text = ded.answer;
                }
            }
            catch (Exception ex)
            { }
          
        }
        private void ResetForm()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var query = (from qc in db.quiz_categories
                         join qq in db.quiz_questions on qc.category_id equals qq.category_id
                         select new
                         {
                             qq.question,
                             qq.answer,
                             qq.choices,
                             qc.category_name,
                         });
            dataGridView1.DataSource = query.ToList();

            var scd = db.quiz_categories.Select(c => new { c.category_name});
            comboUpdate_Category.DataSource = scd.ToList();
            comboUpdate_Category.DisplayMember = "category_name";
            combo_Delete_cat.DataSource = scd.ToList();
            combo_Delete_cat.DisplayMember = "category_name";
            var sqd = db.quiz_questions.Select(c => new { c.question });
            comboQuestion.DataSource = sqd.ToList();
            comboQuestion.DisplayMember = "question";
            combo_delet_ques.DataSource = sqd.ToList();
            combo_delet_ques.DisplayMember = "question";
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {

            try
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                quiz_category spd = (from s in db.quiz_categories
                                     where s.category_name == combo_Delete_cat.Text
                                     select s).FirstOrDefault();
                quiz_question ded = (from s in db.quiz_questions
                                     where s.question == combo_delet_ques.Text
                                     select s).FirstOrDefault();

                db.quiz_categories.DeleteOnSubmit(spd);
                db.quiz_questions.DeleteOnSubmit(ded);
                db.SubmitChanges();
                MessageBox.Show("Record Deleted Successfully!");
                ResetForm();
            }
            catch (Exception ex)
            { }
        }
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                quiz_question ded = (from s in db.quiz_questions
                                     where s.question == combo_delet_ques.Text
                                     select s).FirstOrDefault();
                db.quiz_questions.DeleteOnSubmit(ded);
                db.SubmitChanges();
                MessageBox.Show("Record Deleted Successfully!");
                ResetForm();
            }
            catch (Exception ex)
            { }
        }

        private void combo_Delete_cat_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            quiz_category ded = (from s in db.quiz_categories
                                 where s.category_name == combo_Delete_cat.Text
                                 select s).FirstOrDefault();
        }
      
        private void combo_delet_ques_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            quiz_question ded = (from s in db.quiz_questions
                                 where s.question == combo_delet_ques.Text
                                 select s).FirstOrDefault();
        }

       
    }
}
