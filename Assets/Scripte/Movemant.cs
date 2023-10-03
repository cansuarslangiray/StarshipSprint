using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Movemant : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private AudioSource _audio;

    [SerializeField] private float rotationThrust = 100;

    [SerializeField] private float mainThrust = 100;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        ProcessRotation();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!_audio.isPlaying)
            {
                _audio.Play();
            }
            else
            {
                _audio.Stop();
            }
        }

    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplayRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplayRotation(-rotationThrust);
        }
    }

    void ApplayRotation(float rotationThisFrame)
    {
        _rigidbody.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        _rigidbody.freezeRotation = false;
    }
}