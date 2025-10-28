using System;

namespace _Project._Scripts.Utilities
{
    public class TimeCounter : IDisposable
    {
        private float _time = 0;
        private float _targetTime = 0;
        private bool _isNeedReset = false;
        public System.Action OnTimeReached;

        public TimeCounter(float targetTime, bool isNeedReset)
        {
            OnTimeReached = new System.Action(() => { });
            _isNeedReset = isNeedReset;
            _targetTime = targetTime;
        }

        public void Tick(float deltaTime)
        {
            _time += deltaTime;
            if (!(_time >= _targetTime)) return;
            if (_isNeedReset) _time = 0;
            OnTimeReached?.Invoke();
        }

        public void Dispose()
        {
            OnTimeReached = null;
        }

        public float GetElapsedTime()
        {
            return _time;
        }
    }
}