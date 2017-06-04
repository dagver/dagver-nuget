using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace program
{
    class Program
    {
        public static string[] GetRevList()
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = "rev-list --parents HEAD",
                RedirectStandardOutput = true,
            };
            using (var process = new Process { StartInfo = startInfo })
            {
                process.Start();
                return process.StandardOutput
                    .ReadToEnd()
                    .Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            }
        }

        public static IEnumerable<T> Reversed<T>(ICollection<T> v)
        {
            for(var i = v.Count(); i > 0;)
            {
                --i;
                yield return v.ElementAt(i);
            }
        }

        public static Version GetVersion()
        {
            var map = new Dictionary<string, Version>();

            var revList = GetRevList();

            Version version = null;

            // add all lines to map
            foreach (var rev in Reversed(revList))
            {
                var commit = rev.Split(' ');
                switch(commit.Length)
                {
                    case 1:
                        version = new Version(0, 0, 0);
                        break;
                    case 2:
                        var v = map[commit[1]];
                        version = new Version(
                            v.Height + 1, v.Merge, v.Local + 1);
                        break;
                    default:
                        int h = 0;
                        int m = 0;
                        foreach(var p in commit.Skip(1))
                        {
                            v = map[p];
                            h = Math.Max(h, v.Height);
                            m = Math.Max(m, v.Merge);
                        }
                        version = new Version(h + 1, m + 1, 0);
                        break;
                }
                map[commit[0]] = version;
            }

            return version;
        }

        public sealed class Version
        {
            public int Height { get; }
            public int Merge { get; }
            public int Local { get; }

            public Version(int height, int merge, int local)
            {
                Height = height;
                Merge = merge;
                Local = local;
            }

            public override string ToString()
                => $"0.{Height}.{Merge}.{Local}";
        }

        static void Main(string[] args)
        {
            var version = GetVersion();
            Console.WriteLine(version);
        }
    }
}
