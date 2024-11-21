using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_lab_DB
{
    abstract class DomainObject
    {
        public abstract string NameTable { get; }
        int _id;
        public int Id { get { return _id; } }
        public DomainObject(int id) 
        { 
            if(id > 0)
                _id = id;
        }
        public abstract string GetInfo();
        public abstract string GetTable();
    }
}
