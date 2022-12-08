namespace HouseRentingSystem.Data
{
    public class DataConstants
    {
        public class House
        {
            public const int TitleMaxLength = 50;
            public const int TitleMinLength = 10;

            public const int AddressMaxLength = 150;
            public const int AddressMinLength = 30;

            public const int DescriptionMaxLength = 500;
            public const int DescriptionMinLength = 50;
        }
        public class Category
        {
            public const int NameMaxLength = 50;
        }
        public class Agent
        {
            public const int PhoneNumberMaxLength = 15;
            public const int PhoneNumberMinLength = 7;
        }
    }
}
