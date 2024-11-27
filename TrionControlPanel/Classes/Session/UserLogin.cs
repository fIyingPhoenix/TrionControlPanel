using TrionControlPanel.Classes.Models;
using TrionControlPanel.Classes.Services;
using TrionControlPanel.Pages;

namespace TrionControlPanel.Classes.Session
{
    public class UserLogin
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserModelWeb userSession = new();
        public UserModelWeb loadedSession;

        public void SaveUserSession()
        {
            _httpContextAccessor.HttpContext!.Session.SetObject("UserSession", userSession);
        }

        public void LoadUserSession()
        {
            loadedSession = _httpContextAccessor.HttpContext!.Session.GetObject<UserModelWeb>("UserSession");
        }
    }
}
