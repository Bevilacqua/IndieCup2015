using UnityEngine;
using System.Collections;

public class Self_Destruct : MonoBehaviour {
    private float elapsedTime;
    private const float lifeLength = 15f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (elapsedTime > lifeLength)
            Destroy(gameObject);
        else
            elapsedTime += Time.deltaTime;
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
