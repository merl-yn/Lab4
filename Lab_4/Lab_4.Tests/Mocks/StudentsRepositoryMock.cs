using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab_4.Domain.Abstraction;
using Lab_4.Domain.Entity;

namespace Lab_4.Tests.Mocks
{
    public class StudentsRepositoryMock : IRepository<StudentEntity>
    {
        private static IEnumerable<StudentEntity> _mockedStudents = new List<StudentEntity>
        {
            new StudentEntity("Vasya", "Puplin", "ticket-id-1", 1, "passport-1", 1, "Lviv"),
            new StudentEntity("Ivan", "Dorn", "ticket-id-2", 1, "passport-2", 2, "Kyiv"),
            new StudentEntity("Galya", "Dudkina", "ticket-id-3", 1, "passport-3", 3, "Kyiv"),
            new StudentEntity("Artem", "Dudka", "ticket-id-4", 1, "passport-4", 1, "Odessa"),
        };

        public Task<StudentEntity> GetAsync(string id) =>
            Task.FromResult(_mockedStudents.FirstOrDefault(e => e.PassportSeries == id));

        public Task<IEnumerable<StudentEntity>> GetAllAsync() => Task.FromResult(_mockedStudents);

        public Task<bool> SetAsync(StudentEntity entity) => Task.FromResult(true);
    }
}