using Cysharp.Threading.Tasks;
using UFlow.Addon.FishNet.Core.Runtime;
using UFlow.Core.Runtime;
using UnityEngine;

namespace UFlow.Addon.FishNetECS.Core.Runtime {
    public abstract class BaseNetworkEcsModule<TModule, TNetworkModule> : 
        BaseNetworkSceneModule<TModule, TNetworkModule> 
        where TModule : BaseNetworkEcsModule<TModule, TNetworkModule>
        where TNetworkModule : BaseAsyncModule<TNetworkModule>, INetworkModule {
        public override string SceneName => "NetworkEcs";
        protected abstract ContentRef<GameObject> NetworkVarSyncerContentRef { get; }

        protected override UniTask ServerSetupAsync() {
            NetworkVarSyncerContentRef.NetworkInstantiate();
            return base.ServerSetupAsync();
        }
    }
}