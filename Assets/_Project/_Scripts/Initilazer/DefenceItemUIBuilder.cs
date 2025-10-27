using System.Collections.Generic;
using _Project._Scripts.Cores.Commands;
using _Project._Scripts.Levels;
using _Project._Scripts.Managers;
using _Project._Scripts.Units.Defence;
using DG.Tweening;
using UnityEngine;

namespace _Project._Scripts.Initilazer
{
    public class DefenceItemUIBuilder: Command
    {
        private readonly DefenceUnitUI _defenceUnitUIPrefab;
        private readonly GameObject _buttonsContainer;
        private readonly LevelManager _levelManager;
        
        private List<DefenceUnitUI> _createdUIElements = new List<DefenceUnitUI>();

        public DefenceItemUIBuilder(
            DefenceUnitUI defenceUnitUIPrefab,
            GameObject buttonsContainer,
            LevelManager levelManager)
        {
            _defenceUnitUIPrefab = defenceUnitUIPrefab;
            _buttonsContainer = buttonsContainer;
            _levelManager = levelManager;
        }

        public override void StartCommand()
        {
            _buttonsContainer.SetActive(true);
            SetDefenceItems();
            CompleteCommand();
        }

        private void SetDefenceItems()
        {
            UnitData<DefenceUnit>[] defenceItems = _levelManager.GetLevelDefenceItems();

            for (int i = 0; i < defenceItems.Length; i++)
            {
                DefenceUnitUI defenceUnitUI = Object.Instantiate(_defenceUnitUIPrefab, _buttonsContainer.transform);
                defenceUnitUI.Init(defenceItems[i].UnitInfo, defenceItems[i].UnitCount);
                defenceUnitUI.transform
                    .DOScale(defenceUnitUI.transform.localScale, 1)
                    .From(Vector3.zero)
                    .SetEase(Ease.OutBack);
                
                _createdUIElements.Add(defenceUnitUI);
            }
        }

        public override void ResetCommand()
        {
            _buttonsContainer?.SetActive(false);
        }

        public override void Dispose()
        {
            base.Dispose();
            
            // Clean up created UI elements
            if (_createdUIElements != null)
            {
                foreach (var uiElement in _createdUIElements)
                {
                    if (uiElement != null)
                    {
                        Object.Destroy(uiElement.gameObject);
                    }
                }
                _createdUIElements.Clear();
                _createdUIElements = null;
            }
        }
    }
}