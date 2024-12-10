using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NAME_CONSTRUCTOR
{
    public static class XmlHandler
    {

        public static string GetRootChildValue(string xmlFilePath, string xmlElmentName)
        {
            string XmlElmentValue;
            try
            {
                XDocument xmlDocument = XDocument.Load(xmlFilePath);
                XmlElmentValue = xmlDocument.Descendants(xmlElmentName).FirstOrDefault().Value.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ошибка\n{e}");
                return null;
            }
            return XmlElmentValue;
        }

        public static List<string> GetElementsNamesFromRoot (string xmlFilePath)
        {
            XDocument xmlDocument;
            XElement root;
            try
            {
                xmlDocument = XDocument.Load(xmlFilePath);
                root = xmlDocument.Root;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ошибка\n{e}");
                return null;
            }


            List<string> elementNames = new List<string>();
            foreach (XElement element in root.Elements())
            {
                elementNames.Add(element.Name.ToString());
            }
            return elementNames;
        }

        public static List<ObjectDefinition> GetElementsAsObjectDefinitionFromRoot(string xmlFilePath)
        {
            XDocument xmlDocument;
            XElement root;
            try
            {
                xmlDocument = XDocument.Load(xmlFilePath);
                root = xmlDocument.Root;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ошибка\n{e}");
                return null;
            }

            List<ObjectDefinition> objectTypes = new List<ObjectDefinition>();

            foreach (XElement element in root.Elements())
            {
                ObjectDefinition objectType = new ObjectDefinition
                {
                    ShortName = element.Name.ToString(),
                    FullName = element.Value,
                };
                objectTypes.Add(objectType);
            }
            return objectTypes;
        }

        public static List<string> GetElementsNamesFromRootChild
            (string xmlFilePath, string RootChildElmentName)
        {
            XDocument xmlDocument;
            XElement rootChildElment;
            try
            {
                 xmlDocument = XDocument.Load(xmlFilePath);
                 rootChildElment = 
                    xmlDocument.Descendants(RootChildElmentName).FirstOrDefault();
                if (rootChildElment == null)
                {
                    throw new ArgumentException($"Элемент '{rootChildElment}' " +
                        $"не найден в XML-документе.");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ошибка\n{e}");
                return null;
            }
                        

            List<string> elementNames = new List<string>();
            foreach (XElement element in rootChildElment.Elements())
            {
                elementNames.Add(element.Name.ToString());
            }
            return elementNames;
        }
         
        public static void CreateAndSaveXmlDocWithRoot(
            string xmlFilePath, string rootElementName)
        {
            XElement rootElement = new XElement(rootElementName);
            XDocument xmlDocument = new XDocument(rootElement);
            xmlDocument.Save(xmlFilePath);
        }

        public static void SetElementToRoot(
            string xmlFilePath, string xmlElmentName, string NewElmentValue)
        {
            XDocument xmlDocument = XDocument.Load(xmlFilePath);
            XElement root = xmlDocument.Root;
            if (root != null)
            {
                root.Add(new XElement(xmlElmentName, NewElmentValue));
                xmlDocument.Save(xmlFilePath);
            }
        }

        public static void SetElementToRoot(
            string xmlFilePath, string NewElmentName)
        {
            XDocument xmlDocument = XDocument.Load(xmlFilePath);
            XElement root = xmlDocument.Root;
            if (root != null)
            {
                root.Add(new XElement(NewElmentName));
                xmlDocument.Save(xmlFilePath);
            }
        }

        public static void SetElementToRootChild(
            string xmlFilePath, string RootChildName,
            string NewElmentName, string NewElmentValue)
        {
            XDocument xmlDocument = XDocument.Load(xmlFilePath);

            XElement XmlElement = new XElement(NewElmentName, NewElmentValue);

            XElement RootChildElement = xmlDocument.Root.Element(RootChildName);
            if (RootChildElement != null)
            {
                RootChildElement.Add(XmlElement);
                xmlDocument.Save(xmlFilePath);
            }
        }




    }

}
