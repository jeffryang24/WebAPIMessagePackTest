using LZ4;
using MsgPack;
using MsgPack.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace WebApiWithMsgPack.MediaFormatter
{
    public class MessagePackMediaTypeFormatter : MediaTypeFormatter
    {
        private const string MsgPackMime = "application/x-msgpack";

        Func<Type, bool> IsAllowedType = (t) =>
        {
            if (!t.IsAbstract && !t.IsInterface && t != null && !t.IsNotPublic) return true;

            if (typeof(IEnumerable).IsAssignableFrom(t)) return true;

            return false;
        };

        public MessagePackMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(MsgPackMime));
        }

        public override bool CanReadType(Type type)
        {
            if (type == null) throw new ArgumentNullException("type is null");
            return IsAllowedType(type);
        }

        public override bool CanWriteType(Type type)
        {
            if (type == null) throw new ArgumentNullException("type is null");
            return IsAllowedType(type);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            if (type == null) throw new ArgumentNullException("type is null");
            if (writeStream == null) throw new ArgumentNullException("writeStream is null");

            var tcs = new TaskCompletionSource<object>();

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                value = (value as IEnumerable<object>).ToList();
            }

            var serializer = MessagePackSerializer.Get<dynamic>();
            serializer.Pack(writeStream, value);

            tcs.SetResult(null);

            return tcs.Task;
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            var tcs = new TaskCompletionSource<object>();

            if (content.Headers != null && content.Headers.ContentLength == 0) return null;

            try
            {
                var serializer = MessagePackSerializer.Get(type);

                object result;

                using (var unpacker = Unpacker.Create(readStream))
                {
                    unpacker.Read();
                    result = serializer.UnpackFrom(unpacker);
                }

                tcs.SetResult(result);
            }catch(Exception ex)
            {
                if (formatterLogger == null) throw;
                formatterLogger.LogError(string.Empty, ex.Message);
                tcs.SetResult(GetDefaultValueForType(type));
            }

            return tcs.Task;
        }
    }
}