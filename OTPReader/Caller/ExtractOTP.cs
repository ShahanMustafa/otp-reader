using System;
using System.Text;
using OTPReader.EmailClient;
using OTPReader.Email;
using OpenPop.Mime;

namespace OTPReader.Caller
{
    class ExtractOTP : ICaller
    {
        private EmailDetails _emailDetails;
        private string _otp;
        ClientFactory _clientFactory;
        IEmailClient _client;

        public ClientFactory clientFactory { 
            get => _clientFactory; 
            set => _clientFactory = value; 
        }

        public IEmailClient client { 
            get => _client; 
            set => _client = value; 
        }
        public string otp{
            get => _otp;
            set => _otp = value;
        }

        public EmailDetails emailDetails  // read-write instance property
        {
            get => _emailDetails;
            set => _emailDetails = value;
        }

        public ExtractOTP()
        {
            PreReqs();
            emailDetails = null;
            otp = null;
        }

        /// <summary>
        /// Register encoding provider
        /// </summary>
        private void PreReqs()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var enc1252 = Encoding.GetEncoding(1252);

        }

        private void CreateClient(string domain)
        {
            clientFactory = new ClientFactory();
            client = (IEmailClient)clientFactory.CreateClient(domain);
        }

        /// <summary>
        /// Connect to the client of particular domain
        /// </summary>
        /// <param name="domain">Domain name of email</param>
        /// <param name="userEmailAddr">Email adderess where OTP is sent</param>
        /// <param name="userPassword">Password of the email where OTP is sent</param>
        /// <returns>Returns 1 if client connected and -1 if not</returns>
        public int ConnectToClient
            (string domain, string userEmailAddr, string userPassword)
        {
            CreateClient(domain);
      
            if(client != null)
            {
                emailDetails = new EmailDetails(client.ConnectAndAuthEmailClient
                (userEmailAddr, userPassword), domain);
            }

            if (emailDetails != null)
                return 1;
            else
                return -1;
        }

        /// <summary>
        /// Connect to the client of particular domain. Requires credentials , hostname and port
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="userEmailAddr"></param>
        /// <param name="userPassword"></param>
        /// <param name="hostName"></param>
        /// <param name="port"></param>
        /// <returns>Returns 1 if client connected and -1 if not</returns>
        public int ConnectToClient
            (string domain, string userEmailAddr, string userPassword, string hostName, int port)
        {
            CreateClient(domain);

            if (client != null)
            {
                emailDetails = new EmailDetails(client.ConnectAndAuthEmailClient
                (userEmailAddr, userPassword,hostName, port), domain);
            }

            if (emailDetails != null)
                return 1;
            else
                return -1;
        }

        /// <summary>
        /// Get OTP code from the email
        /// </summary>
        /// <param name="textBeforeOTP">Text present before otp</param>
        /// <returns>OTP code or -1 if OTP extraction fails</returns>
        public string GetOtp(string from, string subject, string textBeforeOTP)
        {
            EmailParser emailParser = new EmailParser();
         
            Message msg = emailDetails.GetEmailMsg(from, subject);

            String emailAsTxt = emailParser.ConvertEmailToTxt(msg);

            _otp = emailParser.ExtractOTP(emailAsTxt, textBeforeOTP);

            if (_otp != null )
                return _otp;
            else 
            { 
                Logger.LogError("OTP not found");
                return "-1";
            }


        }

    }
}
