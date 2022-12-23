namespace Paguyuban.Data
{
    public class AppState
    {

        public event Action<string> OnProfileChange;
        public event Action<string> OnChatChange;
        public event Action<string> OnNotesAdded;
        public event Action<string> OnTodosAdded;

        public void RefreshProfile(string username)
        {            
            ProfileStateChanged(username);
        }
        public void RefreshChat(string username)
        {
            ChatStateChanged(username);
        }
        
        public void RefreshNote(string username)
        {
            NoteStateChanged(username);
        } 
        
        public void RefreshTodo(string username)
        {
            TodoStateChanged(username);
        }

        private void ProfileStateChanged(string username) => OnProfileChange?.Invoke(username);
        private void ChatStateChanged(string username) => OnChatChange?.Invoke(username);
        private void NoteStateChanged(string username) => OnNotesAdded.Invoke(username);
        private void TodoStateChanged(string username) => OnTodosAdded.Invoke(username);
    }
}
