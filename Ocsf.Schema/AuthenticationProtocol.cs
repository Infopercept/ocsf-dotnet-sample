namespace Ocsf.Schema
{
    public enum AuthenticationProtocol
    {
        Unknown = 0,
        NTLM = 1,
        Kerberos = 2,
        Digest = 3,
        OpenID = 4,
        SAML = 5,
        OAUTH_2_0 = 6,
        PAP = 7,
        CHAP = 8,
        EAP = 9,
        RADIUS = 10,
        Other = 99
    }
}
