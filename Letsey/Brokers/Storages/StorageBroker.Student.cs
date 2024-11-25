using Letsey.Models.Foundations.Students;
using Microsoft.EntityFrameworkCore;

namespace Letsey.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Student> Students { get; set; }

        public async ValueTask<Student> InsertStudentAsync(Student student) =>
           await InsertAsync(student);

        public async ValueTask<IQueryable<Student>> SelectAllStudentsAsync() =>
            await SelectAllAsync<Student>();

        public async ValueTask<Student> SelectStudentByIdAsync(int studentId) =>
            await SelectAsync<Student>(studentId);

        public async ValueTask<Student> UpdateStudentAsync(Student student) =>
            await UpdateAsync(student);

        public async ValueTask<Student> DeleteStudentAsync(Student student) =>
            await DeleteAsync(student);
    }
}
