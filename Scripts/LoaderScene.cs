using Photon.Pun;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoaderScene : MonoBehaviour
{
    [SerializeField]
    private int idScene;

    [SerializeField]
    private GameObject loadingPanel;

    [SerializeField]
    private Image progressBar;

    private bool inLoading;

    public void StartLoadSceneMP(bool leaveFromRoom)
    {
        if (leaveFromRoom && PhotonNetwork.LeaveRoom())
        {
            StartCoroutine(LoadScene());
        }
        else if(leaveFromRoom)
        {
            StartCoroutine(LoadScene());
        }


        if (DatasetCountPlayers.Instanse.CountPlayers >= 2)
            StartCoroutine(LoadSceneMP());
    }

    public void StartLoadScene()
    {
        if (DatasetCountPlayers.Instanse.CountPlayers >= 2)
            StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        if (inLoading)
            yield break;

        inLoading = true;

        var operation = SceneManager.LoadSceneAsync(idScene);
        while (operation.progress <= 0.8f)
        {
            if (loadingPanel)
                loadingPanel.SetActive(true);

            if (progressBar)
                progressBar.fillAmount = operation.progress;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator LoadSceneMP()
    {
        if (inLoading)
            yield break;

        inLoading = true;

        PhotonNetwork.LoadLevel(idScene);
        while (PhotonNetwork.LevelLoadingProgress <= 0.8f)
        {
            if (loadingPanel)
                loadingPanel.SetActive(true);

            if (progressBar)
                progressBar.fillAmount = PhotonNetwork.LevelLoadingProgress;
            yield return new WaitForEndOfFrame();
        }
    }
}
