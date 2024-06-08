using UnityEngine;

public class WeaponActivationHandler : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    
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

    public void DisableWeapon()
    {
        SetActive(leftHand, false);
        SetActive(rightHand, false);
    }
    
    private void SetActive(GameObject hand, bool active)
    {
        hand.SetActive(active);
    }
}