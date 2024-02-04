using FishNet;
using FishNet.Managing.Timing;
using UFlow.Addon.ECS.Core.Runtime;
using UFlow.Core.Runtime;
using UnityEngine;

namespace UFlow.Addon.FishNetECS.Core.Runtime {
    public sealed class NetworkEcsModule<T> : BaseModule<NetworkEcsModule<T>> where T : BaseWorldType {
        private EcsModule<T> m_ecsModule;
        private TimeManager m_timeManager;

        public World World => m_ecsModule.World;

        public override void LoadDirect() {
            m_ecsModule = EcsModule<T>.Load();
            m_ecsModule.AutoRunSystemGroups = false;
            m_timeManager = InstanceFinder.TimeManager;
            m_timeManager.OnUpdate += OnUpdate;
            m_timeManager.OnFixedUpdate += OnFixedUpdate;
            m_timeManager.OnLateUpdate += OnLateUpdate;
            m_timeManager.OnPreTick += OnPreTick;
            m_timeManager.OnTick += OnTick;
            m_timeManager.OnPostTick += OnPostTick;
            m_timeManager.OnPrePhysicsSimulation += OnPrePhysicsSimulation;
            m_timeManager.OnPostPhysicsSimulation += OnPostPhysicsSimulation;
        }

        public override void UnloadDirect() {
            m_timeManager.OnUpdate -= OnUpdate;
            m_timeManager.OnFixedUpdate -= OnFixedUpdate;
            m_timeManager.OnLateUpdate -= OnLateUpdate;
            m_timeManager.OnPreTick -= OnPreTick;
            m_timeManager.OnTick -= OnTick;
            m_timeManager.OnPostTick -= OnPostTick;
            m_timeManager.OnPrePhysicsSimulation -= OnPrePhysicsSimulation;
            m_timeManager.OnPostPhysicsSimulation -= OnPostPhysicsSimulation;
            EcsModule<T>.Unload();
        }

        private void OnUpdate() => World.RunSystemGroup<UpdateSystemGroup>(Time.deltaTime);

        private void OnFixedUpdate() => World.RunSystemGroup<FixedUpdateSystemGroup>(Time.fixedDeltaTime);

        private void OnLateUpdate() => World.RunSystemGroup<LateUpdateSystemGroup>(Time.deltaTime);

        private void OnPreTick() => World.RunSystemGroup<PreTickSystemGroup>((float)m_timeManager.TickDelta);

        private void OnTick() => World.RunSystemGroup<TickSystemGroup>((float)m_timeManager.TickDelta);

        private void OnPostTick() => World.RunSystemGroup<PostTickSystemGroup>((float)m_timeManager.TickDelta);

        private void OnPrePhysicsSimulation(float delta) => World.RunSystemGroup<PrePhysicsSimulationSystemGroup>(delta);

        private void OnPostPhysicsSimulation(float delta) => World.RunSystemGroup<PostPhysicsSimulationSystemGroup>(delta);
    }
}