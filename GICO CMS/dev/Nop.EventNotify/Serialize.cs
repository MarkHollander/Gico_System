using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using ProtoBuf;

namespace Nop.EventNotify
{
    public class Serialize
    {

        public static byte[] ProtoBufSerialize(Object item)
        {
            if (item != null)
            {
                try
                {
                    var ms = new MemoryStream();
                    Serializer.Serialize(ms, item);
                    var rt = ms.ToArray();
                    return rt;
                }
                catch (ProtoBuf.ProtoException ex)
                {
                    throw new Exception("Unable to serialize object", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to serialize object", ex);
                }
            }
            else
            {
                throw new Exception("Object serialize is null");
            }
        }

        public static byte[] ProtoBufSerialize(Object item, bool isCompress)
        {
            if (item != null)
            {
                try
                {
                    var ms = new MemoryStream();
                    Serializer.Serialize(ms, item);
                    var rt = ms.ToArray();
                    if (isCompress)
                    {
                        rt = Compress(rt);
                    }
                    return rt;
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to serialize object", ex);
                }
            }
            else
            {
                throw new Exception("Object serialize is null");
            }
        }

        public static Stream ProtoBufSerializeToStream(Object item, bool isCompress)
        {
            if (item != null)
            {
                try
                {
                    var ms = new MemoryStream();
                    Serializer.Serialize(ms, item);

                    if (isCompress)
                    {
                        var rt = ms.ToArray();
                        return CompressToStream(rt);
                    }
                    else
                    {
                        return ms;
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to serialize object", ex);
                }
            }
            else
            {
                throw new Exception("Object serialize is null");
            }
        }

        public static T ProtoBufDeserialize<T>(byte[] byteArray)
        {
            if (byteArray != null && byteArray.Length > 0)
            {
                try
                {
                    var ms = new MemoryStream(byteArray);
                    return Serializer.Deserialize<T>(ms);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to deserialize object", ex);
                    //return default(T);
                }
            }
            else
            {
                throw new Exception("Object Deserialize is null or empty");
                //return default(T);
            }
        }

        public static T ProtoBufDeserialize<T>(byte[] byteArray, bool isDecompress)
        {

            if (byteArray != null && byteArray.Length > 0)
            {
                try
                {
                    if (isDecompress)
                    {
                        byteArray = Decompress(byteArray);
                    }
                    return ProtoBufDeserialize<T>(byteArray);
                }
                catch (Exception ex)
                {
                    //throw new Exception("Unable to deserialize object", ex);
                    return default(T);
                }
            }
            else
            {
                throw new Exception("Object Deserialize is null or empty");
                //return default(T);
            }


        }
        public static object ProtoBufDeserialize(byte[] byteArray, Type type)
        {
            if (byteArray != null && byteArray.Length > 0)
            {
                try
                {
                    var ms = new MemoryStream(byteArray);
                    return Serializer.Deserialize(type, ms);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to deserialize object", ex);
                    //return default(T);
                }
            }
            else
            {
                throw new Exception("Object Deserialize is null or empty");
                //return default(T);
            }
        }
        public static object ProtoBufDeserialize(byte[] byteArray, Type type, bool isDecompress)
        {
            if (byteArray != null && byteArray.Length > 0)
            {
                try
                {
                    if (isDecompress)
                    {
                        byteArray = Decompress(byteArray);
                    }
                    return ProtoBufDeserialize(byteArray, type);
                }
                catch (Exception ex)
                {
                    throw new Exception("Unable to deserialize object", ex);
                    //return default(T);
                }
            }
            else
            {
                throw new Exception("Object Deserialize is null or empty");
                //return default(T);
            }
        }


        public static byte[] Compress(byte[] raw)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
                {
                    gzip.Write(raw, 0, raw.Length);
                }
                return memory.ToArray();
            }
        }

        public static Stream CompressToStream(byte[] raw)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true))
                {
                    gzip.Write(raw, 0, raw.Length);
                }
                return memory;
            }
        }

        public static byte[] Decompress(byte[] gzip)
        {
            // Create a GZIP stream with decompression mode.
            // ... Then create a buffer and write into while reading from the GZIP stream.
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                const int size = 4096;
                byte[] buffer = new byte[size];
                using (MemoryStream memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }

        public static string JsonSerializeObject<T>(T obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });
        }

        public static T JsonDeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        public static object JsonDeserializeObject(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }


    }
}