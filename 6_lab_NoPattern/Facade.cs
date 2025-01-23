using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace _6_lab_NoPattern
{
    internal class Facade
    {

        NpgsqlConnection _npgsqlConnection = null;
        public Facade()
        {
            _npgsqlConnection = new NpgsqlConnection("Host=127.0.0.1;Username=postgres;Password=5dartyr5;Database=doctors");
        }
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
        private void ViewForHospitals()
        {
            using (DoctorsContext db = new DoctorsContext())
            {
                OpenDBConnection();
                NpgsqlCommand _command = new NpgsqlCommand("SELECT * FROM ViewHospitalAdressesAndPhoneNumber", _npgsqlConnection);
                NpgsqlDataReader reader = _command.ExecuteReader();
                string line = "", helpName  = "", helpAddress = "", helpPhoneNumber = "";
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if(i == 0)
                                helpName = String.Format($"{reader.GetValue(i)}");
                            if(i == 1)
                                helpAddress = String.Format($"{reader.GetValue(i)}");
                            if(i == 2)
                                helpPhoneNumber = String.Format($"{reader.GetValue(i)}");
                        }
                        if (helpName.Length > 70)
                        {
                            helpName = helpName.Substring(0, 67);
                            helpName += "...";
                        }
                        else
                        {
                            for (int i = helpName.Length; i < 70; i++)
                                helpName += " ";
                        }
                        if (helpAddress.Length > 70)
                        {
                            helpAddress = helpAddress.Substring(0, 67);
                            helpAddress += "...";
                        }
                        else
                        {
                            for (int i = helpAddress.Length; i < 70; i++)
                                helpAddress += " ";
                        }
                        line += helpName + "|" + helpAddress + "|" + helpPhoneNumber + "\n";
                    }
                }
                Console.WriteLine(line);
                
                CloseDBConnection();

            }
            PrintMenu();
        }
        private void ViewForDoctors()
        {
            using (DoctorsContext db = new DoctorsContext())
            {
                OpenDBConnection();
                NpgsqlCommand _command = new NpgsqlCommand("SELECT * FROM ViewForDoctors", _npgsqlConnection);
                NpgsqlDataReader reader = _command.ExecuteReader();
                string line = "", helpName = "", helpSpeciality = "", helpSpecialityCount = "";
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            if (i == 0)
                                helpName = String.Format($"{reader.GetValue(i)}");
                            if (i == 1)
                                helpSpeciality = String.Format($"{reader.GetValue(i)}");
                            if (i == 2)
                                helpSpecialityCount = String.Format($"{reader.GetValue(i)}");
                        }
                        if (helpName.Length > 60)
                        {
                            helpName = helpName.Substring(0, 57);
                            helpName += "...";
                        }
                        else
                        {
                            for (int i = helpName.Length; i < 60; i++)
                                helpName += " ";
                        }
                        if (helpSpeciality.Length > 23)
                        {
                            helpSpeciality = helpSpeciality.Substring(0, 20);
                            helpSpeciality += "...";
                        }
                        else
                        {
                            for (int i = helpSpeciality.Length; i < 23; i++)
                                helpSpeciality += " ";
                        }
                        line += helpName + "|" + helpSpeciality + "|" + helpSpecialityCount + "\n";
                    }
                }
                Console.WriteLine(line);

                CloseDBConnection();
            }
            PrintMenu();
        }
        public void PrintMenu()
        {
            Console.WriteLine("0.    Просмотр адрес и телефонный номер больниц\n1.    Просмотр статистику по специальностям докторов");
            string line = Console.ReadLine();
            if (line.Length != 1 && (line != "1" || line != "0"))
            {
                Console.WriteLine("Неверная команда");
                PrintMenu();
            }
            else
            {
                switch (line)
                {
                    case "0": ViewForHospitals(); PrintMenu(); break;
                    case "1": ViewForDoctors(); PrintMenu(); break;
                }
            }
        }
    }
}
