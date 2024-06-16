using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;

    private int damage;
    private float knockback;
    
    private GameObject Vfx;
    private Vector3 VfxOffset;
    private float shakeIntensity;
    private float shakeTime;
    private CinemachineShake cinemachineShake;

    private List<Collider> alreadyCollidedWith = new List<Collider>();

    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other == myCollider) { return; }
        Debug.Log("k");
        if (alreadyCollidedWith.Contains(other)) { return; }
        Debug.Log("LLLLL");
        alreadyCollidedWith.Add(other);


        if (other.TryGetComponent<CharacterHealth>(out CharacterHealth health))
        {
            if(Vfx != null)
            {
                Vector3 hitPosition = new Vector3(other.transform.position.x, other.transform.position.y,
                    Vfx.transform.position.z);
                GameObject vfx = Instantiate(Vfx, hitPosition + VfxOffset, Quaternion.identity);
                Destroy(vfx , 5);
            }
            if(cinemachineShake != null)
            {
                cinemachineShake.ShakeCamera(shakeIntensity, shakeTime);
            }
            
            health.DealDamage(damage);
        }
        
        

        if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        { 
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized; 
            forceReceiver.AddForce(direction * knockback);
        }
    }

    public void SetAttackValues(int damage, float knockback)
    {
        this.damage = damage;
        this.knockback = knockback;
    }

    public void SetVfx(GameObject Vfx , Vector3 VfxOffset)
    {
        this.Vfx = Vfx;
        this.VfxOffset = VfxOffset;
    }
    
    public void SetShakeValues(float shakeIntensity , float shakeTime , CinemachineShake cinemachineShake)
    {
        this.shakeIntensity = shakeIntensity;
        this.shakeTime = shakeTime;
        this.cinemachineShake = cinemachineShake;
    }

    private void OnDisable()
    {
        alreadyCollidedWith.Clear();
    }
}