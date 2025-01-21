using UnityEngine;
using UnityEngine.EventSystems;

public class RigidbodyDragController : MonoBehaviour, 
    IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Rigidbody2D body;
    public Animator animator;

    public float moveSpeed;

    public float velocityMultiplier = 20f;
    public float maxVelocity = 50f;

    private Vector2 mousePosition;
    private bool isDragging = false;

    // state machine events
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // normal movement
    void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontal));

            body.AddForce(
                new Vector2(horizontal * moveSpeed * Time.deltaTime, 0));
        }
        else
        {
            body.AddForce(
                new Vector2(0, vertical * moveSpeed * Time.deltaTime));
            //animator.SetFloat("Run", Mathf.Abs(vertical));
        }
    }

    // drag events
    public void OnDrag(PointerEventData pointer)
    {
        mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(pointer.position);
    }

    public void OnBeginDrag(PointerEventData pointer)
    {
        isDragging = true;
        animator.SetBool("IsSpin", true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        animator.SetBool("IsSpin", false);
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
        if (!isDragging)
        {
            Movement();
        }
    }
}
