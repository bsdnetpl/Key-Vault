namespace Key_Vault.Models
{
    public class Logs
    {
        public int id { set; get; }
        public string Message { set; get; }
        public DateTime dateTime { set; get; }
        public User user { set; get; }
        public int UserId { set; get; }
    }
}
