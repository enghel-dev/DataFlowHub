using DataFlowHub.Domain.Entities;
using DataFlowHub.Domain.Interfaces;
using DataFlowHub.Infrastructure.DataBase;

namespace DataFlowHub.Infrastructure.Repository
{
    public class ClassrooomRepository : IClassroomRepository
    {
        //Inyeccion de dependencias
        private readonly DBconnectionFactory _dbconnectionFactory;
        public ClassrooomRepository(DBconnectionFactory dbconnectionFactory)
        {
            _dbconnectionFactory = dbconnectionFactory;
        }

        public Task<int> CreateAsync(Classroom classroom)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Classroom>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Classroom>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Classroom classroom)
        {
            throw new NotImplementedException();
        }
    }
}
