using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sibir.API.Validators;
using Sibir.BL.Services;
using System.Runtime.InteropServices;
using TContracts = Sibir.API.Contracts.Task;

namespace Sibir.API.Controllers
{
    public class TaskController(CRUDTaskService crudTaskService) : Controller
    {
        private readonly CRUDTaskService _crudTaskService = crudTaskService;

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody]TContracts.Create.Request request)
        {
            var result = await _crudTaskService.CreateTask(request.Task);
            if(result.IsFailure)
                ResultValidator.Validate(result);

            return Created(new Uri("https://localhost:7020/api/Task/CreateTask"),
                new TContracts.Create.Response(result.Value));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTask([FromBody] TContracts.Delete.Request request)
        {
            var result = await _crudTaskService.DeleteTask(request.Id);

            if( result.IsFailure )
                ResultValidator.Validate(result);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> GetTaskById([FromBody] TContracts.GetById.Request request)
        {
            var result = await _crudTaskService.GetTaskById(request.Id);

            if (result.IsFailure )
                ResultValidator.Validate(result);

            return Ok(new TContracts.GetById.Response(result.Value));
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredTasksCreatedByEmployee([FromBody]
            TContracts.GetFilteredTasksCreatedByEmployee.Request request)
        {
            var result = await _crudTaskService.GetFilteredTasksCreatedByEmployee(request.Page, request.EmployeeId,
                request.Status, request.Titile, request.ObjectOfSorting, request.SortDirction);

            if(result.IsFailure )
                ResultValidator.Validate(result);

            return Ok(new TContracts.GetFilteredTasksCreatedByEmployee.Response(result.Value.Item2,result.Value.Item1));
        }

        [HttpPost]
        public async Task<IActionResult> GetFilteredTasksPerformedByEmployee([FromBody]
            TContracts.GetFilteredTasksPerformedByEmployee.Request request)
        {
            var result = await _crudTaskService.GetFilteredTasksPerformendByEmployee(request.Page, request.EmployeeId,
                request.Status, request.Titile, request.ObjectOfSorting, request.SortDirction);

            if (result.IsFailure)
                ResultValidator.Validate(result);

            return Ok(new TContracts.GetFilteredTasksPerformedByEmployee.Response(result.Value.Item2, result.Value.Item1));
        }

        [HttpPost]
        public async Task<IActionResult> GetProjectsFilteredTasks([FromBody]
            TContracts.GetProjectsFilteredTasks.Request request)
        {
            var result = await _crudTaskService.GetProjectFilteredTasks(request.Page, request.ProjectId,
                request.Status, request.Titile, request.ObjectOfSorting, request.SortDirction);

            if (result.IsFailure)
                ResultValidator.Validate(result);

            return Ok(new TContracts.GetProjectsFilteredTasks.Response(result.Value.Item2, result.Value.Item1));
        }

        [HttpPost]
        public async Task<IActionResult> GetProjectTasks([FromBody] TContracts.GetProjectTasks.Request request)
        {
            var result = await _crudTaskService.GetProjectTasks(request.Page, request.ProjectId);

            if (result.IsFailure)   
                ResultValidator.Validate(result);

            return Ok(new TContracts.GetProjectTasks.Response(result.Value.Item2,result.Value.Item1));
        }

        [HttpPost]
        public async Task<IActionResult> GetTaskPerformedByEmployee([FromBody] TContracts.GetTaskPerformedByEmployee.Request request)
        {
            var result = await _crudTaskService.GetTasksPerformendByEmployee(request.Page, request.EmployeeId);

            if (result.IsFailure)
                ResultValidator.Validate(result);

            return Ok(new TContracts.GetTaskPerformedByEmployee.Response(result.Value.Item2, result.Value.Item1));
        }

        [HttpPost]
        public async Task<IActionResult> GetTasksCreatedByEmployee([FromBody] TContracts.GetTasksCreatedByEmployee.Request request)
        {
            var result = await _crudTaskService.GetTasksCreatedByEmployee(request.Page, request.EmployeeId);

            if (result.IsFailure)
                ResultValidator.Validate(result);

            return Ok(new TContracts.GetTasksCreatedByEmployee.Response(result.Value.Item2, result.Value.Item1));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTasksExecuter([FromBody] TContracts.UpdateExecuter.Request request)
        {
            var result = await _crudTaskService.UpdateTaskExecuter(request.TaskId,request.EmployeeId);

            if(result.IsFailure)
                ResultValidator.Validate(result);

            return Ok(new TContracts.UpdateExecuter.Response(result.Value));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTasksPriority([FromBody] TContracts.UpdatePriority.Request request)
        {
            var result = await _crudTaskService.UpdateTaskPriority(request.TaskId, request.Priority);

            if (result.IsFailure)
                ResultValidator.Validate(result);

            return Ok(new TContracts.UpdatePriority.Response(result.Value));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTasksComment([FromBody] TContracts.UpdateComments.Request request)
        {
            var result = await _crudTaskService.UpdateTaskComment(request.TaskId, request.Comment);

            if (result.IsFailure)
                ResultValidator.Validate(result);

            return Ok(new TContracts.UpdateComments.Response(result.Value));
        }
    }
}
