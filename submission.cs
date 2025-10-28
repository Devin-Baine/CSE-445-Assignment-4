using System;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class Program
    {
        // XML File URL Variables
        public static string xmlURL = "https://devin-baine.github.io/CSE-445-Assignment-4/Hotels.xml";
        public static string xmlErrorURL = "https://devin-baine.github.io/CSE-445-Assignment-4/HotelsErrors.xml";
        public static string xsdURL = "https://devin-baine.github.io/CSE-445-Assignment-4/Hotels.xsd";

        // Main Entry Point
        public static void Main(string[] args)
        {
            // Validate Standard XML
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);

            // Validate Error XML
            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);

            // Convert XML To JSON
            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // XML Validation Method
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            try
            {
                // Create Schema Set
                XmlSchemaSet schemas = new XmlSchemaSet();
                schemas.Add(null, xsdUrl);

                // Initialize Error Storage
                string errors = string.Empty;

                // Configure Reader Settings
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas = schemas;
                settings.ValidationType = ValidationType.Schema;

                // Assign Validation Handler
                settings.ValidationEventHandler += (sender, e) =>
                {
                    errors += e.Message + Environment.NewLine;
                };

                // Perform Validation Read
                using (XmlReader reader = XmlReader.Create(xmlUrl, settings))
                {
                    while (reader.Read()) { }
                }

                // Return Validation Result
                return string.IsNullOrWhiteSpace(errors) ? "No Error" : errors.Trim();
            }
            catch (Exception ex)
            {
                // Return Exception Message
                return ex.Message;
            }
        }

        // XML To JSON Conversion Method
        public static string Xml2Json(string xmlUrl)
        {
            try
            {
                // Load XML Document
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlUrl);

                // Serialize XML To JSON
                string jsonText = JsonConvert.SerializeXmlNode(
                    xmlDoc,
                    Newtonsoft.Json.Formatting.Indented,
                    true
                );

                // Return JSON Output
                return jsonText;
            }
            catch (Exception ex)
            {
                // Return Exception Message
                return ex.Message;
            }
        }
    }
}
