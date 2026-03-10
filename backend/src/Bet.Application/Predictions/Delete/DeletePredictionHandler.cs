namespace Bet.Application.Predictions.Delete;

// public class DeletePredictionHandler
// {
//     private readonly IPredictionsRepository _repository;
//     private readonly ILogger<DeletePredictionHandler> _logger;
//
//     public DeletePredictionHandler(
//         IPredictionsRepository repository, 
//         ILogger<DeletePredictionHandler> logger)
//     {
//         _repository = repository;
//         _logger = logger;
//     }
//
//     public async Task<Result<Guid, Error>> Handle(
//         DeletePredictionCommand command, 
//         CancellationToken cancellationToken)
//     {
//         var predictionFromDb = await _repository.GetById(command.PredictionId, cancellationToken);
//         if (predictionFromDb.IsFailure)
//             return predictionFromDb.Error;
//         
//         predictionFromDb.Value.Delete();
//
//         var result = await _repository.Save(predictionFromDb.Value, cancellationToken);
//         
//         _logger.LogInformation("Delete prediction with id {result}", result);
//
//         return result;
//     }
// }