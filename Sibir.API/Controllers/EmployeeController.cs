using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Sibir.API.Validators;
using Sibir.Domain.Abstraction;
using System.Diagnostics.Contracts;
using EContracts = Sibir.API.Contracts.Employee;

namespace Sibir.API.Controllers
{
    public class EmployeeController(ICRUREmployeeService employeeService) : Controller
    {
        private readonly ICRUREmployeeService _crudEmployeeService = employeeService;

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody]EContracts.Create.Request request)
        {
            var Result = await _crudEmployeeService.Create(request.Employee);

            if(Result.IsFailure)
                return ResultValidator.Validate(Result);

            return Created(new Uri("https://localhost:7020/api/Project/GetProjectById"),
                new EContracts.Create.Response(Result.Value));
        }

        [HttpPatch]
        public async Task<IActionResult> EditEmployeePersonalData([FromBody] EContracts.EditPerosnalData.Request request)
        {
            var Result  = await _crudEmployeeService.EditEmployeePersonalData(request.Id,request.Name,request.Email);

            if(Result.IsFailure)
                return ResultValidator.Validate(Result);

            return Ok(new EContracts.EditPerosnalData.Resopnse(Result.Value));
        }

        [HttpPatch]
        public async Task<IActionResult> EditEmployeeRole([FromBody] EContracts.EditRole.Request request)
        {
            var Result = await _crudEmployeeService.EditRole(request.Id, request.Role);

            if (Result.IsFailure)
                return ResultValidator.Validate(Result);

            return Ok(new EContracts.EditRole.Resopnse(Result.Value));
        }

        [HttpPost]
        public async Task<IActionResult> GetAllEmployee([FromBody] EContracts.GetAll.Request request)
        {

            var Result = await _crudEmployeeService.GetAllEmployees(request.Page);

            if(Result.IsFailure)
                ResultValidator.Validate(Result);

            return Ok(new EContracts.GetAll.Response(Result.Value.Item1, Result.Value.Item2));
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployeeById([FromBody] EContracts.GetById.Request request)
        {
            var employee = await _crudEmployeeService.GetEmployeeById(request.Id);
            if (employee.IsFailure)
                return ResultValidator.Validate(employee);
            return Ok(new EContracts.GetById.Response(employee.Value));
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployeesByProject([FromBody] EContracts.GetByProject.Request request)
        {
            var employees = await _crudEmployeeService.GetEmployeesByProject(request.ProjectId);
            if(employees.IsFailure)
                return ResultValidator.Validate(employees);
            return Ok(new EContracts.GetByProject.Response(employees.Value));
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredEmployees([FromBody] EContracts.GetFilteredEmployees.Request request)
        {
            var employees = await _crudEmployeeService.GetFilteredEmployee(request.Page, request.Name, request.Role);
            if(employees.IsFailure)
                return ResultValidator.Validate(employees);
            return Ok(new EContracts.GetFilteredEmployees.Response(employees.Value.Item2,employees.Value.Item1));
        }
    }
}
