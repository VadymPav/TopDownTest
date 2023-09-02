using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller<GlobalInstaller>
{
    public DataManager Config;
    public PlayerController Player;
    public override void InstallBindings()
    {
        Container.Bind<DataManager>().FromInstance(Config).AsSingle();
        Container.Bind<PlayerController>().FromInstance(Player).AsSingle();
    }
}