namespace CrestASPNETCoreIdentityDemo
{
    public class CrestUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string NormalisedUsername { get; set; }
        public string PasswordHash { get; set; }
    }
}
