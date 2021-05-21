using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public float speed, jump_force, min_angle, max_angle;
    public float ground_height = 0.5f, ground_radius = 0.05f, bumper_height = 0.35f, bumper_radius = 0.15f;
    public bool sun_is_close = false, ground_is_meteor = false;
    private Rigidbody2D rb;
    private Animator animator;
    public bool grounded;
    public static bool golden = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!golden)
            animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        else
            animator = transform.GetChild(1).gameObject.GetComponent<Animator>();
        Vector3 pos = transform.position;
        if (sun_is_close)
        {
            transform.position = new Vector3(pos.y, pos.x, pos.z);
            transform.eulerAngles = new Vector3(0, 0, -90);
        }

        if (golden)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        float move;

        #if UNITY_STANDALONE
            move = Input.GetAxis("Horizontal");
        #elif UNITY_ANDROID
            float angle = - Input.acceleration.x / (Input.acceleration.y == 0 ? 0.001f : Input.acceleration.y);
            if (Mathf.Abs(angle) <= min_angle)
                move = 0;
            else if (angle >= max_angle)
                move = 1;
            else if (angle <= -max_angle)
                move = -1;
            else
                move = Mathf.Sin(Mathf.PI/2 * angle / max_angle);
        #endif

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

        grounded =
            (Physics2D.OverlapCircle((Vector2)transform.position - up*ground_height, ground_radius, (1 << 8) + (1 << 9)) &&
            !Physics2D.OverlapCircle((Vector2)transform.position - up*bumper_height, bumper_radius, (1 << 8)));
        ground_is_meteor = false;
        if (Physics2D.OverlapCircle((Vector2)transform.position - up*ground_height, ground_radius, (1 << 9)))
            ground_is_meteor = true;

        animator.SetBool("grounded", grounded);
    }

    void Update()
    {
        #if UNITY_STANDALONE
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                Jump();
            }
        #elif UNITY_ANDROID
            if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
            {
                Jump();
            }
        #endif

        animator.SetBool("going_right", Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));
        animator.SetBool("going_left", Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow));
    }

    public void Jump()
    {
        Vector2 pos = transform.position;
        rb.AddForce(pos.normalized*jump_force, ForceMode2D.Impulse);
    }
}