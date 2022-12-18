using UnityEngine;

namespace SRDebuggerUtils.Runtime
{
    [DisallowMultipleComponent]
    public class BillBoard : MonoBehaviour
    {
        [SerializeField] private bool _inverseY = true;

        void Update()
        {
            var camera = Camera.main;
            if (camera == null) return;
            var target = camera.transform.position;

            transform.LookAt(target);

            if (_inverseY)
            {
                transform.Rotate(new Vector3(0f, 180f, 0f));
            }
        }
    }
}