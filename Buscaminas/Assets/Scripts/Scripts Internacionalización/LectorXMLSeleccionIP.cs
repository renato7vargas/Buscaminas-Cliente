using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class LectorXMLSeleccionIP : MonoBehaviour {
    public Text textoEncabezadoSeleccionarServidor;
    public Text textoSevidorLocal;
    public Text textoServidorNoEncontrado;
    public Text textoServidorExterno;
    public Text textoIpNoValida;
    public Text textoIngresarIP;
    public Text textoAceptar;
    public Text textoRegresar;
    string encabezadoSeleccionarServidor;
    string servidorLocal;
    string servidorNoEncontrado;
    string servidorExterno;
    string ipNoValida;
    string ingresarIp;
    string aceptar;
    string regresar;
    public TextAsset diccionario;
    public string nombreIdioma;
    private int idiomaActual = Localizador.idiomaActual;
    List<Dictionary<string, string>> idiomas = new List<Dictionary<string, string>>();
    Dictionary<string, string> obj;

    private void Awake()
    {
        Lector();
    }

    void Update()
    {
        idiomas[idiomaActual].TryGetValue("Nombre", out nombreIdioma);
        idiomas[idiomaActual].TryGetValue("EncabezadoSelecciónServidor", out encabezadoSeleccionarServidor);
        idiomas[idiomaActual].TryGetValue("ServidorLocal", out servidorLocal);
        idiomas[idiomaActual].TryGetValue("ServidorNoEncontrado", out servidorNoEncontrado);
        idiomas[idiomaActual].TryGetValue("ServidorExterno", out servidorExterno);
        idiomas[idiomaActual].TryGetValue("IPnoValida", out ipNoValida);
        idiomas[idiomaActual].TryGetValue("IngresarIP", out ingresarIp);
        idiomas[idiomaActual].TryGetValue("botonAceptar", out aceptar);
        idiomas[idiomaActual].TryGetValue("botonRegresar", out regresar);

    }
    private void OnGUI()
    {
        textoEncabezadoSeleccionarServidor.text=encabezadoSeleccionarServidor;
        textoSevidorLocal.text=servidorLocal;
        textoServidorNoEncontrado.text=servidorNoEncontrado;
        textoServidorExterno.text=servidorExterno;
        textoIpNoValida.text=ipNoValida;
        textoIngresarIP.text=ingresarIp;
        textoAceptar.text=aceptar;
        textoRegresar.text=regresar;
}

    void Lector()
    {
        XmlDocument docXml = new XmlDocument();
        docXml.LoadXml(diccionario.text);
        XmlNodeList listaIdiomas = docXml.GetElementsByTagName("idioma");
        foreach (XmlNode valorIdioma in listaIdiomas)
        {
            XmlNodeList contenidoIdioma = valorIdioma.ChildNodes;
            obj = new Dictionary<string, string>();
            foreach (XmlNode valor in contenidoIdioma)
            {
                if (valor.Name == "Nombre")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                if (valor.Name == "EncabezadoSelecciónServidor")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                if (valor.Name == "ServidorLocal")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                if (valor.Name == "ServidorNoEncontrado")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                if (valor.Name == "ServidorExterno")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                if (valor.Name == "IPnoValida")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                if (valor.Name == "IngresarIP")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                if (valor.Name == "botonAceptar")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                if (valor.Name == "botonRegresar")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                idiomas.Add(obj);

            }
        }
    }

}

