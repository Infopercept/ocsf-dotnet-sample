namespace Ocsf.Schema
{
    /// <summary>
    /// The normalized logon type identifier.
    /// 0	System: Used only by the System account, for example at system startup.
    /// 2	Interactive: A local logon to device console.
    /// 3	Network: A user or device logged onto this device from the network.
    /// 4	Batch: A batch server logon, where processes may be executing on behalf of a user without their direct intervention.
    /// 5	OS Service: A logon by a service or daemon that was started by the OS.
    /// 7	Unlock: A user unlocked the device.
    /// 8	Network Cleartext: A user logged on to this device from the network. The user's password in the authentication package was not hashed.
    /// 9	New Credentials: A caller cloned its current token and specified new credentials for outbound connections.The new logon session has the same local identity, but uses different credentials for other network connections.
    /// 10	Remote Interactive: A remote logon using Terminal Services or remote desktop application.
    /// 11	Cached Interactive: A user logged on to this device with network credentials that were stored locally on the device and the domain controller was not contacted to verify the credentials.
    /// 12	Cached Remote Interactive: Same as Remote Interactive. This is used for internal auditing.
    /// 13	Cached Unlock: Workstation logon.
    /// 99	Other: Other logon type.
    /// </summary>
    public enum LogonType
    {
        /// <summary>
        /// The type is unknown.
        /// </summary>
        Unknown = 0,
                      
        /// <summary>
        /// A local logon to device console.
        /// </summary>
        Interactive = 2,
        
        /// <summary>
        /// A user or device logged onto this device from the network.
        /// </summary>
        Network = 3,
        
        /// <summary>
        /// A batch server logon, where processes may be executing on behalf of a user without their direct intervention.
        /// </summary>
        Batch = 4,
        
        /// <summary>
        /// A logon by a service or daemon that was started by the OS.
        /// </summary>
        Service = 5,
        
        /// <summary>
        /// A user unlocked the device.
        /// </summary>
        Unlock = 7,
        
        /// <summary>
        /// A user logged on to this device from the network. The user's password in the authentication package was not hashed.
        /// </summary>
        NetworkCleartext = 8,
        
        /// <summary>
        /// A caller cloned its current token and specified new credentials for outbound connections.The new logon session has the same local identity, but uses different credentials for other network connections.
        /// </summary>
        NewCredentials = 9,
        
        /// <summary>
        /// A remote logon using Terminal Services or remote desktop application.
        /// </summary>
        RemoteInteractive = 10,
        
        /// <summary>
        /// A user logged on to this device with network credentials that were stored locally on the device and the domain controller was not contacted to verify the credentials.
        /// </summary>
        CachedInteractive = 11,
        
        /// <summary>
        /// Same as Remote Interactive. This is used for internal auditing.
        /// </summary>
        CachedRemoteInteractive = 12,
        
        /// <summary>
        /// Workstation logon.
        /// </summary>
        CachedUnlock = 13,
        
        /// <summary>
        /// Other logon type.
        /// </summary>
        Other = 99

    }
}
