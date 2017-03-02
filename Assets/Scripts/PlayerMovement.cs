using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    [SyncVar]
    Vector3 realPosition = Vector3.zero;

    [SyncVar]
    Quaternion realRotation;

    private float updateInterval;

    public float turningSpeed;
    public float rotateSpeed = 4f;
    private float changeSpeed = 5f;
    private bool keyNotPressed = true;

    private float yaw = 0f;
    private float pitch = 0f;
    private float roll = 0f;

    private Rigidbody rbody;

    private Vector3 velocity = Vector3.zero;
    private Vector3 force = Vector3.zero;

    [SyncVar]
    Vector3 realView = Vector3.zero;

    private Vector3 view;   // the dragon's view vector - for shooting, etc

    // Android movement
    private MoveJoystick joystick;
    private float prevAccelerometerX = 0f;
    private float prevAccelerometerZ = 0f;

    public void SetJoystick(MoveJoystick _joystick)
    {
        joystick = _joystick;
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

    public void SetDefault()
    {
        view = new Vector3(0, 0, 1);
        if (rbody == null)
            rbody = GetComponent<Rigidbody>();
        rbody.velocity = Vector3.zero;
        rotateSpeed = 4f;
        changeSpeed = 20f;
        keyNotPressed = true;
        yaw = 0f;
        pitch = 0f;
        roll = 0f;
    }

    // Update is called once per frame
    //[Client]
    void Update()
    {
        if (isLocalPlayer)
        {
            // Checks for overlay active done in Player.cs

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

#if UNITY_ANDROID

            // get joystick movement
            force = joystick.GetJoystickDelta() * 100f * view;
            keyNotPressed = false;

#else
            if (Input.GetKey(KeyBoardBindings.GetForwardKey()))
            {
                force = 100f * view;
                keyNotPressed = false;
            }
            else if (Input.GetKey(KeyBoardBindings.GetBackwardKey()))
            {
                force = -100f * view;
                keyNotPressed = false;
            }
#endif

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

            if (rbody.velocity.magnitude > 0.5f)
            {
                rbody.velocity.Normalize();
                rbody.velocity *= 0.5f;
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

    //[Client]
    void UpdatePitch()
    {
        float inputValue = 0f;

#if UNITY_ANDROID

        Vector3 dir = Input.acceleration;
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        inputValue = dir.z + 0.4f;

        //float deltaZ = dir.z - prevAccelerometerZ;
        //if (Mathf.Abs(deltaZ) > 0.05f)
        //{
        //    inputValue = 20f * deltaZ;
        //    prevAccelerometerZ = dir.z;
        //}

#else
        inputValue = SettingsData.GetInvertVerticalAxis() * Input.GetAxis("Mouse Y");

#endif
        float vertical = inputValue * rotateSpeed;

        pitch += vertical;
        if (pitch > 80f)
            pitch = 80f;
        else if (pitch < -80f)
            pitch = -80f;
    }

    //[Client]
    void UpdateYaw()
    {
        float inputValue = 0f;

#if UNITY_ANDROID

        Vector3 dir = Input.acceleration;
        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        inputValue = dir.x;

        //float deltaX = dir.x - prevAccelerometerX;
        //if (Mathf.Abs(deltaX) > 0.05f)
        //{
        //    inputValue = 20f * deltaX;
        //    prevAccelerometerX = dir.x;
        //}

#else
        inputValue = SettingsData.GetInvertHorizontalAxis() * Input.GetAxis("Mouse X");

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
}
