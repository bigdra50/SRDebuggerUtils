#if OCULUS
using System;
using SRDebuggerUtils.Runtime;
using UnityEngine;

namespace SRDebuggerUtils.Samples.Oculus
{
    public class OculusDebugController : MonoBehaviour
    {
        public event Action OnDoubleTaped = () => { };
        public event Action OnTripleTaped = () => { };

        [SerializeField] private SRDebuggerInitializer _srDebuggerInitializer;
        [SerializeField] private OVRInput.RawButton _controllerButton = OVRInput.RawButton.RThumbstick;

        private bool _isDoubleTapStart;
        private float _doubleTapTime;

        private bool _isTripleTapStart;
        private float _tripleTapTime;

        private void Start()
        {
            // コントローラーをトリプルタップでデバッグメニューを表示する
            OnTripleTaped += () => { _srDebuggerInitializer.ToggleVisible(); };
        }

        private void Update()
        {
            if (_isTripleTapStart)
            {
                TripleTap();
            }
            else if (_isDoubleTapStart)
            {
                DoubleTap();
            }
            else
            {
                if (OVRInput.GetDown(_controllerButton) || Input.GetMouseButtonDown(1))
                {
                    _isDoubleTapStart = true;
                }
            }
        }

        private void DoubleTap()
        {
            _doubleTapTime += Time.deltaTime;
            if (_doubleTapTime < .2f)
            {
                if (OVRInput.GetDown(_controllerButton) || Input.GetMouseButtonDown(1))
                {
                    _isDoubleTapStart = false;
                    OnDoubleTaped();
                    _doubleTapTime = 0f;
                    _isTripleTapStart = true;
                }
            }
            else
            {
                _isDoubleTapStart = false;
                _doubleTapTime = 0f;
            }
        }

        private void TripleTap()
        {
            _tripleTapTime += Time.deltaTime;
            if (_tripleTapTime < .2f)
            {
                if (OVRInput.GetDown(_controllerButton) || Input.GetMouseButtonDown(1))
                {
                    _isTripleTapStart = false;
                    OnTripleTaped();
                    _tripleTapTime = 0f;
                }
            }
            else
            {
                _isTripleTapStart = false;
                _tripleTapTime = 0f;
            }
        }
    }
}
#endif