namespace DesafioEclipseworks.WebAPI.Abstractions.Data
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
