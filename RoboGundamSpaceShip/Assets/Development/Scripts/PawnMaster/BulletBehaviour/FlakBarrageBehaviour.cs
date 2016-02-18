﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FlakBarrageBehaviour : BulletBehaviour{

    #region Public Variables
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
        Debug.Log(m_deathDelay);
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