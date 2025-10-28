using System;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class Program
    {
        // Xml File Urls
        public static string xmlURL = "https://devin-baine.github.io/CSE-445-Assignment-4/Hotels.xml";
        public static string xmlErrorURL = "https://devin-baine.github.io/CSE-445-Assignment-4/HotelsErrors.xml";
        public static string xsdURL = "https://devin-baine.github.io/CSE-445-Assignment-4/Hotels.xsd";

        // Main Entry Point
        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);

            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);

            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Xml Validation
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            try
            {
                XmlSchemaSet schemas = new XmlSchemaSet();
                schemas.Add(null, xsdUrl);

                string errors = string.Empty;

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas = schemas;
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationEventHandler += (sender, e) =>
                {
                    errors += e.Message + Environment.NewLine;
                };

                using (XmlReader reader = XmlReader.Create(xmlUrl, settings))
                {
                    while (reader.Read()) { }
                }

                return string.IsNullOrWhiteSpace(errors) ? "No Error" : errors.Trim();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // Xml To Json Conversion
        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlUrl);

                string jsonText = JsonConvert.SerializeXmlNode(xmlDoc, Newtonsoft.Json.Formatting.Indented, true);
                return jsonText;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}