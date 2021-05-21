using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lab_4.Domain.Abstraction;
using Lab_4.Domain.Entity;
using Newtonsoft.Json;

namespace Lab_4.DataAccess.Implementation
{
    public class StudentsRepository : IRepository<StudentEntity>
    {
        private static readonly string _fileName = "students.json";
        
        private readonly object _lock = new object();
        
        public async Task<StudentEntity> GetAsync(string id)
        {
            var entities = await GetAllAsync();

            return entities?.FirstOrDefault(ent => ent.PassportSeries == id);
        }

        public Task<IEnumerable<StudentEntity>> GetAllAsync()
        {
            var serializedData = ReadFromFile();
            var entities = JsonConvert.DeserializeObject<IEnumerable<StudentEntity>>(serializedData);

            return Task.FromResult(entities);
        }

        public async Task<bool> SetAsync(StudentEntity entity)
        {
            var entities = await GetAllAsync() ?? new List<StudentEntity>();
            (entities as IList<StudentEntity>).Add(entity);
            
            var serializedData = JsonConvert.SerializeObject(entities);
            WriteToFile(serializedData);
            return true;
        }

        private string ReadFromFile()
        {
            lock (_lock)
            {
                if (!File.Exists(_fileName)) return string.Empty;
                
                return File.ReadAllText(_fileName);
            }
        }
        
        private void WriteToFile(string data)
        {
            lock (_lock)
            {
                File.WriteAllText(_fileName, data);
            }
        }
    }
}