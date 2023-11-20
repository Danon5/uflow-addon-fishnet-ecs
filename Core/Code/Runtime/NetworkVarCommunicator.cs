using System;
using System.Collections.Generic;
using FishNet;
using FishNet.Managing;
using FishNet.Object.Synchronizing;
using FishNet.Object.Synchronizing.Internal;
using FishNet.Serializing;

namespace UFlow.Addon.FishNetECS.Core.Runtime {
    public sealed class NetworkVarCommunicator : SyncBase, ICustomSync {
        private readonly Dictionary<Type, INetworkVarBank> m_banks = new();
        private readonly Dictionary<Type, ushort> m_typeToIdMap = new();
        private readonly Dictionary<ushort, Type> m_idToTypeMap = new();
        private NetworkManager m_networkManager;

        public object GetSerializedType() => null;

        protected override void Initialized() {
            base.Initialized();
            m_networkManager = InstanceFinder.NetworkManager;
        }

        protected override void WriteDelta(PooledWriter writer, bool resetSyncTick = true) {
            base.WriteDelta(writer, resetSyncTick);
        }

        protected override void WriteFull(PooledWriter writer) {
            base.WriteFull(writer);
        }

        protected override void Read(PooledReader reader, bool asServer) {
            base.Read(reader, asServer);
            
        }
    }
}