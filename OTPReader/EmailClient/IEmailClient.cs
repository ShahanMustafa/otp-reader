using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPop.Pop3;

namespace OTPReader.EmailClient
{
    interface IEmailClient
    {
        int port { get; set; }
        string hostName { get; set; }
        Pop3Client client { get; set; }

        /// <summary>
        /// Connects and authenticate user credentials. Default hostname and port will be used
        /// </summary>
        /// <param name="emailAddr"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Pop3Client ConnectAndAuthEmailClient(string emailAddr, string password);

        /// <summary>
        /// Connects and authenticate user credentials. User provides credentials as well as hostname and port
        /// </summary>
        /// <param name="emailAddr"></param>
        /// <param name="password"></param>
        /// <param name="hostname"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        Pop3Client ConnectAndAuthEmailClient(string emailAddr, string password, string hostname, int port);



    }
}
