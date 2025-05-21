using UnityEngine;

public class MovNPC : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 5f;
    public float stopDistance = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private int currentPointIndex = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SelectNextPoint();
    }

    void FixedUpdate()
    {
        if (!IsGrounded()) return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        if (targetPoint == null) return;

        Vector3 direction = (targetPoint.position - transform.position);
        direction.y = 0f;

        if (direction.magnitude < stopDistance)
        {
            SelectNextPoint();
            return;
        }

        direction = direction.normalized;
        Vector3 move = direction * speed;
        move.y = rb.linearVelocity.y;

        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 5f * Time.deltaTime);
        }
    }

    void SelectNextPoint()
    {
        int nextPoint = Random.Range(0, patrolPoints.Length);
        while (nextPoint == currentPointIndex && patrolPoints.Length > 1)
        {
            nextPoint = Random.Range(0, patrolPoints.Length);
        }

        currentPointIndex = nextPoint;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "Player")
        {
            transform.Rotate(0f, 180f, 0f);
            SelectNextPoint();
        }
        else if (tag == "Obstacle" || tag == "Enemigo")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 pushDirection = -transform.forward;
                float pushForce = 3f;
                rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
            transform.Rotate(0f, 180f, 0f);
            SelectNextPoint();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Enemigo"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 pushDirection = -transform.forward;
                float pushForce = 3f;
                rb.AddForce(pushDirection * pushForce, ForceMode.Impulse);
            }
            transform.Rotate(0f, 180f, 0f);
            SelectNextPoint();
        }
    }
}
