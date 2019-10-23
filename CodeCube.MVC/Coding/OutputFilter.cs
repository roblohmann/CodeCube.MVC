using System;
using System.IO;
using System.Text;

namespace CodeCube.Mvc.Coding
{
    internal class OutputFilter : Stream
    {
        private readonly Stream _shrink;
        private readonly Func<string, string> _filter;

        public OutputFilter(Stream shrink, Func<string, string> filter)
        {
            _shrink = shrink;
            _filter = filter;
        }


        public override bool CanRead => true;
        public override bool CanSeek => true;
        public override bool CanWrite => true;
        public override void Flush() { _shrink.Flush(); }
        public override long Length => 0;
        public override long Position { get; set; }
        public override int Read(byte[] buffer, int offset, int count)
        {
            return _shrink.Read(buffer, offset, count);
        }
        public override long Seek(long offset, SeekOrigin origin)
        {
            return _shrink.Seek(offset, origin);
        }
        public override void SetLength(long value)
        {
            _shrink.SetLength(value);
        }
        public override void Close()
        {
            _shrink.Close();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            // capture the data and convert to string 
            var data = new byte[count];
            Buffer.BlockCopy(buffer, offset, data, 0, count);
            //var stringToFilter = Encoding.Default.GetString(buffer);
            var stringToFilter = Encoding.UTF8.GetString(buffer);

            // filter the string
            stringToFilter = _filter(stringToFilter);

            // write the data to stream 
            //var outdata = Encoding.Default.GetBytes(stringToFilter);
            var outdata = Encoding.UTF8.GetBytes(stringToFilter);
            _shrink.Write(outdata, 0, outdata.GetLength(0));
        }
    }
}
