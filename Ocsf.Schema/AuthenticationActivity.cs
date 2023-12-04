namespace Ocsf.Schema
{
    public readonly struct AuthenticationActivity
    {
        public readonly int Id;
        public readonly string Name;

        private AuthenticationActivity(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override readonly string ToString()
        {
            return Name;
        }

        public static implicit operator int(AuthenticationActivity value)
        {
            return value.Id;
        }

        public static implicit operator AuthenticationActivity(int id)
        {
            return id switch
            {
                0 => Unknown,
                1 => Login,
                2 => Logout,
                3 => AuthenticationTicket,
                4 => ServiceTicket,
                99 => Other,
                _ => throw new IndexOutOfRangeException($"Value {id} is outside of range"),
            };
        }

        // Predefined instances

        /// <summary>
        /// The event activity is unknown.
        /// </summary>
        public static AuthenticationActivity Unknown => new(0, "Unknown");

        /// <summary>
        /// A new logon session was requested.
        /// </summary>
        public static AuthenticationActivity Login => new(1, "Login");

        /// <summary>
        /// A logon session was terminated and no longer exists.
        /// </summary>
        public static AuthenticationActivity Logout => new(2, "Logout");

        /// <summary>
        /// A Kerberos authentication ticket (TGT) was requested.
        /// </summary>
        public static AuthenticationActivity AuthenticationTicket => new(3, "AuthenticationTicket");

        /// <summary>
        /// A Kerberos service ticket was requested.
        /// </summary>
        public static AuthenticationActivity ServiceTicket => new(4, "ServiceTicket");

        /// <summary>
        /// The event activity is not mapped.
        /// </summary>
        public static AuthenticationActivity Other => new(99, "Other");
    }
}
