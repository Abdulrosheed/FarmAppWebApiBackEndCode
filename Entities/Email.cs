namespace FirstProject.Entities
{
    public class Email : BaseEntity
    {
        public string Content {get; set;}
        public string Subject {get; set;}
        public EmailType EmailType {get;set;}
    }
}