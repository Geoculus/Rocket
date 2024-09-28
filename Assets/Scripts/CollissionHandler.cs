
using System.IO.Compression;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollissionHandler : MonoBehaviour
{
    [SerializeField] AudioClip crashSound;

    [SerializeField] AudioClip finishSound;
    AudioSource[] audioSources;
    AudioSource otherRocketAudioSource;
    [SerializeField] float levelLoadTime = 1f;
    [SerializeField] ParticleSystem FinishParticles;

    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        otherRocketAudioSource = audioSources[1];
    }
    void OnCollisionEnter(Collision other) 
    {
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Fuel":
                Debug.Log("FUEL!");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void RealoadLevel()
    {
        LoadLevel(0);
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if(nextScene < SceneManager.sceneCountInBuildSettings)
        {
            LoadLevel(1);
        }
        else 
        {
            SceneManager.LoadScene(0);
        }
    }

    void LoadLevel(int sceneOffSet)
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + sceneOffSet);
    }

    void StartSuccessSequence()
    {
        PlaySound(finishSound);

        FinishParticles.Play();
        Invoke("LoadNextLevel",levelLoadTime);
        DisableMoving();
    }
    void StartCrashSequence()
    {
        PlaySound(crashSound);

        Invoke("RealoadLevel", levelLoadTime);
        DisableMoving();
    }

    void PlaySound(AudioClip audioClip)
    {
        Debug.Log("PLAY SOUND");
        if(!otherRocketAudioSource.isPlaying)
            otherRocketAudioSource.PlayOneShot(audioClip);
        else
            otherRocketAudioSource.Stop();
    }
    private void DisableMoving()
    {
        audioSources[0].enabled = false;
        GetComponent<Movement>().enabled = false;
    }
}
