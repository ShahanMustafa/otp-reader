using OpenPop.Pop3;
using System;
using System.Configuration;

namespace OTPReader.EmailClient
{
    class Gmail : IEmailClient
    {

        private int _port;
        private string _hostName;
        private Pop3Client _client = new Pop3Client();

        /// <summary>
        /// Sets hostName and port for Gmail client
        /// </summary>
        public Gmail()
        { }

        public Pop3Client client  // read-write instance property
        {
            get => _client;
            set => _client = value;
        }
        public int port  // read-write instance property
        {
            get => _port;
            set => _port = value;
        }
        public string hostName  // read-write instance property
        {
            get => _hostName;
            set => _hostName = value;
        }

        public void GetConfiguration()
        {
            if (String.IsNullOrEmpty(hostName) && port == 0)
            {
                hostName = ConfigurationManager.AppSettings.Get("gmailHostName");
                port = Int32.Parse(ConfigurationManager.AppSettings.Get("gmailPort"));
            }
            
        }
        public Pop3Client ConnectAndAuthEmailClient(string emailAddr, string password)
        {
            try
            {

                GetConfiguration();
                client.Connect(hostName, port, true);
                client.Authenticate("recent:" + emailAddr, password);
                //Less secure app access
                if (client.Connected == true)
                {
                    Logger.LogDebug("Client Connected to " + hostName);
                    return client;
                }

            }

            catch(Exception e)
            {
                Logger.LogError(e.Message);
                return null;
            }

            Logger.LogError("Client not connected or authenticated. Please verify that provided email adderess and password is correct");
            return null;
        }

        public Pop3Client ConnectAndAuthEmailClient(string emailAddr, string password, string hostname, int port) {

            this.hostName = hostname;
            this.port = port;

            return ConnectAndAuthEmailClient(emailAddr, password);
        }
     

        
    }
}
