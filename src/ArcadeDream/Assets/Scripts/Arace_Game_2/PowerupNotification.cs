using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Deactivates powerup collection notifications
/// Author: Lew Fortwangler
/// </summary>
 
public class PowerupNotification : MonoBehaviour
{
    public GameObject Notification;

    private void Start()
    {
        if (Notification.activeInHierarchy == true)
            StartCoroutine("Deactivate");
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(3);
        Notification.SetActive(false);
    }

}
