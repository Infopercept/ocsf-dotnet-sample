namespace Ocsf.Schema
{
    public readonly struct ClassType
    {
        public readonly int Id;
        public readonly string Name;
        public readonly string Description;

        private ClassType(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public override readonly string ToString()
        {
            return $"{Name} [{Id}] - {Description}";
        }

        public static implicit operator int(ClassType value)
        {
            return value.Id;
        }

        public static implicit operator ClassType(int id)
        {
            return id switch
            {
                0 => BaseEvent,
                1001 => FileSystemActivity,
                1002 => KernelExtensionActivity,
                1003 => KernelActivity,
                1004 => MemoryActivity,
                1005 => ModuleActivity,
                1006 => ScheduledJobActivity,
                1007 => ProcessActivity,
                2001 => SecurityFinding,
                3001 => AccountChange,
                3002 => Authentication,
                3003 => AuthorizeSession,
                3004 => EntityManagement,
                3005 => UserAccessManagement,
                3006 => GroupManagement,
                4001 => NetworkActivity,
                4002 => HttpActivity,
                4003 => DnsActivity,
                4004 => DhcpActivity,
                4005 => RdpActivity,
                4006 => SmbActivity,
                4007 => SshActivity,
                4008 => FtpActivity,
                4009 => EmailActivity,
                4010 => NetworkFileActivity,
                4011 => EmailFileActivity,
                4012 => EmailUrlActivity,
                5001 => InventoryInfo,
                5002 => ConfigState,
                6001 => WebResourcesActivity,
                6002 => ApplicationLifecycle,
                6003 => ApiActivity,
                6004 => WebResourceAccessActivity,
                _ => throw new IndexOutOfRangeException($"Value {id} is outside of range"),
            };
        }

        // Predefined instances
        public static ClassType BaseEvent => new(0, "Base Event", "The base event is a generic and concrete event. It also defines a set of attributes available in most event classes. As a generic event that does not belong to any event category, it could be used to log events that are not otherwise defined by the schema.");
        public static ClassType FileSystemActivity => new(1001, "File System Activity", "File System Activity events report when a process performs an action on a file or folder.");
        public static ClassType KernelExtensionActivity => new(1002, "Kernel Extension Activity", "Kernel Extension events report when a driver/extension is loaded or unloaded into the kernel.");
        public static ClassType KernelActivity => new(1003, "Kernel Activity", "Kernel Activity events report when an process creates, reads, or deletes a kernel resource.");
        public static ClassType MemoryActivity => new(1004, "Memory Activity", "Memory Activity events report when a process has memory allocated, read/modified, or other manipulation activities - such as a buffer overflow or turning off data execution protection (DEP).");
        public static ClassType ModuleActivity => new(1005, "Module Activity", "Module Activity events report when a process loads or unloads the module.");
        public static ClassType ScheduledJobActivity => new(1006, "Scheduled Job Activity", "Scheduled Job Activity events report activities related to scheduled jobs or tasks.");
        public static ClassType ProcessActivity => new(1007, "Process Activity", "Process Activity events report when a process launches, injects, opens or terminates another process, successful or otherwise.");
        public static ClassType SecurityFinding => new(2001, "Security Finding", "Security Finding events describe findings, detections, anomalies, alerts and/or actions performed by security products");
        public static ClassType AccountChange => new(3001, "Account Change", "Account Change events report when specific user account management tasks are performed, such as a user/role being created, changed, deleted, renamed, disabled, enabled, locked out or unlocked.");
        public static ClassType Authentication => new(3002, "Authentication", "Authentication events report authentication session activities such as user attempts a logon or logoff, successfully or otherwise.");
        public static ClassType AuthorizeSession => new(3003, "Authorize Session", "Authorize Session events report privileges or groups assigned to a new user session, usually at login time.");
        public static ClassType EntityManagement => new(3004, "Entity Management", "Entity Management events report activity by a managed client, a micro service, or a user at a management console. The activity can be a create, read, update, and delete operation on a managed entity.");
        public static ClassType UserAccessManagement => new(3005, "User Access Management", "User Access Management events report management updates to a user's privileges.");
        public static ClassType GroupManagement => new(3006, "Group Management", "Group Management events report management updates to a group, including updates to membership and permissions.");
        public static ClassType NetworkActivity => new(4001, "Network Activity", "Network Activity events report network connection and traffic activity.");
        public static ClassType HttpActivity => new(4002, "HTTP Activity", "HTTP Activity events report HTTP connection and traffic information.");
        public static ClassType DnsActivity => new(4003, "DNS Activity", "DNS Activity events report DNS queries and answers as seen on the network.");
        public static ClassType DhcpActivity => new(4004, "DHCP Activity", "DHCP Activity events report MAC to IP assignment via DHCP from a client or server.");
        public static ClassType RdpActivity => new(4005, "RDP Activity", "Remote Desktop Protocol (RDP) Activity events report remote client connections to a server as seen on the network.");
        public static ClassType SmbActivity => new(4006, "SMB Activity", "Server Message Block (SMB) Protocol Activity events report client/server connections sharing resources within the network.");
        public static ClassType SshActivity => new(4007, "SSH Activity", "SSH Activity events report remote client connections to a server using the Secure Shell (SSH) Protocol.");
        public static ClassType FtpActivity => new(4008, "FTP Activity", "File Transfer Protocol (FTP) Activity events report file transfers between a server and a client as seen on the network.");
        public static ClassType EmailActivity => new(4009, "Email Activity", "Email events report activities of emails.");
        public static ClassType NetworkFileActivity => new(4010, "Network File Activity", "Network File Activity events report activities on a cloud file storage service such as Box, MS OneDrive, or Google Drive.");
        public static ClassType EmailFileActivity => new(4011, "Email File Activity", "Email File Activity events report files within emails.");
        public static ClassType EmailUrlActivity => new(4012, "Email URL Activity", "Email URL Activity events report URLs within an email.");
        public static ClassType InventoryInfo => new(5001, "Device Inventory Info", "Device Inventory Info events report device inventory data.");
        public static ClassType ConfigState => new(5002, "Device Config State", "Device Config State events report device configuration data.");
        public static ClassType WebResourcesActivity => new(6001, "Web Resources Activity", "Web Resources Activity events describe actions executed on a set of Web Resources.");
        public static ClassType ApplicationLifecycle => new(6002, "Application Lifecycle", "Application Lifecycle events report installation, removal, start, stop of an application or service.");
        public static ClassType ApiActivity => new(6003, "API Activity", "API events describe general CRUD (Create, Read, Update, Delete) API activities, e.g. (AWS Cloudtrail)");
        public static ClassType WebResourceAccessActivity => new(6004, "Web Resource Access Activity", "Web Resource Access Activity events describe successful/failed attempts to access a web resource over HTTP.");

    }
}