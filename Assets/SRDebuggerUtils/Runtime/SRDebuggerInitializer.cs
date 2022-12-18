using System.Collections;
using UnityEngine;
using UnityEngine.UI;
#if NREAL
using NRKernal;
#endif

namespace SRDebuggerUtils.Runtime
{
    public class SRDebuggerInitializer : MonoBehaviour
    {
        [SerializeField] private bool _autoStart;

        // meter
        [SerializeField] private float _scaleWidth = 1f;
        [SerializeField] private bool _enableBillBoard = true;
        [SerializeField] private bool _enableHud;
        [SerializeField] private HUDCanvas _hudCanvasPrefab;


        private RectTransform _rect;
        private bool _initialized = false;

        private void Start()
        {
            if (_autoStart) StartCoroutine(Init());
        }

        private IEnumerator Init()
        {
            if (_initialized) yield break;
            _rect = SRDebug.Instance.EnableWorldSpaceMode();
            InitScaleUnit();
            if (_enableBillBoard)
            {
                _rect.gameObject.AddComponent<BillBoard>();
            }

            if (_enableHud)
            {
                var hudCanvas = Instantiate(_hudCanvasPrefab);
                _rect.transform.SetParent(hudCanvas.transform);
                _rect.transform.localPosition += Vector3.forward;
            }


#if NREAL
            _rect.gameObject.AddComponent<CanvasRaycastTarget>();
            yield return null;
            var otherRaycasters = _rect.GetComponentsInChildren<GraphicRaycaster>(true);
            Debug.Log(otherRaycasters.Length);
            // 他のSRTabsの初期化を待つ
            foreach (var graphicRaycaster in otherRaycasters)
            {
                graphicRaycaster.gameObject.AddComponent<CanvasRaycastTarget>();
            }
#endif
            _initialized = true;
        }


        private void InitScaleUnit()
        {
            // to 1m
            _rect.localScale *= .001f;
            _rect.localScale *= _scaleWidth;
        }

        [ContextMenu(nameof(Show))]
        public void Show()
        {
            if (!_initialized) StartCoroutine(Init());
            SRDebug.Instance.ShowDebugPanel();
        }

        [ContextMenu(nameof(Hide))]
        public void Hide()
        {
            SRDebug.Instance.HideDebugPanel();
        }

        [ContextMenu(nameof(ToggleVisible))]
        public void ToggleVisible()
        {
            if (SRDebug.Instance.IsDebugPanelVisible)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }
}