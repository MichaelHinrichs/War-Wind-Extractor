//Written for War Wind. https://store.steampowered.com/app/1741140/

internal class Program
{
    private static void Main(string[] args)
    {
        BinaryReader br = new(File.OpenRead(args[0]));

        List<int> offsets = [];
        Directory.CreateDirectory(Path.GetDirectoryName(args[0]) + "//" + Path.GetExtension(args[0]));
        int fileCount = br.ReadInt32();

        for (int i = 0; i < fileCount; i++)
            offsets.Add(br.ReadInt32());

        offsets.Add((int)br.BaseStream.Length);

        for (int i = 0; i < fileCount - 1; i++)
        {
            br.BaseStream.Position = offsets[i];
            using FileStream FS = File.Create(Path.GetDirectoryName(args[0]) + "//" + Path.GetExtension(args[0]) + "//" + i);
            BinaryWriter bw = new(FS);
            bw.Write(br.ReadBytes(offsets[i + 1] - offsets[i]));
            bw.Close();
        }
    }
}