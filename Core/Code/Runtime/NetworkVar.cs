using System;

namespace UFlow.Addon.FishNetECS.Core.Runtime {
    [Serializable]
    public sealed class NetworkVar<T> : IDisposable {
        private bool m_initialized;

        public void Initialize() {
            if (m_initialized)
                throw new Exception("Attempting to initialize a NetworkVar twice.");
            m_initialized = true;
        }
        public void Dispose() { }
    }
}