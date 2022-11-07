using System;
using System.IO;

namespace Helper {

    class Reader : StreamReader
    {
        public Reader(Stream stream)
            : base(stream)
        {

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(false);
        }
    }

    class Writer : StreamWriter
    {
        public Writer(Stream stream)
            : base(stream)
        {

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(false);
        }
    }

    public class PipeStream : IDisposable
    {
        private readonly Stream _stream;
        private readonly Reader _reader;
        private readonly Writer _writer;

        public PipeStream(Stream stream) { 
            
            _stream = stream;
            _reader = new Reader(stream);
            _writer = new Writer(stream);
        }

        public string Receive()
        {
            return _reader.ReadLine();
        }

        public void Send(string message)
        {
            _writer.WriteLine(message);
            _writer.Flush();
        }

        public void Dispose()
        {
            _reader.Dispose();
            _writer.Dispose();

            _stream.Dispose();
        }
    }
}
