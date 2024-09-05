using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Lessons.Architecture.PM.CharacterPopupPresenter;
using Lessons.Architecture.PM.Player;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Lessons.Architecture.PM.PopupView
{
    public class CharacterPopupView : MonoBehaviour, IPopupView
    {
        [Title("Character Fields")]
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _currentLevel;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _profilePicture;
        
        [Title("Character XP")]
        [SerializeField] private TMP_Text _xp;
        [SerializeField] private GameObject _fullBar;
        [SerializeField] private GameObject _notFullBar;
        [SerializeField] private Image _notFullBarImage;


        [Title("Character Stats")]
        [SerializeField] private TMP_Text _moveSpeedStat;
        [SerializeField] private TMP_Text _staminaStat;
        [SerializeField] private TMP_Text _dexterityStat;
        [SerializeField] private TMP_Text _intelligenceStat;
        [SerializeField] private TMP_Text _damageStat;
        [SerializeField] private TMP_Text _regenerationStat;
        
        [Title("SubmitBtn")]
        [SerializeField] private GameObject _inactiveBtn;
        [SerializeField] private GameObject _activeBtn;

        private uint _maxBarXp;
        private uint _currentXp;
        private bool _isXpBarFull;
        private bool _canLevelUp;
        

        void IPopupView.Open(ICharacterPopupPresenter args)
        {
            gameObject.SetActive(true);
            
            _name.text = args.Name;
            _description.text = args.Description;
            _currentLevel.text = args.CurrentLevel;
            _xp.text = args.XpFullStr;
            _profilePicture.sprite = args.Icon;
            
            _maxBarXp = args.MaxBarXp;
            _currentXp = args.CurrentXp;
            _isXpBarFull = args.IsXpBarFull;
            _canLevelUp = args.CanLevelUp;

            if (_canLevelUp)
                ToggleUpdateBtnToActive();
            else
                ToggleUpdateBtnToInactive();

            if (_isXpBarFull)
                ToggleXpToFull();
            else
                ToggleXpToNotFull();
            
            // Вот это нарушает OCP, но я не придумал как это обойти и решил сделать по KISS
            _moveSpeedStat.text = args.CharacterStats[Stats.MOVE_SPEED];
            _staminaStat.text = args.CharacterStats[Stats.STAMINA];
            _dexterityStat.text = args.CharacterStats[Stats.DEXTERITY];
            _intelligenceStat.text = args.CharacterStats[Stats.INTELLIGENCE];
            _damageStat.text = args.CharacterStats[Stats.DAMAGE];
            _regenerationStat.text = args.CharacterStats[Stats.REGENERATION];
        }


        void IPopupView.Close()
        {
            gameObject.SetActive(false);
        }
        
        private void SetXpBarFillingWidth(uint currXp, uint maxXp)
        {
            float percentageOfFilling = currXp / maxXp;

            _notFullBarImage.fillAmount = percentageOfFilling;
        }

        private void ToggleUpdateBtnToActive()
        {
            _inactiveBtn.SetActive(false);
            _activeBtn.SetActive(true);
        }

        private void ToggleUpdateBtnToInactive()
        {
            _inactiveBtn.SetActive(true);
            _activeBtn.SetActive(false);
        }

        private void ToggleXpToFull()
        {
           _fullBar.SetActive(true); 
           _notFullBar.SetActive(false);
        }

        private void ToggleXpToNotFull()
        {
            _fullBar.SetActive(false); 
            _notFullBar.SetActive(true);
            SetXpBarFillingWidth(_currentXp, _maxBarXp);
        }

    }
}