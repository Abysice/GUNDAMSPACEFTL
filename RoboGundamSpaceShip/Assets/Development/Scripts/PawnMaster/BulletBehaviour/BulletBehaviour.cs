using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BulletBehaviour : NetworkBehaviour {

    #region Public Variables
    public float m_deathDelay;
    public int m_damagePoints;
    public Vector2 m_target;
    public Vector2 m_bulletPos;
    [SyncVar] public Vector3 m_velocity;
    #endregion

    #region Protected Variables
    protected Rigidbody2D m_rb;
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
            Destroy(gameObject, m_deathDelay);
        }
        m_rb = gameObject.GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void Update()
	{
        m_rb.velocity = m_velocity;

	}

    void OnDestroy()
    {
        NetworkServer.Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Ship")
        {
            if (isServer)
            {
                IDamageable L_target = (IDamageable)other.GetComponent(typeof(IDamageable));
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
