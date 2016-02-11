using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TurretFire : NetworkBehaviour {

    #region Public Variables
    public int m_fireRate;
    public int m_speed;
    public GameObject m_bullet;
    #endregion

    #region Protected Variables
    #endregion

    #region Private Variables

    #endregion

    #region Accessors
    #endregion

    #region Unity Defaults
    // Use this for initialization
    void Start () {

        //m_projectile = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0))
        {
            Rigidbody2D l_projectile = Instantiate(m_bullet, transform.position, transform.rotation) as Rigidbody2D;
            l_projectile.velocity = transform.TransformDirection(Vector2.up*m_speed);
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
