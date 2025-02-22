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
    private bool isOnCooldown = false;

    public float dragCooldown = 2f; // in seconds
    private float cooldownTimer = 0f;

    // state machine events
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // drag events
    public void OnDrag(PointerEventData pointer)
    {
        if (!isOnCooldown){
            mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(pointer.position);
        }
    }

    public void OnBeginDrag(PointerEventData pointer)
    {
        if (!isOnCooldown){
            isDragging = true;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            isDragging = false;
            isOnCooldown = true;
            cooldownTimer = dragCooldown;
        }
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

        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isOnCooldown = false;
            }
        }
    }
}
