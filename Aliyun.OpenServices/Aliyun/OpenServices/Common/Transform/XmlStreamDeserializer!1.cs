namespace Aliyun.OpenServices.Common.Transform
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    internal class XmlStreamDeserializer<T> : IDeserializer<Stream, T>
    {
        public T Deserialize(Stream xml)
        {
            T temp;
            Stream temp1 = xml;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                temp = (T) serializer.Deserialize(xml);
            }
            catch (XmlException ex)
            {
                throw new ResponseDeserializationException(ex.Message, ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new ResponseDeserializationException(ex.Message, ex);
            }
            finally
            {
                if (temp1 != null)
                {
                    temp1.Dispose();
                }
            }
            return temp;
        }
    }
}

