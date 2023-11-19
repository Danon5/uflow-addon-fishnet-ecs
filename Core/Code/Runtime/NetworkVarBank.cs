using System;
using System.Runtime.CompilerServices;
using FishNet.Object.Synchronizing;
using FishNet.Object.Synchronizing.Internal;
using FishNet.Serializing;
using UFlow.Core.Runtime;

namespace UFlow.Addon.FishNetECS.Core.Runtime {
    public sealed class NetworkVarBank<T> : SyncBase, ICustomSync {
        private readonly IdStack m_idStack;
        private T[] m_bank;
        
        public object GetSerializedType() => null;
        
        protected override void WriteDelta(PooledWriter writer, bool resetSyncTick = true) {
            base.WriteDelta(writer, resetSyncTick);
            writer.WriteByte((byte)ChangeType.Delta);
        }

        protected override void WriteFull(PooledWriter writer) {
            base.WriteFull(writer);
            writer.WriteByte((byte)ChangeType.Full);
            writer.WriteUInt16((ushort)m_bank.Length);
        }

        protected override void Read(PooledReader reader, bool asServer) {
            base.Read(reader, asServer);
            var length = reader.ReadUInt16();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureBankLength(int length) {
            m_bank ??= Array.Empty<T>();
            UFlowUtils.Collections.EnsureLength(ref m_bank, length);
        }

        private enum ChangeType : byte {
            Delta,
            Full
        }
    }
}