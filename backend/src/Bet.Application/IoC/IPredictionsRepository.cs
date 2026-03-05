using Bet.Domain.PredictionManagement;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;

namespace Bet.Application.IoC;

public interface IPredictionsRepository
{
    Task<Guid> Add(Prediction prediction, CancellationToken cancellationToken);
    Task<Result<Prediction, Error>> GetById(Guid guid, CancellationToken cancellationToken);
    Task<Guid> Save(Prediction prediction, CancellationToken cancellationToken);
}