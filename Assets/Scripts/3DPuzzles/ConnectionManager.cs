using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConnectionManager : MonoBehaviour
{
    public static List<Connection> Connections;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Connection connection in Connections)
        {
            connection.Follow();
        }
    }

    private void OnEnable()
    {
        ConnectionSO.AddConnection += AddConnection;
        ConnectionSO.RemoveConnection += RemoveConnection;
    }

    private void OnDisable()
    {
        ConnectionSO.AddConnection -= AddConnection;
        ConnectionSO.RemoveConnection -= RemoveConnection;
    }

    public bool IsConnected(GameObject main, GameObject target)
    {
        foreach (Connection connection in Connections)
        {
            if (connection.CheckConnection(main, target))
            {
                return true;
            }
        }

        return false;
    }

    private void AddConnection(Connection c)
    {
        Connections.Add(c);
    }

    private void RemoveConnection(Connection c)
    {
        foreach (Connection connection in Connections)
        {
            Debug.Log("In this connection...");
            if (connection.Connection1 == c.Connection1 && connection.Connection2 == c.Connection2 || connection.Connection1 == c.Connection2 && connection.Connection2 == c.Connection1)
            {
                Debug.Log("Connection found.");
                Connections.Remove(connection);
                break;
            }
        }
    }

    public void ChangeMainPiece(GameObject piece)
    {
        Debug.Log(piece.name);
        
        foreach (Connection connection in Connections)
        {
            if (connection.CheckConnection(piece))
            {
                if (connection.Connection1Main && connection.Connection2 == piece)
                {
                    connection.Connection1Main = false;

                    Transform otherPiece = connection.Connection1.transform.parent;

                    // from here
                    for (int i = 0; i < otherPiece.childCount; i++)
                    {
                        if (otherPiece.GetChild(i).gameObject != connection.Connection1) // check tag too btw
                        {
                            Debug.Log(otherPiece.GetChild(i).name + "is now the main piece or smth");
                            ChangeMainPiece(otherPiece.GetChild(i).gameObject);
                        }
                    }


                }
                
                else if (!connection.Connection1Main && connection.Connection1 == piece)
                {
                    connection.Connection1Main = true;

                    Transform otherPiece = connection.Connection2.transform.parent;

                    // from here

                    for (int i = 0; i < otherPiece.childCount; i++)
                    {
                        if (otherPiece.GetChild(i).gameObject != connection.Connection2) // check tag too btw
                        {
                            Debug.Log(otherPiece.GetChild(i).name + "is now the main piece or smth");
                            ChangeMainPiece(otherPiece.GetChild(i).gameObject);
                        }
                    }
                }
            }
        }
    }
}


[Serializable]
public class Connection
{
    public GameObject Connection1;
    public GameObject Connection2;
    public bool Connection1Main;

    public GameObject GetMain()
    {
        return Connection1Main ? Connection1 : Connection2;
    }

    public GameObject GetSecond()
    {
        return !Connection1Main ? Connection1 : Connection2;
    }

    public void Follow()
    {
        Vector3 distance = GetSecond().transform.position - GetMain().transform.position;
        GetSecond().transform.parent.transform.position -= distance;
    }

    public bool CheckConnection(GameObject piece)
    {
        if (GetMain() == piece || GetSecond() == piece)
        {
            return true;
        }

        return false;
    }

    public bool CheckConnection(GameObject main, GameObject second)
    {
        if (GetMain() == main || GetMain() == second || GetSecond() == main || GetSecond() == second)
        {
            return true;
        }

        return false;
    }
}