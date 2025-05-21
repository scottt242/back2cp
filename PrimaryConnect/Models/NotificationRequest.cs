namespace PrimaryConnect.Models
{

    public enum UserType

   {
         Teacher,
        Parent
    }

    public class NotificationRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; } // معرف المستخدم (AdminId, TeacherId, ParentId)
        public UserType UserType { get; set; }
        public string Message { get; set; }
        public string Title  {get;set;}
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
    }

}