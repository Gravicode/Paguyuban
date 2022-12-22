namespace Paguyuban.Data
{
    public class AppState
    {

        public event Action<string> OnProfileChange;
        public event Action<string> OnChatChange;

        public void RefreshProfile(string username)
        {            
            ProfileStateChanged(username);
        }
        public void RefreshChat(string username)
        {

            ChatStateChanged(username);
        }

        private void ProfileStateChanged(string username) => OnProfileChange?.Invoke(username);
        private void ChatStateChanged(string username) => OnChatChange?.Invoke(username);
    }
}
