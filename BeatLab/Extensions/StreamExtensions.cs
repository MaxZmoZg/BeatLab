using System.IO;

namespace System.Web
{
    public static class StreamExtensions
    {
        public static byte[] ToByteArray(this HttpPostedFileBase source)
        {
            using (var binaryReader = new BinaryReader(source.InputStream))
            {
                return binaryReader.ReadBytes(source.ContentLength);
            }
        }
    }
}