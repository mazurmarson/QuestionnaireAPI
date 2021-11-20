using QuestionnaireAPI.Models;

namespace QuestionnaireAPI.Helpers
{
    public class UserIdAndRole
    {
        public UserType UserType {get; set;}
        public int UserId {get; set;}

     public static bool IsAdmin(UserIdAndRole userIdAndRole)
    {
        if(userIdAndRole.UserType == UserType.Admin)
            return true;
        
        else
            return false;
             
        
    }
    }


}