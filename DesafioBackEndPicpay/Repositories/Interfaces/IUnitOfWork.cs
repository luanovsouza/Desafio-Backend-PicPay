namespace DesafioBackEndPicpay.Repositories.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync();
}