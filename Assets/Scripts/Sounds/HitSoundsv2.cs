using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using MonkeyManagers.McMonkeyManager;

public class HitSoundsv2 : MonoBehaviour
{
    public AudioSource audioSource;
    public bool LeftController;
    private float hapticWaitSeconds = 0;
    private Dictionary<string, AudioClip[]> audio;

    void Awake(){
        
    }
    
    void Update() { 
        hapticWaitSeconds = McMonkeyManager.Instance.hapticWait;
        audio = McMonkeyManager.Instance.audio;
        //Debug.Log("audio:" + audio);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (audio.ContainsKey(other.gameObject.tag)){
            //Debug.Log("hitsound");
            PlayRandomSound(audio[other.gameObject.tag], audioSource);
            StartVibration(LeftController, 0.15f, 0.15f);
        }
    }

    void PlayRandomSound(AudioClip[] audioClips, AudioSource audioSource)
    {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }

    public void StartVibration(bool forLeftController, float amplitude, float duration)
    {
        StartCoroutine(HapticPulses(forLeftController, amplitude, duration));
    }

    private IEnumerator HapticPulses(bool forLeftController, float amplitude, float duration)
    {
        float startTime = Time.time;
        uint channel = 0u;
        InputDevice device = ((!forLeftController) ? InputDevices.GetDeviceAtXRNode(XRNode.RightHand) : InputDevices.GetDeviceAtXRNode(XRNode.LeftHand));
        while (Time.time < startTime + duration)
        {
            device.SendHapticImpulse(channel, amplitude, hapticWaitSeconds);
            yield return new WaitForSeconds(hapticWaitSeconds * 0.9f);
        }
    }
}