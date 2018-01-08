using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class LectorXMLInicioSesion : MonoBehaviour {
    public Text inicioSesionTexto;
    public Text nombreUsuarioTexto;
    public Text contrasenaTexto;
    public Text ingresarTexto1;
    public Text ingresarTexto2;
    public Text botonInicioSesion;
    public Text botonRegresar;
    string inicioSesion;
    string nombreUsuario;
    string contrasena;
    string ingresar;
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
        idiomas[idiomaActual].TryGetValue("inicioSesion", out inicioSesion);
        idiomas[idiomaActual].TryGetValue("nombreUsuario", out nombreUsuario);
        idiomas[idiomaActual].TryGetValue("contraseña", out contrasena);
        idiomas[idiomaActual].TryGetValue("ingresarTexto", out ingresar);
        idiomas[idiomaActual].TryGetValue("botonRegresar", out regresar);

    }
    private void OnGUI()
    {
        inicioSesionTexto.text = inicioSesion;
        nombreUsuarioTexto.text=nombreUsuario;
        contrasenaTexto.text=contrasena;
        ingresarTexto1.text=ingresar;
        ingresarTexto2.text=ingresar;
        botonRegresar.text=regresar;
        botonInicioSesion.text = inicioSesion;

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
                if (valor.Name == "nombreUsuario")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                if (valor.Name == "inicioSesion")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                if (valor.Name == "contraseña")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                if (valor.Name == "ingresarTexto")
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
