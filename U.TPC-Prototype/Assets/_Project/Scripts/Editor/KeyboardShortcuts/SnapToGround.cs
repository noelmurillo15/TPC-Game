using UnityEditor;
using UnityEngine;

namespace ANM.Editor
{
    public class SnapToGround : MonoBehaviour
    {
        [MenuItem("Custom/Snap To Ground %g")]
        public static void Ground()
        {
            foreach (var transform in Selection.transforms)
            {
                var hits = Physics.RaycastAll(transform.position + Vector3.up, Vector3.down, 15f);
                foreach (var raycastHit in hits)
                {
                    if(raycastHit.collider.gameObject == transform.gameObject)
                        continue;

                    transform.position = raycastHit.point;
                    break;
                }
            }
        }
    }
}
