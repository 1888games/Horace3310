using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleAnimate : MonoBehaviour

	
{

	
    // Start is called before the first frame update
    void Start()
    {

		Scale ();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void Scale () {

		transform.DOPunchScale (new Vector3 (0.005f, 0.005f, 0f), 0.5f, 0, 1f).OnComplete (Scale);

	}
}
