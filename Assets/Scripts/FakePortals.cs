using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePortals : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InfoBarManager.instance.gameObject.SetActive(true);
            InfoBarManager.instance.SendQuickSpecial("Fake portal, gotcha! :D", "");
        }
    }
}
