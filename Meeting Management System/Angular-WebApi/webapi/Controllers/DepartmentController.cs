using LoginAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LoginAPI.Controllers
{
    [RoutePrefix("Api/Department")]
    public class DepartmentController : ApiController
    {
        private EmployeeEntities1 objEntity = new EmployeeEntities1();

        [HttpGet]
        [Route("AllDepartments")]
        public IQueryable<Department> GetDepartment()
        {
            try
            {
                return objEntity.Departments;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetDepartmentsById/{DepartmentID}")]
        public IHttpActionResult GetDepartmentById(string DepartmentID)
        {
            Department objDep = new Department();
            int ID = Convert.ToInt32(DepartmentID);
            try
            {
                objDep = objEntity.Departments.Find(ID);
                if (objDep == null)
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }

            return Ok(objDep);
        }

        [HttpPost]
        [Route("InsertDepartments")]
        public IHttpActionResult PostDepartment(Department data)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                objEntity.Departments.Add(data);
                objEntity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(data);
        }

        [HttpPut]
        [Route("UpdateDepartments")]
        public IHttpActionResult PutDepartment (Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Department objDep = new Department();
                objDep = objEntity.Departments.Find(department.DepartmentID);
                if (objDep != null)
                {
                    objDep.DepartmentName = department.DepartmentName;
                }
                int i = this.objEntity.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(department);
        }

        [HttpDelete]
        [Route("DeleteDepartments")]
        public IHttpActionResult DeleteDepartment (int id)
        {
            //int empId = Convert.ToInt32(id);  
            Department department = objEntity.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }

            objEntity.Departments.Remove(department);
            objEntity.SaveChanges();

            return Ok(department);
        }
    }
}
