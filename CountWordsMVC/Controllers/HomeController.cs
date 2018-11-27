using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CountWordsMVC.Models;
using System.Xml;

namespace CountWordsMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<Words> words = new List<Words>();

            //Load the XML file in XmlDocument.
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("~/XML/kelimeler.xml"));

            //Loop through the selected Nodes.
            foreach (XmlNode node in doc.SelectNodes("/kelimeler/Word"))
            {
                //Fetch the Node values and assign it to Model.
                words.Add(new Words
                {
                    word = node["text"].InnerText,
                    count = int.Parse(node["count"].InnerText),
                   
                    
                });
            }

            return View(words.OrderByDescending(t=>t.count).Take(10));
        }
    }
}