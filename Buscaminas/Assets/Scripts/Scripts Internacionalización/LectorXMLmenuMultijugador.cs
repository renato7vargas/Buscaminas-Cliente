using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class LectorXMLmenuMultijugador : MonoBehaviour {
    public Text encabezadoTexto;
    public Text inicioSesionTexto;
    public Text registrarseTexto;
    public Text rankingTexto;
    public Text textoRegresar;
    string encabezado;
    string inicioSesion;
    string registrarse;
    string ranking;
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
        idiomas[idiomaActual].TryGetValue("tituloMultijugador", out encabezado);
        idiomas[idiomaActual].TryGetValue("inicioSesion", out inicioSesion);
        idiomas[idiomaActual].TryGetValue("registrarse", out registrarse);
        idiomas[idiomaActual].TryGetValue("ranking", out ranking);
        idiomas[idiomaActual].TryGetValue("botonRegresar", out regresar);

    }
    private void OnGUI()
    {
        encabezadoTexto.text = encabezado;
        inicioSesionTexto.text= inicioSesion;
        registrarseTexto.text = registrarse;
        rankingTexto.text = ranking;
        textoRegresar.text = regresar;
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
                if (valor.Name == "tituloMultijugador")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                if (valor.Name == "inicioSesion")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                if (valor.Name == "registrarse")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                if (valor.Name == "ranking")
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

