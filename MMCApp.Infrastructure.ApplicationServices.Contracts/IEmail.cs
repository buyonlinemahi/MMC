
namespace MMCApp.Infrastructure.ApplicationServices.Contracts
{
    public interface IEMail
    {
        void SendEmail(string msg, string sender,string EMailCc, string subject, string[] AttachDoc);
    }
}
