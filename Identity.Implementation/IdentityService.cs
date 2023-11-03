using Identity.Interfaces;

namespace Identity.Implementation
{
    public class CurrentUserService : ICurrentUserService
    {
        public string Email => "user@mail.ru";
    }
}