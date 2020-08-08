using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed, jump_force;
    public float ground_height = 0.5f, ground_radius = 0.05f, bumper_height = 0.35f, bumper_radius = 0.15f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool grounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        Vector2 pos = transform.position;

        float r = pos.magnitude;
        float phi = Mathf.Atan(pos.x/pos.y) * Mathf.Rad2Deg;
        phi = (pos.y>=0 ? phi : 180+phi);
        float delta_phi = move*speed*Time.fixedDeltaTime/r;

        float velocity = Vector2.Dot(rb.velocity, pos.normalized);

        transform.position = new Vector3(r * Mathf.Sin((phi + delta_phi)*Mathf.Deg2Rad),
            r * Mathf.Cos((phi + delta_phi)*Mathf.Deg2Rad), transform.position.z);
        Vector2 up = ((Vector2)transform.position).normalized;
        rb.velocity = velocity*up;

        grounded = (Physics2D.OverlapCircle((Vector2)transform.position - up*ground_height, ground_radius, 1, 5, 10)
            && !Physics2D.OverlapCircle((Vector2)transform.position - up*bumper_height, bumper_radius, 1, 5, 10));

        animator.SetBool("grounded", grounded);
    }

    void Update()
    {
        Vector2 pos = transform.position;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(pos.normalized*jump_force, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        animator.SetBool("going_right", Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));
        animator.SetBool("going_left", Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
    }
}
