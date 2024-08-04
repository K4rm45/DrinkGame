using InexperiencedDeveloper.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public LocalUIScene LocalUI;
    public void Connect()
    {
        if (LocalUI == null) {
            Debug.LogError("No local UI on this scene");
            return;
        }
        string connectInput = "ConnectInput";
        if (!LocalUI.Components.TryGetValue(connectInput, out UIComponent component)) {
            Debug.LogError($"No input component found {connectInput}");
            return;
        }

        InputComponent Input = (InputComponent)component;
        string username = Input.input.text;
        NetworkManager.Instance.Connect(username);   
    }

}
