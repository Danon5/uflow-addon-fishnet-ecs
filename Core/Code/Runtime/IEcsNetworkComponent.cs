using UFlow.Addon.ECS.Core.Runtime;

namespace UFlow.Addon.FishNetECS.Core.Runtime {
    public interface IEcsNetworkComponent : IEcsComponent {
        void RegisterNetworkVars();
    }
}