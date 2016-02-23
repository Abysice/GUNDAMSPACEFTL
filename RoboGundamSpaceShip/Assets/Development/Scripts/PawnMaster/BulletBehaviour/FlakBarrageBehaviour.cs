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
    #endregion

    #region Accessors
    #endregion

    #region Unity Defaults

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
               
                NetworkServer.Destroy(gameObject);
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
