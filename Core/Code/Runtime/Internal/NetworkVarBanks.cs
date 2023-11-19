using System;
using UFlow.Core.Runtime;

namespace UFlow.Addon.FishNetECS.Core.Runtime {
    internal static class NetworkVarBanks<T> {
        private static T[] s_bank;

        static NetworkVarBanks() {
            UnityGlobalEventHelper.RuntimeInitializeOnLoad += ClearStaticCache;
        }

        private static void ClearStaticCache() {
            if (s_bank != null)
                s_bank = Array.Empty<T>();
        }
    }
}