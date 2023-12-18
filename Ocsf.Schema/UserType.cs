using Microsoft.VisualBasic;
using System.Net;
using System.Runtime.InteropServices;
using System;

namespace Ocsf.Schema
{
    public enum UserType
    {
        /// <summary>
        /// The type is unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Regular user account.
        /// </summary>
        User = 1,

        /// <summary>
        /// Admin/root user account.
        /// </summary>
        Admin = 2,

        /// <summary>
        /// System account. For example, Windows computer accounts with a trailing dollar sign ($).
        /// </summary>
        System = 3,

        /// <summary>
        /// The type is not mapped. See the type attribute, which may contain a data source specific value.
        /// </summary>
        Other = 99
    }
}
