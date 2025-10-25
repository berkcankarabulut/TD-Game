using _Project._Scripts.Cores.Commands;
using _Project._Scripts.Cores.Services;
using _Project._Scripts.Levels;
using _Project._Scripts.Managers;
using _Project._Scripts.Units.Defence;
using DG.Tweening;
using UnityEngine; 

namespace Project._Scripts.Initilazer
{
    public class DefenceItemUIBuilder : Command
    {
        [SerializeField] private DefenceUnitUI defenceUnitUI;
        [SerializeField] private GameObject _buttonsContainer;
        private LevelManager _levelManager;

        public override void StartCommand()
        {
            _buttonsContainer.SetActive(true);
            _levelManager = ServiceLocator.Instance.Get<LevelManager>();
            SetDefenceItems();
            CompleteCommand();
        }

        private void SetDefenceItems()
        {
            UnitData<DefenceUnit>[] defenceItems = _levelManager.GetLevelDefenceItems();

            for (int i = 0; i < defenceItems.Length; i++)
            {
                DefenceUnitUI defenceUnitUI = Instantiate(this.defenceUnitUI, _buttonsContainer.transform);
                defenceUnitUI.Init(defenceItems[i].UnitInfo, defenceItems[i].UnitCount);
                defenceUnitUI.transform
                    .DOScale(transform.localScale, 1)
                    .From(Vector3.zero)
                    .SetEase(Ease.OutBack);
            }
        }

        public override void ResetCommand()
        {
            _buttonsContainer.SetActive(false);
        }
    }
}