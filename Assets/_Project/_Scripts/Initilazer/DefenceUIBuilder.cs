using System.Collections.Generic;
using _Project._Scripts.Defences;
using _Project._Scripts.Utilities.Commands;
using _Project._Scripts.Levels;
using _Project._Scripts.Managers; 
using DG.Tweening;
using UnityEngine;

namespace _Project._Scripts.Initilazer
{
    public class DefenceUIBuilder : Command
    {
        private readonly DefenceUnitUIButton _defenceUnitUIButtonPrefab;
        private readonly GameObject _buttonsContainer;
        private readonly LevelManager _levelManager;

        private List<DefenceUnitUIButton> _createdUIElements = new List<DefenceUnitUIButton>();

        public DefenceUIBuilder(DefenceUnitUIButton defenceUnitUIButtonPrefab,
            GameObject buttonsContainer,
            LevelManager levelManager)
        {
            _defenceUnitUIButtonPrefab = defenceUnitUIButtonPrefab;
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
                DefenceUnitUIButton defenceUnitUIButton =
                    Object.Instantiate(_defenceUnitUIButtonPrefab, _buttonsContainer.transform);
                defenceUnitUIButton.Init(defenceItems[i].UnitInfo, defenceItems[i].UnitCount);
                defenceUnitUIButton.transform
                    .DOScale(defenceUnitUIButton.transform.localScale, 1)
                    .From(Vector3.zero)
                    .SetEase(Ease.OutBack);

                _createdUIElements.Add(defenceUnitUIButton);
            }
        }

        public override void ResetCommand()
        {
            if (_buttonsContainer == null) return;
            _buttonsContainer.SetActive(false);
        }

        public override void Dispose()
        {
            base.Dispose();

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