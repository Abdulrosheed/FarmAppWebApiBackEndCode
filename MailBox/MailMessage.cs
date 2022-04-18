using FirstProject.Dtos;
using FirstProject.Interfaces.Repositories;
using MailKit.Net.Smtp;
using MimeKit;

namespace FirstProject.MailBox
{
    public class MailMessage : IMailMessage
    {
        private readonly IEmailRepository _emailRepository;

        public MailMessage(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public async void RegistrationNotificationEmail(string RecieverEmail , string link)
        {
            var emailInfo = await _emailRepository.GetEmailByEmailTypeReturningEmailObjectDtoAsync(EmailType.RegistrationApproval);
            MimeMessage mssg = new MimeMessage ();
            mssg.From.Add(new MailboxAddress("Agbeloba", "ClhProjectEmail@gmail.com"));
            mssg.To.Add(MailboxAddress.Parse(RecieverEmail));
            mssg.Subject = emailInfo.Content;
            mssg.Body = new TextPart("html")
            {
                Text = emailInfo.Subject + link
            };
            string email = "ClhProjectEmail@gmail.com";
            string passWord = "ClhProjectEmail12345";
            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect("smtp.gmail.com" , 465 , true);
                client.Authenticate(email,passWord);
                client.Send(mssg);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }

        }

        public async void NotifyFarmerAboutFarmInspectorEmail(string farmerEmail, string farmerLink)
        {
            var emailInfo = await _emailRepository.GetEmailByEmailTypeReturningEmailObjectDtoAsync(EmailType.NotifyingFarmerAboutFarmInspector);
            MimeMessage mssg = new MimeMessage ();
            mssg.From.Add(new MailboxAddress("Agbeloba", "ClhProjectEmail@gmail.com"));
            mssg.To.Add(MailboxAddress.Parse(farmerEmail));
            mssg.Subject = emailInfo.Content;
            mssg.Body = new TextPart("html")
            {
                Text = emailInfo.Subject + farmerLink
            };
            string email = "ClhProjectEmail@gmail.com";
            string passWord = "ClhProjectEmail12345";
            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect("smtp.gmail.com" , 465 , true);
                client.Authenticate(email,passWord);
                client.Send(mssg);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }

        }

        public async void RegistrationNotificationForFarmInspector(string RecieverEmail, string FarmInspectorpassWord , string link)
        {
            var emailInfo = await _emailRepository.GetEmailByEmailTypeReturningEmailObjectDtoAsync(EmailType.RegistrationApproval);
            MimeMessage mssg = new MimeMessage ();
            mssg.From.Add(new MailboxAddress("Agbeloba", "ClhProjectEmail@gmail.com"));
            mssg.To.Add(MailboxAddress.Parse(RecieverEmail));
            mssg.Subject = emailInfo.Content;
            mssg.Body = new TextPart("html")
            {
                Text = emailInfo.Subject + $". Your password is {FarmInspectorpassWord}.Login with this link{link} to view profile and update profile"
            };
            string email = "ClhProjectEmail@gmail.com";
            string passWord = "ClhProjectEmail12345";
            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect("smtp.gmail.com" , 465 , true);
                client.Authenticate(email,passWord);
                client.Send(mssg);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        public async void NotifyFarmInspectorAboutToBeInspectedFarm(string farmInspectorEmail, string farmerLink)
        {
            var emailInfo = await _emailRepository.GetEmailByEmailTypeReturningEmailObjectDtoAsync(EmailType.AssigningInspectorToFarm);
            MimeMessage mssg = new MimeMessage ();
            mssg.From.Add(new MailboxAddress("Agbeloba", "ClhProjectEmail@gmail.com"));
            mssg.To.Add(MailboxAddress.Parse(farmInspectorEmail));
            mssg.Subject = emailInfo.Content;
            mssg.Body = new TextPart("html")
            {
                Text = emailInfo.Subject + farmerLink
            };
            string email = "ClhProjectEmail@gmail.com";
            string passWord = "ClhProjectEmail12345";
            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect("smtp.gmail.com" , 465 , true);
                client.Authenticate(email,passWord);
                client.Send(mssg);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }

        }

        public async void NotifyCompanyAboutOrder(string RecieverEmail)
        {
            var emailInfo = await _emailRepository.GetEmailByEmailTypeReturningEmailObjectDtoAsync(EmailType.NotifyingCompanyAboutOrder);
            MimeMessage mssg = new MimeMessage ();
            mssg.From.Add(new MailboxAddress("Agbeloba", "ClhProjectEmail@gmail.com"));
            mssg.To.Add(MailboxAddress.Parse(RecieverEmail));
            mssg.Subject = emailInfo.Content;
            mssg.Body = new TextPart("html")
            {
                Text = emailInfo.Subject 
            };
            string email = "ClhProjectEmail@gmail.com";
            string passWord = "ClhProjectEmail12345";
            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect("smtp.gmail.com" , 465 , true);
                client.Authenticate(email,passWord);
                client.Send(mssg);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }

        }

        public async void UpdateNotificationEmail(string RecieverEmail, string link)
        {
            var emailInfo = await _emailRepository.GetEmailByEmailTypeReturningEmailObjectDtoAsync(EmailType.UpdateNotificationEmail);
            MimeMessage mssg = new MimeMessage ();
            mssg.From.Add(new MailboxAddress("Agbeloba", "ClhProjectEmail@gmail.com"));
            mssg.To.Add(MailboxAddress.Parse(RecieverEmail));
            mssg.Subject = emailInfo.Content;
            mssg.Body = new TextPart("html")
            {
                Text = emailInfo.Subject 
            };
            string email = "ClhProjectEmail@gmail.com";
            string passWord = "ClhProjectEmail12345";
            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect("smtp.gmail.com" , 465 , true);
                client.Authenticate(email,passWord);
                client.Send(mssg);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        public async void RegistrationNotificationForFarm(string RecieverEmail)
        {
            var emailInfo = await _emailRepository.GetEmailByEmailTypeReturningEmailObjectDtoAsync(EmailType.RegistrationApprovalForFarm);
            MimeMessage mssg = new MimeMessage ();
            mssg.From.Add(new MailboxAddress("Agbeloba", "ClhProjectEmail@gmail.com"));
            mssg.To.Add(MailboxAddress.Parse(RecieverEmail));
            mssg.Subject = emailInfo.Content;
            mssg.Body = new TextPart("html")
            {
                Text = emailInfo.Subject 
            };
            string email = "ClhProjectEmail@gmail.com";
            string passWord = "ClhProjectEmail12345";
            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect("smtp.gmail.com" , 465 , true);
                client.Authenticate(email,passWord);
                client.Send(mssg);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        public async void NotifyCompanyAboutFailedRequest(string RecieverEmail, string link)
        {
             var emailInfo = await _emailRepository.GetEmailByEmailTypeReturningEmailObjectDtoAsync(EmailType.NotifyingCompanyAboutFailedRequest);
            MimeMessage mssg = new MimeMessage ();
            mssg.From.Add(new MailboxAddress("Agbeloba", "ClhProjectEmail@gmail.com"));
            mssg.To.Add(MailboxAddress.Parse(RecieverEmail));
            mssg.Subject = emailInfo.Content;
            mssg.Body = new TextPart("html")
            {
                Text = emailInfo.Subject + link
            };
            string email = "ClhProjectEmail@gmail.com";
            string passWord = "ClhProjectEmail12345";
            SmtpClient client = new SmtpClient();
            try
            {
                client.Connect("smtp.gmail.com" , 465 , true);
                client.Authenticate(email,passWord);
                client.Send(mssg);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }

        

        }
    }
}