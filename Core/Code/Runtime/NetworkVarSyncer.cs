using FishNet.Object;
using UFlow.Core.Runtime;

namespace UFlow.Addon.FishNetECS.Core.Runtime {
    public sealed class NetworkVarSyncer : NetworkBehaviour {
        public override void OnStartNetwork() {
            base.OnStartNetwork();
            UFlowUtils.Scenes.MoveObjectToScene(gameObject, "NetworkEcs");
        }
    }
}