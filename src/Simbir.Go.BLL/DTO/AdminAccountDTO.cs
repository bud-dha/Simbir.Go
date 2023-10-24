namespace Simbir.Go.BLL.DTO
{
    public class AdminAccountDTO
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsAdmin { get; set; }

        public double Balance { get; set; }
    }
}