using System;

namespace First3Things.ODPProductAttributeConnector.DataAnnotations
{

    [AttributeUsage(AttributeTargets.Property)]
    public class OdpProductSyncAttribute : Attribute
    {
        public OdpProductSyncAttribute(string fieldName)
        {
            FieldName = fieldName;
        }

        public string FieldName { get; }
    }
}