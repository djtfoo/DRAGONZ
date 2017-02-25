using UnityEngine;
using System.Collections;

public class MoveJoystick : MonoBehaviour {

    public GameObject joystick;
    bool moveJoystick = false;
    float minJoystickY;
    float maxJoystickY;

    Touch touch;
    //int fingerID = 0;

	// Use this for initialization
	void Start () {
        float joystickSizeY = this.gameObject.GetComponent<RectTransform>().sizeDelta.y;
        minJoystickY = this.transform.position.y - joystickSizeY;
        maxJoystickY = this.transform.position.y + joystickSizeY;
    }
	
	// Update is called once per frame
	void Update () {

	    if (moveJoystick)
        {
            touch = Input.GetTouch(0);
            Vector2 touchPos = touch.position;
            joystick.transform.position = new Vector3(this.transform.position.x, Mathf.Clamp(touchPos.y, minJoystickY, maxJoystickY), 0f);
        }
	}

    public void StartMoveJoystick()
    {
        if (Input.touchCount == 1)
            moveJoystick = true;
    }

    public void EndMoveJoystick()
    {
        moveJoystick = false;
        joystick.transform.position = this.transform.position;
    }

    public float GetJoystickDelta()
    {
        return (joystick.transform.position.y - this.transform.position.y) / (maxJoystickY - this.transform.position.y);
    }

}
