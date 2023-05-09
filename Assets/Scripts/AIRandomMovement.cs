using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRandomMovement : MonoBehaviour
{
    public float movSpeed;
    public float rotSpeed = 100f;

    private bool _isWandering = false;
    private bool _isRotL = false;
    private bool _isRotR = false;
    private bool _isWalking = false;

    Rigidbody _rb;
    Animator _animator;

    [SerializeField] private AudioSource idleAudio;
    private static readonly int Walk = Animator.StringToHash("Walk");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (_isWandering == false)
        {
            StartCoroutine(Wander());
        }
        if (_isRotR == true)
        {
            transform.Rotate(transform.up * (Time.deltaTime * rotSpeed));
        }
        if (_isRotL == true)
        {
            transform.Rotate(transform.up * (Time.deltaTime * -rotSpeed));
        }
        if (_isWalking == true)
        {
            _rb.transform.position += transform.forward * movSpeed;
            _animator.SetInteger(Walk, 1);
        }

        if (_isWalking == false)
        {
            _animator.SetInteger(Walk, 0);
        }
    }
    IEnumerator Wander()
    {
        var rottime = Random.Range(1, 3);
        var rotwait = Random.Range(1, 3);
        var rotateDir = Random.Range(1, 3);
        var walkwait = Random.Range(1, 3);
        var walktime = Random.Range(1, 3);

        _isWandering = true;

        yield return new WaitForSeconds(walkwait);
        _isWalking = true;
        yield return new WaitForSeconds(walktime);
        _isWalking = false;
        yield return new WaitForSeconds(rotwait);
        if (rotateDir == 1)
        {
            _isRotR = true;
            yield return new WaitForSeconds(rottime);
            _isRotR = false;
        }
        if (rotateDir == 2)
        {
            _isRotL = true;
            yield return new WaitForSeconds(rottime);
            _isRotL = false;
        }
        _isWandering = false;
        idleAudio.Play();
    }
}
