﻿using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class Ship_Statistics : NetworkBehaviour, IDamageable{

    #region Public Variables
    public int MAX_HP = 100;
    [SyncVar]
    public int m_currentHP;
    public Image m_HealthBar;
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
        m_currentHP = MAX_HP;
	}

	// Update is called once per frame
	void Update()
	{
        m_HealthBar.fillAmount = (float)m_currentHP / (float)MAX_HP;
	}

    #endregion

    #region Public Methods
    public void Damage(int l_damage)
    {
        m_currentHP -= l_damage;
        //if (m_currentHP <= 0)
        //{
        //    Destroy(gameObject);
        //}
    }
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion

}
