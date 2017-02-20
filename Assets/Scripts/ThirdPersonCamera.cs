using UnityEngine;
using UnityEngine.Networking;

public class ThirdPersonCamera : NetworkBehaviour
{
    public GameObject player;
    public GameObject target;   // DON'T USE THIS
    public float rotateSpeed = 4;
    Vector3 positionOffset = new Vector3(0, 30f, -100f); // dragon's offset from camera center

    // view = (player.transform.position - this.transform.position);
    // view.Normalize();

    private float yaw = 0f;
    private float pitch = 0f;

    Player playerScript;

    // Use this for initialization
    [Client]
    void Start()
    {
        playerScript = player.GetComponent<Player>();
        //offset = player.transform.position - transform.position;
        this.transform.position = player.transform.position - positionOffset;

        target.transform.position = this.transform.position + 10f * playerScript.GetView();
    }

    // Update is called once per frame
    [Client]
    void Update()
    {
        if (!playerScript.isLocalPlayer || PauseMenu.isOn)
            return;

        //Debug.Log(playerScript.name);

        // Rotate
        UpdatePitch();
        UpdateYaw();

        // rotate player
        player.transform.eulerAngles = new Vector3(-pitch, yaw, 0f);

        // rotate camera
        this.transform.position = player.transform.position;
        this.transform.Translate(positionOffset);
        this.transform.eulerAngles = new Vector3(-pitch, yaw, 0f);

        // rotate view vector
        Quaternion rotation = Quaternion.Euler(-pitch, yaw, 0f);
        playerScript.SetView(new Vector3(0, 0, 1));
        playerScript.SetView(rotation * playerScript.GetView());
        target.transform.position = this.transform.position + 10f * playerScript.GetView();
        //Debug.Log(playerScript.name + "view: " + playerScript.GetView());
    }

    [Client]
    void UpdatePitch()
    {
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pitch += vertical;
        if (pitch > 360f)
            pitch -= 360f;
        else if (pitch < -360f)
            pitch += 360f;
    }

    [Client]
    void UpdateYaw()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        yaw += horizontal;
        if (yaw > 360f)
            yaw -= 360f;
        else if (yaw < -360f)
            yaw += 360f;
        //target.transform.Rotate(0, horizontal, 0);

        //float desiredAngle = target.transform.eulerAngles.y;
        //Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        //transform.position = target.transform.position - (rotation * offset);
        //
        //transform.LookAt(target.transform);
    }
}
