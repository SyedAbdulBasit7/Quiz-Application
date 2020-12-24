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
    public partial class AttemptQuiz : Form
    {
        private int seconds;
        private int minutes;
        private int score;
        private string get_category_id;
        private int cuser_id;
        private string get_result_id;
        private int timerLength;
        public AttemptQuiz(string categoryName,string categoryId,int get_user_id, string get_result)
        {
            InitializeComponent();
            lbl_quiz_category.Text = categoryName+" Quiz";
            string categoryID = categoryId;
            displayQuiz(categoryID);
            get_category_id = categoryId;
            seconds = minutes = score = 0;
            timer1.Start();
            cuser_id = get_user_id;
            get_result_id = get_result;
            DataClasses1DataContext db = new DataClasses1DataContext();
            var query = from pd in db.quiz_questions
                        select pd;
            timerLength = query.Count();


        }
        public void displayQuiz(string categoryId)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            quiz_question ded = (from s in db.quiz_questions
                                 where s.category_id == categoryId
                                 select s).FirstOrDefault();
            if (ded == null)
            {
                Console.WriteLine("Null Value");
            }
            else
            {
                Char[] myChars = { ',', ',', ',', ',' };
                string myChoice = ded.choices;
                string[] splitChoice = myChoice.Split(myChars);
                radioButton1.Text = splitChoice[0];
                radioButton2.Text = splitChoice[1];
                radioButton3.Text = splitChoice[2];
                radioButton4.Text = splitChoice[3];
                var sqd = db.quiz_questions.Select(c => new { c.question });
                comboQuestion.DataSource = sqd.ToList();
                comboQuestion.DisplayMember = "question";
                //string checkValue = "";
                //if (radioButton1.Checked) { checkValue = radioButton1.Text; }
                //if (radioButton2.Checked) { checkValue = radioButton2.Text; }
                //if (radioButton3.Checked) { checkValue = radioButton3.Text; }
                //if (radioButton4.Checked) { checkValue = radioButton4.Text; }
                //if (checkValue == ded.answer)
                //{
                //    score += 10;
                //}
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            seconds++;
            if (seconds > 59)
            {
                minutes++;
                seconds = 0;
            }
            if (minutes == timerLength)
            {
                minutes = 0;
                seconds = 0;
                //displayQuiz(get_category_id);
                timer1.Stop();
                Result r = new Result();
                r.Show();
                this.Hide();
            }
            Minute.Text = Change(minutes);
            Second.Text = Change(seconds);
        }
        private string Change(int value)
        {
            if (value <= 9)
                return "0" + value;
            else
                return value.ToString();
        }

        private void btn_register_user_Click(object sender, EventArgs e)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            quiz_question ded = (from s in db.quiz_questions
                                 where s.question == comboQuestion.Text
                                 select s).FirstOrDefault();            

            //quiz_model qm = new quiz_model();
            //qm.question = ded.question;
            //qm.choices = ded.choices;
            //qm.qId = ded.question_id;
            //qm.catId = ded.category_id;
            //qm.answer = ded.answer;
            //List<quiz_model> arrData = new List<quiz_model>();
            //int record = Int32.Parse(ded.question_id);
            //ded.question_id = record.ToString();
            //if (record > 0)
            //{
           
            string checkValue = "";
            if (radioButton1.Checked) { checkValue = radioButton1.Text; }
            if (radioButton2.Checked) { checkValue = radioButton2.Text; }
            if (radioButton3.Checked) { checkValue = radioButton3.Text; }
            if (radioButton4.Checked) { checkValue = radioButton4.Text; }
            if (checkValue == ded.answer)
            {
                score += 10;
            }
            MessageBox.Show("Your Score is " + score + " select next question from Combo Box");

           



            //    record++;
            //}


        }

        private void comboQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            quiz_question ded = (from s in db.quiz_questions
                                 where s.question == comboQuestion.Text
                                 select s).FirstOrDefault();
            if (ded == null)
            {
                Console.WriteLine("Null value");
            }
            else
            {
                Char[] myChars = { ',', ',', ',', ',' };
                string myChoice = ded.choices;
                string[] splitChoice = myChoice.Split(myChars);
                radioButton1.Text = splitChoice[0];
                radioButton2.Text = splitChoice[1];
                radioButton3.Text = splitChoice[2];
                radioButton4.Text = splitChoice[3];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Result r = new Result();
            r.Show();
            this.Hide();
        }

        private void done_quiz_Click(object sender, EventArgs e)
        {
            try
            {
                DataClasses1DataContext db = new DataClasses1DataContext();
                result addResult = new result()
                {
                    result_id = get_result_id,
                    user_id = cuser_id,
                    score = score,
                    category_id = get_category_id,
                };
                db.results.InsertOnSubmit(addResult);
                db.SubmitChanges();
                done_quiz.Enabled =false;
                timer1.Stop();
                MessageBox.Show("Quiz Score save click on result button");
            }
             catch (Exception ex)
            { }
        }
    }
}
