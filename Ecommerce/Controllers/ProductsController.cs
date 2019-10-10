using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DA;
using Ecommerce.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        ProductDA _pro = new ProductDA();
        DepartmentDA _dep = new DepartmentDA();

        [HttpGet]
        public ICollection<Product> GetProducts()
        {
            var res = _pro.GetProducts();
            return res;                   
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var res = _pro.GetProductById(id);
            if (res != null)
                return Ok(res);
            else
                return BadRequest(new { message = "Product not found"});
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            product.creationDate = DateTime.Today.Date;
            var res = _pro.AddProduct(product);
            if (res)
            {
                return Ok(new { message = "Product added successfully" });
            }
            else
                return BadRequest(new { message = "Error, can't add product" });
        }

        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            var pro = _pro.GetProductById(product.productId);

            product.creationDate = pro.creationDate;
            product.updationDate = DateTime.Today.Date;

            var res = _pro.UpdateProduct(product);
            if (res)
            {
                return Ok(new { message = "Product updated successfully" });
            }
            else
                return BadRequest(new { message = "Error, can't update product" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var res = _pro.DeleteProduct(id);
            if (res)
                return Ok(new { message = "Product deleted successfully" });
            else
                return BadRequest(new { message = "Error, can't delete product" });
        }

        [HttpGet("search/{query}")]
        public ICollection<Product> SearchProduct(string query)
        {
            var res = _pro.SearchProducts(query);
            return res;
        }

        [HttpGet("bydepartment/{dep}")]
        public IActionResult GetProductsByDepartment(string dep)
        {
            var depId = _dep.GetDepIdByDepName(dep);
            if (depId != -1)
            {
                var res = _pro.GetProductsByDepartment(depId);
                   return Ok(res);  
            }
            else
            {
                return BadRequest(new { message = "Department not found"});
            }
            
        }

        [HttpGet("related/{id}")]
        public IActionResult GetRelatedProducts(int id)
        {
            var depId = _dep.getDepIdByProId(id);
            
                var pro = _pro.GetRelatedProducts(depId, id);
                return Ok(pro);
            
           
        }

    }
}