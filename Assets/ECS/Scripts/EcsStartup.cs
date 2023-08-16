using Leopotam.EcsLite;
using UnityEngine;

sealed class EcsStartup : MonoBehaviour {

    private EcsWorld _world;
    private IEcsSystems _systems;

    private void Start() {
        _world = new EcsWorld ();
        _systems = new EcsSystems (_world);
        _systems
#if UNITY_EDITOR
            .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())

#endif
            .Init();
    }

    private void Update() {
        _systems?.Run();
    }

    private void OnDestroy() {
        if (_systems != null) {
            _systems.Destroy();
            _systems = null;
        }

        if (_world != null) {
            _world.Destroy();
            _world = null;
        }
    }
}
