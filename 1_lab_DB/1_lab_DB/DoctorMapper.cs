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
        static int column = 6;
        public DoctorMapper()
        {
            Doctor doctor = new Doctor();
            tName = doctor.NameTable;
        }
        protected override DomainObject SelectStmt(string object_id)
        {
            int id = Int32.Parse(object_id.Remove(0, object_id.IndexOf('_') + 1));
            string query = $"SELECT * FROM {tName} WHERE id = {id}";
            string _data = _connection.SelectQuery(query, column);
            Console.Write($"Из таблицы {tName}; id = {id}\n");
            string[] _getData = _data.Split('_');
            _objectsList.Add(object_id, new Doctor(id, _getData[1], _getData[2], _getData[3], _getData[4], _getData[5]));
            return _objectsList.GetObject(object_id);
        }
        protected override int UpdateStmt(DomainObject domainObject)
        {
            string line = domainObject.GetInfo();
            string[] data = line.Split(',');
            string query = $"UPDATE {domainObject.NameTable} SET created_at = {data[0]}, updated_at = {data[1]}, name = {data[2]}, address ={data[3]}, address = {data[4]}, passport_details = {data[5]} WHERE Id = {domainObject.Id}";
            Console.WriteLine(query);
            int k = _connection.UpdateDB(query);
            return k;
        }
        protected override List<int> SelectAll(string table_name)
        {
            string query = $"SELECT id FROM {tName}";
            List<int> _data = _connection.SelectAll(query);
            return _data;
        }
    }
}
