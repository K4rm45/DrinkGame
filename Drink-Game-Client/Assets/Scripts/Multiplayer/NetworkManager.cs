using InexperiencedDeveloper.Core;
using Riptide;
using Riptide.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ServerToClientMSG : ushort { 
    ApproveLogin,
}
public enum ClientToServertMSG : ushort
{
    RequestLogin,
}
public class NetworkManager : Singleton<NetworkManager> 
{
    protected override void Awake()
    {
        base.Awake();
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, true);
    }
    public Client Client;
    [SerializeField] private string m_Ip = "127.0.0.1";
    [SerializeField] private ushort m_PORT = 7777;

    private static string s_Localusername;
    private void Start()
    {
        Client = new Client();
        Client.Connected += OnClientConnected;
    }

    private void OnClientConnected(object sender, EventArgs e)
    {
        PlayerManager.instance.SpawnInitialPlayer(s_Localusername);
    }

    public void Connect(string username)
    {
        s_Localusername = string.IsNullOrEmpty(username) ? $"Guest" : username;
        Client.Connect($"{m_Ip}:{m_PORT}");
    }
    private void FixedUpdate()
    {
        Client.Update();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        Client.Connected-= OnClientConnected;
    }
}
