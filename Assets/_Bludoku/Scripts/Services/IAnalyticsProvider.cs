using System.Collections.Generic;

namespace _Bludoku.Services
{
    public interface IAnalyticsProvider
    {
        void Initialize();
        void SendEvent(string eventName, IReadOnlyDictionary<string, string> parameters);
    }
}
