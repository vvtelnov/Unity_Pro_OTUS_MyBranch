using Lessons.Architecture.PM.Player;
using UnityEngine;

namespace Lessons.Architecture.PM.CharacterPopupPresenter
{
    public interface IPopupPresenterFactory
    {
        public ICharacterPopupPresenter CreatePresenter();
    }
    
    public class PopupPresenterFactory : IPopupPresenterFactory
    {
        private PlayerLevel _playerLevel;
        private UserInfo _userInfo;
        private CharacterStats _characterStats;

        public PopupPresenterFactory(PlayerLevel playerLevel, UserInfo userInfo, CharacterStats characterStats)
        {
            _playerLevel = playerLevel;
            _userInfo = userInfo;
            _characterStats = characterStats;
        }

        public ICharacterPopupPresenter CreatePresenter()
        {
            bool canLevelUp = _playerLevel.CanLevelUp();
            uint requiredExperience = _playerLevel.RequiredExperience;
            var stats = _characterStats.GetStats();

            return new PopupPresenter(
                stats: stats,
                name: _userInfo.Name,
                description: _userInfo.Description,
                icon: _userInfo.Icon,
                currentLevel: _playerLevel.CurrentLevel,
                currentXp: _playerLevel.CurrentExperience,
                maxBarXp: requiredExperience,
                canLevelUp: canLevelUp
            );
        }
    }
}