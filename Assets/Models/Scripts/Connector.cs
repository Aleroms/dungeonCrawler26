using UnityEngine;

public class Connector : MonoBehaviour
{
    [SerializeField] private Vector2 _size = Vector2.one;



    private void OnDrawGizmos()
    {
        // draws the forward gizmo in center
        Vector2 halfsize = _size * 0.5f;
        Vector3 center = transform.position + transform.up * halfsize.y;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(center, center + transform.forward);

        Vector3 height = transform.position + transform.up * _size.y;
        Vector3 width = transform.position + transform.right * halfsize.x;

        // draws the doorway gizmo in center
        Gizmos.color *= 0.75f;
        Vector3 topLeft = height - width;
        Vector3 topRight = height + width;
        Vector3 botLeft = -width;
        Vector3 botRight = width;

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(topLeft, botLeft);
        Gizmos.DrawLine(topRight, botRight);
        Gizmos.DrawLine(topRight, botLeft);
        Gizmos.DrawLine(topLeft, botRight);
    }
}
