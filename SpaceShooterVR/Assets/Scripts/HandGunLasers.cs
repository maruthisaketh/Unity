using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HandGunLasers : MonoBehaviour
{
    [SerializeField] private Animator _laserAnimator;
    [SerializeField] private AudioClip _audioClip;
    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireLasers() {
        //animate the gun
        _laserAnimator.SetTrigger("Fire");

        // instantiate the lasers audio
        _audioSource.PlayOneShot(_audioClip);

        //raycast
    }
}
