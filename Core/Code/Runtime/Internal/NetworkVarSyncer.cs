using FishNet.Object;
using UFlow.Core.Runtime;

namespace UFlow.Addon.FishNetECS.Core.Runtime {
    internal sealed class NetworkVarSyncer : NetworkBehaviour {
        private readonly NetworkVarCommunicator m_communicator = new();
        
        public static NetworkVarSyncer Singleton { get; private set; }

        private void Awake() => Singleton = this;

        public override void OnStartNetwork() {
            base.OnStartNetwork();
            UFlowUtils.Scenes.MoveObjectToScene(gameObject, "NetworkEcs");
        }
    }
}