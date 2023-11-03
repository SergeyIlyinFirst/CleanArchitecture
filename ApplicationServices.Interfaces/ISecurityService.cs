namespace ApplicationServices.Interfaces
{
    public interface ISecurityService
    {
        public bool IsCurrentUserAdmin { get; }
        public string[] CurrentUserPermissions { get; }
    }
}
