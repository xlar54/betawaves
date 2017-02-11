using UnityEngine;
using System.Collections;

public class doorwaycontroller : MonoBehaviour {

    public bool isGoal = false;
    public GameObject player;
    public AudioClip doorwayEnterSound;
    public GameObject connectDoorway;
    private Vector3 stopPosition;

    private bool hitGoal = false;

	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            hitGoal = true;
        }

        if (hitGoal)
        {
            stopPosition = connectDoorway.transform.position;
            stopPosition.y += 5;
            stopPosition.x -= 10;

            player.transform.position = Vector3.Lerp(player.transform.position, stopPosition, Time.deltaTime);

            StartCoroutine(Wait(1));
        }
    }

    IEnumerator Wait(float duration)
    {
        //This is a coroutine
        //Debug.Log("Start Wait() function. The time is: " + Time.time);
        //Debug.Log("Float duration = " + duration);
        yield return new WaitForSeconds(duration);   //Wait
        Debug.Log("End Wait() function and the time is: " + Time.time);

        player.transform.position = stopPosition;

        player.GetComponent<playercontroller>().enabled = true;
        player.GetComponent<playercontroller>().jumpSpeed = 0;
        hitGoal = false;

        GameObject.Find("GameController").GetComponent<gamecontroller>().changeRoom = true;
    }


    void OnTriggerEnter(Collider collider)
    {
        if (isGoal)
        {
            if (collider.tag == "Player")
            {
                hitGoal = true;
                player.GetComponent<playercontroller>().enabled = false;
                player.GetComponent<playercontroller>().jumpSpeed = 0;
                GetComponent<AudioSource>().PlayOneShot(doorwayEnterSound);
            }
        }
    }
}
