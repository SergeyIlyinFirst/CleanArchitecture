using ApplicationServices.Interfaces;

namespace ApplicationServices.Implementation
{
    public class SecurityService : ISecurityService
    {
        public bool IsCurrentUserAdmin => throw new System.NotImplementedException();
        public string[] CurrentUserPermissions { get => throw new System.NotImplementedException(); }
    }
}
