using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour

{

	public float minGap = 0.25f;
	public float maxGap = 1f;

	public float xMax = 0.8f;

	public float flagXMax = 0.59f;

	public GameObject treePrefab;
	public GameObject flagPrefab;
	public GameObject bumpPrefab;

	public int chanceOfFlag = 10;

	public int flagCooldown = 0;

	public float gapChange = 0.00001f;
	
	
    // Start is called before the first frame update
    void Start()
    {
		//StartSpawn ();
        
    }

	public void StartSpawn () {

		Invoke ("SpawnTree", Random.Range (minGap, maxGap));
		Invoke ("SpawnBump", Random.Range (minGap, maxGap));


	}
    

    // Update is called once per frame
    void Update()
    {

		if (flagCooldown > 0) {
			flagCooldown--;
		}
    	
        
    }



	void SpawnBump () {

		if (GameController.Instance.gameIsActive) {

			GameObject go = SimplePool.Spawn (bumpPrefab, Vector3.zero, Quaternion.identity);
			go.name = "Bump";
			go.transform.localPosition = new Vector3 (Random.Range (-xMax, xMax), -0.65f, 0f);
			go.GetComponent<MoveTree> ().actualY = go.transform.localPosition.y;

			if (GameController.Instance.gameObjects.Contains (go) == false) {
				GameController.Instance.gameObjects.Add (go);
			}

			Invoke ("SpawnBump", Random.Range (minGap, maxGap));

		}


	}
	
	
	void SpawnTree () {

		if (GameController.Instance.gameIsActive) {

			GameObject go;

			if (flagCooldown == 0) {

				if (Random.Range (0, chanceOfFlag + 1) > 0) {

					go = SimplePool.Spawn (treePrefab, Vector3.zero, Quaternion.identity);
					go.name = "Tree";
					go.transform.localPosition = new Vector3 (Random.Range (-xMax, xMax), -0.65f, 0f);
				} else {
					go = SimplePool.Spawn (flagPrefab, Vector3.zero, Quaternion.identity);
					go.name = "Flag";
					go.transform.localPosition = new Vector3 (Random.Range (-flagXMax, flagXMax), -0.65f, 0f);
					flagCooldown = 60;

				}

				go.GetComponent<MoveTree> ().actualY = go.transform.localPosition.y;
				
				if (GameController.Instance.gameObjects.Contains (go) == false) {
					GameController.Instance.gameObjects.Add (go);
				}

			}
			
			minGap -= gapChange;
			maxGap -= gapChange;

			Invoke ("SpawnTree", Random.Range (minGap, maxGap));
			

		}

		
	
	}
    
    
}
