using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class ListPlayersView : MonoBehaviour
{
    [SerializeField]
    private UIViewPlayer prefabUIViewPlayer;

    [SerializeField]
    private Transform originForUIViewPlayer;

    [SerializeField]
    private List<UIViewPlayer> uiViewPlayers;

    public void CreateUIViewPlayers(List<Player> players)
    {
        foreach (var player in players)
        {
            var uiViewPlayer = Instantiate(prefabUIViewPlayer, originForUIViewPlayer);
            uiViewPlayer.Setup(player);
            uiViewPlayers.Add(uiViewPlayer);
        }

    }

    public void HandlerChangeActivePlayer(Player newActivePlayer)
    {
        uiViewPlayers.ForEach(view =>
        {
            view.TurnView(view.Player == newActivePlayer);
        });
    }
}
