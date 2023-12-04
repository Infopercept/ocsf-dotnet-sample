namespace Ocsf.Schema
{
    public enum AuthenticationActivity
    {
        /// <summary>
        /// The event activity is unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// A new logon session was requested.
        /// </summary>
        Login = 1,

        /// <summary>
        /// A logon session was terminated and no longer exists.
        /// </summary>
        Logout = 2,

        /// <summary>
        /// A Kerberos authentication ticket (TGT) was requested.
        /// </summary>
        AuthenticationTicket = 3,

        /// <summary>
        /// A Kerberos service ticket was requested.
        /// </summary>
        ServiceTicket = 4,

        /// <summary>
        /// The event activity is not mapped.
        /// </summary>
        Other = 99
    }
}
