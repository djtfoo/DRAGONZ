using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float movementSpeed;
    public float turningSpeed;

    private Rigidbody rbody;

    private Vector3 velocity = Vector3.zero;
    private Vector3 force = Vector3.zero;

    private Vector3 view = new Vector3(0, 0, 1);   // the dragon's view vector - for shooting, etc

    // Get the view vector
    public Vector3 GetView()
    {
        return view;
    }

    public void SetView(Vector3 newView)
    {
        view = newView;
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontal = Input.GetAxis("Horizontal") * turningSpeed * Time.deltaTime;
        //transform.Rotate(0, horizontal, 0);

        //float vertical = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        //transform.Translate(0, 0, vertical);

        //transform.Translate(movementSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, movementSpeed * Input.GetAxis("Vertical") * Time.deltaTime);


        //float inputX = Input.GetAxis("Horizontal");
        //float inputZ = Input.GetAxis("Vertical");
        //
        //float moveX = inputX * movementSpeed * Time.deltaTime;
        //float moveZ = inputZ * movementSpeed * Time.deltaTime;
        //
        //transform.Translate(moveX, 0f, moveZ);
        
        //rbody.AddForce(moveX, 0f, moveZ);

        force = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            force = 100f * view;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            force = -100f * view;
        }

        velocity += force * Time.deltaTime;

        if (!velocity.Equals(Vector3.zero)) {
            this.transform.position += velocity * Time.deltaTime;

            // decelerate
            Vector3 velDir = velocity.normalized;
            velocity -= (10f + velocity.magnitude * 0.5f) * velDir * Time.deltaTime;

            double cosOfAngle = (velDir.x * velocity.x + velDir.y * velocity.y + velDir.z * velocity.z);
            if (cosOfAngle < 0)     // -ve, parallel & opp direction
            {
                velocity = Vector3.zero;
            }
        }
    }

}
