using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class Mecha_Statistics : NetworkBehaviour, IDamageable{

    #region Public Variables
    public int MAX_HP = 20;
    [SyncVar]
    public int m_currentHP;
    public Image m_HPbar;
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
        m_HPbar.fillAmount = (float)m_currentHP / (float)MAX_HP;
	}

    #endregion

    #region Public Methods
    public void Damage (int p_damageTaken)
    {
        m_currentHP -= p_damageTaken;

    }
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion

}
