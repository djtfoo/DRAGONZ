using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour 
{
    public float MaxHealth;
    public float currentHealth;

    [SyncVar]
    private float realHealth;

    public Image HealthImage;

    private float updateInterval;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    [Client]
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

            updateInterval += Time.deltaTime;
            if (updateInterval > 0.11f) // 9 times per sec (default unity send rate)
            {
                updateInterval = 0;
                CmdSync(currentHealth);
            }
        }
        else
        {
            currentHealth = realHealth;
        }
	}

    [ClientRpc]
    public void RpcTakeDamage(int _damage)
    {
        currentHealth -= _damage;
    }

    [Command]
    void CmdSync(float _health)
    {
        realHealth = _health;
    }

    public void SetDefault()
    {
        currentHealth = MaxHealth;
    }
}
