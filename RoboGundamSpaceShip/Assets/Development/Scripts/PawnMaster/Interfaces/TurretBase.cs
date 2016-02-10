using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TurretBase : NetworkBehaviour,  IDamageable, IKillable, IRepairable
{

	#region Public Variables
	public int HP;
	public int fireDamage;
	public float fireRate;
	#endregion

	#region Protected Variables
	#endregion

	#region Private Variables
	#endregion

	#region Accessors
	#endregion

	#region Unity Defaults
	//initialization
	public void Start()
	{

	}

	//runs every frame
	public void Update()
	{
	}
	#endregion

	#region Public Methods
	
	public virtual void Fire()
	{

	}

	public void Damage(int damageTaken)
	{

	}

	public void Repair()
	{

	}

	public void FixDamage(int repairAmount)
	{

	}

	public void Kill()
	{

	}
	#endregion

	#region Protected Methods
	#endregion

	#region Private Methods
	#endregion

    
    
    
}
