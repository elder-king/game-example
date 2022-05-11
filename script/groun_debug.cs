using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groun_debug : MonoBehaviour
{ 
    void OnDrawGizmosSelected()
    {
        GameObject Player = GameObject.Find("player2");
        playerFPSMovementRegitBody PlayerControllerScript = Player.GetComponent<playerFPSMovementRegitBody>();

        Gizmos.DrawSphere(transform.position, PlayerControllerScript.ground_Distens);
    }

}
