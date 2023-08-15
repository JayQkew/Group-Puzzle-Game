using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    //public static AudioClip whistle, drag;
    static AudioSource whistle;
    //static AudioSource drag;
    // Start is called before the first frame update
    void Start()
    {
        //whistle = Resources.Load<AudioClip>("whistle");
        //drag = Resources.Load<AudioClip>("drag");

        //audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public static void PlaySound (string clip)
    //{
    //    switch (clip)
    //    {
    //        case "Tomtom":
    //            audioSource.PlayOneShot(whistle);
    //            break;

    //        case "Gumgum":
    //            audioSource.PlayOneShot(drag);
    //            break;

    //    }
    //}

    public void WhistleSound()
    {
        whistle.Play();
    }

    //public void DragSound()
    //{
    //    drag.Play();
    //}
}
