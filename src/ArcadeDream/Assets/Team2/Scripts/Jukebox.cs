using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Jukebox functionality. Allows the player to cycle forwards or backwards through all available sound tracks
/// Author: Lew Fortwangler
/// Version: 1
/// </summary>
/// 
public class Jukebox : MonoBehaviour
{
    AudioSource Audio;

    public AudioClip[] Music;
    private AudioClip currentSong_m;

    public GameObject UI;
    public Text songName;


    private void Start()
    {
        currentSong_m = Music[0]; //sets the current song to the first item in array as default
        songName.text = currentSong_m.name; //displays the current song text
        Audio.clip = currentSong_m;
        Audio.Play();
    }

    private void Update()
    {
        if(UI.activeInHierarchy == true) //players are able to change the music if they are interacting with the jukebox
        {
            changeMusic();
        }
    }

    private void changeMusic()
    {
        int pos_m = findPostion();

        if (Input.GetKeyDown(KeyCode.LeftArrow)) //change to previous song
        {
            if(Music[pos_m - 1] == null) //check to see if a previous song exists
            {
                pos_m = Music.Length - 1; //if no set position to last song
            }
            else
            {
                pos_m -= 1; //if yes decrement position
            }
            currentSong_m = Music[pos_m]; //set the new song
            songName.text = currentSong_m.name; //display the new song name
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) //change to next song
        {
            if(Music[pos_m + 1] == null) //check to see if a next song exists
            {
                pos_m = 0; //if no set position to first song
            }
            else
            {
                pos_m += 1; //if yes increment position
            }
            currentSong_m = Music[pos_m]; //set the new song
            songName.text = currentSong_m.name; //display the new song name
        }
    }

    private int findPostion()
    {
        int position = 0;

        for (int i = 0; i < Music.Length; ++i) //finds the position of the current song in the array
        {
            if (Music[i] == currentSong_m)
            {
                position = i;
            }
            else
            {
                ++i;
            }
        }
        return position;
    }
}
