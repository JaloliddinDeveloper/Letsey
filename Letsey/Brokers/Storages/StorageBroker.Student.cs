using Letsey.Models.Foundations.Students;
using Microsoft.EntityFrameworkCore;

namespace Letsey.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Student> Students { get; set; }
    }
}
