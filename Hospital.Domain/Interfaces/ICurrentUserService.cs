namespace Hospital.Domain.Interfaces;

public interface ICurrentUserService
{
    Guid UserId { get; }
}