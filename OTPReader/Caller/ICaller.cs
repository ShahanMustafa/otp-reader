using OTPReader.Email;

namespace OTPReader.Caller
{
    interface ICaller
    {
        int ConnectToClient(string domain, string userEmailAddr, string userPassword);

        string GetOtp(string from, string subject, string textBeforeOTP);

        EmailDetails emailDetails { get; set; }
        string otp { get; set; }

    }
}
