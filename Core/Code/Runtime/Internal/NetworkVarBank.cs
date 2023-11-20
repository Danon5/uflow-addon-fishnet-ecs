using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FishNet.Serializing;
using UFlow.Core.Runtime;

namespace UFlow.Addon.FishNetECS.Core.Runtime {
    internal sealed class NetworkVarBank<T> : INetworkVarBank {
        private readonly IdStack m_idStack;
        private readonly HashSet<int> m_changedIndices;
        private T[] m_bank;
        
        public NetworkVarBank() {
            m_idStack = new IdStack(1);
            m_changedIndices = new HashSet<int>();
        }
        
        public object GetSerializedType() => typeof(T);

        public void WriteDelta(PooledWriter writer) {
            writer.WriteByte((byte)ChangeType.Delta);
            writer.WriteUInt16((ushort)m_changedIndices.Count);
            foreach (var index in m_changedIndices) {
                writer.WriteUInt16((ushort)index);
                writer.Write(m_bank[index]);
            }
            m_changedIndices.Clear();
        }

        public void WriteFull(PooledWriter writer) {
            writer.WriteByte((byte)ChangeType.Full);
            writer.WriteUInt16((ushort)m_bank.Length);
            foreach (var value in m_bank)
                writer.Write(value);
        }

        public void Read(PooledReader reader, bool asHostClient) {
            var changeType = (ChangeType)reader.ReadByte();
            var length = reader.ReadUInt16();
            EnsureBankLength(length);
            switch (changeType) {
                case ChangeType.Delta:
                    for (var i = 0; i < length; i++)
                        m_bank[reader.ReadUInt16()] = reader.Read<T>();
                    break;
                case ChangeType.Full:
                    for (var i = 0; i < length; i++)
                        m_bank[i] = reader.Read<T>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public int ReserveIndex() {
            var newIndex = m_idStack.GetNextId();
            EnsureBankLength(newIndex + 1);
            return newIndex;
        }

        public void ReleaseIndex(int index) {
            m_idStack.RecycleId(index);
            m_bank[index] = default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void MarkIndexDirty(int index) => m_changedIndices.Add(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T GetValue(int index) => ref m_bank[index];
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValue(int index, in T newValue) {
            ref var currentValue = ref m_bank[index];
            if (currentValue.Equals(newValue)) return;
            currentValue = newValue;
            MarkIndexDirty(index);
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