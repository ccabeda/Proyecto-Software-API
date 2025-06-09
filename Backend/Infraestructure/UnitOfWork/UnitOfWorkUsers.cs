using Proyecto_Software_Individual.Aplication.IRepository.IQuery;
using Proyecto_Software_Individual.Aplication.IUnitOfWork;

namespace Proyecto_Software_Individual.Infraestructure.UnitOfWork
{
    public class UnitOfWorkUsers : IUnitOfWorkUserQuery
    {
        public IRepositoryUserQuery _repositoryUserQuery { get; }

        public UnitOfWorkUsers(
        IRepositoryUserQuery userQuery)
        {
            _repositoryUserQuery = userQuery;
        }
    }
}
