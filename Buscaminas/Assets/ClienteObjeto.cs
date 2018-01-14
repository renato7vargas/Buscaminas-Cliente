using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;

public class ClienteObjeto : MonoBehaviour
{
    public static GeneradorListaEstatico generadorGlobal;
    public string nombre = "renato?";
    public Text direccionIpTexto;
    public GameObject errorLocal;
    public GameObject errorExterno;
    //public string direccionIP;
    public static string direccionConectarse;
    private static readonly Socket ClientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    private const int PORT = 100;

    public void IniciarCliente()
    {
        Debug.Log(nombre);
        //errorLocal.gameObject.SetActive(false);
        //errorExterno.gameObject.SetActive(false);
        ConnectToServer();

    }
    public void ActivarErrorExterno()
    {
        errorExterno.gameObject.SetActive(true);
    }

    public void SetDireccionConectarse(string nuevaDireccion) {
        if (nuevaDireccion == "local")
        {
            direccionConectarse = nuevaDireccion;
        }
        else {
            direccionConectarse = direccionIpTexto.text.ToString();
        }
        
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
    public void IniciarHilos() {
        Thread hiloRespuesta = new Thread(RecibirRespuestaVictoria);
        hiloRespuesta.Start();
    }

    public void AdquirirLista(Peticion peticionLista) {
        string json = JsonUtility.ToJson(peticionLista);
        SendString(json);
        RecibirRespuestaLista();
    }

    public void EnviarVictoria(Peticion peticionVictoria) {
        print("Se va a enviar la peticion desde cliente objeto, la funcion ya fué invocada");
        print(peticionVictoria.TipoPeticion);
        string json = JsonUtility.ToJson(peticionVictoria);
        SendString(json);
        print("peticionEnviada");
    }

    /*private static*/ void ConnectToServer()
    {
        int attempts = 0;
        SiguienteEscena se = new SiguienteEscena();
        //while (!ClientSocket.Connected)
        //{
        try
        {
            attempts++;
            Debug.Log("Connection attempt " + attempts);
            // Change IPAddress.Loopback to a remote IP to connect to a remote host. IPAddress.Loopback
            if (direccionConectarse == "local")
            {
                ClientSocket.Connect(IPAddress.Loopback, PORT);
                se.cambiarEscena(4);
                Debug.Log("Connected");
            }
            else
            {
                ClientSocket.Connect(direccionConectarse, PORT);
                se.cambiarEscena(4);
                Debug.Log("Connected");
            }

            if (attempts >= 2)
            {
                //Debug.Log("No ha sido posible establecer una conexión");

                throw new SocketException();
                //ClientSocket.Shutdown(SocketShutdown.Both);
                //ClientSocket.Close();
                //break;
                //Environment.Exit(0);
            }

        }
        catch (ArgumentNullException)
        {
            errorLocal.gameObject.SetActive(false);
            errorExterno.gameObject.SetActive(true);
            Debug.Log("Error Externo");
            
        }
        catch (SocketException)
        {
            if (direccionConectarse == "local")
            {
                errorExterno.gameObject.SetActive(false);
                errorLocal.gameObject.SetActive(true);
                Debug.Log("Error Local");
            }//else
            //{
             //   errorExterno.gameObject.SetActive(true);
             //   Debug.Log("Error Externo");
            //}

            //Debug.Log("Error");
        }
        






        //}

        //Console.Clear();
        
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
    private static void RecibirRespuestaVictoria() {
        while (true) {
            var buffer = new byte[2048];
            int received = ClientSocket.Receive(buffer, SocketFlags.None);
            if (received == 0) return;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);
            Debug.Log(text);
        }

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
