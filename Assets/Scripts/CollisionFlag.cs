

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InAudioSystem;

public class CollisionFlag : MonoBehaviour
{

	public InAudioNode node;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
       public void OnTriggerEnter2D (Collider2D other) {

		Debug.Log ("HIT");
		if (other.gameObject.name == "Horace") {

			Debug.Log (tag);

			if (tag == "Obstacle" && transform.localPosition.y <= other.transform.localPosition.y) {
				GameController.Instance.GameOver ();
				InAudio.Play (this.gameObject, node);
				
				
			}

			if (tag == "Pass") {
				GameController.Instance.AddScore (15);
				
				
			}
		}
	}
}
