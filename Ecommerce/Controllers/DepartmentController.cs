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
    public class DepartmentController : ControllerBase
    {
        EcommerceContext _db = new EcommerceContext();
        DepartmentDA _dep = new DepartmentDA();

        [HttpGet]
        public IActionResult GetDepartments()
        {
            var res = _dep.GetDepartments();
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            var res = _dep.DeleteDepartment(id);
            if (res)
                return Ok(new { message = "Department deleted successfully" });
            else
                return BadRequest(new { message = "Error, can't delete department." });
        }

        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            var res = _dep.AddDepartment(department);
            if (res)
                return Ok( new { message = "Department added successfully"} );
            else
                return BadRequest(new { message = "Error, can't add department." });
        }

        [HttpPut]
        public IActionResult UpdateDepartment(Department department)
        {
            var res = _dep.UpdateDepartment(department);
            if (res)
                return Ok(new { message = "Department updated successfully" });
            else
                return BadRequest(new { message = "Error, can't update department." });
        }

    }
}