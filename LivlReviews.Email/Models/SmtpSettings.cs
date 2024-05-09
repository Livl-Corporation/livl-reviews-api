﻿namespace LivlReviews.Email
{
    public class SmtpSettings
    {
        public required string Server { get; set; }
        public required int Port { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required bool EnableSsl { get; set; }
        public required string SenderEmail { get; set; }
    }
}
