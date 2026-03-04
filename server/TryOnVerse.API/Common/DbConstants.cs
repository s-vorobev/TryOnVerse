namespace TryOnVerse.API.Common;

public static class DbConstants
{
    public static class User
    {
        public const int FirstNameMaxLength = 50;
        public const int LastNameMaxLength = 50;
        public const int EmailMaxLength = 100;
        public const int PasswordMinLength = 12;
        public const int PasswordHashMaxLength = 255;
        public const int RoleMaxLength = 20;
    }

    public static class RefreshToken
    {
        public const int TokenMaxLength = 255;
        public const int TokenDuration = 86400000; // 24 hours in milliseconds
    }

    public static class Payment
    {
        public const int PaymentMethodMaxLength = 30;
        public const int PaymentStatusMaxLength = 30;
        public const int TransactionReferenceMaxLength = 100;

    }

    public static class Clothing
    {
        public const int SizeMaxLength = 20;
        public const int NameMaxLength = 100;
        public const int CategoryMaxLength = 50;
        public const int ColorMaxLength = 30;
        public const int ImgUrlMaxLength = 255;
        public const int ModelUrlMaxLength = 255;

    }

    public static class Order
    {
        public const int StatusMaxLength = 30;

    }

    public static class Address
    {
        public const int StreetMaxLength = 150;
        public const int CityMaxLength = 100;
        public const int StateMaxLength = 100;
        public const int ZipCodeMaxLength = 20;
        public const int CountryMaxLength = 100;
        public const int AddressTypeMaxLength = 20;

    }
}