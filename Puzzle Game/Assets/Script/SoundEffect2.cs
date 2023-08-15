using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect2 : MonoBehaviour
{
    public AudioSource drag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DragSound()
    {
        drag.Play();
    }
}
