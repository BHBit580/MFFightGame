using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveSpecialBall : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private VoidEventChannelSO QSpecialAnimationEvent;
    private GameObject player;
    private Tween myTween;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myTween = DOVirtual.DelayedCall(7, () => Destroy(gameObject));
    }

    private void Update()
    {
        transform.Translate(player.transform.forward * (speed * Time.deltaTime));
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            QSpecialAnimationEvent.RaiseEvent();
            other.gameObject.GetComponent<CharacterHealth>().DealDamage(50 , true);
            myTween.Kill();
            Destroy(gameObject , 0.1f);
        }
    }
}
