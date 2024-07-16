using Photon.Pun;

using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

using Hash = ExitGames.Client.Photon.Hashtable;

public class WinPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text winField;

    [SerializeField]
    private LoaderScene loaderScene;

    private void Start()
    {
        Player winPlayer = null;

        SingletonDataset.Instanse.QueueManager.Players.ForEach(player =>
        {
            if (!player.Lose)
                winPlayer = player;
        });
        winField.text = $"Поздравляем с победой игрока: {winPlayer.Name}";


        Hash hash = new();
        hash.Add("EndGame", null);
        hash.Add("Winner", winPlayer.PlayerLink);
        PhotonNetwork.SetPlayerCustomProperties(hash);
    }

    public void HanlderClickBack()
    {

    }
}
