using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace _1_lab_No_Pattern
{
    internal class Dbconnection
    {
        NpgsqlConnection _npgsqlConnection = null;
        private void OpenDBConnection()
        {
            if (_npgsqlConnection.State == System.Data.ConnectionState.Closed)
                _npgsqlConnection.Open();
        }
        private void CloseDBConnection()
        {
            if (_npgsqlConnection.State == System.Data.ConnectionState.Open)
                _npgsqlConnection.Close();
        }
        public Dbconnection(string stringConnection)
        {
            _npgsqlConnection = new NpgsqlConnection(stringConnection);
        }
        public Dbconnection()
        {
            _npgsqlConnection = new NpgsqlConnection("Host=127.0.0.1;Username=postgres;Password=5dartyr5;Database=doctors");
        }
        public List<int> SelectAll(string query)
        {
            OpenDBConnection();
            NpgsqlCommand _command = new NpgsqlCommand($"{query}", _npgsqlConnection);
            //Console.WriteLine(query);
            NpgsqlDataReader reader = _command.ExecuteReader();
            List<int> line = new List<int>();
            if (reader.HasRows)
            {
                int i = 0;
                while (reader.Read()) // построчно считываем данные
                {
                    object id = reader.GetValue(0);
                    line.Add((Int32.Parse(id.ToString())));
                }
            }
            CloseDBConnection();
            return line;
        }
        public string SelectQuery(string query, int column)
        {
            OpenDBConnection();
            string line = "";
            NpgsqlCommand _command = new NpgsqlCommand(query, _npgsqlConnection);
            NpgsqlDataReader reader = _command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    for (int i = 0; i < column; i++)
                    {
                        line += String.Format($"{reader.GetValue(i)}_");
                    }
                }
            }
            CloseDBConnection();
            return line;
        }
        public int InsertQuery(string query)
        {
            OpenDBConnection();
            NpgsqlCommand _command = new NpgsqlCommand(query, _npgsqlConnection);
            int k = Int32.Parse((_command.ExecuteScalar().ToString()));
            CloseDBConnection();
            return k;
        }
        public int UpdateOrDeleteQuery(string query)
        {
            OpenDBConnection();
            NpgsqlCommand _command = new NpgsqlCommand(query, _npgsqlConnection);
            int k = Int32.Parse((_command.ExecuteNonQuery().ToString()));
            CloseDBConnection();
            return k;
        }
    }
}
