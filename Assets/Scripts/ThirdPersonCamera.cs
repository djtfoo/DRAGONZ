using UnityEngine;
using UnityEngine.Networking;

public class ThirdPersonCamera : NetworkBehaviour
{
    public GameObject player;
    public GameObject target;   // DON'T USE THIS
    Vector3 positionOffset = new Vector3(0, 30f, -150f); // dragon's offset from camera center

    // view = (player.transform.position - this.transform.position);
    // view.Normalize();

    private float yaw = 0f;
    private float pitch = 0f;
    private float roll = 0f;

    public float changeSpeed = 1f;

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
        //UpdatePitch();
        //UpdateYaw();

        // Update rotation values
        float playerPitch = playerScript.GetPitch();
        float playerYaw = playerScript.GetYaw();
        if (this.pitch != playerPitch)
        {
            if (this.pitch < playerPitch)
            {
                this.pitch += (playerPitch - this.pitch) * changeSpeed * Time.deltaTime;
                if (this.pitch > playerPitch)
                    this.pitch = playerPitch;
            }
            else
            {
                this.pitch -= (this.pitch - playerPitch) * changeSpeed * Time.deltaTime;
                if (this.pitch < playerPitch)
                    this.pitch = playerPitch;
            }
        }
        if (this.yaw != playerYaw)
        {
            if (this.yaw < playerYaw)
            {
                this.yaw += (playerYaw - this.yaw) * changeSpeed * Time.deltaTime;
                if (this.yaw > playerYaw)
                    this.yaw = playerYaw;
            }
            else
            {
                this.yaw -= (this.yaw - playerYaw) * changeSpeed * Time.deltaTime;
                if (this.yaw < playerYaw)
                    this.yaw = playerYaw;
            }
        }
        //this.pitch = playerScript.GetPitch();
        //this.yaw = playerScript.GetYaw();

        this.roll = playerScript.GetRoll();

        // rotate player
        //player.transform.eulerAngles = new Vector3(-pitch, yaw, -yaw * 0.5f);

        // rotate & move camera
        this.transform.position = player.transform.position;
        this.transform.Translate(positionOffset);
        this.transform.eulerAngles = new Vector3(-pitch + 30f, yaw, roll);

        // rotate up vector
        //float playerRoll = playerScript.GetRoll();
        //this.transform.up = new Vector3(playerRoll, 0f, 0f);
        //Vector3 right = Vector3.Cross(playerScript.GetView(), this.transform.up);
        //right.y = 0f;
        //right.Normalize();
        //this.transform.up = Vector3.Cross(right, playerScript.GetView());

        // rotate view vector
        //Quaternion rotation = Quaternion.Euler(-pitch, yaw, 0f);
        //playerScript.SetView(new Vector3(0, 0, 1));
        //playerScript.SetView(rotation * playerScript.GetView());

        // change target
        target.transform.position = this.transform.position + 10f * playerScript.GetView();
        //Debug.Log(playerScript.name + "view: " + playerScript.GetView());
    }

}
