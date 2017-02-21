using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public string sceneName = "";

    public void ChangeSceneOnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
