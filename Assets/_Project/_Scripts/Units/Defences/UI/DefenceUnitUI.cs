using _Project._Scripts.Cores.Events;
using _Project._Scripts.Cores.Units;
using _Project._Scripts.Levels;
using TMPro;
using UnityEngine; 
using UnityEngine.UI;

namespace _Project._Scripts.Units.Defence
{
    public class DefenceUnitUI : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _defencerNameTxt;
        [SerializeField] private TextMeshProUGUI _defencerCountTxt;
        [SerializeField] private Button _button;
        private UnitInfo<DefenceUnit> _defenceUnit;
        private int _howManyDefenceItems;
        private bool _isSelected = false;

        [Header("Broadcasting")] [SerializeField]
        private UnitEventChannelSO _onDefenceItemSelected;

        [Header("Listening On..")] [SerializeField]
        private UnitEventChannelSO _onDefenceItemFailedToSet;

        [SerializeField] private UnitEventChannelSO _onDefenceItemSetOnBoard;


        public void Init(UnitInfo<DefenceUnit> defenceItem, int howManyDefenceItems)
        {
            _defenceUnit = defenceItem;
            _icon.sprite = defenceItem.Icon;
            _howManyDefenceItems = howManyDefenceItems;
            _defencerNameTxt.text = _defenceUnit.UnitName;
            _defencerCountTxt.text = _howManyDefenceItems.ToString();
        }

        private void OnEnable()
        {
            _button?.onClick.AddListener(OnButtonClick);
            _onDefenceItemFailedToSet.onEventRaised += DefenceItemFailedToSet;
            _onDefenceItemSetOnBoard.onEventRaised += DefenceItemSetOnBoard;
        }

        private void OnDisable()
        {
            _button?.onClick.RemoveAllListeners();
            _onDefenceItemFailedToSet.onEventRaised -= DefenceItemFailedToSet;
            _onDefenceItemSetOnBoard.onEventRaised -= DefenceItemSetOnBoard;
        }

        private void OnButtonClick()
        {
            _isSelected = true;
            _button.interactable = false;
            _onDefenceItemSelected?.RaiseEvent(_defenceUnit.Unit);
        }

        private void DefenceItemFailedToSet(Unit unit)
        {
            if (!_isSelected) return;
            _isSelected = false;
            _button.interactable = _howManyDefenceItems != 0;
            _defencerCountTxt.text = _howManyDefenceItems.ToString();
        }

        private void DefenceItemSetOnBoard(Unit unit)
        {
            if (!_isSelected) return;
            _isSelected = false;
            _howManyDefenceItems--;
            _button.interactable = _howManyDefenceItems != 0;
            _defencerCountTxt.text = _howManyDefenceItems.ToString();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_button == null) _button = GetComponentInChildren<Button>();
        }
#endif
    }
}