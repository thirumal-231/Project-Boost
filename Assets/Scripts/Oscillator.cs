using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] Vector3 endPosition;
    [SerializeField] [Range( 0, 1 )] float offsetFactor;
    [SerializeField] float period = 2f;

    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period == 0) { return; }
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin( cycles * tau );

        offsetFactor = (rawSinWave + 1f) / 2;

        Vector3 offset = endPosition * offsetFactor;
        transform.position = startPosition + offset;
    }
}
