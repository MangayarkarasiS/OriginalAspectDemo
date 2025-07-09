using StudentService.Models;

namespace StudentService.AuthFolder
{
    public interface IAuth
    {
        string Authentication(UserCredentials userCredential);
    }
}
