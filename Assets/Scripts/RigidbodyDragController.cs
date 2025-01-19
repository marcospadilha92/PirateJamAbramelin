using UnityEngine;
using UnityEngine.EventSystems;

public class RigidbodyDragController : MonoBehaviour, 
    IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Rigidbody2D body;

    public float velocityMultiplier = 20f;
    public float maxVelocity = 50f;

    private Vector2 mousePosition;
    private bool isDragging = false;

    // state machine events
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // drag events
    public void OnDrag(PointerEventData pointer)
    {
        mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(pointer.position);
    }

    public void OnBeginDrag(PointerEventData pointer)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }

    public void FixedUpdate()
    {
        if(isDragging)
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
    }
}
