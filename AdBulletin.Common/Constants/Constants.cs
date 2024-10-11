namespace AdBulletin.Common.Constants
{
    public class Constants
    {
        public class Status
        {
            public enum BasicStatus
            {
                Active = 1,
                Inactive = 2,
                Draft = 3,
                ToValidate = 4,
                Deleted = 5,
            }
        }

        public class Roles
        {
            public const string ADMIN = "ADM";
            public const string SUPERVISOR = "SUP";
            public const string ADVERTISER = "ADS";
            public const string GENERAL = "GRL";

            public static IList<string> LIST_ROLES = new List<string>
            {
                ADMIN,
                SUPERVISOR,
                ADVERTISER,
                GENERAL,
            };
        }

        public class Email
        {
            public enum EmailTemplates
            {
                Welcoming = 0,
                ResetPassword = 1
            }

            public class Rrss
            {
                public const string Facebook = "https://www.facebook.com/lirmi";
                public const string Twitter = "https://twitter.com/Lirmidotcom";
                public const string Instagram = "https://www.instagram.com/lirmi_com/";
                public const string Youtube = "https://www.youtube.com/user/LirmiTV/";
                public const string Linkedin = "https://www.linkedin.com/company/lirmi/";
            }
        }

        public class System
        {
            public const string PRODUCT_MAIN = "main";

            public class App
            {
                public const string Title = "WISBulletin";
                public const string Slogan = "Services, Deals and more...";
                public const string LongSlogan = "A Centralized Ads Platform in Wisconsin Dells, Where The Best Services, Deals, and Products are Found...";

                public class BrandImages
                {
                    public string LOGO_BRAND = "https://i.ibb.co/HPCVfVW/WIS-BULLETIN.png";
                    public string LOGO_BRAND_NOBG = "https://i.ibb.co/LY904WH/WIS-BULLETIN-2-nobg.png";
                    public string LOGO_BRAND_BIG = "https://i.ibb.co/3dBhG2n/WIS-BULLETIN-2-big.png";
                    public string LOGO_BRAND_WHITE_NOBG = "https://i.ibb.co/zVQnL2z/WIS-BULLETIN-WHITE-nobg.png";
                    public string LOGO_BRAND_WHITE_2_NOBG = "https://i.ibb.co/Xkj8V3H/WIS-BULLETIN-WHITE-2-nobg.png";
                }
            }

            public class Tokens
            {
                public const string USER_SESSION = "us_";
                public const string JWT_AUTH_TOKEN = $"{USER_SESSION}jt";
            }
        }

        public class Metrics
        {
            public const int MIN_VIEWS_FOR_TREND = 1000;
            public const int MIN_REVIEWS_FOR_TREND = 15;
            public const int MIN_VIEWS_FOR_TREND_PREMIUM = 100;
            public const int MIN_REVIEWS_FOR_TREND_PREMIUM = 5;
            public const int MIN_REPORTS_FOR_SHADOW_BAN = 15;
            public const int MIN_REPORTS_FOR_BAN = 25;
        }
    }
}
