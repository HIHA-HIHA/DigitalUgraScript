using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

public class MPListRooms : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private RoomItem prefabRoom;

    [SerializeField]
    private Transform parentForRoom;

    [SerializeField]
    private GameObject viewObject;

    [SerializeField]
    private List<RoomItem> listCreatedRooms;

    private void Awake()
    {
        listCreatedRooms = new List<RoomItem>();
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (listCreatedRooms == null)
            listCreatedRooms = new List<RoomItem>();


        foreach (var newRoom in roomList)
        {

            if (listCreatedRooms.Count == 0 && newRoom.MaxPlayers > 0)
            {
                CreateRoom(newRoom);
                print("TEN");
            }
            else
            {
                bool isNewRoom = true;

                print("TEN2");
                foreach (var createdRoom in listCreatedRooms)
                {

                    if (createdRoom.RoomLink == newRoom)
                    {
                        print("TEN3");
                        isNewRoom = false;
                        break;
                    }

                }


                print("TEn4");
                if (isNewRoom && !newRoom.RemovedFromList && newRoom.PlayerCount > 0 && newRoom.MaxPlayers > 0)
                {
                    CreateRoom(newRoom);
                    print("TEn4_1");
                    continue;
                }

                print("TEn5");

                var listForDeleteRooms = new List<RoomItem>();
                listCreatedRooms.ForEach(createdRoom =>
                {
                    print("TEn5_1");
                    if (createdRoom.RoomLink.Name == newRoom.Name && (newRoom.RemovedFromList || (newRoom.PlayerCount == 0 || newRoom.MaxPlayers == 0)))
                    {
                        print("TEn5_2");
                        listForDeleteRooms.Add(createdRoom);
                    }
                });

                foreach (var roomForDelete in listForDeleteRooms)
                {
                    print("TEn6");
                    listCreatedRooms.Remove(roomForDelete);
                    Destroy(roomForDelete.gameObject);
                }

            }

            //print(roomList.Count);

            //if (listCreatedRooms.Count > 0)
            //{
            //    ClearListRooms();
            //}

            //roomList.ForEach(roomLink =>
            //{
            //    if (roomLink.IsVisible && roomLink.PlayerCount > 0)
            //    {
            //        CreateRoom(roomLink);
            //    }
            //});
        }
    }

    private void ClearRoom(RoomItem needRoom)
    {
        listCreatedRooms.Remove(needRoom);
        Destroy(needRoom.gameObject);
    }
    private void ClearListRooms()
    {
        listCreatedRooms.ForEach(createdRoom =>
        {
            Destroy(createdRoom.gameObject);
        });
        listCreatedRooms.Clear();
    }



    private void CreateRoom(RoomInfo roomLink)
    {
        var createdRoom = Instantiate(prefabRoom, parentForRoom);
        createdRoom.Setup(roomLink);
        listCreatedRooms.Add(createdRoom);
    }

    public List<RoomItem> GetRooms()
    {
        return listCreatedRooms;
    }

    public void TurnView(bool value)
    {
        viewObject.SetActive(value);
    }

}
