using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO/ConnectionSO")]
public class ConnectionSO : ScriptableObject
{
    public static event Action<Connection> AddConnection;
    public static event Action<Connection> RemoveConnection;

    public void Connect(GameObject main, GameObject target)
    {
        Connection c = new Connection();
        c.Connection1 = main;
        c.Connection2 = target;
        c.Connection1Main = true;
        AddConnection?.Invoke(c);
    }

    public void Disconnect(GameObject main, GameObject target)
    {
        Connection c = new Connection();
        c.Connection1 = main;
        c.Connection2 = target;
        c.Connection1Main = true;
        RemoveConnection?.Invoke(c);
    }
}
