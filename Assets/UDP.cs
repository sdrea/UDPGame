using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UDP : MonoBehaviour {

    string Chatname;
    UdpClient socket;
    IPEndPoint target = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 51600);
    static string ChatWindow;
    public Text txt;
    Button btn;
    public Text cht;
    public InputField inp;

    static void OnUdpData(IAsyncResult result)
    {
        UdpClient socket = result.AsyncState as UdpClient;
        IPEndPoint source = new IPEndPoint(IPAddress.Any, 51600);
        try
        {
            byte[] message = socket.EndReceive(result, ref source);
            ChatWindow = ChatWindow + "\n" + Encoding.Default.GetString(message) ;
        }
        catch
        {

        }

        socket.BeginReceive(new AsyncCallback(OnUdpData), socket);
    }

    enum UDPCommands //4 bytes
    {
        CONNECT,
        HEARTBEAT,
        MESSAGE
    }

    // Use this for initialization
    void Start () {

        Chatname = PlayerPrefs.GetString("Name");

        byte[] UDPName = Encoding.Default.GetBytes(Chatname.PadRight(8));
        byte[] UDPCommand = BitConverter.GetBytes((int)UDPCommands.CONNECT);
        byte[] ConnectMessage = new byte[UDPName.Length + UDPCommand.Length];
        Buffer.BlockCopy(UDPName, 0, ConnectMessage, 0, 8);
        Buffer.BlockCopy(UDPCommand, 0, ConnectMessage, 8, 4);

        socket = new UdpClient(0);
        print(socket.Client.LocalEndPoint.ToString());
        print(target.ToString());
        socket.BeginReceive(new AsyncCallback(OnUdpData), socket);
        socket.Send(ConnectMessage, ConnectMessage.Length, target);
    }

    void Update()
    {
        txt.text = ChatWindow;

        if (Input.GetKeyDown(KeyCode.Return)) {
            if (inp.IsActive() && inp.text != "")
            {
                TaskOnClick();
                inp.text = "";
                inp.gameObject.SetActive(false);

            }
            else 
            {
                inp.gameObject.SetActive(true);
                inp.ActivateInputField();

            }
        }
       
    }

    private void TaskOnClick()
    {

            byte[] message = null;
            byte[] UDPName = Encoding.Default.GetBytes(Chatname.PadRight(8));
            byte[] UDPCommand = BitConverter.GetBytes((int)UDPCommands.CONNECT);

            UDPName = Encoding.Default.GetBytes(Chatname.PadRight(8));
            UDPCommand = BitConverter.GetBytes((int)UDPCommands.MESSAGE);
            message = Encoding.Default.GetBytes(cht.text);
            byte[] SendMessage = new byte[UDPName.Length + UDPCommand.Length + message.Length];
            Buffer.BlockCopy(UDPName, 0, SendMessage, 0, 8);
            Buffer.BlockCopy(UDPCommand, 0, SendMessage, 8, 4);
            Buffer.BlockCopy(message, 0, SendMessage, 12, message.Length);
            socket.Send(SendMessage, SendMessage.Length, target);
        
    }
}
