using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR5_2
{
    public partial class Form1 : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Books.mdb;";
        private OleDbConnection myConnection;

        private void makeSelectFromDB(string query)
        {
            OleDbCommand command = new OleDbCommand(query, myConnection);
            OleDbDataReader reader = command.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0].ToString() + " " + reader[1].ToString() + " "
                    + reader[2].ToString() + " " + reader[3].ToString());
            }
            reader.Close();
        }

        public Form1()
        {
            InitializeComponent();

            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
        }

        private void Select_Click(object sender, EventArgs e)
        {
            string element = listBox1.SelectedItem.ToString();
            element = element.Substring(element.IndexOf(' '));
            textBox1.Text = element;
            string query = "SELECT book_id, title, author, genre FROM Books ORDER BY book_id";
            makeSelectFromDB(query);
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            string query = "SELECT book_id, title, author, genre FROM Books ORDER BY book_id";
            makeSelectFromDB(query);
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            string element = textBox1.Text.Trim();

            string[] words = element.Split(new char[] { ' ' });
            string Title = words[0];
            string Author = words[1];
            string Genre = words[2];

            string query = "INSERT INTO Books (title, author, genre) VALUES ('" + Title + "', '" + Author + "', '" + Genre + "')";

            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();

            string query1 = "SELECT book_id, title, author, genre FROM Books ORDER BY book_id";
            makeSelectFromDB(query1);
        }

        private void Update_Click(object sender, EventArgs e)
        {
            string element = listBox1.SelectedItem.ToString();
            string id = element.Remove(element.IndexOf(" "));

            element = textBox1.Text.Trim();

            string[] words = element.Split(new char[] { ' ' });
            string Title = words[0];
            string Author = words[1];
            string Genre = words[2];

            string query = "UPDATE Books SET title = '" + Title + "', author = '" + Author + "', genre = '" + Genre + "' WHERE book_id = " + id;

            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();

            string query1 = "SELECT book_id, title, author, genre FROM Books ORDER BY book_id";
            makeSelectFromDB(query1);
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            string element = listBox1.SelectedItem.ToString();
            element = element.Remove(element.IndexOf(' '));
            string query = "DELETE FROM Books WHERE book_id = " + element;
            OleDbCommand command = new OleDbCommand(query, myConnection);
            command.ExecuteNonQuery();
            string query1 = "SELECT book_id, title, author, genre FROM Books ORDER BY book_id";
            makeSelectFromDB(query1);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            myConnection.Close();
        }
    }
}
