using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    string collisionTag;
    AudioSource audiosource;
    bool isTransitioning = false;
    bool isCollisionDisabled = false;

    [SerializeField] float levelDelay = 1.5f;
    [SerializeField] AudioClip finishClip;
    [SerializeField] AudioClip crashClip;

    [SerializeField] ParticleSystem finishParticles;
    [SerializeField] ParticleSystem crashParticles;

    

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        collisionTag = collision.gameObject.tag;

        if ( collisionTag == "Finish" )
        {
            SuccesSequence();
            isTransitioning = true;
        } 
        else if ( collisionTag == "Friendly" )
        {

        } else if (collisionTag != "Friendly" || collisionTag != "Finish" )
        {
            CrashSequence();
            isTransitioning = true;
        }
    }

    void SuccesSequence()
    {
        if ( !isTransitioning )
        {
            GetComponent<Movement>().enabled = false;
            audiosource.PlayOneShot(finishClip);
            finishParticles.Play();
            Invoke("NextLevel", levelDelay);
        }
    }

    void CrashSequence()
    {
        if ( !isTransitioning )
        {
            GetComponent<Movement>().enabled = false;
            audiosource.PlayOneShot(crashClip);
            crashParticles.Play();
            Invoke("ReloadLevel", levelDelay);
        }
    }

    void ReloadLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }
    void NextLevel()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = activeSceneIndex + 1;
        try
        {
            Scene nextScene = SceneManager.GetSceneByBuildIndex(nextSceneIndex);
            if (nextScene != null)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
        } catch( Exception e )
        {
            SceneManager.LoadScene(0);
        }
    }

    private void Update()
    {
        RespondToDebugKeys();

    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
            Debug.Log("Loading next level");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isCollisionDisabled = !isCollisionDisabled;
            GetComponent<BoxCollider>().isTrigger = isCollisionDisabled;
            Debug.Log("Collisions: " + !isCollisionDisabled); 
        }
    }
}
