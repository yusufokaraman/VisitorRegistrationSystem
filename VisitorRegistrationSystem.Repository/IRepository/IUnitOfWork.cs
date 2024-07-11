namespace VisitorRegistrationSystem.Repository.IRepository
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IVisitorRepository Visitors { get; }

        IDepartmentRepository Departments { get; }
        //int etkilenen kayıt sayısını almak isteyebiliriz.
        Task<int> SaveAsync();

    }
}
