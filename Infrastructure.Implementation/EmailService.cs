using Email.Interfaces;
using System;
using System.Threading.Tasks;

namespace Infrastructure.MailHandler
{
    public class EmailService : IEmailService
    {
        public Task SendAsync(string address, string subject, string body)
        {
            Console.WriteLine($"{address} {subject} {body}");

            return Task.CompletedTask;
        }
    }
}
