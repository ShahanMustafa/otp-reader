using mshtml;
using OpenPop.Mime;
using System.Text;
using System.Text.RegularExpressions;

namespace OTPReader.Email
{
    public class EmailParser
    {
        string nullEmailErrorString = "Error : No Email found";
        public string ConvertEmailToTxt(Message msg)
        {
            if (msg != null) { 
            StringBuilder builder = new StringBuilder();
            MessagePart html = msg.FindFirstHtmlVersion();

            builder.Append(html.GetBodyAsText());
            HTMLDocument htmldoc = new HTMLDocument();
            IHTMLDocument2 htmldoc2 = (IHTMLDocument2)htmldoc;
            htmldoc2.write(new object[] { builder.ToString() });
            string emailasTxt = htmldoc2.body.outerText;

            if (!string.IsNullOrEmpty(emailasTxt))
            {
                Logger.LogDebug("Email converted to Text");
                return emailasTxt;
            }
            

            else
            {
                Logger.LogError("Email converter to Text Failed");
                return null;
            }
            }

            else
            {
                Logger.LogError("Email message cannot be coverted to text");
                return null;
            }
        }

        /// <summary>
        /// Extracts and return OTP present at the beginning of email, returns -1 if OTP extraction fails
        /// </summary>
        /// <param name="emailasTxt">Email as text</param>
        /// <returns>Returns OTP code if succesfull, else returns -1</returns>
        public string ExtractOTP(string emailasTxt)
        {
            if (string.IsNullOrEmpty(emailasTxt))
            {
                Logger.LogDebug("OTP Extracted");
                return ExtractOTP(emailasTxt, "");
            }
        
            else
            {
                Logger.LogDebug("Email or string brfore otp was empty or null");
                Logger.LogError("OTP Extraction failed");
                return "-1";
            }
        }

        /// <summary>
        /// Extracts and return OTP present after string from email, returns -1 if OTP extraction fails.
        /// 
        /// </summary>
        /// <param name="emailasTxt">Email as text</param>
        /// <param name="StringBeforeOTP">String before OTP code</param>
        /// <returns> Returns OTP code if succesfull, else returns -1 </returns>
        public string ExtractOTP(string emailasTxt, string StringBeforeOTP)
        {
            if (!string.IsNullOrEmpty(emailasTxt) && StringBeforeOTP != null) 
            { 
            string sResult = Regex.Replace(emailasTxt, @"\t|\r", "");
            sResult = Regex.Replace(sResult, @"\n", " ");

            int index = sResult.IndexOf(StringBeforeOTP);
            string substring = sResult.Substring(index + StringBeforeOTP.Length);

            substring = RemoveSpecialCharacters(substring);
            substring = RemoveWhitespaceBeforeString(substring);
            substring = ExtractStringTillFirstWhitespace(substring);

            Logger.LogDebug("OTP Extracted");
            return substring;
            }
            else
            {
                Logger.LogDebug("Email or string brfore otp was empty or null");
                Logger.LogError("OTP Extraction failed");
                return "-1";
            }
                
        }

        /// <summary>
        /// Removes special character from string
        /// </summary>
        /// <param name="Text">String to be parsed</param>
        /// <returns>Parsed String</returns>
        public string RemoveSpecialCharacters(string Text)
        {
            string StringWihtoutcharacters = Regex.Replace(Text, @"[^0-9a-zA-Z ]+", "");//remove evertything from string except 0 to 9 digits.

            return StringWihtoutcharacters;
        }

        /// <summary>
        /// Removes whitespace before a string
        /// </summary>
        /// <param name="Text">String to be parsed</param>
        /// <returns>Parsed String</returns>
        public string RemoveWhitespaceBeforeString(string Text)
        {
            Regex rx = new Regex(@"^\p{Zs}", RegexOptions.Compiled);
            string WOSpaceAtStart = rx.Replace(Text.Trim(), string.Empty);

            return WOSpaceAtStart;
        }

        /// <summary>
        /// Extract string until first appearance of whitespace
        /// </summary>
        /// <param name="Text">String to be parsed</param>
        /// <returns>Parsed string</returns>
        public string ExtractStringTillFirstWhitespace(string Text)
        {
            string StringTillFirstWhitespace = Text.Substring(0, Text.IndexOf(' '));

            return StringTillFirstWhitespace;
        }

    }
}
