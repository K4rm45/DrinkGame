using Riptide;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
public ushort Id { get; private set; }
    public string userName { get; private set; }
 

    public void Init(ushort id, string username) {
        Id = id;
        userName = username;
       
    }
    private void OnDestroy() {
        PlayerManager.RemovePlayer(Id);
    }

    #region Messages

    /* ------------------------- MESSAGE SENDING ------------------------- */
    public void ApprovedLogin(bool approve) {
        Message msg = Message.Create(MessageSendMode.Reliable, ServerToClientMSG.ApproveLogin);
        msg.AddBool(approve);
        NetworkManager.Instance.Server.Send(msg, Id);
    }

    #endregion
}
