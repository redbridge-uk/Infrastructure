using NUnit.Framework;
using Redbridge.Xml;

namespace Easilog.Tests
{
    [TestFixture]
    public class TestGenericSerializer
    {
        public class SerializableObjectClass
        {
            public SerializableObjectClass() { }

            public SerializableObjectClass(string name) 
            {
                Name = name;
            }
            
            public string Name
            {
                get;
                set;
            }
        }

        [Test]
        public void GenericSerializerSerializeDeserializeStringSuccess()
        {
            var serializer = new GenericSerializer<SerializableObjectClass>();
            var testObject = new SerializableObjectClass() { Name = "Object Name" };
            string resultantXml = serializer.Serialize(testObject);
            SerializableObjectClass recreatedInstance = serializer.Deserialize(resultantXml);

            Assert.AreNotSame(testObject, recreatedInstance, "Expected different instances.");
            Assert.AreEqual("Object Name", recreatedInstance.Name, "Expected a recalled name.");
        }
    }
}
