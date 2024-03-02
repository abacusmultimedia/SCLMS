using System;
using System.Net;
using System.Net.Mail;

public class EmailSender : IEmailSender
{
    private readonly string smtpServer;
    private readonly int smtpPort;
    private readonly string smtpUsername;
    private readonly string smtpPassword;

    //public EmailSender(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
    //{
    //    this.smtpServer = smtpServer;
    //    this.smtpPort = smtpPort;
    //    this.smtpUsername = smtpUsername;
    //    this.smtpPassword = smtpPassword;
    //}

    public void SendEmail(string from, string to, string subject, string body)
    {
        // Create a MailMessage object
        MailMessage mailMessage = new MailMessage(from, to)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = false // Set to true if using HTML in the body
        };

        // Create a SmtpClient object
        SmtpClient smtpClient = new SmtpClient(smtpServer)
        {
            Port = smtpPort,
            Credentials = new NetworkCredential(smtpUsername, smtpPassword),
            EnableSsl = true
        };

        try
        {
            // Send the email
            smtpClient.Send(mailMessage);
            Console.WriteLine("Email sent successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }
}
