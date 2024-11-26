namespace ASPNETMOD192
{
    public static class ASPNETMOD192Constants
    {
        public readonly struct USERS
        {
            public readonly struct ADMIN
            {
                public static readonly string USERNAME = "admin";
                public static readonly string EMAIL = "admin@sms.pt";
                public static readonly string PASSWORD = "xpto1234";
            }

            public readonly struct DRIVER 
            {
                public static readonly string USERNAME = "driver";
                public static readonly string EMAIL = "driver@sms.pt";
                public static readonly string PASSWORD = "01012024";
            }

            public readonly struct ADMINISTRATIVE
            {
                public static readonly string USERNAME = "administrative";
                public static readonly string EMAIL = "administrative@sms.pt";
                public static readonly string PASSWORD = "qwerty1234";
            }
        }


        public readonly struct ROLES
        {
            public const string ADMIN = "ADMIN";
            public const string DRIVER = "DRIVER";
            public const string ADMINISTRATIVE = "ADMINISTRATIVE";
        }


        public readonly struct POLICIES
        {
            public readonly struct APP_POLICY
            {
                public const string NAME = "APP_POLICY";
                public static readonly string[] POLICY_ROLES =
                {
                    ROLES.ADMINISTRATIVE,
                    ROLES.ADMIN,
                    ROLES.DRIVER
                };
            }

            public readonly struct APP_POLICY_EDITABLE_CRUD
            {
                public const string NAME = "APP_POLICY_EDITABLE_CRUD";
                public static readonly string[] POLICY_ROLES =
                {
                    ROLES.ADMINISTRATIVE,
                    ROLES.ADMIN
                };
            }

            public readonly struct APP_POLICY_ADMIN
            {
                public const string NAME = "APP_POLICY_ADMIN";
                public static readonly string[] POLICY_ROLES =
                {
                    ROLES.ADMIN
                };
            }
        }


    }
}
