using FSC_CNest.TerminalAdvanced;
using System;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FileSortRenamer2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Terminal.Title = "FileSortRenamer II";

            while (true)
            {
                try
                {
                    Terminal.WriteLine("""






                =================================================================
                ||                      FileSortRenamer II                     ||
                =================================================================

                                     <--- W E L C O M E --->



                """);

                    Thread.Sleep(3_000);

                    var path = string.Empty;

                    do
                    {
                        Terminal.Clear();

                        path = Terminal.ReadLine("Please enter the directory path: ");
                    }
                    while (string.IsNullOrWhiteSpace(path) || !Directory.Exists(path));

                    Terminal.Clear();

                    Terminal.WriteLine("Do you want to use a search pattern? Using patterns makes it possible to only load special files ...");
                    var pattern = Terminal.ReadLine("Pattern: ")!;

                    Terminal.Clear();

                    Terminal.WriteLine("Loading files ...");

                    var files = new DirectoryInfo(path).GetFiles(pattern).ToList();

                    Terminal.Clear();

                    Terminal.WriteLine("Files loaded!");

                    Thread.Sleep(1_500);

                    var filename = string.Empty;

                    while (true)
                    {
                        do
                        {
                            Terminal.Clear();

                            Terminal.WriteLine("""
                    || Filename Builder ||

                    Variables:

                    Counter:
                    $[i] Count up

                    Real Time:
                    $[day] The current day (Monday, Tuesday, ...)
                    $[d] The current day as date number (1)
                    $[dd] The current day as date double number 01
                    $[month] The current month (January, February, ...)
                    $[m] The current month as date number (1)
                    $[mm] The current month as date double number (01)
                    $[yy] The current year 50% (22)
                    $[yyyy] The current year 100% (2022)
                    $[h] The current hour as time number (1)
                    $[hh] The current hour as time double number(1)
                    $[mi] The current minute as time number (1)
                    $[mmi] The current minute as time double number(1)
                    $[s] The current second as time number (1)
                    $[ss] The current second as time double number(1)

                    File Creation Time:
                    $[fcday] The file creation day (Monday, Tuesday, ...)
                    $[fcd] The file creation day as date number (1)
                    $[fcdd] The file creation day as date double number 01
                    $[fcmonth] The file creation month (January, February, ...)
                    $[fcm] The file creation month as date number (1)
                    $[fcmm] The file creation month as date double number (01)
                    $[fcyy] The file creation year 50% (22)
                    $[fcyyyy] The file creation year 100% (2022)
                    $[fch] The file creation hour as time number (1)
                    $[fchh] The file creation hour as time double number(1)
                    $[fcmi] The file creation minute as time number (1)
                    $[fcmmi] The file creation minute as time double number(1)
                    $[fcs] The file creation second as time number (1)
                    $[fcss] The file creation second as time double number(1)

                    File Last Edited Time:
                    $[feday] The file edited day (Monday, Tuesday, ...)
                    $[fed] The file edited day as date number (1)
                    $[fedd] The file edited day as date double number 01
                    $[femonth] The file edited month (January, February, ...)
                    $[fem] The file edited month as date number (1)
                    $[femm] The file edited month as date double number (01)
                    $[feyy] The file edited year 50% (22)
                    $[feyyyy] The file edited year 100% (2022)
                    $[feh] The file edited hour as time number (1)
                    $[fehh] The file edited hour as time double number(1)
                    $[femi] The file edited minute as time number (1)
                    $[femmi] The file edited minute as time double number(1)
                    $[fes] The file edited second as time number (1)
                    $[fess] The file edited second as time double number(1)

                    File Size:
                    $[b] File Size in Byte

                    File Size in Kilo (1000):
                    $[kb] File Size in MegaByte
                    $[mb] File Size in MegaByte
                    $[gb] File Size in GigaByte
                    $[tb] File Size in TerraByte

                    File Size in Binary (1024):
                    $[kibi] File Size in MegaByte
                    $[mibi] File Size in MegaByte
                    $[gibi] File Size in GigaByte
                    $[tibi] File Size in TerraByte

                    File Name:
                    $[fname] Name of the file without extension
                    $[fext] Extension of the file

                    """);

                            filename = Terminal.ReadLine("Filename (Use the variables on top): ")!;

                            foreach (var c in filename)
                            {
                                if (Path.GetInvalidFileNameChars().Contains(c) && !(new List<char>() { '$', '[', ']' }).Contains(c))
                                {
                                    filename = filename.Replace(c.ToString(), "");
                                }
                            }

                            filename = filename.Trim();

                            Terminal.Clear();

                            Terminal.WriteLine($"Created name: ${filename}");
                        }
                        while (string.IsNullOrWhiteSpace(filename));

                        if (Terminal.ReadBool("Write yes, if you accept the name for the file: "))
                        {
                            break;
                        }
                    }

                    Terminal.Clear();

                    if (Terminal.ReadBool("The files were loaded by naming. Do you want to sort the file list in a different way? [yes|no]: "))
                    {
                        var sort = Terminal.ReadLine("Please enter a sort method {name, extension, size, creationdate, editdate}: ");

                        Terminal.Clear();

                        Terminal.WriteLine("Sorting ...");

                        switch (sort)
                        {
                            case "name":
                                files = (from f in files orderby f.Name select f).ToList();
                                break;
                            case "extension":
                                files = (from f in files orderby f.Extension select f).ToList();
                                break;
                            case "size":
                                files = (from f in files orderby f.Length select f).ToList();
                                break;
                            case "creationdate":
                                files = (from f in files orderby f.CreationTime select f).ToList();
                                break;
                            case "editdate":
                                files = (from f in files orderby f.LastWriteTime select f).ToList();
                                break;
                        }

                        Terminal.Clear();
                    }

                    Terminal.WriteLine("Start Renaming:");

                    Thread.Sleep(3_000);

                    var i = Terminal.ReadInt("Start counter at: ");

                    Terminal.WriteLine();

                    foreach (var file in files)
                    {
                        var tempname = filename
                            .Replace("$[i]", i.ToString().PadLeft((files.Count - 1).ToString().Length, '0'));

                        tempname = tempname
                            .Replace("$[day]", DateTime.Now.ToString("dddd"))
                            .Replace("$[d]", DateTime.Now.ToString("d"))
                            .Replace("$[dd]", DateTime.Now.ToString("dd"))
                            .Replace("$[month]", DateTime.Now.ToString("MMMM"))
                            .Replace("$[m]", DateTime.Now.ToString("M"))
                            .Replace("$[mm]", DateTime.Now.ToString("MM"))
                            .Replace("$[yy]", DateTime.Now.ToString("yy"))
                            .Replace("$[yyyy]", DateTime.Now.ToString("yyyy"))
                            .Replace("$[h]", DateTime.Now.Hour.ToString())
                            .Replace("$[hh]", DateTime.Now.Hour.ToString().PadLeft(2, '0'))
                            .Replace("$[mi]", DateTime.Now.Minute.ToString())
                            .Replace("$[mmi]", DateTime.Now.Minute.ToString().PadLeft(2, '0'))
                            .Replace("$[s]", DateTime.Now.ToString("s"))
                            .Replace("$[ss]", DateTime.Now.Second.ToString().PadLeft(2, '0'));

                        tempname = tempname
                            .Replace("$[fcday]", file.CreationTime.ToString("dddd"))
                            .Replace("$[fcd]", file.CreationTime.ToString("d"))
                            .Replace("$[fcdd]", file.CreationTime.ToString("dd"))
                            .Replace("$[fcmonth]", file.CreationTime.ToString("MMMM"))
                            .Replace("$[fcm]", file.CreationTime.ToString("M"))
                            .Replace("$[fcmm]", file.CreationTime.ToString("MM"))
                            .Replace("$[fcyy]", file.CreationTime.ToString("yy"))
                            .Replace("$[fcyyyy]", file.CreationTime.ToString("yyyy"))
                            .Replace("$[fch]", file.CreationTime.Hour.ToString())
                            .Replace("$[fchh]", file.CreationTime.Hour.ToString().PadLeft(2, '0'))
                            .Replace("$[fcmi]", file.CreationTime.Minute.ToString())
                            .Replace("$[fcmmi]", file.CreationTime.Minute.ToString().PadLeft(2, '0'))
                            .Replace("$[fcs]", file.CreationTime.ToString("s"))
                            .Replace("$[fcss]", file.CreationTime.Second.ToString().PadLeft(2, '0'));

                        tempname = tempname
                            .Replace("$[feday]", file.LastWriteTime.ToString("dddd"))
                            .Replace("$[fed]", file.LastWriteTime.ToString("d"))
                            .Replace("$[fedd]", file.LastWriteTime.ToString("dd"))
                            .Replace("$[femonth]", file.LastWriteTime.ToString("MMMM"))
                            .Replace("$[fem]", file.LastWriteTime.ToString("M"))
                            .Replace("$[femm]", file.LastWriteTime.ToString("MM"))
                            .Replace("$[feyy]", file.LastWriteTime.ToString("yy"))
                            .Replace("$[feyyyy]", file.LastWriteTime.ToString("yyyy"))
                            .Replace("$[feh]", file.LastWriteTime.Hour.ToString())
                            .Replace("$[fehh]", file.LastWriteTime.Hour.ToString().PadLeft(2, '0'))
                            .Replace("$[femi]", file.LastWriteTime.Minute.ToString())
                            .Replace("$[femmi]", file.LastWriteTime.Minute.ToString().PadLeft(2, '0'))
                            .Replace("$[fes]", file.LastWriteTime.ToString("s"))
                            .Replace("$[fess]", file.LastWriteTime.Second.ToString().PadLeft(2, '0'));

                        tempname = tempname
                            .Replace("$[b]", (file.Length).ToString());

                        tempname = tempname
                            .Replace("$[kb]", (file.Length / 1_000).ToString())
                            .Replace("$[mb]", (file.Length / 1_000 / 1_000).ToString())
                            .Replace("$[gb]", (file.Length / 1_000 / 1_000 / 1_000).ToString())
                            .Replace("$[tb]", (file.Length / 1_000 / 1_000 / 1_000 / 1_000).ToString());

                        tempname = tempname
                            .Replace("$[kibi]", (file.Length / 1_024).ToString())
                            .Replace("$[mibi]", (file.Length / 1_024 / 1_024).ToString())
                            .Replace("$[gibi]", (file.Length / 1_024 / 1_024 / 1_024).ToString())
                            .Replace("$[tibi]", (file.Length / 1_024 / 1_024 / 1_024 / 1_024).ToString());

                        tempname = tempname
                            .Replace("$[fname]", Path.GetFileNameWithoutExtension(file.Name))
                            .Replace("$[fext]", file.Extension.Replace(".", ""));

                        try
                        {
                            file.MoveTo(Path.Combine(path, tempname));
                            Terminal.WriteLine($"Renamed {file.Name} to {tempname}");
                        }
                        catch
                        {
                            Terminal.WriteLine($"Can not rename {file.Name}");
                        }

                        i++;
                    }

                    Terminal.WriteLine("Done!");
                    Terminal.ReadKey();
                }
                catch (Exception ex)
                {
                    Terminal.Clear();
                    Terminal.WriteLine(ex);
                    Terminal.ReadKey();
                    continue;
                }
            }
        }
    }
}