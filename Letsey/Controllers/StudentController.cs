using Letsey.Brokers.Storages;
using Letsey.Models.Foundations.Students;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTFulSense.Controllers;

namespace Letsey.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StudentController: RESTFulController
    {
        private readonly IStorageBroker storageBroker;
        private readonly IWebHostEnvironment environment;

        public StudentController(
            IStorageBroker storageBroker,
            IWebHostEnvironment environment)
        {
            this.storageBroker = storageBroker;
            this.environment = environment;
        }


        [HttpGet]
        public async ValueTask<ActionResult<IQueryable<Student>>> GetAllStudentsAsync()
        {
            try
            {
                IQueryable<Student> students =
                    await this.storageBroker.SelectAllStudentsAsync();
                return Ok(students);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{studentId}")]
        public async Task<ActionResult<Student>> GetStudentById(int studentId)
        {
            try
            {
                var student = await this.storageBroker.SelectStudentByIdAsync(studentId);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent([FromForm] Student student, IFormFile picture)
        {
            if (picture != null)
            {
                string uploadsFolder = Path.Combine(this.environment.WebRootPath, "images");
                Directory.CreateDirectory(uploadsFolder);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(picture.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await picture.CopyToAsync(fileStream);
                }

                student.PictureUrl = $"images/{fileName}";
            }

           await this.storageBroker.InsertStudentAsync(student);

            return Created(student);
        }
    }
}
