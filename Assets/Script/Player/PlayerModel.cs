using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] GameObject[] weaponList;
    [SerializeField] GameObject[] shildList;

    private int currentWeaponIndex = 0;
    private int currentShiledInex = 0;

    public void ChangeNextWeapon()
    {
        if (weaponList.Length == 0)
        {
            return;
        }

        weaponList[currentWeaponIndex].SetActive(false);
        currentWeaponIndex = (currentWeaponIndex + 1 == weaponList.Length) ? 0 : currentWeaponIndex + 1;
        weaponList[currentWeaponIndex].SetActive(true);
    }

    public void ChangeNextShield()
    {
        if (shildList.Length == 0)
        {
            return;
        }

        shildList[currentShiledInex].SetActive(false);
        currentShiledInex = (currentShiledInex + 1 == shildList.Length) ? 0 : currentShiledInex + 1;
        shildList[currentShiledInex].SetActive(true);
    }
}
