using Riptide;
using Riptide.Utils;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private Server server = new();

    public static bool IsConnected;

    // Codes for initial setup and sending data are from:
    // https://riptide.tomweiland.net/manual/overview/getting-started.html

    void Start()
    {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);
        server.Start(33417, 10);
    }

    private void FixedUpdate()
    {
        server.Update();
        if (Server.ClientID != 0)
            IsConnected = true;
        else
            IsConnected = false;
    }

    private void OnDestroy()
    {
        Server.ClientID = 0;
        server.Stop();
    }

    [MessageHandler(0)]
    private static void HandleMessage1FromServer(ushort i, Message message)
    {
        if (!TimingRecording.IsFinished)
            GameObject.Find("Marble").GetComponent<MarbleMovement>().MarbleMove(message.GetInt(), message.GetInt());
    }

    [MessageHandler(1)]
    private static void HandleMessage2FromServer(ushort i, Message message)
    {
        if (!TimingRecording.IsFinished)
            GameObject.Find("Marble").GetComponent<MarbleMovement>().MarbleJump();
    }
}
