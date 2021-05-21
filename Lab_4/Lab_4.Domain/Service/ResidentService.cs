using System.Collections.Generic;
using System.Linq;
using Lab_4.Domain.Entity;

namespace Lab_4.Domain.Service
{
    public interface IResidentService
    {
        double GetNonResidentStudentsPercentage(IEnumerable<StudentEntity> students,
            string universityTown);
    }

    public class ResidentService : IResidentService
    {
        public double GetNonResidentStudentsPercentage(IEnumerable<StudentEntity> students,
            string universityTown)
        {
            var nonResidentsCount = students.Count(ent => ent.Class == 1 && ent.HomeTown != universityTown);
            return (double)nonResidentsCount / students.Count() * 100;
        }
    }
}