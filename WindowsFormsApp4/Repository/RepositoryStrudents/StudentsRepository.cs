using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Web;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp4.Repository.RepositoryStrudents
{
    internal class StudentsRepository : IStudentRepository
    {
        private string ConnString { get; set; }

        public StudentsRepository(string Host, string db, string user, string password)
        {
            ConnString = $"server={Host};uid={user}; password={password}; database={db};";
        }

        public List<Student> GetAll()
        {
            List<Student> students = new List<Student>();
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = $"SELECT * FROM Students.Student;";
            try
            {
                conn.OpenAsync();
                MySqlDataReader reader;
                reader = comm.ExecuteReader();
                while(reader.Read())
                {
                    students.Add(new Student { id=reader.GetInt32(0), name=reader.GetString(1), second_name=reader.GetString(2),age=reader.GetInt32(3), average_score=reader.GetInt32(4)});
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Err:{e.Message}");
            }
            conn.CloseAsync();
            return students;
        }

        public int Insert(Student value)
        {
            int rows = 0;
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = $"INSERT INTO Students.Student(name, second_name, age, average_score) VALUES('{value.name}','{value.second_name}','{value.age}','{value.average_score}');";
            try
            {
                conn.OpenAsync();
                rows =comm.ExecuteNonQuery();
            }
            catch(Exception e)
            {
                MessageBox.Show($"Err:{e.Message}");
            }
            conn.CloseAsync();
            return rows;
        }

        public int Update(int id, Student value)
        {
            int rows = 0;
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = $"UPDATE  Students.Student SET name ='{value.name}', second_name='{value.second_name}', age='{value.age}', average_score='{value.average_score}'WHERE id={id};";
            try
            {
                conn.OpenAsync();
                rows = comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Err:{e.Message}");
            }
            conn.CloseAsync();
            return rows;
        }

        public int Delete(int id)
        {
            int rows = 0;
            MySqlConnection conn = new MySqlConnection(ConnString);
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = $"DELETE FROM Students.Student WHERE id={id};";
            try
            {
                conn.OpenAsync();
                rows = comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Err:{e.Message}");
            }
            conn.CloseAsync();
            return rows;
        }
    }
}
