using System;
using Lessons.Architecture.PM.CharacterPopupPresenter;

namespace Lessons.Architecture.PM.PopupView
{
    public interface IPopupView
    {
        public void Open(ICharacterPopupPresenter args);
        public void Close();
    }

    public interface IPopupEventEmitter
    {
        public event Action OnClose;
        public event Action OnActionButtonClick;
    }
}