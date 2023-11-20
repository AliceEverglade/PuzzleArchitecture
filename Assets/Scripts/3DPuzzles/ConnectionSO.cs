using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// a scriptable object that handles creating and destroying connections between connection points.
/// </summary>
[CreateAssetMenu(menuName = "SO/ConnectionSO")]
public class ConnectionSO : ScriptableObject
{
    // events that trigger on adding or removing a connection to the system.
    public static event Action<Connection> AddConnection;
    public static event Action<Connection> RemoveConnection;

    //creates a connection
    public void Connect(GameObject main, GameObject target)
    {
        Connection c = new Connection();
        c.Connection1 = main;
        c.Connection2 = target;
        c.Connection1Main = true;
        AddConnection?.Invoke(c);
    }

    //destroys a connection
    public void Disconnect(GameObject main, GameObject target)
    {
        Connection c = new Connection();
        c.Connection1 = main;
        c.Connection2 = target;
        c.Connection1Main = true;
        RemoveConnection?.Invoke(c);
    }
}
