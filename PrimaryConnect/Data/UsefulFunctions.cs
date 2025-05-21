using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ClosedXML.Excel;
namespace PrimaryConnect.Data
{
    public class UsefulFunctions
    {



        public enum DeliveryMethode
{
    Email = 1 , Pickup =2
};
      static  public string GenerateandSendKey(string Email)
        {
            // معلومات بريد Gmail (المرسل)
            string senderEmail = "abani.abdelkader123@gmail.com"; // بريد Gmail الخاص بك
            string appPassword = "jydz zzsk bkyz bywy"; // كلمة مرور التطبيق التي تم إنشاؤها

            // توليد رقم عشوائي بين 100000 و 999999 (6 أرقام)
            Random random = new Random();
            int confirmationKey = random.Next(100000, 999999);

            // إعداد رسالة البريد
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(senderEmail);
            mailMessage.Subject = "Confirmation key";
            mailMessage.To.Add(Email);
            mailMessage.Body = confirmationKey.ToString();
            mailMessage.IsBodyHtml = true;

            // إعداد خادم SMTP لـ Gmail
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(senderEmail, appPassword),
                Port = 587
            };

            try
            {
                smtpClient.Send(mailMessage);
                return confirmationKey.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw;
            }
        }

        // static  public string GenerateandSendKey(string Email)
        // {
        //     string fMail = "";

        //     MailMessage mailMessage = new MailMessage();
        //     mailMessage.From=new MailAddress(Email);
        //     mailMessage.Subject = "Confermation key";
        //     mailMessage.To.Add(Email);
        //     int num = Convert.ToInt32(RandomNumberGenerator.Create());
        //     mailMessage.Body = (num%1000000).ToString();
        //     mailMessage.IsBodyHtml=false;
        //     string szzezz = "";
        //     SmtpClient smtpClient = new SmtpClient("satp-mail.outlook.com")
        //     {
        //         EnableSsl = true,
        //         Credentials = new NetworkCredential(fMail, szzezz)
        //         ,Port=587

        //     };
        //     smtpClient.Send(mailMessage);
        //     return num.ToString();
        // }


public static string GetMimeType(string fileName) {
     var extension = Path.GetExtension(fileName).ToLowerInvariant();     return extension switch
     {         ".pdf" => "application/pdf",
         ".jpg" => "image/jpeg",         ".jpeg" => "image/jpeg",
         ".png" => "image/png",         ".gif" => "image/gif",
         ".txt" => "text/plain",         ".doc" => "application/msword",
         ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",         ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
         ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",         _ => "application/octet-stream"
     }; }



public static MemoryStream CreateExcelWithDropdowns(
    List<string> parents,
    List<string> schools,
    List<string> classes
)
        {
            var stream = new MemoryStream();
            var wb = new XLWorkbook();

            // Options Sheet
            var wsOptions = wb.AddWorksheet("Options");
            wsOptions.Cell("A1").Value = "Parents";
            wsOptions.Cell("B1").Value = "Schools";
            wsOptions.Cell("C1").Value = "Classes";

            for (int i = 0; i < parents.Count; i++)
                wsOptions.Cell(i + 2, 1).Value = parents[i];

            for (int i = 0; i < schools.Count; i++)
                wsOptions.Cell(i + 2, 2).Value = schools[i];

            for (int i = 0; i < classes.Count; i++)
                wsOptions.Cell(i + 2, 3).Value = classes[i];

            // Named Ranges
            wb.NamedRanges.Add("ParentList", wsOptions.Range(2, 1, parents.Count + 1, 1));
            wb.NamedRanges.Add("SchoolList", wsOptions.Range(2, 2, schools.Count + 1, 2));
            wb.NamedRanges.Add("ClassList", wsOptions.Range(2, 3, classes.Count + 1, 3));

            // Students Sheet
            var wsStudents = wb.AddWorksheet("Students");
            wsStudents.Cell("A1").Value = "Student";
            wsStudents.Cell("B1").Value = "Degree";
            wsStudents.Cell("C1").Value = "Parent";
            wsStudents.Cell("D1").Value = "School";
            wsStudents.Cell("E1").Value = "Class";

            // Dropdowns
            wsStudents.Column("C").SetDataValidation().List("ParentList");
            wsStudents.Column("D").SetDataValidation().List("SchoolList");
            wsStudents.Column("E").SetDataValidation().List("ClassList");

            wb.SaveAs(stream);
            stream.Position = 0;
            return stream;
        }
    }

}
