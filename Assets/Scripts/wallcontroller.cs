using UnityEngine;
using System.Collections;

public class wallcontroller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponentInParent<playercontroller>().hitWall = true;
        }
    }

    void OnTriggerExit_ex(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponentInParent<playercontroller>().speed = 5f;
        }
    }
}
