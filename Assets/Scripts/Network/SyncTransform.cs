using UnityEngine;
using UnityEngine.Networking;

public class SyncTransform : NetworkBehaviour {

    public bool syncRotation = false;

    [SyncVar]
    Vector3 realPosition = Vector3.zero;

    [SyncVar]
    Quaternion realRotation;

    private float updateInterval;

    // Call under isLocalPlayer
    public void UpdateSync()
    {
        // Update the server with position/rotation
        updateInterval += Time.deltaTime;
        if (updateInterval > 0.11f) // 9 times per sec (default unity send rate)
        {
            updateInterval = 0;
            CmdSync(transform.position, transform.rotation);
        }
    }

    public void UpdateOthers()
    {
        transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
        transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
    }

    [Command]
    void CmdSync(Vector3 position, Quaternion rotation)
    {
        realPosition = position;
        realRotation = rotation;
    }
}
