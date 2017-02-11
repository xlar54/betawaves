using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class gamecontroller : MonoBehaviour {

    public bool changeRoom = true;
    public AudioClip hitWallSound;
    public AudioClip bounceFloorSound;
    public AudioClip bouncePlatformSound;
    public AudioClip gameMusic;
    public int playerScore;
    public float timePerRoom = 60F;
    private float origTimePerRoom;

    private AudioSource soundsAudioSource = new AudioSource();
    private AudioSource musicAudioSource = new AudioSource();


    // Use this for initialization
    void Start () {

        playerScore = 0;

        AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
        soundsAudioSource = allMyAudioSources[0];
        musicAudioSource = allMyAudioSources[1];

        musicAudioSource.PlayOneShot(gameMusic);

        origTimePerRoom = timePerRoom;

    }
	
	// Update is called once per frame
	void Update () {

        if (changeRoom)
            NewRoom();

        timePerRoom -= Time.deltaTime;
        var timeRem = "Time Remaining: " + System.Convert.ToInt32(timePerRoom).ToString();
        GameObject.Find("txtTimeRemaining").GetComponent<Text>().text = timeRem;


    }

    private void NewRoom()
    {
        changeRoom = false;
        timePerRoom = origTimePerRoom;

        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        GameObject.Find("Floor").GetComponent<MeshRenderer>().material.color = newColor;
        GameObject.Find("Ceiling").GetComponent<MeshRenderer>().material.color = newColor;


        newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        var walls = GameObject.FindGameObjectsWithTag("wall");

        foreach(var wall in walls)
        {
            wall.GetComponent<MeshRenderer>().material.color = newColor;
        }

    }

    public void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "hitwall":
                {
                    GetComponent<AudioSource>().PlayOneShot(hitWallSound);
                    break;
                }
            case "bounceFloor":
                {
                    GetComponent<AudioSource>().PlayOneShot(bounceFloorSound);
                    break;
                }
            case "bouncePlatform":
                {
                    GetComponent<AudioSource>().PlayOneShot(bouncePlatformSound);
                    break;
                }
        }
    }
}
