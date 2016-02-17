using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class EnemyShipController : NetworkBehaviour, IDamageable {

    #region Public Variables
    [SyncVar] public int m_HP = 100;
	#endregion

	#region Protected Variables
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
		
	}

	// Update is called once per frame
	void Update()
	{
        if (isServer)
        {
            if(m_HP <=0)
                NetworkServer.Destroy(gameObject);
        }
    }

    #endregion

    #region Public Methods
    public void Damage(int damageTaken)
    {
        m_HP -= damageTaken;
        
    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion

}
