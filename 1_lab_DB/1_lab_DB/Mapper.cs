using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_lab_DB
{
    abstract class Mapper
    {
        protected static DBConnection _connection = null;
        protected ObjectWatcher _objectsList = ObjectWatcher.Instance;
        protected string tName = null;
        public Mapper()
        {
            if(_connection == null)
                _connection = new DBConnection();
        }
        public DomainObject Find(int id)
        {
            if(tName != null)
            {
                if (_objectsList.GetObject(tName + '_' + id) == null)
                    return SelectStmt(tName + '_' + id);
                return _objectsList.GetObject(tName + '_' + id);
            }
            throw new Exception("Не задано имя таблицы");
        }
        public List<Doctor> FindAll()
        {
            List<Doctor> domainObjects = new List<Doctor>();
            if (tName != null)
            {
                List<int> domainObjectsId = SelectAll(tName);
                for (int i = 0; i < domainObjectsId.Count; i++)
                {
                    if (_objectsList.GetObject(tName + '_' + domainObjectsId[i]) == null)
                        domainObjects.Add((Doctor)SelectStmt(tName + '_' + domainObjectsId[i]));
                    else
                        domainObjects.Add((Doctor)_objectsList.GetObject(tName + '_' + domainObjectsId[i]));
                }
            }
            return domainObjects;
        }
        public int Delete(int id)
        {
            string query = $"DELETE FROM {tName} WHERE id = {id}";
            return _connection.QueryOnChange(query);
        }
        public int DeleteMany(int[] idTable) 
        {
            for(int i = 0;i < idTable.Length;i++)
                Delete(idTable[i]);
            return idTable.Length;
        }
        public int Insert(DomainObject domainObject)
        {
            return InsertStmt(domainObject);
        }
        public int Update(DomainObject domainObject)
        {
            return UpdateStmt(domainObject);
        }
        protected abstract DomainObject SelectStmt(string object_id);
        protected abstract List<int> SelectAll(string table_name);
        protected int InsertStmt(DomainObject domainObject)
        {
            string query = $"INSERT INTO {domainObject.GetTable()} VALUES({domainObject.GetInfoForInsertOrUpdate()})";
            //query = "INSERT INTO TableComplexNumber(RealNumber, ImiginaryNumber, TableTypeNumber_Id) VALUES(4, 5, 3)";
            int k = _connection.QueryOnChange(query);
            return k;
        }
        protected abstract int UpdateStmt(DomainObject domainObject);
    }
}
