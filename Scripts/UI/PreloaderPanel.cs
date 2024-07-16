using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PreloaderPanel : MonoBehaviour
{
    [SerializeField]
    private MPPUNLauncher launcher;

    [SerializeField]
    private Image progressBarImage;

    [SerializeField]
    private UnityEvent onEndLoading;

    private void Start()
    {
        StartCoroutine(Preload());
    }

    private IEnumerator Preload()
    {
        while(progressBarImage.fillAmount < 0.9)
        {
            if (launcher.ClientState == Photon.Realtime.ClientState.JoiningLobby || 
                launcher.ClientState == Photon.Realtime.ClientState.JoinedLobby)
                progressBarImage.fillAmount = 1;

           
            if (progressBarImage.fillAmount < 0.6f)
                progressBarImage.fillAmount += 0.01f;

            yield return new WaitForSeconds(0.1f);
        }
        onEndLoading?.Invoke();
    }
}
