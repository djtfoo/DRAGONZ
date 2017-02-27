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

        if (isServer)
        {
            if (currentHealth <= 0.0f)
                RpcComponentsActive(false);
            else if (GetComponent<Player>().hasRespawned)
                RpcComponentsActive(true);
        }
	}

    public void TakeDamage(int _damage)
    {
        if (!isServer)
           return;

        currentHealth -= _damage;
    }

    [ClientRpc] // This allows methods to be invoked on clients from server
    void RpcComponentsActive(bool isActive)
    {
        if (!isLocalPlayer)
        {
            Renderer renderer = GetComponent<Renderer>();
            if (renderer != null)
                renderer.enabled = isActive;

            Collider col = GetComponent<Collider>();
            if (col != null)
                col.enabled = isActive;
        }
    }

    public void SetDefault()
    {
        currentHealth = MaxHealth;
    }
}
