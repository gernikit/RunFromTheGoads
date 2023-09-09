using UniRx;
using Zenject;

public class EventBusInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MessageBroker>().To<MessageBroker>().FromNew().AsSingle();
    }
}
