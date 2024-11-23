using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sibir.BL.Services;
using System.Diagnostics.Contracts;
using PContracts=Sibir.API.Contracts.Project;
using Sibir.API.Validators;
using CSharpFunctionalExtensions;


namespace Sibir.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(CRUDProjectService crudProjectService) : ControllerBase
    {
        readonly CRUDProjectService _crudProjectSevice=crudProjectService;

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] PContracts.Create.Request request)
        {
            var Result = await _crudProjectSevice.CreateProject(request.ProjectCreate);

            if(Result.IsFailure)
                return ResultValidator.Validate(Result.Error);

            return Created(new Uri("https://localhost:7020/api/Project/GetProjectById"), 
                new PContracts.Create.Response (Result.Value));
        }

        [HttpPatch("EditManager")]
        public async Task<IActionResult> EditManager([FromBody] PContracts.EditManager.Request request)
        {
            var Result = await _crudProjectSevice.EditProjectManager(request.ProjectId,request.ManagerId);
            if (Result.IsFailure)
                return ResultValidator.Validate(Result.Error);

            return Ok(new PContracts.EditManager.Response(Result.Value));
        }



        [HttpGet("GetAllProject")]
        public async Task<IActionResult> GetAllProject([FromQuery] PContracts.GetAllProject.Request Page) 
        {
            if (Page.Page < 0)
                return BadRequest("Page number cant be less than zero");

            var Result = await _crudProjectSevice.GetAllProject(Page.Page);
            (var viewModels, var PageCount) = Result.Value;
            return Ok(new PContracts.GetAllProject.Response(viewModels,PageCount));
        }

        [HttpGet("GetProjectById")]
        public async Task<IActionResult> GetProjectById([FromQuery] Guid id)
        {
            var Result = await _crudProjectSevice.GetProjectById(id);

            if (Result.IsFailure)
                return ResultValidator.Validate(Result.Error);

            return Ok(new PContracts.GetProjectById.Response(Result.Value));
        }

        [HttpPatch("UpadeteProjectExecuters")]
        public async Task<IActionResult> UpadeteProjectExecuters([FromBody] PContracts.UpdateExecuters.Request request)
        {
            var Result = await _crudProjectSevice.UpdateProjectExecuters
            (
                request.ProjectId,
                request.EmployeesRemoveId,
                request.EmployeesAddId
            );

            if (Result.IsFailure) 
                return ResultValidator.Validate(Result.Error);

            return Ok(new PContracts.UpdateExecuters.Response(Result.Value));
        }

        [HttpPatch("UpdateProjectPriority")]
        public async Task<IActionResult> UpdateProjectPriority([FromBody] PContracts.UpdatePriority.Request request)
        {
            var Result = await _crudProjectSevice.UpdateProjectPriority(request.ProjectId,request.NewPriority);
            if(Result.IsFailure)
                return ResultValidator.Validate(Result.Error);

            return Ok(new PContracts.UpdatePriority.Response(Result.Value));
        }

        [HttpPut("UpdateProjectData")]
        public async Task<IActionResult> UpdateProjectData([FromBody] PContracts.UpdateProjectData.Request request)
        {
            var Result = await _crudProjectSevice.UpdateProjectData
            (
                request.ProjectId, 
                request.Title, 
                request.Comapny, 
                request.DevelopmentTime
            );

            if (Result.IsFailure)
                return ResultValidator.Validate(Result.Error);
            
            return Ok(new PContracts.UpdateProjectData.Response(Result.Value));
        }

        [HttpPost("GetFilteredProjects")]
        public async Task<IActionResult> GetFilteredProjects(PContracts.GetFilteredProjects.Request request)
        {
            if (request.Page < 0)
                return BadRequest("Page number cant be less than zero");
            if (!((DateOnly.TryParse(request.BeginingOfTimeRange, out var BeginingOfTimeRange) ||
                    request.BeginingOfTimeRange == "") &&
               (DateOnly.TryParse(request.EndOfTimeRange, out var EndOfTimeRange) ||
                    request.EndOfTimeRange == "")))
            {
                return BadRequest("Failur data format");
            }

            var Result = await _crudProjectSevice.GetFilteredProject
            (
                request.Page,
                request.SearchByTitle != "" ? request.SearchByTitle : null,
                request.SearchByCompanyConsumer != "" ? request.SearchByCompanyConsumer : null,
                request.SearchByCompanyExecuter != "" ? request.SearchByCompanyExecuter : null,
                request.BeginingOfTimeRange != "" ? BeginingOfTimeRange : null,
                request.EndOfTimeRange != "" ? EndOfTimeRange : null,
                request.SortBy,
                request.SortingDerection
            );

            if (Result.IsFailure)
                return ResultValidator.Validate(Result.Error);

            return Ok(new PContracts.GetFilteredProjects.Response(Result.Value.Item1, Result.Value.Item2));

        }
    }
}
