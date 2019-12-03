using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MainAudio : MonoBehaviour
{

    private AudioSource audioSource_m;
    public AudioClip mainTheme;


    // Start is called before the first frame update
    void Start()
    {
        audioSource_m = GetComponent<AudioSource>();
        StartCoroutine(x());

        
    }

    IEnumerator x()
    {
        yield return new WaitForSeconds(0.1f);
        audioSource_m.clip = mainTheme;
        audioSource_m.Play();
        audioSource_m.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
