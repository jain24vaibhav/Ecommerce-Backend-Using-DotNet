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
            var dep = _db.departments.Where(x => x.departmentId == id).FirstOrDefault();
           
            try
            {
                _db.Remove(dep);
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

        public int GetDepIdByDepName (string depName)
        {
            try
            {
                var res = _db.departments.Where(x => x.departmentName == depName).Select(x => x.departmentId).FirstOrDefault();
                return res;
            }
            catch
            {
                return -1;
            }
        }

        public int getDepIdByProId (int id)
        {
            var depId = _db.products.Where(x => x.productId == id).Select(x => x.departmentId).FirstOrDefault();
            return depId;
        }

    }
}
