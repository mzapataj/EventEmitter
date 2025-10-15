namespace EventEmitter
{
    public interface IEventEmitter
    {

        public void AddEventListener(string eventName, Action<object[]> _event);
        public void RemoveEventListener(string eventName, Action<object[]> _event);
        public void RemoveAllListeners(string eventName);
        public void On(string eventName, Action<object[]> _event);
        public void Once(string eventName, Action<object[]> _event);
        public void Off(string eventName, Action<object[]> _event);
        public void Emit(string eventName, object[] parameters);

    }
}