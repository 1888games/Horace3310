using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviourSingleton<GameController>

{

	public float scrollSpeed = 0.01f;
	public float defaultSpeed = 0.01f;
	public float slowSpeed = 0.005f;
	public float currentNormal = 0.01f;

	public int score = 0;
	
	

	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI bestText;

	public float speedIncreaseFromSlowing = 0.0001f;
	public float speedIncreaseGeneral = 0.000005f;

	public bool gameIsActive = false;

	public int increaseCount = 60;

	public int best = 0;

	public TreeSpawner spawner;

	public List<GameObject> gameObjects;

	public Transform horace;
	public Vector3 horacePos;
	public Sprite defaultSprite;

	public GameObject gameOver;
	public GameObject title;

	public bool okToContinue = false;

	public InAudioNode music;
	
	
    // Start is called before the first frame update
    void Start()
    {
    
    	best = PlayerPrefs.GetInt ("highScore", 0);

		//StartGame ();

		horacePos = new Vector3 (0f, 0.38f, 0f);
		
		gameOver.SetActive (false);
		title.SetActive (true);
		
		Invoke ("OkToContinue", 2f);

		InAudio.Play (this.gameObject,music);
		
		
        
    }



	public void OkToContinue () {

		okToContinue = true;

	}


	public void AddScore (int amount) {

		score = score + amount;
		scoreText.text = string.Format (score.ToString (), "d4");

		if (score > best) {
			best = score;
			bestText.text = string.Format (best.ToString (), "d4");
		}

	}

    // Update is called once per frame
    void Update()
    {

		if (gameIsActive) {

			increaseCount--;

			if (increaseCount == 0) {
				currentNormal += speedIncreaseGeneral;
				increaseCount = 60;
			}





			if (scrollSpeed == slowSpeed) {
				currentNormal += speedIncreaseFromSlowing;

			}

			slowSpeed = currentNormal / 2f;

		} else {

			if (Input.anyKeyDown && okToContinue) {

				gameOver.SetActive (false);
				title.SetActive (false);
				StartGame ();
			

			}


		}
       
        
    }

	public void GameOver () {

		gameIsActive = false;

		PlayerPrefs.SetInt ("highScore", best);

		Invoke ("OkToContinue", 2f);

		gameOver.SetActive (true);
		okToContinue = false;
		
		
	}
	
	
	public void StartGame () {
		
		horace.position = new Vector3(0f, 0.37f, 0f);
		horace.GetComponent<SpriteRenderer> ().sprite = defaultSprite;
		
		foreach (GameObject go in GameController.Instance.gameObjects) {
			SimplePool.Despawn (go);
		}
		
		gameIsActive = true;
		score = 0;

		spawner.StartSpawn ();
		
		scoreText.text = string.Format (score.ToString (), "d4");
		bestText.text = string.Format (best.ToString (), "d4");

		currentNormal = defaultSpeed;
		slowSpeed = defaultSpeed / 2f;

	 

	}
}
