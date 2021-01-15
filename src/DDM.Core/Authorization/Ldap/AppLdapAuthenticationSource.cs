using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using DDM.Authorization.Users;
using DDM.MultiTenancy;

namespace DDM.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}