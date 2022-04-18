namespace FirstProject.Dtos
{
    public class BaseResponse<T>
    {
        public bool IsSucess {get;set;}
        public string Message {get;set;}
        public T Data {get;set;}
    }
    public class LoginBaseResponse
    {
        public bool IsSucess {get;set;}
        public string Message {get;set;}
        public UserDto Data {get;set;}
        public string Token {get;set;}
    }
}