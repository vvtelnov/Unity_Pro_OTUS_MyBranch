using Homeworks.PresentationModel.Scripts.Player;
using Lessons.Architecture.PM.CharacterPopupPresenter;
using Lessons.Architecture.PM.Player;
using Lessons.Architecture.PM.PopUpHelper;
using Zenject;

namespace Lessons.Architecture.PM.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CharacterStats>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerLevel>().AsSingle();
            Container.BindInterfacesAndSelfTo<UserInfo>().AsSingle();
            Container.Bind<CharacterInitInfoSetter>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<CharacterInfoSetter>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PopupPresenterFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterStatIncreaser>().AsSingle();
        } 
    }
}