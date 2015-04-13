using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedBlog.Model.Infrastructure
{
    public class CustomDateTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var ticks = (long)reader.Value;

            var date = new DateTime(1970, 1, 1);
            date = date.AddSeconds(ticks);

            return date;
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteValue(string.Format("{0:dd.MM.yyyy hh:mm:ss.fff}", value));
        }
    }
}
