using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FinderRooms : MonoBehaviour
{
    [SerializeField]
    private MPListRooms mpListRooms;

    [SerializeField]
    private Transform originForRoom;

    [SerializeField]
    private RoomItem prefabRoom;

    [SerializeField]
    private GameObject viewObject;

    private List<RoomItem> listCreatedRooms;

    private void Awake()
    {
        listCreatedRooms = new List<RoomItem>();
    }

    public void FindRoom(string nameRoom)
    {
        listCreatedRooms.ForEach(createdRoom =>
        {
            Destroy(createdRoom.gameObject);
        });

        listCreatedRooms.Clear();

        if (string.IsNullOrEmpty(nameRoom) || nameRoom == "")
        {
            mpListRooms.TurnView(true);
            TurnView(false);
            return;
        }

        var listRooms = mpListRooms.GetRooms();
        List<RoomItem> neededRooms = new List<RoomItem>();
        listRooms.ForEach(roomItem =>
        {
            if (roomItem.RoomLink.Name.Contains(nameRoom))
                neededRooms.Add(roomItem);
        });


        neededRooms.ForEach(roomItem =>
        {
            if (roomItem.RoomLink.IsVisible)
            {
                var createdRoom = Instantiate(prefabRoom, originForRoom);
                createdRoom.Setup(roomItem.RoomLink);
                listCreatedRooms.Add(createdRoom);
            }
        });
        TurnView(true);
        mpListRooms.TurnView(false);
    }

    private void TurnView(bool value)
    {
        viewObject.SetActive(value);
    }
}
