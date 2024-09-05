using System;
using Lessons.Architecture.PM.CharacterPopupPresenter;
using Lessons.Architecture.PM.PopupView;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace Lessons.Architecture.PM.PopUpHelper
{
    public class PopupFactoryOpener : MonoBehaviour
    {
        [SerializeField]
        private GameObject _characterPopupPrefab;
        
        private DiContainer _container;
        private IPopupPresenterFactory _presenterFactory;

        [Inject]
        public void Constructor(DiContainer container, IPopupPresenterFactory factory)
        {
            _container = container;
            _presenterFactory = factory;
        }
        
        [Button(ButtonSizes.Large)]
        private void OpenPopup()
        {
            Debug.Log(_presenterFactory);
            var args = _presenterFactory.CreatePresenter();
            Debug.Log(args);
            Open(args);
        }

        private void Open(ICharacterPopupPresenter args)
        {
            CharacterPopupView popup = _container.InstantiatePrefabForComponent<CharacterPopupView>(_characterPopupPrefab);

            if (popup is null)
            {
                throw new NullReferenceException("There is no view script (e.g. CharacterPopupView) on prefab");
            }
            
            ((IPopupView)popup).Open(args);
        }
    }
}