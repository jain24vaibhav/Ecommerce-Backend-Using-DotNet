using Ecommerce.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecommerce.DA
{
    public class ProductDA
    {
        EcommerceContext _db = new EcommerceContext();

        public ProductDA()
        {

        }

        public ICollection<Product> GetProducts()
        {
            var res = _db.products.ToArray();
            return res;
        }

        public Product GetProductById(int id)
        {
            var res = _db.products.Where(x => x.productId == id).AsNoTracking().FirstOrDefault();
            return res;
        }

        public bool AddProduct(Product product)
        {
            _db.products.Add(product);
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

        public bool DeleteProduct(int id)
        {
            var pro = _db.products.Where(x => x.productId == id).FirstOrDefault();
            
            try
            {
                _db.Remove(pro);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateProduct(Product product)
        {

            _db.Entry(product).State = EntityState.Modified;
            
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

        public ICollection<Product> SearchProducts(string query)
        {
            var res = _db.products.Where(x => x.productName.Contains(query)).ToArray();
            return res;
        }

        public ICollection<Product> GetProductsByDepartment(int depId)
        {
            var res = _db.products.Where(x => x.departmentId == depId).ToArray();
            return res;
        }

        public ICollection<Product> GetRelatedProducts(int depid, int proId)
        {
            var res = _db.products.Where(x => x.departmentId == depid && x.productId != proId).Take(6).ToArray();
            return res;
        }
    }
}
