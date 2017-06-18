using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardController : MonoBehaviour {
    public GameObject qrView;

    public void ShowQRCode(int points)
    {
        if (points <= ApplicationManager.Instance.points)
        {
            ApplicationManager.Instance.AddPoints(-points);
            qrView.SetActive(true);
            ApplicationManager.Instance.AddListenerToBackButton(HideQRCode);
        }
        
    }

    public void HideQRCode()
    {
        ApplicationManager.Instance.HideBackbutton();
        qrView.SetActive(false);
    }
}
