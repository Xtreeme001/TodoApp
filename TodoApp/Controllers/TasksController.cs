using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Dto;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly DataContext _db;

        public TasksController(DataContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<IActionResult> GetAlltasks()
        {
            var task = await _db.Tasks.ToListAsync();

            var taskDto = new List<todoDTO>();

            foreach (var tasks in task)
            {
                taskDto.Add(new todoDTO()
                {
                    Title = tasks.Title,
                    Description = tasks.Description,
                    IsCompleted = tasks.IsCompleted,

                });

            }
            return Ok(taskDto);
        }
        [HttpGet("{int id}")]
        public async Task<IActionResult> GetAlltasks(int id)
        {
            var task = await _db.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (task == null)
            {
                Console.WriteLine("EMPTY FIELDS");

            }

            var taskDto = new todoDTO
            {
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
            };
                

            return Ok(taskDto);
        }

        [HttpDelete("{int id}")]
        public async Task<IActionResult> DeleteAlltasks(int id)
        {
            var task = await _db.Tasks.FirstOrDefaultAsync(x => x.Id == id);
            if (task == null)
            {
                Console.WriteLine("EMPTY FIELDS");
            }
            _db.Tasks.Remove(task);
            _db.SaveChanges();

            var taskDto = new todoDTO
            {
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
            };


            return Ok(taskDto);
            //return Ok(task);
        }
    }
}
