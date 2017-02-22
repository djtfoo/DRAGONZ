using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
    public float MaxHealth, currentHealth;
    public Image HealthImage;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
            return;

        if(currentHealth>=MaxHealth)
        {
            currentHealth = MaxHealth;
        }
        if(Input.GetKeyDown("z"))
        {
            currentHealth--;
        }
        if (Input.GetKeyDown("x"))
        {
            currentHealth++;
        }
        HealthImage.fillAmount = (currentHealth / MaxHealth);
	}
}
