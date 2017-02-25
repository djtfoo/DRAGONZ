using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour 
{
    public float MaxHealth;

    [SyncVar]
    public float currentHealth;

    public Image HealthImage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isLocalPlayer)
        {
            //Debug.Log(transform.name + " health: " + currentHealth);

            if (currentHealth <= Mathf.Epsilon)
                currentHealth = 0.0f;

            if (currentHealth >= MaxHealth)
            {
                currentHealth = MaxHealth;
            }
            if (Input.GetKeyDown("z"))
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

    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;
    }

    public void SetDefault()
    {
        currentHealth = MaxHealth;
    }
}
