using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Mixer : MonoBehaviour
{
    public AudioMixer mixer;
    // Start is called before the first frame update
    public void StompSfx(float velocity)
    {
        Debug.Log("velocity is " + velocity + ", pitch set at " + velocity/20);
        mixer.SetFloat("SpecialPitch", velocity/20);
    }
}
