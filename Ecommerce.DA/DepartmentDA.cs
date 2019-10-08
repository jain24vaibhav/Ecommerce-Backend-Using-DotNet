using Ecommerce.models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DA
{
    public class DepartmentDA
    {
        EcommerceContext _db = new EcommerceContext();

        public DepartmentDA()
        {

        }

        public ICollection<Department> GetDepartments()
        {
            var res = (from dep in _db.departments select dep).ToArray();
            return res;
        }

        public bool AddDepartment(Department departments)
        {
            _db.Add(departments);
            try
            {
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteDepartment(int id)
        {
            var dep = _db.departments.Where(x => x.departmentId == id);
            _db.Remove(dep);
            try
            {
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateDepartment(Department department)
        {
            _db.Entry(department).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
