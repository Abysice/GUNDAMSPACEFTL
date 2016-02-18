using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TurretFireAbility : NetworkBehaviour {

    #region Public Variables
    public float m_fireRate;
    public int m_speed;
    public GameObject m_bullet;
    #endregion

    #region Protected Variables
    #endregion

    #region Private Variables
    private Transform m_spawnLoacation;
    private float m_nextFire;
	private NetworkIdentity m_id;
    #endregion

    #region Accessors
    #endregion

    #region Unity Defaults
    // Use this for initialization
    void Start () {

        m_spawnLoacation = transform.GetChild(0);
		m_id = gameObject.GetComponent<NetworkIdentity>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasAuthority)
            return;

		if (isServer && m_id.clientAuthorityOwner == null) //prevent server default authority bug
			return;

        if (Input.GetMouseButton(0) && Time.time > m_nextFire)
        {
            m_nextFire = Time.time + m_fireRate;
            CmdRequestBullet(m_spawnLoacation.position, m_spawnLoacation.rotation, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
	
	}
    #endregion

    #region Public Methods
    [Command]
    public void CmdRequestBullet(Vector2 p_position, Quaternion p_rotation, Vector2 target)
    {
        float L_totalDist = Vector2.Distance(target, p_position);
        Vector2 l_velocity = transform.TransformDirection(Vector2.up * m_speed);
        float timeDelay = Mathf.Abs(L_totalDist)/ l_velocity.magnitude;
        GameObject l_projectile = (GameObject)Instantiate(m_bullet, p_position, p_rotation);
        l_projectile.GetComponent<BulletBehaviour>().m_velocity = l_velocity;
        l_projectile.GetComponent<BulletBehaviour>().m_deathDelay = timeDelay;
        NetworkServer.Spawn(l_projectile);

    }
    #endregion

    #region Protected Methods
    #endregion

    #region Private Methods
    #endregion
}
