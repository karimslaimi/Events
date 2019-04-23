

namespace Service { 

    public interface IServiceMS
    {

         void sendSMS(string body, string phone);
        void sendMail(string mails, string obj, string body);

    }
}
