
using UnityEngine;

public class weapownSwitch : MonoBehaviour
{
    public int slectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        slectWeapon();
    }

    // Update is called once per frame
    void Update()
    {// changing the weapon by the mouse scroll
        int preGUn = slectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (slectedWeapon >= transform.childCount - 1)
                slectedWeapon = 0;
            else
            slectedWeapon++;
        }


        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (slectedWeapon <= 0)
                slectedWeapon = transform.childCount - 1;
            else

            slectedWeapon--;
        }
        if (preGUn != slectedWeapon)
        {
            slectWeapon();
        }
        //changing the weapon by keyborde
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            slectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            slectedWeapon = 1;
        }
    }
    void slectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == slectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
