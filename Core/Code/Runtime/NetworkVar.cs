using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UFlow.Addon.FishNetECS.Core.Runtime {
    [Serializable]
    public sealed class NetworkVar<T> {
        [SerializeField, LabelText("Value"), HideIf(nameof(IsPlayingAndInitialized))]
        private T m_authoringValue;
        [SerializeField, LabelText("Value"), ShowIf(nameof(IsPlayingAndInitialized))] 
        private T m_runtimeValue;
        private bool m_initialized;
        
        private bool IsPlayingAndInitialized => Application.isPlaying && m_initialized;

        public void Register() {
            if (m_initialized)
                throw new Exception("Attempting to initialize a NetworkVar twice.");
            
            m_initialized = true;
        }
    }
}