// Entrance console for point defense
// Written by: Adam Bysice
using UnityEngine;
using System.Collections;

public class PointDefenseConsole : MonoBehaviour {

	#region Public Variables
	public int TurretLocation1;
	public int TurretLocation2;
	public int TurretLocation3;
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
				l_ab.UpdateEnterable(Managers.GetInstance().GetPlayerManager().m_pointDefense[TurretLocation1]);
				l_ab.UpdateEnterable(Managers.GetInstance().GetPlayerManager().m_pointDefense[TurretLocation2]);
				l_ab.UpdateEnterable(Managers.GetInstance().GetPlayerManager().m_pointDefense[TurretLocation3]);
			}
		}
	}

	public void OnTriggerExit2D(Collider2D other)
	{
		if (other && Managers.GetInstance().GetNetworkController().isServer)
		{
			EnterAbility l_ab = other.gameObject.GetComponent<EnterAbility>();
			if (l_ab)
				l_ab.UpdateEnterable(null);
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
