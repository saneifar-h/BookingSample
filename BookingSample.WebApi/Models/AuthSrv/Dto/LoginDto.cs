namespace BookingSample.WebApi.Models.AuthSrv.Dto
{
    public class LoginDto
    {
        public bool IsPrivateMode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}