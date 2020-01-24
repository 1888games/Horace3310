
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTree : MonoBehaviour

	
{

	public float actualY = -0.7f;
	public InAudioNode died;
	public InAudioNode passed;
		
    // Start is called before the first frame update
    void Start()
    {
		actualY = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {

		if (GameController.Instance.gameIsActive) {

			actualY += GameController.Instance.scrollSpeed;

			float newY = Mathf.Round ((actualY) / 0.0195f) * 0.0195f;


			transform.localPosition = new Vector3 (transform.localPosition.x, newY, 0f);

			if (transform.localPosition.y > 0.7f) {
				SimplePool.Despawn (transform.gameObject);

				if (transform.gameObject.name == "Tree") {
					GameController.Instance.AddScore (1);
				}
			}

		}
    }
    
    
    public void OnTriggerEnter2D (Collider2D other) {

		Debug.Log ("HIT");
		if (other.gameObject.name == "Horace") {

			Debug.Log (tag);

			if (tag == "Obstacle" && transform.localPosition.y <= other.transform.localPosition.y) {
				GameController.Instance.GameOver ();
				InAudio.Play (this.gameObject, died);
				
				
			}

			if (tag == "Pass") {
				GameController.Instance.AddScore (15);
				InAudio.Play (this.gameObject, passed);
			
			}
		}
	}
}
