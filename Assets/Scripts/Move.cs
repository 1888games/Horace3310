using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

	public float xSpeed = 0f;
	public float maxSpeed = 0.01f;
	public float speedChange = 0.001f;
	public float actualX = 0f;

	public List<Sprite> sprites;

	SpriteRenderer SpriteRenderer;
	
	
    // Start is called before the first frame update
    void Start()
    {
		SpriteRenderer = GetComponent<SpriteRenderer> ();
    }

    // Update is called once per frame
    void Update()
    {

		if (GameController.Instance.gameIsActive) {

			bool turning = false;
			bool slowing = false;
			GameController.Instance.scrollSpeed = GameController.Instance.currentNormal;

			if (Input.GetKey (KeyCode.UpArrow)) {
				slowing = true;
			}

			if (Input.GetKey (KeyCode.LeftArrow)) {
				xSpeed = Mathf.Max (-maxSpeed, xSpeed - speedChange);




			} else {

				if (Input.GetKey (KeyCode.RightArrow)) {
					xSpeed = Mathf.Min (maxSpeed, xSpeed + speedChange);
					turning = true;

				} else {

					if (xSpeed > 0f) {

						xSpeed = Mathf.Max (0f, xSpeed - speedChange / 4f);
					} else {
						if (xSpeed < 0f) {

							xSpeed = Mathf.Min (0f, xSpeed + speedChange / 4f);
						} else {

						}
					}

				}

			}


			SpriteRenderer.sprite = sprites [2];

			int add = 0;
			if (slowing) {
				add = 3;
				GameController.Instance.scrollSpeed = GameController.Instance.slowSpeed;

			}

			if (xSpeed > maxSpeed / 3f) {
				SpriteRenderer.sprite = sprites [1 + add];
			}

			if (xSpeed < -maxSpeed / 3f) {
				SpriteRenderer.sprite = sprites [0 + add];
			}



			actualX = actualX + xSpeed;

			float newX = Mathf.Clamp (Mathf.Round ((actualX) / 0.0195f) * 0.0195f, -0.8f, 0.8f);

			transform.position = new Vector3 (newX, transform.position.y, transform.position.z);


		}
			
			
        
    }



	
		public void OnColliderEnter (Collider other) {

		Debug.Log ("HIT");
		if (other.gameObject.name == "Tree") {

			Debug.Log ("HIT TREE");
		}
	}
}
