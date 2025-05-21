using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float Speed = 5f;
    public float SpeedRotation = 10f;

    public string Horizontal;
    public string Vertical;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(-1 * Input.GetAxis(Vertical), 0f, Input.GetAxis(Horizontal)).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            Vector3 velocity = moveDirection * Speed;
            velocity.y = rb.linearVelocity.y;
            rb.linearVelocity = velocity;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, targetRotation, SpeedRotation * Time.fixedDeltaTime));
        }
        else
        {
            Vector3 velocity = rb.linearVelocity;
            velocity.x = 0;
            velocity.z = 0;
            rb.linearVelocity = velocity;
        }
    }
}
