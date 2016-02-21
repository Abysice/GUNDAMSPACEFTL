using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FlakBarrageBehaviour : BulletBehaviour
{

    #region Public Variables
    #endregion

    #region Protected Variables
    #endregion

    #region Private Variables
	private bool AMISERVER = false;
    #endregion

    #region Accessors
    #endregion

    #region Unity Defaults

    void Awake()
    {
    }
    void Start()
    {

        if (isServer)
        {
			AMISERVER = true;
            Destroy(gameObject, m_deathDelay);
        }
    }

    void OnDestroy()
    {
		if (AMISERVER)
		{
			GameObject l_projectile = (GameObject)Instantiate(Managers.GetInstance().GetGameProperties().flakBarrage, transform.position, transform.rotation);
			NetworkServer.Spawn(l_projectile);
			NetworkServer.Destroy(gameObject);
		}

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Ship")
        {
            if (isServer)
            {
<<<<<<< HEAD
                IDamageable L_target = (IDamageable)other.GetComponent(typeof(IDamageable));
                L_target.Damage(m_damagePoints);
                NetworkServer.Destroy(gameObject);
=======
				IDamageable L_target = (IDamageable)other.GetComponent(typeof(IDamageable));
				L_target.Damage(m_damagePoints);
				NetworkServer.Destroy(gameObject);
>>>>>>> 150b7690e4980e91d74ee561df68f6f4ff73c4b0
            }
        }
    }

    #endregion

    #region Public Methods
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion

}
