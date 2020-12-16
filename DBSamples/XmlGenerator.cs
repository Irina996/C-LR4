using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace DB.Samples
{
    public class XmlGenerator
    {
        private readonly string pathToXml;

        public XmlGenerator(string path)
        {
            pathToXml = path;
        }

        public void XmlGenerate<T>(IEnumerable<T> info) 
        {
            try
            {
                List<T> emp = new List<T>(info);

                XmlSerializer formatter = new XmlSerializer(typeof(List<T>));

                using (FileStream fs = new FileStream(pathToXml, FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, emp);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
