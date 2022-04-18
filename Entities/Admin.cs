namespace FirstProject.Entities
{
    public class Admin : UserInfo
    {
        public int UserId {get;set;}
        public User User {get;set;}
    }
}