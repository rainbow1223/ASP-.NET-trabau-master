using Microsoft.AspNetCore.Mvc;
using Trabau.Common.Properties;
using Trabau.DataAccess.Interfaces;
using TrabauClassLibrary;

namespace TrabauAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        [Route("GetProjectContStatus")]
        public IActionResult GetProjectContStatus()
        {
            return Ok("Controller is working fine");
        }

        [HttpPost]
        [Route("GetChangeEvents")]
        public IActionResult GetChangeEvents([FromBody] ChangeEventParameters payload)
        {
            var changeEvents = _projectRepository.GetChangeEvents(payload);
            var events = changeEvents.Select(d => new
            {
                TargetFieldId = d.ChangeEventTargetFieldId.ToString(),
                TargetField_DataSource = !string.IsNullOrWhiteSpace(d.ChangeEventTargetField_DataSource) ?
                                            MiscFunctions.EncryptAndEncode(d.ChangeEventTargetField_DataSource, Trabau_Keys.Project_Key) : string.Empty
            }).ToList();
            return new JsonResult(events);
        }
    }
}
