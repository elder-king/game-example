using UnityEngine;
using System.Collections;

public class ammoRefall : MonoBehaviour
{
    public Collider col;
    public GameObject myObjectGun; //make ref. in inspector window
    public GameObject myObjectAoutomatec; //make ref. in inspector window
    public GameObject myObjectSniper; //make ref. in inspector window
    public float speed = 1;
    private void Start()
    {
        col = GetComponent<Collider>();
    }
    private void Update()
    {
        transform.Rotate ( new Vector3 (0, speed * 2 * Time.deltaTime, 0));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            var player = other.GetComponent<gunPistol>();
            myObjectGun.GetComponent<gunPistol>().ammo_refall();
            Destroy(gameObject);
        }
        if (other.tag.Equals("Player"))
        {
            var player = other.GetComponent<sniper>();
            myObjectSniper.GetComponent<sniper>().ammo_refall();
            Destroy(gameObject);
        }
        if (other.tag.Equals("Player"))
        {
            Debug.Log("ammo");
            var player = other.GetComponent<gunAutomatec>();
            myObjectAoutomatec.GetComponent<gunAutomatec>().ammo_refall();
            Destroy(gameObject);
        }

    }
}
