namespace vidya.Common
{
    public static class GlobalConstants
    {
        public const int SupportTicketTitleMaxLength = 40;
        public const int SupportTicketTitleMinLength = 2;

        public const int SupportTicketContentMaxLength = 500;
        public const int SupportTicketContentMinLength = 2;

        public const int GameNameMaxLength = 80;
        public const int GameNameMinLength = 2;

        public const int GameDescriptionMaxLength = 300;
        public const int GameDescriptionMinLength = 2;

        public const double GamePriceMax = 1000;
        public const double GamePriceMin = 0;

        public const double DiscountMax = 100;
        public const double DiscountMin = 0;

        public const string ActivationKeyRegex = @"^[A-Z0-9]{5}(?:-[A-Z0-9]{5}){3,4}$";
    }
}
