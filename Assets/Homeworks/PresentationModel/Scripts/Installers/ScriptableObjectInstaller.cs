using Lessons.Architecture.PM.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM.Installers
{
    [CreateAssetMenu(menuName = "Data/New DataInstaller", fileName = "DataInstaller")]
    public class DataInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private CharacterPopupElements _popupElements;

        public override void InstallBindings()
        {
            Container.Bind<CharacterPopupElements>().FromInstance(_popupElements).AsSingle();
        }
    }
}