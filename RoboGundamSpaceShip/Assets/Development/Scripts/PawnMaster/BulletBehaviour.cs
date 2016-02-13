using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BulletBehaviour : NetworkBehaviour {

    #region Public Variables
    public float m_deathDelay;
    public float m_speed;
    [SyncVar] public Vector3 m_velocity;
    #endregion

    #region Protected Variables
    #endregion

    #region Private Variables
    private Rigidbody2D m_rb;
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults

	void Awake()
	{
	}
	void Start()
	{
        Destroy(gameObject, m_deathDelay);
        m_rb = gameObject.GetComponent<Rigidbody2D>();
    }

	// Update is called once per frame
	void Update()
	{
        m_rb.velocity = m_velocity;

	}

	#endregion

	#region Public Methods
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion

}
