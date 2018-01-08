using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using UnityEngine.UI;
using UnityEngine;


public class LectorXML : MonoBehaviour {
    public Text tituloTexto;
    public Text unJugadorTexto;
    public Text multijugadorTexto;
    string titulo;
    string unJugador;
    string multijugador;
    public TextAsset diccionario;
    public string nombreIdioma;
    private int idiomaActual=Localizador.idiomaActual;
    List<Dictionary<string, string>> idiomas = new List<Dictionary<string, string>>();
    Dictionary<string, string> obj;

    private void Awake()
    {
        Lector();
    }

    void Update()
    {
        idiomas[idiomaActual].TryGetValue("Nombre", out nombreIdioma);
        idiomas[idiomaActual].TryGetValue("TituloJuego", out titulo);
        idiomas[idiomaActual].TryGetValue("botonUnJugador", out unJugador);
        idiomas[idiomaActual].TryGetValue("botonMultijugador", out multijugador);

    }
    private void OnGUI()
    {
        tituloTexto.text = titulo;
        unJugadorTexto.text = unJugador;
        multijugadorTexto.text = multijugador;
    }

    void Lector() {
        XmlDocument docXml = new XmlDocument();
        docXml.LoadXml(diccionario.text);
        XmlNodeList listaIdiomas = docXml.GetElementsByTagName("idioma");
        foreach (XmlNode valorIdioma in listaIdiomas) {
            XmlNodeList contenidoIdioma = valorIdioma.ChildNodes;
            obj = new Dictionary<string, string>();
            foreach (XmlNode valor in contenidoIdioma) {
                if (valor.Name=="Nombre") {
                    obj.Add(valor.Name, valor.InnerText);

                }
                if (valor.Name == "TituloJuego")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                if (valor.Name == "botonUnJugador")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }
                if (valor.Name == "botonMultijugador")
                {
                    obj.Add(valor.Name, valor.InnerText);

                }

                idiomas.Add(obj);
  
            }
        }
    }

}
