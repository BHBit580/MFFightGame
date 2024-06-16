using UnityEngine;

public class WeaponActivationHandler : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject rightLeg;

    
    public void EnableWeapon()
    {
        SetActive(leftHand, true);
        SetActive(rightHand, true);
    }

    public void EnableOnlyRightHand()
    {
        SetActive(leftHand, false);
        SetActive(rightHand, true);
    }

    public void EnableOnlyLeftHand()
    {
        SetActive(leftHand, true);
        SetActive(rightHand, false);
    }

    public void EnableRightLeg()
    {
        SetActive(rightLeg, true);
    }

    public void DisableWeapon()
    {
        SetActive(leftHand, false);
        SetActive(rightHand, false);
        SetActive(rightLeg , false);
    }
    
    private void SetActive(GameObject hand, bool active)
    {
        hand.SetActive(active);
    }
}