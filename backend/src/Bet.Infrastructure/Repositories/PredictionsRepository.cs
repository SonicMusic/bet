using Bet.Application;
using Bet.Domain.PredictionManagement;
using Bet.Domain.Shared;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Bet.Infrastructure.Repositories;

public class PredictionsRepository : IPredictionsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PredictionsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Add(Prediction prediction, CancellationToken cancellationToken)
    {
        await _dbContext.Predictions.AddAsync(prediction, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return prediction.Id;
    }

    public async Task<Result<Prediction, Error>> GetById(Guid guid, CancellationToken cancellationToken)
    {
        var prediction = await _dbContext.Predictions
            .FirstOrDefaultAsync(t => t.Id == guid);

        if (prediction is null)
            return Errors.General.NotFound(guid);

        return prediction;
    }

    public async Task<Guid> Save(Prediction prediction, CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);

        return prediction.Id;
    }
}