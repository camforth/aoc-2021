using System.Text;

namespace AOC._2022;

public static class Day7
{
    public static string Part1()
    {
        var directory = GetSolution();
        var directories = new List<Entry>();
        GetDirectories(directory, directories);

        return directories.Where(x => x.GetSize() <= 100_000).Sum(x => x.GetSize()).ToString();
    }

    public static string Part2()
    {
        var directory = GetSolution();
        const int maxSize = 70_000_000;
        const int atLeastUnusedSize = 30_000_000;
        var totalSize = directory.GetSize();
        var directories = new List<Entry>();
        GetDirectories(directory, directories);
        var directoriesToDelete = directories.Where(x => x.GetSize() >= atLeastUnusedSize - (maxSize - totalSize))
            .OrderBy(x => x.GetSize())
            .ToList();
        return directoriesToDelete.First().GetSize().ToString();
    }

    private static Entry GetSolution()
    {
        var lines = AocHelpers.ReadInputsAsString("input-day7.txt").Skip(1).ToList();

        var currentDirectory = new Entry() { Type = EntryType.Directory, Name = "/" };
        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            if (line.StartsWith('$'))
            {
                var args = line[2..].Split(' ');
                switch (args[0])
                {
                    case "cd":
                        currentDirectory = ExecuteCd(currentDirectory, args[1]);
                        break;
                    case "ls":
                        while (i + 1 < lines.Count && !lines[i + 1].StartsWith('$'))
                        {
                            ExecuteLs(currentDirectory, lines[i + 1]);
                            i++;
                        }
                        break;
                }
            }
        }

        return currentDirectory.GetRootEntry();
    }

    private static Entry ExecuteCd(Entry currentDirectory, string path)
    {
        return path switch
        {
            ".." => currentDirectory.Parent ?? currentDirectory,
            _ => currentDirectory.ChildEntries.First(x => x.Name == path)
        };
    }

    private static void ExecuteLs(Entry currentDirectory, string entry)
    {
        var entrySplit = entry.Split(' ');
        var name = entrySplit[1];
        if (entrySplit[0] == "dir")
        {
            if (currentDirectory.ChildEntries.All(x => x.Name != name))
            {
                currentDirectory.ChildEntries.Add(new Entry { Type = EntryType.Directory, Name = name, Parent = currentDirectory });
            }

            return;
        }
        
        currentDirectory.ChildEntries.Add(new Entry { Type = EntryType.File, Name = name, Size = int.Parse(entrySplit[0]) });
    }
    
    private static void GetDirectories(Entry directory, List<Entry> directories)
    {
        directories.Add(directory);
        foreach (var entry in directory.ChildEntries.Where(x => x.Type == EntryType.Directory))
        {
            GetDirectories(entry, directories);
        }
    }

    private class Entry
    {
        public required EntryType Type { get; init; }
        public required string Name { get; init; }
        public List<Entry> ChildEntries { get; } = new();
        public Entry? Parent { get; set; }
        public int? Size { get; set; }

        public Entry GetRootEntry()
        {
            return Parent is null ? this : Parent.GetRootEntry();
        }

        public int GetSize()
        {
            return Type switch
            {
                EntryType.Directory => ChildEntries.Sum(x => x.GetSize()),
                _ => Size!.Value
            };
        }
    }

    private enum EntryType
    {
        File,
        Directory
    }
}