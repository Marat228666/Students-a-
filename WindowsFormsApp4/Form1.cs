using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp4.Repository.RepositoryStrudents;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        int UpdId;
        IStudentRepository sr;
        public Form1()
        {
            InitializeComponent();
            sr = new StudentsRepository("localhost", "Students", "root", "root");
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox3.Text) || String.IsNullOrEmpty(textBox4.Text))
            {

                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
            }
            else
            {
                button2.Enabled = true;
                button3.Enabled = true;
                button1.Enabled = true;

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                UpdId = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                label4.Text = $"UpdId={UpdId}";
                if (UpdId > 0)
                {
                    button2.Enabled = true;
                }
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            }
            catch (Exception er)
            {
                MessageBox.Show($"Err: {er.Message}");
            }

        }
        private void RefreshTable()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = sr.GetAll();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                
                RefreshTable();
            }
            catch (Exception er)
            {
                MessageBox.Show($"Err: {er.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int rowsaffected = 0;
                rowsaffected = sr.Insert(new Student { name = textBox1.Text, second_name = textBox2.Text, age = int.Parse(textBox3.Text), average_score = int.Parse(textBox4.Text) });
                MessageBox.Show($"Rows affected: {rowsaffected}");
                RefreshTable(); ;
            }
            catch (Exception er)
            {
                MessageBox.Show($"Err: {er.Message}");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int rowsaffected = 0;
                rowsaffected = sr.Update(UpdId, new Student { name = textBox1.Text, second_name = textBox2.Text, age = int.Parse(textBox3.Text), average_score = int.Parse(textBox4.Text) });
                MessageBox.Show($"Rows affected: {rowsaffected}");
                RefreshTable();
                UpdId = 0;
                button2.Enabled = false;
            }
            catch(Exception er)
            {
                MessageBox.Show($"Err: {er.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int rowsaffected = 0;
                rowsaffected = sr.Delete(UpdId);
                MessageBox.Show($"Rows affected: {rowsaffected}");
                RefreshTable();
            }
            catch (Exception er)
            {
                MessageBox.Show($"Err: {er.Message}");
            }
        }
    }
}
