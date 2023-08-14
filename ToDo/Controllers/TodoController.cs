using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : Controller
    {

        private readonly TodoDbContext dbContext;
        public TodoController(TodoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodo()
        {
            return Ok(await dbContext.Todos.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetTodo1([FromRoute] Guid id)
        {
            var todo = await dbContext.Todos.FindAsync(id);

            if(todo!=null)
            {
                return Ok(todo);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo(Adder addrequest)
        {
            var todoobj = new Todo
            {
                Id = Guid.NewGuid(),
                Description = addrequest.Description,
                Title = addrequest.Title,
            };

            await dbContext.Todos.AddAsync(todoobj);
            await dbContext.SaveChangesAsync();

            return Ok(todoobj);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateTodo([FromRoute] Guid id,Updater updatereq)
        {
            var todoobj = await dbContext.Todos.FindAsync(id);

            if(todoobj!=null)
            {
                todoobj.Title = updatereq.Title;
                todoobj.Description = updatereq.Description;
                await dbContext.SaveChangesAsync();
                return Ok(todoobj);
            }

            return NotFound();

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] Guid id)
        {
            var todoobj = await dbContext.Todos.FindAsync(id);

            if (todoobj!=null)
            {
                dbContext.Remove(todoobj);
                await dbContext.SaveChangesAsync();
                return Ok("Deleted Task");
            }
            return NotFound();
        }



    }
}
