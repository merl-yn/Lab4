using System.Collections.Generic;
using System.Linq;
using Lab_4.Domain.Entity;

namespace Lab_4.Domain.Service
{
    public interface IHostelService
    {
        IEnumerable<StudentEntity> GetStudentsForHostel(IEnumerable<StudentEntity> students, string universityTown);
    }
    
    public class HostelService : IHostelService
    {
        public IEnumerable<StudentEntity> GetStudentsForHostel(IEnumerable<StudentEntity> students,
            string universityTown)
        {
            return students.Where(ent => ent.HomeTown != universityTown);
        }
    }
}