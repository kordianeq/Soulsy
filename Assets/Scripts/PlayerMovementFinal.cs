using UnityEngine;

public class PlayerMovementFinal : MonoBehaviour
{
    [SerializeField] public float speed = 5.0f;
    [SerializeField] public float jumpForce = 5.0f;
    [SerializeField] public float sprintSpeed = 10.0f;

    GameObject camera;
    
    public GameObject AnimatorRef; //refka do animatora
    Animator animator;

    float horizontalInput, verticalInput;
    Rigidbody RB2B;
    bool isGrounded, isRunning;


    void Start()
    {
        animator = AnimatorRef.GetComponent<Animator>();
        RB2B = GetComponent<Rigidbody>();
        camera = GameObject.Find("CamHolder");

        isGrounded = true;
        isRunning = false;
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        

        if (isRunning)
        {
            transform.Translate(camera.transform.forward * Time.deltaTime * sprintSpeed * verticalInput);
            transform.Translate(camera.transform.right * Time.deltaTime * sprintSpeed * horizontalInput);

            
            animator.SetBool("isRunning", true); //ustawanie isRuning w animatorze

        }
        else
        {
            transform.Translate(camera.transform.forward * Time.deltaTime * speed * verticalInput);
            transform.Translate(camera.transform.right * Time.deltaTime * speed * horizontalInput);
            animator.SetBool("isRunning", false); //ustawanie isRuning w animatorze
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            RB2B.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            isGrounded = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }


        void OnCollisionStay(Collision collision)
        {
            isGrounded = true;
        }
    }
}



