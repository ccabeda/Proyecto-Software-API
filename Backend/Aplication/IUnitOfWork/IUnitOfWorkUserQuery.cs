using Proyecto_Software_Individual.Aplication.IRepository.IQuery;

namespace Proyecto_Software_Individual.Aplication.IUnitOfWork
{
    public interface IUnitOfWorkUserQuery
    {
        IRepositoryUserQuery _repositoryUserQuery { get; }
    }
}
