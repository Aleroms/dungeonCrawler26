using UnityEngine;

public class Connector : MonoBehaviour
{
    [SerializeField] private Vector2 _size = Vector2.one;
    public bool IsConnected { get; set; } = false;


    private void OnDrawGizmos()
    {
        // draws the forward gizmo in center
        Vector2 halfsize = _size * 0.5f;
        Vector3 center = transform.position + transform.up * halfsize.y;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(center, center + transform.forward);

        Vector3 top =  transform.up * _size.y;
        Vector3 side = transform.right * halfsize.x;

        // draws the doorway gizmo in center
        Gizmos.color *= 0.75f;
        Vector3 topLeft = transform.position + top - side;
        Vector3 topRight = transform.position + top + side;
        Vector3 botLeft = transform.position -side;
        Vector3 botRight = transform.position + side;

        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(topLeft, botLeft);
        Gizmos.DrawLine(topRight, botRight);
        Gizmos.DrawLine(topRight, botLeft);
        Gizmos.DrawLine(topLeft, botRight);
    }
}
