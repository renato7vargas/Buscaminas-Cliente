using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using System.Net.Sockets;
using System.Net;
using System.Text;

public class ClienteObjeto : MonoBehaviour
{
    public static GeneradorListaEstatico generadorGlobal;
    public string nombre = "renato?";
    private static readonly Socket ClientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    private const int PORT = 100;

    public void IniciarCliente()
    {
        Debug.Log(nombre);
        ConnectToServer();
        
    }

    public void ObtenerHora() {
        Peticion obtenerHora = new Peticion();
        obtenerHora.TipoPeticion = "get time";
        string json = JsonUtility.ToJson(obtenerHora);
        SendString(json);
        //ReceiveResponse();
        RecibirRespuesta();
    }
    public bool EnviarUsuario(Peticion usuario) {
        print(usuario.TipoPeticion);
        print(usuario.NombreUsuario);
        print(usuario.Contrasena);
        string json = JsonUtility.ToJson(usuario);
        print(json);
        SendString(json);
        return RecibirRespuestaUsuario();
    }

    public void AdquirirLista(Peticion peticionLista) {
        string json = JsonUtility.ToJson(peticionLista);
        SendString(json);
        RecibirRespuestaLista();

    }

    private static void ConnectToServer()
    {
        int attempts = 0;

        while (!ClientSocket.Connected)
        {
            try
            {
                attempts++;
                Debug.Log("Connection attempt " + attempts);
                // Change IPAddress.Loopback to a remote IP to connect to a remote host. IPAddress.Loopback
                ClientSocket.Connect(IPAddress.Loopback, PORT);
            }
            catch (SocketException)
            {
                Debug.Log("");//Console.Clear();
            }
        }

        //Console.Clear();
        Debug.Log("Connected");
    }

    /*private static void RequestLoop()
    {
        //Console.WriteLine(@"<Type ""exit"" to properly disconnect client>");

        while (true)
        {
            SendRequest();
            ReceiveResponse();
        }
    }*/

    private static void SendRequest()
    {

        string request = "get time";
        SendString(request);

        //if (request.ToLower() == "exit")
        //{
        //Exit();
        //}
    }



    public static void SendString(string text)
    {
        Debug.Log("Mandare el string");
        Debug.Log(text);
        byte[] buffer = Encoding.ASCII.GetBytes(text);
        ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
    }
    /*public static void MandarUsuario()
    {
        Debug.Log("Mandare al usuario");
        Debug.Log(text);
        byte[] buffer = Encoding.ASCII.GetBytes(text);
        ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
    }*/

    private static void RecibirRespuesta() {
        var buffer = new byte[2048];
        int received = ClientSocket.Receive(buffer, SocketFlags.None);
        if (received == 0) return;
        var data = new byte[received];
        Array.Copy(buffer, data, received);
        string text = Encoding.ASCII.GetString(data);
        Debug.Log(text);
    }

    private static bool RecibirRespuestaUsuario()
    {
        bool existe=false;
        var buffer = new byte[2048];
        int received = ClientSocket.Receive(buffer, SocketFlags.None);
        if (received == 0) return false;
        var data = new byte[received];
        Array.Copy(buffer, data, received);
        string text = Encoding.ASCII.GetString(data);
        Debug.Log(text);
        if (text == "El usuario existe") {
            existe = true;
        }
        return existe;
    }


    private static void RecibirRespuestaLista()
    {

        var buffer = new byte[2048];
        int received = ClientSocket.Receive(buffer, SocketFlags.None);
        if (received == 0) return;
        var data = new byte[received];
        Array.Copy(buffer, data, received);
        string text = Encoding.ASCII.GetString(data);
        Debug.Log(text);
        GeneradorListaEstatico generador;
        generador = JsonUtility.FromJson<GeneradorListaEstatico>(text);
        Debug.Log(generador.PosicionesMinasX[0]);
        Debug.Log(generador.PosicionesMinasX[1]);
        Debug.Log(generador.PosicionesMinasX[2]);
        Debug.Log(generador.PosicionesMinasX[3]);
        Debug.Log(generador.PosicionesMinasX[4]);
        Debug.Log(generador.PosicionesMinasX[5]);
        Debug.Log(generador.PosicionesMinasX[6]);
        Debug.Log(generador.PosicionesMinasX[7]);
        generadorGlobal = generador;
    }

    private static void Exit()
    {
        SendString("exit"); // Tell the server we are exiting
        ClientSocket.Shutdown(SocketShutdown.Both);
        ClientSocket.Close();
        Environment.Exit(0);
    }
    public GeneradorListaEstatico GetGenerador()
    {
        return generadorGlobal;
    }

}
