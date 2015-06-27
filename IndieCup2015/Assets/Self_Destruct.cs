using UnityEngine;
using System.Collections;

public class Self_Destruct : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnBecameInvisible()
    {
        Debug.Log(gameObject.GetInstanceID() + " destroyed.");
        Destroy(gameObject);
    }
}
