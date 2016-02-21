// Script description goes here.
//
// Written by: Adam Bysice
using UnityEngine;
using System.Collections;

public class CannonConsole : MonoBehaviour {

	#region Public Variables
	public int TurretLocation1;
	public int TurretLocation2;
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other && Managers.GetInstance().GetNetworkController().isServer)
		{
			EnterAbility l_ab = other.gameObject.GetComponent<EnterAbility>();
			if (l_ab)
			{
				l_ab.UpdateEnterable(Managers.GetInstance().GetPlayerManager().m_cannons[TurretLocation1]);
				l_ab.UpdateEnterable(Managers.GetInstance().GetPlayerManager().m_cannons[TurretLocation2]);
			}
		}
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if (other && Managers.GetInstance().GetNetworkController().isServer)
		{
			if (other && Managers.GetInstance().GetNetworkController().isServer)
			{
				EnterAbility l_ab = other.gameObject.GetComponent<EnterAbility>();
				if (l_ab)
					l_ab.UpdateEnterable(null);
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
