using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace CountWords
{
    class Program
    {
        
        static void Main(string[] args)
        {

            string inFileName = "~Book/mobydick.txt";
            StreamReader sr = new StreamReader(inFileName);
            string text = System.IO.File.ReadAllText(@"~Book/mobydick.txt");
            Regex regex = new Regex("[^a-zA-Z]");
            text = regex.Replace(text, " ").ToLower();
            string[] words = text.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);
            var sorgu = (from string word in words orderby word select word).Distinct();
            string[] sonuc = sorgu.ToArray();
            int sayac = 0;
            string bolme = " ,.";
            string[] fields = null;
            string satir = null;
            
            
            while (!sr.EndOfStream)
            {
                satir = sr.ReadLine();
                satir.Trim();
                fields = satir.Split(bolme.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                sayac += fields.Length;                   
            }
            sr.Close();
            XmlTextWriter yaz = new XmlTextWriter("kelimeler.xml", System.Text.UTF8Encoding.UTF8);
            yaz.Formatting = Formatting.Indented;
            yaz.WriteStartDocument();
            yaz.WriteStartElement("kelimeler");
            foreach (string item in sonuc)
            {
                int count = 0;
                int i = 0;
                if (item.Length>2)
                {
                    while ((i = text.IndexOf(item, i)) != -1)
                    {
                        i += item.Length;
                        count++;

                    }
                    Console.WriteLine("{0} {1}", item, count + " tane");


                    yaz.WriteStartElement("Word");
                    yaz.WriteElementString("text", item);
                    yaz.WriteElementString("count", Convert.ToString(count));
                    yaz.WriteEndElement();
                }
            }
            yaz.WriteEndElement();
            yaz.Close();

            Console.WriteLine("XML dosyası oluşturuldu ve veriler eklendi.");
            Console.WriteLine("Toplam kelime sayısı {0}", sayac);
            


            Console.ReadLine();

        }



    }
}

