using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_lab_DB
{
    internal class Doctor : DomainObject
    {
        public override string NameTable => "doctors";
        public string _created_at;
        public string _updated_at;
        public string _name;
        public string _address;
        public string _passport_details;
        public Doctor(int id, string created_at, string updated_at, string name, string address, string passport_details) : base(id)
        {
            _created_at = created_at;
            _updated_at = updated_at;
            _name = name;
            _address = address;
            _passport_details = passport_details;
        }
        public Doctor(string created_at, string updated_at, string name, string address, string passport_details) : base(0)
        {
            _created_at = created_at;
            _updated_at = updated_at;
            _name = name;
            _address = address;
            _passport_details = passport_details;
        }
        public Doctor() : base(0) { }
        public override string GetInfo()
        {
            return $"{_created_at}, {_updated_at}, {_name}, {_address}, {_passport_details}";
        }
        public override string GetTable() 
        {
            return "doctors(created_at, updated_at, name, address, passport_details)";
        }
    }
}
