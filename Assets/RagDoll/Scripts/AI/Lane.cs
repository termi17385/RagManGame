using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
	[SerializeField] private List<GameObject> cars = new List<GameObject>();
	[SerializeField, Min(2)] private int carAmount;
	
	[SerializeField] private Transform carContent;

	private string resourcesPath = "Car";
	private Transform[] spawnLocations = new Transform[2];
	private Transform[] exitLocations = new Transform[2];

	private int index = 0;
	private float timer = 1.5f;
	private bool spawn = true;

	private void Awake() => SetupPool();
	/// <summary> handles setting up the pool for the
	/// cars spawning in a set ammount to move </summary>
	private void SetupPool()
	{
		// sets up the spawn and exit locations
		var spawnLocationsT = transform.Find("[SpawnLocations]");
		var exitLocationsT = transform.Find("[ExitLocations]");
		
		spawnLocations[0] = spawnLocationsT.Find("SpawnLocation");
		spawnLocations[1] = spawnLocationsT.Find("SpawnLocation2");
		
		exitLocations[0] = exitLocationsT.Find("ExitLocation");
		exitLocations[1] = exitLocationsT.Find("ExitLocation2");
		
		// Loads all the cars needed
		for(int i = 0; i < carAmount; i++)
		{
			var car = Instantiate(Resources.Load<GameObject>(resourcesPath));
			car.SetActive(false);
			cars.Add(car);
		}
		
		// sets up the cars positions
		for(int i = 0; i < cars.Count; i++)
		{
			Transform spawnT = null;
			Transform exitT = null;
			
			spawnT = i % 2 == 0 ? spawnLocations[0] : spawnLocations[1];
			exitT = i % 2 == 0 ? exitLocations[0] : exitLocations[1];
			
			cars[i].transform.position = spawnT.position;
			cars[i].transform.SetParent(carContent);
			
			cars[i].GetComponent<Car>().target = exitT;
			cars[i].GetComponent<Car>().spawnLocation = spawnT;
			
			var rot = cars[i].transform.rotation;
			rot.x = rot.y = rot.z = 0;
			cars[i].transform.rotation = rot;
		}
	}

	private void LateUpdate()
	{
		if(spawn)
		{
			timer = 1.5f;
			
			cars[index].SetActive(true);
			index++;
			if(index >= cars.Count) index = 0;
			if(index % 2 == 0)
			{
				cars[index].GetComponent<Car>().agent.speed = 45; 
				spawn = false;
				
			}
		}
		else if(!spawn)
		{
			timer -= Time.deltaTime;
			if(timer <= 0){timer = 0; spawn = true;}
		}
	}

	private void Update()
	{
		foreach(var car in cars)
		{
			var carCom = car.GetComponent<Car>();
			if(carCom.DistanceToExit())
			{
				car.SetActive(false);
				car.transform.position = carCom.spawnLocation.position;
				
				var rot = car.transform.rotation;
				rot.x = rot.y = rot.z = 0;
				car.transform.rotation = rot;
			}
		}
	}
}
