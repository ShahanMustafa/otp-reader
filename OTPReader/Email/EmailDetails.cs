using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;

namespace OTPReader.Email
{
    public class EmailDetails
    {
        public Message email { get; set; }
        private string sentFrom { get; set; }
        private List<string> sentTo { get; set; }
        private string subject { get; set; }
        public int index { get; set; }
        public string domain { get; set; }
        public Pop3Client client { get; set; }

        public EmailDetails(Pop3Client client, string sentFrom, List<string> sentTo, string subject , string domain)
        {
            this.client = client;
            this.sentFrom = sentFrom;
            this.sentTo = sentTo;
            this.subject = subject;
            this.domain = domain;
        }

        public EmailDetails(Pop3Client client, String domain)
        {
            this.client = client;
            this.domain = domain;
        }
        
        /// <summary>
        /// Returns most recent email on the basis of Sender and Subject
        /// </summary>
        /// <param name="sentFrom">Email Sender</param>
        /// <param name="emailSubject">Email Subject</param>
        /// <returns></returns>
        public Message GetEmailMsg(string _sentfrom, string _subject)
        {
            //sentFrom = _sentfrom;
            //subject = _subject;

            var count = client.GetMessageCount();
            Message msg;
            
            for (int i = count; i > 0; i--)
            {
                msg = client.GetMessage(i);
                if (msg.Headers.From.Address.ToString() == sentFrom && msg.Headers.Subject == subject)
                {
                    Logger.LogDebug("Latest Email from sender " + sentFrom + " with subject " + subject + " found");
                    return msg;
                }
            }

            Logger.LogError("Email from sender " + sentFrom + " with subject " + subject + " NOT found");
            return null;

        }
    }
}
