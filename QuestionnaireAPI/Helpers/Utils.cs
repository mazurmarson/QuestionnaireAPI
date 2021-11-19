using System;
using QuestionnaireAPI.Models;

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

    public static UserIdAndRole VariablesToUserIdAndRole(int userId, string userRole)
    {
                    UserType userRoleEnum = (UserType)Enum.Parse(typeof(UserType),userRole, true);
            var userIdAndRole = new UserIdAndRole{
                UserId = userId,
                UserType = userRoleEnum
            };

            return userIdAndRole;
    }
    }
}