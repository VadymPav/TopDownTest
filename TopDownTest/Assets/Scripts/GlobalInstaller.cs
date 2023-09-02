using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller<GlobalInstaller>
{
    public DataManager Config;
    public GameManager GameManager;
    public PlayerController Player;
    public EnemyAI Enemy;
    public override void InstallBindings()
    {
        Container.Bind<DataManager>().FromInstance(Config).AsSingle();
        Container.Bind<GameManager>().FromInstance(GameManager).AsSingle();
        Container.Bind<PlayerController>().FromInstance(Player).AsSingle();
        Container.Bind<EnemyAI>().FromInstance(Enemy).AsSingle();
    }
}