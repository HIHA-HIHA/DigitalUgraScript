using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SingletonDataset : MonoBehaviour
{
    [SerializeField]
    private PurchaseManager purchaseManager;

    [SerializeField]
    private QueueManager queueManager;

    [SerializeField]
    private CreatorPlayers creatorPlayers;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private EndGame endGameObject;

    public static SingletonDataset Instanse;

    public QueueManager QueueManager { get => queueManager; private set => queueManager = value; }
    public PurchaseManager PurchaseManager { get => purchaseManager; private set => purchaseManager = value; }
    public CreatorPlayers CreatorPlayers { get => creatorPlayers; set => creatorPlayers = value; }
    public EndGame EndGameObject { get => endGameObject; set => endGameObject = value; }
    public PlayerMovement PlayerMovement { get => playerMovement; set => playerMovement = value; }

    public List<Cell> Cells;



    private void Awake()
    {
        Instanse = this;
    }
}
