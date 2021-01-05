using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataAccess
{
    public class HamXuLy
    {


        public static string MaHoa(string pass)
        {
            MD5 md5hash = new MD5CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();
            byte[] hashBytes;
            hashBytes = md5hash.ComputeHash(encoder.GetBytes(pass));
            return BitConverter.ToString(hashBytes);
        }

        public static string RandomString(int length, bool LowerString, bool Number)
        {
            Random AppRandom = new Random((int)DateTime.Now.Ticks);
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (Number)
            {
                str = str + "0123456789";
            }
            if (LowerString)
            {
                str = str + "abcdefghijklmnopqrstuvwxyz";
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int r = AppRandom.Next(0, str.Length);
                sb.Append(str.Substring(r, 1));
            }
            return sb.ToString();
        }

        [Obsolete]
        public static bool SendMail(string to_address, string subject, string body)
        {
            try
            {

                string from_email = ConfigurationSettings.AppSettings["fromemail"];
                string from_password = ConfigurationSettings.AppSettings["frompass"];
                string from_name = ConfigurationSettings.AppSettings["fromname"];

                MailMessage msg = new MailMessage();
                msg.To.Add(to_address);
                msg.From = new MailAddress(from_email, from_name, Encoding.UTF8);
                msg.Subject = subject;
                //
                msg.SubjectEncoding = Encoding.UTF8;
                msg.Body = body;
                msg.BodyEncoding = Encoding.UTF8;
                msg.IsBodyHtml = true;
                //
                msg.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                NetworkCredential _Credentials = new NetworkCredential(from_email, from_password);
                client.Credentials = _Credentials;
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Send(msg);
                return true;

            }
            catch
            {
                return false;
            }
        }
     
    }
}
