using Lessons.Architecture.PM.PopUpHelper;
using UnityEngine;
using Zenject;

namespace Lessons.Architecture.PM.Player
{
    public class PlayerInfoSetter
    {
        private PlayerLevel _playerLevel;
        private UserInfo _userInfo;
        private CharacterStats _characterStats;
        private CharacterInitInfo _characterInitInfo;

        [Inject]
        public void Construct(PlayerLevel playerLevel, UserInfo userInfo, CharacterStats characterStats, CharacterInitInfo characterInitInfo)
        {
            _playerLevel = playerLevel;
            _userInfo = userInfo;
            _characterStats = characterStats;
            _characterInitInfo = characterInitInfo;

            SetInitInfo();
        }

        private void SetInitInfo()
        {
            Debug.Log("Init Info setting");
            _playerLevel.CurrentLevel = _characterInitInfo.Lvl;
            _playerLevel.CurrentExperience = _characterInitInfo.Xp;
            
            _userInfo.ChangeName(_characterInitInfo.Name);
            _userInfo.ChangeDescription(_characterInitInfo.Description);
            _userInfo.ChangeIcon(_characterInitInfo.ProfilePicture);

            foreach (var key in _characterInitInfo.InitStats.Keys)
            {
                CharacterStat charStat = new(key, _characterInitInfo.InitStats[key]);
                _characterStats.AddStat(charStat);
            }
        }
    }
}