using UnityEngine;

public delegate void LimbDamage(float _dmgAmt, string _limbName);
public class LimbManager : MonoBehaviour
{
	public static LimbManager instance;
	public event LimbDamage DamageEvent;
	
	private void Start()
	{
		if(instance == null) instance = this;
		else Destroy(this);
	}

	/// <summary> And event method used to sends data to the rag score when a limb takes damage </summary>
	/// <param name="_dmgAmt">how much damage taken</param>
	/// <param name="_limbName">which limb</param>
	public void LimbDamage(float _dmgAmt, string _limbName) => DamageEvent?.Invoke(_dmgAmt, _limbName);
}
