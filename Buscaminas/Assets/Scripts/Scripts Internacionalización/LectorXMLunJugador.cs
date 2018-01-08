using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class LectorXMLunJugador : MonoBehaviour {
    public Text textoRegresar;
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
        idiomas[idiomaActual].TryGetValue("botonRegresar", out regresar);

    }
    private void OnGUI()
    {
        textoRegresar.text =regresar;
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
                if (valor.Name == "botonRegresar")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                idiomas.Add(obj);

            }
        }
    }

}
