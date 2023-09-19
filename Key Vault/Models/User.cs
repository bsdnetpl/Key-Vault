namespace Key_Vault.Models
{
    public class User
    {
        public int id { set; get; }
        public string name { set; get; }
        public string email { set; get; }
        public string password { set; get; }
        public DateTime dateTimeCreate { set; get; }
        public Logs logs { set; get; }
    }
}
