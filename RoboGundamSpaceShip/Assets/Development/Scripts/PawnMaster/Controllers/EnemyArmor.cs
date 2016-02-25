// Script is base class for things that can be ripped off by the grapple ability
//
// Written By: Adam Bysice

using UnityEngine;
using System.Collections;

public class EnemyArmor : MonoBehaviour, IDamageable {

	#region Public Variables
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	#endregion

	#region Public Methods
	public void Damage(int damageTaken)
	{
		IDamageable l_target = (IDamageable)transform.parent.GetComponent(typeof(IDamageable));
		if (l_target != null)
			l_target.Damage(damageTaken);
	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion
}
