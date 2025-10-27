using _Project._Scripts.Cores.StateMachines;
using UnityEngine;

namespace _Project._Scripts.UI.GamePanel
{
    public abstract class PanelState : IState
    {
        protected GameObject _panel;

        protected PanelState(GameObject panel)
        {
            _panel = panel;
        }

        public virtual void Enter()
        {
            _panel.SetActive(true);
        }

        public virtual void Exit()
        {
            _panel.SetActive(false);
        }

        public virtual void Tick(float deltaTime)
        {
        }
    }
}