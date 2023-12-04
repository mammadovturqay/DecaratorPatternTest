using System;

interface IDataSource
{
    void WriteData(string data);
    string ReadData();
}

class FileDataSource : IDataSource
{
    private string filename;

    public FileDataSource(string filename)
    {
        this.filename = filename;
    }

    public void WriteData(string data)
    {
        Console.WriteLine($"Writing data  file '{filename}': {data}");
    }

    public string ReadData()
    {
        Console.WriteLine($"Reading data  file '{filename}'");
        return "Sample data  file";
    }
}

abstract class DataSourceDecorator : IDataSource
{
    protected IDataSource wrappedDataSource;

    public DataSourceDecorator(IDataSource dataSource)
    {
        wrappedDataSource = dataSource;
    }

    public virtual void WriteData(string data)
    {
        wrappedDataSource.WriteData(data);
    }

    public virtual string ReadData()
    {
        return wrappedDataSource.ReadData();
    }
}

class EncryptionDecorator : DataSourceDecorator
{
    public EncryptionDecorator(IDataSource dataSource) : base(dataSource)
    {
    }

    public override void WriteData(string data)
    {
        string encryptedData = Encrypt(data); 
        Console.WriteLine("Encrypting data...");
        base.WriteData(encryptedData);
    }

    public override string ReadData()
    {
        string data = base.ReadData();
        Console.WriteLine("Decrypting data...");
        return Decrypt(data); 
    }

    private string Encrypt(string data)
    {
        return "Encrypted: " + data;
    }

    private string Decrypt(string data)
    {
        return data.Replace("Encrypted: ", "");
    }
}

class CompressionDecorator : DataSourceDecorator
{
    public CompressionDecorator(IDataSource dataSource) : base(dataSource)
    {
    }

    public override void WriteData(string data)
    {
        string compressedData = Compress(data); 
        Console.WriteLine("Compressing data...");
        base.WriteData(compressedData);
    }

    public override string ReadData()
    {
        string data = base.ReadData();
        Console.WriteLine("Decompressing data...");
        return Decompress(data); 
    }

    private string Compress(string data)
    {
        return "Compressed: " + data;
    }

    private string Decompress(string data)
    {
        return data.Replace("Compressed: ", "");
    }
}

class Program
{
    static void Main(string[] args)
    {
        IDataSource fileDataSource = new FileDataSource("TurqayMammadov.txt");

        IDataSource encryptedDataSource = new EncryptionDecorator(fileDataSource);

        IDataSource compressedDataSource = new CompressionDecorator(encryptedDataSource);

        compressedDataSource.WriteData("Write data");

        string data = compressedDataSource.ReadData();
        Console.WriteLine($"Read data: {data}");

        Console.ReadLine();
    }
}
