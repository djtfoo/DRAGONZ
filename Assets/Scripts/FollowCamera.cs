using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{
    public GameObject player;
    public GameObject target;   // DON'T USE THIS
    public float rotateSpeed = 4;
    Vector3 positionOffset = new Vector3(0, 30f, -100f); // dragon's offset from camera center
    Vector3 view = new Vector3(0, 0, 1);   // the dragon's view vector - for shooting, etc

    // view = (player.transform.position - this.transform.position);
    // view.Normalize();

    private float yaw = 0f;
    private float pitch = 0f;

    public Vector3 GetView()
    {
        return view;
    }

    // Use this for initialization
    void Start()
    {
        //offset = player.transform.position - transform.position;
        this.transform.position = player.transform.position - positionOffset;

        target.transform.position = this.transform.position + 10f * view;
    }

    // Update is called once per frame
    void Update()
    {
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
        view = new Vector3(0, 0, 1);
        view = rotation * view;
        target.transform.position = this.transform.position + 10f * view;
    }

    void UpdatePitch()
    {
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pitch += vertical;
        if (pitch > 360f)
            pitch -= 360f;
        else if (pitch < -360f)
            pitch += 360f;
    }

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