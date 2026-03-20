namespace Bet.Domain.GameManagement.ValueObjects;

public enum StatusPrediction
{
    Waiting, // ожидание
    Live,   // в игре
    Win,    // победа
    Loss,   // проигрыш
    Draw,   //  ничья (возврат ставки, ставка засчитывается)
    Push,   //  возврат ставки (результат ровно по линии, ставка не засчитывается)
    Void,   //  прогноз аннулирован (матч отменён, прерван и т.п.)
    Refund, //  возврат (по правилам платформы)
}