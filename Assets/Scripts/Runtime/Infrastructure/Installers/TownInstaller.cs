using Cysharp.Threading.Tasks;
using RussSurvivor.Runtime.Gameplay.Battle.Characters;
using RussSurvivor.Runtime.Gameplay.Common.Cinema;
using RussSurvivor.Runtime.Gameplay.Common.Player;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues;
using RussSurvivor.Runtime.Gameplay.Town.Dialogues.Data;
using RussSurvivor.Runtime.Infrastructure.Scenes;
using RussSurvivor.Runtime.UI.Gameplay.Town.Dialogues;
using UnityEngine;
using Zenject;

namespace RussSurvivor.Runtime.Infrastructure.Installers
{
  public class TownInstaller : MonoInstaller, IInitializable
  {
    [SerializeField] private PlayerSpawnPoint _playerSpawnPoint;
    [SerializeField] private DialogueEntryPresenter _dialogueEntryPresenter;
    private CameraFollower _cameraFollower;

    private ICurtain _curtain;

    [Inject]
    private void Construct(ICurtain curtain, CameraFollower cameraFollower)
    {
      _curtain = curtain;
      _cameraFollower = cameraFollower;
    }

    public async void Initialize()
    {
      await UniTask.WhenAll(
        Container.Resolve<IConversationDataBase>().Initialize(),
        Container.Resolve<IPlayerPrefabProvider>().Initialize());
      _playerSpawnPoint.Initialize();
      _cameraFollower.Initialize(Container.Resolve<IPlayerRegistry>().GetPlayer());
      _dialogueEntryPresenter.Initialize();
      _curtain.Hide();
    }

    public override void InstallBindings()
    {
      Container
        .BindInterfacesTo<TownInstaller>()
        .FromInstance(this)
        .AsSingle();

      Container
        .Bind(typeof(IPlayerRegistry), typeof(ITownPlayerRegistry))
        .To<TownPlayerRegistry>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IConversationDataBase>()
        .To<ConversationDataBase>()
        .FromNew()
        .AsSingle();

      Container
        .Bind<IDialogueSystem>()
        .To<DialogueSystem>()
        .FromNew()
        .AsSingle();
    }
  }
}