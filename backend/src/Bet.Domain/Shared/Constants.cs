namespace Bet.Domain.Shared;

public class Constants
{
    public static class General
    {
        public const int MAX_LOW_TEXT_LENGTH = 100;
        public const int MAX_MEDIUM_TEXT_LENGTH = 255;
        public const int MAX_HIGH_TEXT_LENGTH = 2000;
    }
    public static class Team
    {
        public const string LOGO = "logo";
        public const string JPG = ".jpg";
        public const int SHORT_NAME_LENGTH = 3;
        public const int MIN_NAME_LENGTH = 3;
        public const int MAX_NAME_LENGTH = 50;
    }
}