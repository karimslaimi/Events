using MessageBird;
using System;

using System.Net;
using System.Net.Mail;
using System.Text;


namespace Service
{
    public class ServiceMS : IServiceMS
    {


        public void sendMail(string mails,string obj,string body)
        {
           
                try
                {
                    string sendermail = System.Configuration.ConfigurationManager.AppSettings["SenderMail"].ToString();
                    string senderpassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    client.EnableSsl = true;
                    client.Timeout = 1000000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    MailMessage mailMessage = new MailMessage();//sendermail, spu.GetById(id).mail, "verify your mail", "http://localhost:8080/User/verifymail/?id=" + id + "&key=" + key);
                    mailMessage.To.Add(mails);
                    mailMessage.From=new MailAddress("cck@rnu.com");
                    mailMessage.Body = body;    
                    client.Credentials = new NetworkCredential(sendermail, senderpassword);
                    mailMessage.IsBodyHtml = true;
                    mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                    client.Send(mailMessage);

                }
                catch (Exception)
                {

                }
            }
        

        public void sendSMS(string body,string phone)
        {
            const string YourAccessKey = "PlARM4zTsShj1IDg9q3Fwa3UM";
            Client client = Client.CreateDefault(YourAccessKey);
            long Msisdn = long.Parse("00216" + phone);

            MessageBird.Objects.Message message =
            client.SendMessage("AnnonceRNU", body, new[] { Msisdn });
        }
    }
}
