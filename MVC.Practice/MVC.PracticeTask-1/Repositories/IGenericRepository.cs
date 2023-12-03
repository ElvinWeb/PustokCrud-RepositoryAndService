namespace MVC.PracticeTask_1.Repositories
{
    public interface IGenericRepository
    {
        Task<T> Create<T>();
    }
}
