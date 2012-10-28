namespace Hqub.GlobalSat
{
    public class SimpleLogger
    {
        public enum MessageLevel
        {
            None,
            Success,
            Information,
            Debug,
            DebugError,
            Error
        }

        public delegate void HandleLogger(string message, MessageLevel messageLevel);

        public event HandleLogger EventLog;

        public void Log(string message, MessageLevel messageLevel)
        {
            if(EventLog != null)
            {
                EventLog(message, messageLevel);
            }
        }
    }
}
