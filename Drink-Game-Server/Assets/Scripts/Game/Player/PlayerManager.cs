using InexperiencedDeveloper.Core;
using Riptide;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private GameObject m_PlayerPrefab;
    private static GameObject s_PlayerPrefab;
    private static Dictionary<ushort, Player> s_Player = new Dictionary<ushort, Player>();
    public static Player GetPlayer(ushort id) {
        s_Player.TryGetValue(id, out Player player);
        return player;
    }
    public static bool RemovePlayer(ushort id) {
        if (s_Player.TryGetValue(id, out Player player)) {
            s_Player.Remove(id);
            return true;
        }
        return false;
    }


    protected override void Awake()
    {
        base.Awake();
        s_PlayerPrefab = m_PlayerPrefab;
    }
    private static void SpawnPlayer(ushort id, string username) { 

        Player player = Instantiate(s_PlayerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        player.name = $"{username} -- {id}";
        player.Init(id, username);
        s_Player.Add(id, player);
        bool shouldApprove = true;
        player.ApprovedLogin(shouldApprove);
    }

    #region Messages

    /* ------------------------- MESSAGE RECIEVING ------------------------- */
    [MessageHandler((ushort)ClientToServertMSG.RequestLogin)]
    private static void RecievingMessage(ushort fromid, Message msg){

        string username = msg.GetString();
        SpawnPlayer(fromid, username);
    }

    #endregion
}
