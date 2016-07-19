namespace Micon.Portable.Files
{
    using Graphics;
    using Newtonsoft.Json;
    using System;
    using System.Reflection;

    /// <summary>
    /// Custom converter for serializing color to json double arrays.
    /// </summary>
    public class ColorConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.GetTypeInfo().IsAssignableFrom(typeof(NGraphics.Color).GetTypeInfo());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonToken.StartArray)
            {
                var array = serializer.Deserialize<double[]>(reader);
                var r = array[0];
                var g = array[1];
                var b = array[2];

                return NGraphics.Color.FromRGB(r, g, b);
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var color = (NGraphics.Color)value;

            if(color != null)
            {
                writer.WriteStartArray();
                writer.WriteValue(color.R);
                writer.WriteValue(color.G);
                writer.WriteValue(color.B);
                writer.WriteEndArray();
            }
            else
            {
                writer.WriteNull();
            }
        }
    }
}
