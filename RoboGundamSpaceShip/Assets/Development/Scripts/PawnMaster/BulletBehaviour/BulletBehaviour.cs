using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BulletBehaviour : NetworkBehaviour {

    #region Public Variables
    public float m_deathDelay;
    public int m_damagePoints;
    public Vector2 m_target;
    public Vector2 m_bulletPos;
    #endregion

    #region Protected Variables
    protected bool AMISERVER = false;
    #endregion

    #region Private Variables

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
            GameObject l_projectile = (GameObject)Instantiate(Managers.GetInstance().GetGameProperties().BulletExplosion, transform.position, transform.rotation);
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
                IDamageable L_target = (IDamageable)other.GetComponent(typeof(IDamageable));
                if (L_target != null)
                    L_target.Damage(m_damagePoints);
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
