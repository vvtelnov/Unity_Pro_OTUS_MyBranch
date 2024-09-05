using Lessons.Architecture.PM.CharacterPopupPresenter;
using Lessons.Architecture.PM.Player;
using Lessons.Architecture.PM.PopUpHelper;
using Lessons.Architecture.PM.PopupView;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject CharacterPopup;
        
        public override void InstallBindings()
        {
            Container.Bind<CharacterStats>().AsSingle();
            Container.Bind<PlayerLevel>().AsSingle();
            Container.Bind<UserInfo>().AsSingle();
            Container.Bind<CharacterInitInfo>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerInfoSetter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PopupPresenterFactory>().AsSingle();
        } 
    }
}