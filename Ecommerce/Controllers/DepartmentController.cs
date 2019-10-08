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

    }
}