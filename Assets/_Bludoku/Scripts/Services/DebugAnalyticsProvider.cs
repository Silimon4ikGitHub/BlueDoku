using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace _Bludoku.Services
{
    public sealed class DebugAnalyticsProvider : IAnalyticsProvider
    {
        public void Initialize()
        {
            Debug.Log($"[DebugAnalyticsProvider] Initialized");
        }

        public void SendEvent(string eventName, IReadOnlyDictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                Debug.Log($"[DebugAnalyticsProvider] SendEvent {eventName}");
                return;
            }

            string body = ($"[DebugAnalyticsProvider] SendEvent {eventName} ");

            foreach (var pair in parameters)
            {
                body += ($" {pair.Key} = {pair.Value}; ");
            }

            Debug.Log(body);
        }
    }
}
