using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    [SyncVar]
    Vector3 realPosition = Vector3.zero;

    [SyncVar]
    Quaternion realRotation;

    [SyncVar]
    private bool isDead = false;

    private float updateInterval;

    public float turningSpeed;
    public float rotateSpeed = 4f;
    private float changeSpeed = 20f;
    private bool keyNotPressed = true;

    private float yaw = 0f;
    private float pitch = 0f;
    private float roll = 0f;

    public int kills;
    public int deaths;

    private Rigidbody rbody;

    private Vector3 velocity = Vector3.zero;
    private Vector3 force = Vector3.zero;

    [SyncVar]
    Vector3 realView = Vector3.zero;

    private Vector3 view;   // the dragon's view vector - for shooting, etc

    public void SetIsDead(bool _isDead)
    {
        isDead = _isDead;
    }

    public bool GetIsDead()
    {
        return isDead;
    }

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

    public float GetPitch()
    {
        return this.pitch;
    }

    public float GetYaw()
    {
        return this.yaw;
    }

    public float GetRoll()
    {
        return this.roll;
    }

    // Use this for initialization
    public override void OnStartLocalPlayer()
    {
        view = new Vector3(0, 0, 1);

        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    [Client]
    void Update()
    {
        if (isLocalPlayer)
        {
            if (OverlayActive.IsOverlayActive())
                return;

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

            // Rotation & Angles
            UpdatePitch();
            UpdateYaw();    // roll updates here too

            // update player's Euler angles
            //this.transform.eulerAngles = new Vector3(-pitch, yaw, -yaw * 0.5f);
            this.transform.eulerAngles = new Vector3(-pitch, yaw, roll);

            Quaternion rotation = Quaternion.Euler(-pitch, yaw, 0f);
            SetView(rotation * new Vector3(0, 0, 1));

            // SLOWLY PITCH BACK
            if (this.pitch != 0)
            {
                if (this.pitch < 0)
                {
                    this.pitch += changeSpeed * Time.deltaTime;
                    if (this.pitch > 0)
                        this.pitch = 0;
                }
                else
                {
                    this.pitch -= changeSpeed * Time.deltaTime;
                    if (this.pitch < 0)
                        this.pitch = 0;
                }
            }

            // SLOWLY ROLL BACK
            if (this.roll != 0)
            {
                if (this.roll < 0)
                {
                    this.roll += changeSpeed * Time.deltaTime;
                    if (this.roll > 0)
                        this.roll = 0;
                }
                else
                {
                    this.roll -= changeSpeed * Time.deltaTime;
                    if (this.roll < 0)
                        this.roll = 0;
                }
            }

            // Movement
            force = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                force = 100f * view;
                keyNotPressed = false;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                force = -100f * view;
                keyNotPressed = false;
            }

            velocity += force * Time.deltaTime;

            if (!velocity.Equals(Vector3.zero))
            {
                this.transform.position += velocity * Time.deltaTime;

                // decelerate
                Vector3 velDir = velocity.normalized;
                float decelerator = (5f + velocity.magnitude * 0.5f);
                if (keyNotPressed)
                {
                    decelerator *= 2f;
                }
                else
                {
                    keyNotPressed = true;

                    if (decelerator > 20f)
                        decelerator = 20f;
                }
                velocity -= decelerator * velDir * Time.deltaTime;

                double cosOfAngle = (velDir.x * velocity.x + velDir.y * velocity.y + velDir.z * velocity.z);
                if (cosOfAngle < 0)     // -ve, parallel & opp direction
                {
                    velocity = Vector3.zero;
                }
            }

            if (GetComponent<Health>().currentHealth <= 0.0f)
            {
                Die();
                //transform.position.Set(0, 0, 0);
                //Debug.Log("dieded");
            }

            // Update the server with position/rotation
            updateInterval += Time.deltaTime;
            if (updateInterval > 0.11f) // 9 times per sec (default unity send rate)
            {
                updateInterval = 0;
                CmdSync(transform.position, transform.rotation, view);
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
            view = Vector3.Lerp(view, realView, 0.1f);
        }
    }

    [Client]
    void UpdatePitch()
    {
        float inputValue = 0f;

#if UNITY_ANDROID
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        dir.y = Input.acceleration.y;
        dir.z = Input.acceleration.z;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        inputValue = dir.z;

#else
        inputValue = Input.GetAxis("Mouse Y");

#endif
        float vertical = inputValue * rotateSpeed;

        pitch += vertical;
        if (pitch > 80f)
            pitch = 80f;
        else if (pitch < -80f)
            pitch = -80f;
    }

    [Client]
    void UpdateYaw()
    {
        float inputValue = 0f;

#if UNITY_ANDROID
        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        dir.y = Input.acceleration.y;
        dir.z = Input.acceleration.z;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        inputValue = dir.x;

#else
        inputValue = Input.GetAxis("Mouse X");

#endif
        float horizontal = inputValue * rotateSpeed;

        float newRoll = roll - 0.5f * horizontal;
        float overflow = 0f;
        if (newRoll > 50f || newRoll < -50f)
            overflow = newRoll - roll;

        roll = newRoll - overflow;

        yaw += horizontal - overflow;

        //if (yaw > 360f)
        //    yaw -= 360f;
        //else if (yaw < -360f)
        //    yaw += 360f;

        //target.transform.Rotate(0, horizontal, 0);

        //float desiredAngle = target.transform.eulerAngles.y;
        //Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        //transform.position = target.transform.position - (rotation * offset);
        //
        //transform.LookAt(target.transform);
    }

    [Command]
    void CmdSync(Vector3 position, Quaternion rotation, Vector3 view)
    {
        realPosition = position;
        realRotation = rotation;
        realView = view;
    }

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log(col.gameObject.name);
    }

    private void Die()
    {
        isDead = true;

        deaths++;

        // DisableOnDeath stuff...

        // Disable collider

        // Time till respawn
    }
}
