using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pickup : MonoBehaviour {

    public AudioClip pickupSound;
    private AudioSource soundsAudioSource;

    // Use this for initialization
    void Start () {

        soundsAudioSource = gameObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(new Vector3(45, 45, 45), 12);

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int points = GameObject.Find("GameController").GetComponent<gamecontroller>().playerScore;
            points += 10;

            GameObject.Find("GameController").GetComponent<gamecontroller>().playerScore = points;
            GameObject.Find("txtScore").GetComponent<Text>().text = "Score:" + points.ToString();
            soundsAudioSource.PlayOneShot(pickupSound);
            transform.GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, pickupSound.length);
        }
    }
}
