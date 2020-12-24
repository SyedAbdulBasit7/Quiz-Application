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
    public partial class Result : Form
    {
    
        public Result()
        {
            InitializeComponent();
            showData();


        }
        public void showData()
        {
            DataClasses1DataContext db = new DataClasses1DataContext();
            var query = (from qc in db.Users
                         join qq in db.results on qc.user_id equals qq.user_id
                         select new
                         {
                             qc.username,
                             qq.score,
                             qq.category_id,
                         });
            dataGridView1.DataSource = query.ToList();
        }
    }
}
