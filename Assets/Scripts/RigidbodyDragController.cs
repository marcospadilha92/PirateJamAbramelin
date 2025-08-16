using UnityEngine;
using UnityEngine.EventSystems;

public class RigidbodyDragController : MonoBehaviour,
    IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Rigidbody2D body;

    private LineRenderer dragCircle;
    public int circleSegments = 128;

    public float velocityMultiplier = 20f;
    public float maxVelocity = 50f;

    private Vector2 mousePosition;
    private bool isDragging = false;
    private bool isOnCooldown = false;

    public float dragCooldown = 2f; // in seconds
    private float cooldownTimer = 0f;

    public float dragRadius = 5f;
    private Vector2 dragStartPosition;

    // state machine events
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        dragCircle = GetComponent<LineRenderer>();
        dragCircle.enabled = false;
    }

    // drag events
    public void OnDrag(PointerEventData pointer)
    {
        if (!isOnCooldown)
        {
            mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(pointer.position);

            if (Vector2.Distance(dragStartPosition, mousePosition) > dragRadius)
            {
                isDragging = false;
                isOnCooldown = true;
                cooldownTimer = dragCooldown;
                dragCircle.enabled = false;
            }
        }
    }

    public void OnBeginDrag(PointerEventData pointer)
    {
        if (!isOnCooldown)
        {
            isDragging = true;
            dragStartPosition = (Vector2)Camera.main.ScreenToWorldPoint(pointer.position);
            DrawDragCircle(dragStartPosition, dragRadius);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            isDragging = false;
            isOnCooldown = true;
            cooldownTimer = dragCooldown;
            dragCircle.enabled = false;
        }
    }

    public void FixedUpdate()
    {
        if (isDragging)
        {
            float displacement = velocityMultiplier * Time.fixedDeltaTime;
            Vector2 position = Vector2.Lerp(body.position, mousePosition, displacement);

            body.MovePosition(position);

            Vector2 velocity = (position - body.position) / Time.fixedDeltaTime;
            if (velocity.magnitude > maxVelocity)
            {
                velocity = velocity.normalized * maxVelocity;
            }
            body.linearVelocity = velocity;
        }

        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isOnCooldown = false;
            }
        }
    }

    private void DrawDragCircle(Vector2 center, float radius)
    {
        dragCircle.positionCount = circleSegments + 1;
        dragCircle.startWidth = 0.1f;
        dragCircle.endWidth = 0.1f;

        for (int i = 0; i <= circleSegments; i++)
        {
            float angle = i * Mathf.PI * 2 / circleSegments;
            Vector2 point = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            dragCircle.SetPosition(i, point);
        }

        dragCircle.enabled = true;
    }
}
