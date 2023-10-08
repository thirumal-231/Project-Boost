using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 25;
    [SerializeField] float steerThrust = 25;
    [SerializeField] AudioClip mainThrustClip;

    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

    Rigidbody rb;
    AudioSource audiosource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audiosource = GetComponent<AudioSource>();
    }
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        Vector3 mainThrustAmount = new Vector3( 0, mainThrust, 0 ) * Time.deltaTime;
        if ( Input.GetKey( KeyCode.Space ) )
        {
            StartThrusting(mainThrustAmount);
        }
        else
        {
            StopThrusting();
        }
    }
    private void StopThrusting()
    {
        if (audiosource.isPlaying)
        {
            mainBooster.Stop();
            audiosource.Stop();
        }
    }
    private void StartThrusting(Vector3 mainThrustAmount)
    {
        if (!audiosource.isPlaying)
        {
            mainBooster.Play();
            audiosource.PlayOneShot(mainThrustClip);
        }
        rb.AddRelativeForce(mainThrustAmount);
    }
    void ProcessRotation()
    {
        Vector3 steerThrustAmount = new Vector3( 0, 0, steerThrust ) * Time.deltaTime;
        if ( Input.GetKey( KeyCode.A ) )
        {
            StartSteer(steerThrustAmount, rightBooster);
        }
        else if ( Input.GetKey( KeyCode.D ) )
        {
            StartSteer(-steerThrustAmount, leftBooster);
        }
        else
        {
            StopSteering();
        }
    }
    private void StopSteering()
    {
        rightBooster.Stop();
        leftBooster.Stop();
        RotationFreezer(false);
    }
    private void StartSteer(Vector3 steerThrustAmount, ParticleSystem Booster)
    {
        RotationFreezer(true);
        Booster.Play();
        transform.Rotate(steerThrustAmount);
    }
    void RotationFreezer( bool freeze )
    {
        rb.freezeRotation = freeze;
    }
}
