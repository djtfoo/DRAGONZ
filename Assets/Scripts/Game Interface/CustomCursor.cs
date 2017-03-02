using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class CustomCursor : MonoBehaviour {
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public CursorLockMode cursorLockMode;
    bool LoadedScene = false;
	// Use this for initialization
	void Start () {
        Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);
        Cursor.lockState = cursorLockMode;
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("TestScene"))
        {
            if(!LoadedScene)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                LoadedScene = true;
            }

            if (OverlayActive.IsOverlayActive()) {
                Cursor.visible = true;
                Cursor.lockState = cursorLockMode;
            }
            else {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
	}

}
