namespace LivlReviews.Email
{
    public class SmtpSettings
    {
        public required string Server { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public string SenderEmail { get; set; }
    }
}
