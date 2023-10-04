using System;
using src.Email;

namespace Email
{
    public interface IEmailSender
    {
        Task SendEmailAync(EMessage email);
    }
}