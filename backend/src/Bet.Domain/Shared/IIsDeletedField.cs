namespace Bet.Domain.Shared;

public interface IIsDeletedField
{
    void Delete();
    void Restore();
}