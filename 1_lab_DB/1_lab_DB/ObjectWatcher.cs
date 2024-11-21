using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_lab_DB
{
    internal class ObjectWatcher
    {
        private static ObjectWatcher _instance = new ObjectWatcher();
        private ObjectWatcher() { }
        public static ObjectWatcher Instance { get { return _instance; } }
        private Dictionary<string, DomainObject> _objectMap = new Dictionary<string, DomainObject>();
        public DomainObject GetObject(string object_id)
        {
            if (_objectMap.ContainsKey(object_id))
            {
                Console.WriteLine("Подгружаем объект");
                return _objectMap[object_id];
            }
            else
            {
                Console.WriteLine("Нет такого объекта");
                return null;
            }
        }
        public void Add(string object_id, DomainObject obj)
        {
            _objectMap[object_id] = obj;
        }
    }
}
