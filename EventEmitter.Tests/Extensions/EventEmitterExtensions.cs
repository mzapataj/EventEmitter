namespace EventEmitter.Tests.Extensions
{
    public static class EventEmitterExtensions
    {
        public static void AddEmitEvent(this IEventEmitter eventEmitter, string eventName,
                                        Action<object[]> method, object[] parameters = null)
        {
            eventEmitter.AddEventListener(eventName, method);
            eventEmitter.Emit(eventName, parameters);
        }

    }
}
