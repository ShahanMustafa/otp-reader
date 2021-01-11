using OTPReader.EmailClient;
using OTPReader.Email;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPop.Mime;
using System.Text.RegularExpressions;

namespace OTPReader
{
    class Program
    {
        static void Main(string[] args)
        {/*/
            string otp="";

            //To login in please use
            Console.WriteLine(otp);

            Console.ReadLine();
            /*/

            Logger.InitializeLogger();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var enc1252 = Encoding.GetEncoding(1252);
            ClientFactory clientFactory = new ClientFactory();
           
            string emailAddr = ConfigurationManager.AppSettings.Get("email");
            string password = ConfigurationManager.AppSettings.Get("password");
            string domain  = ConfigurationManager.AppSettings.Get("domain");
            string from = ConfigurationManager.AppSettings.Get("from");
            string subject = ConfigurationManager.AppSettings.Get("subject");
            //string hostName = ConfigurationManager.AppSettings.Get("gmailHostName");
            //string port = ConfigurationManager.AppSettings.Get("gmailPort");

            List<String> sentTo = new List<string>();
            sentTo.Add(emailAddr);

            IEmailClient client = (IEmailClient)clientFactory.CreateClient(domain);

            //EmailDetails email = new EmailDetails(client.ConnectAndAuthEmailClient
              // (emailAddr, password), from, sentTo, subject, domain);
            EmailDetails email = new EmailDetails(client.ConnectAndAuthEmailClient
                (emailAddr, password,"test",121), from, sentTo, subject, domain);
            // TRY OVERLOAD WITH PORT AND HOSTNAME

            EmailParser emailParser = new EmailParser();
            Message msg = email.GetEmailMsg(from, subject);

            String emailAsTxt = emailParser.ConvertEmailToTxt(msg);
            
            String otp = emailParser.ExtractOTP(emailAsTxt, "To login in please use");
            //String otp = emailHandler.ExtractOTP(emailAsTxt);


            Console.WriteLine(otp);

            Console.ReadLine();
            Logger.CloseTheFile();




        }
    }
}
