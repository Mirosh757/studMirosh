using Npgsql.PostgresTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_lab_DB
{
    internal class DoctorMapper : Mapper
    {
        static int column = 7;
        public DoctorMapper()
        {
            tName = "doctors";
        }
        protected override DomainObject SelectStmt(string object_id)
        {
            int id = Int32.Parse(object_id.Remove(0, object_id.IndexOf('_') + 1));
            string query = $"SELECT * FROM {tName} WHERE id = {id}";
            string _data = _connection.SelectQuery(query, column);
            Console.Write($"Из таблицы {tName}; id = {id}\n");
            string[] _getData = _data.Split('_');
            _objectsList.Add(object_id, new Doctor(id, StringToDateTime(_getData[1]), StringToDateTime(_getData[2]), _getData[3], _getData[4], _getData[5], StringToDate(_getData[6])));
            return _objectsList.GetObject(object_id);
        }
        protected override int UpdateStmt(DomainObject domainObject)
        {
            string line = domainObject.GetInfo();
            //while(line.IndexOf(',') != -1)
            line = line.Replace(',', '_');
            string[] data = line.Split('_');
            string query = $"UPDATE {domainObject.NameTable} SET created_at = {data[0]}, updated_at = '{DateTime.Now}', name = {data[2]}, address = {data[3]}, passport_details = {data[4]}, date_birth = {data[5]} WHERE Id = {domainObject.Id}";
            Console.WriteLine(query);
            int k = _connection.QueryOnChange(query);
            return k;
        }
        protected override List<int> SelectAll(string table_name)
        {
            string query = $"SELECT id FROM {tName}";
            List<int> _data = _connection.SelectAll(query);
            return _data;
        }
        private DateTime StringToDateTime(string value)
        {
            string[] data = value.Split('.', ' ', ':');
            return new DateTime(Int32.Parse(data[2]), Int32.Parse(data[1]), Int32.Parse(data[0]), Int32.Parse(data[3]), Int32.Parse(data[4]), Int32.Parse(data[5]));
        }
        public DateTime StringToDate(string value)
        {
            string[] data = value.Split('.', ' ', ':');
            return new DateTime(Int32.Parse(data[2]), Int32.Parse(data[1]), Int32.Parse(data[0]));
        }
        public DateTime StringToDateFromDataBase(string value)
        {

            string[] data = value.Split('.', ' ', ':', '-');
            return new DateTime(Int32.Parse(data[0]), Int32.Parse(data[1]), Int32.Parse(data[2]));
        }
    }
}
