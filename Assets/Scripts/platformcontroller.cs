using UnityEngine;
using System.Collections;

public class platformcontroller : MonoBehaviour {

    public bool move = false;
    private Vector3 startingPosition;
    public Vector3 endingPosition;
    public bool loop;
    float lerpTime = 1f;
    float currentLerpTime;
    public float moveSpeed = 0.2F;

    private Vector3 origStartingPosition;
    private Vector3 origEndingPosition;
	// Use this for initialization
	void Start () {

        startingPosition = transform.localPosition;
        origStartingPosition = startingPosition;
        origEndingPosition = endingPosition;
        currentLerpTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {

        if(move)
        {
            currentLerpTime += moveSpeed *Time.deltaTime;

            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            float perc = currentLerpTime / lerpTime;
            transform.localPosition = Vector3.Lerp(startingPosition, endingPosition, perc);

            if (loop)
            {
                if (transform.localPosition == endingPosition)
                {
                    currentLerpTime = 0;
                    startingPosition = endingPosition;
                    endingPosition = origStartingPosition;
                }

                if (transform.localPosition == origStartingPosition)
                {
                    currentLerpTime = 0;
                    startingPosition = origStartingPosition;
                    endingPosition = origEndingPosition;
                }
            }

        }
	
	}

    void OnTriggerEnter(Collider collider)
    {

        if (collider.tag == "Player")
        {
            GameObject.Find("PlayerContainer").GetComponent<playercontroller>().hitPlatform = true;
        }
    }
}
