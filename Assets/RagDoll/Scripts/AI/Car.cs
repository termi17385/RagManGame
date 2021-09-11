using System;
using UnityEngine;
using UnityEngine.AI;
public class Car : MonoBehaviour
{
	[NonSerialized] public Transform target;
	[NonSerialized] public Transform spawnLocation;
	
	[NonSerialized] public NavMeshAgent agent;

	private void Awake() => agent = GetComponent<NavMeshAgent>();
	private void Update() => agent.destination = target.position;

	public bool DistanceToExit()
	{
		var dist = Vector3.Distance(transform.position, target.position);
		return dist <= 0.5f;
	}
}
