namespace Trion.API.Utilities;

/// <summary>Generates a memory-efficient random stream for speed testing.</summary>
public static class Network
{
    public static Stream GenerateRandomStream(int sizeInMb)
        => new OnTheFlyRandomStream((long)sizeInMb * 1024 * 1024);
}

/// <summary>Streams random bytes on-the-fly without holding the full payload in memory.</summary>
internal sealed class OnTheFlyRandomStream(long length) : Stream
{
    private long _pos;

    public override bool CanRead  => true;
    public override bool CanSeek  => false;
    public override bool CanWrite => false;
    public override long Length   => length;
    public override long Position
    {
        get => _pos;
        set => throw new NotSupportedException();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        if (_pos >= length) return 0;
        int n = (int)Math.Min(count, length - _pos);
        Random.Shared.NextBytes(buffer.AsSpan(offset, n));
        _pos += n;
        return n;
    }

    public override void Flush() { }
    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
    public override void SetLength(long value)                => throw new NotSupportedException();
    public override void Write(byte[] buf, int off, int cnt)  => throw new NotSupportedException();
}
