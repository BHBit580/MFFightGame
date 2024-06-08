using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour , IHasCoolDown
{
    [SerializeField] private CoolDownSystem coolDownSystem;
    [SerializeField] private int iD = 20;
    [SerializeField] private float coolDownTime = 3f;
    public int ID => iD;
    public float CoolDownDuration => coolDownTime;

    private void Update()
    {
        bool isOnCooldown = coolDownSystem.IsInCoolDown(iD);
    
        if (!isOnCooldown)
        {
            Debug.Log("HOHOHOHOHHO");
            coolDownSystem.StartCoolDown(this);
        }
    }
}
