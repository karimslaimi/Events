using MessageBird;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

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


        public void sendSMS(string body, string phone)
        {
            //const string YourAccessKey = "k4lCCTW1NKDvQhWEszuWNeXjy";
            //Client client = Client.CreateDefault(YourAccessKey);
            //long Msisdn = long.Parse("00216" + phone);

            //MessageBird.Objects.Message message =
            //client.SendMessage("AnnonceRNU", body, new[] { Msisdn });
            String message = HttpUtility.UrlEncode("This is your message");
            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues("https://api.txtlocal.com/send/", new NameValueCollection()
                {
                {"apikey" , "XF2tuaNRaZ8-tuuzMiApYRolMvapWyUuxhRBx6c0xo"},
                {"numbers" , "00216"+phone},
                {"message" , body},
                {"sender" , "AnnonceRnu"}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);

            }
        }
    }
}
