namespace QuestionnaireAPI.Helpers
{
    public static class Utils
    {
    public static bool IsAdmin(this UserIdAndRole userIdAndRole)
    {
        if(userIdAndRole.UserType == Models.UserType.Admin)
            return true;
        
        else
            return false;
                
    }
    }
}