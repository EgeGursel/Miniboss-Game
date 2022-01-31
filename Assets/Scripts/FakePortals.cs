using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePortals : MonoBehaviour
{
    public GameObject infoBar;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            infoBar.SetActive(true);
            InfoBarManager.instance.SendQuickSpecial("Fake portal, gotcha! :D", "");
        }
    }
}
