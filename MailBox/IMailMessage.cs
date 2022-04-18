using FirstProject.Dtos;

namespace FirstProject.MailBox
{
    public interface IMailMessage
    {
        public void RegistrationNotificationEmail (string RecieverEmail , string link);
        public void RegistrationNotificationForFarm (string RecieverEmail);
        public void UpdateNotificationEmail (string RecieverEmail , string link);
        public void NotifyFarmInspectorAboutToBeInspectedFarm(string farmInspectorEmail,string farmerLink );
        public void NotifyFarmerAboutFarmInspectorEmail (string farmerEmail,string farmInsectorLink );
        public void NotifyCompanyAboutOrder(string RecieverEmail);
        public void NotifyCompanyAboutFailedRequest(string RecieverEmail , string link);
        public void RegistrationNotificationForFarmInspector (string RecieverEmail,string FarmInspectorPassWord,string link);

    }
}