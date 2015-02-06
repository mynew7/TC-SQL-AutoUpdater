using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoupdater
{
    class Program
    {
        static void Main(string[] args)
        {
            UpdateChecker checker = new UpdateChecker(@"D:\wow\TrinityCore");
            foreach (var fileName in checker.GetNewUpdates("0b8a86886bec1979b698b4109c1f667ba1257fc0"))
            {
                Console.WriteLine("Found update file: " + fileName);
            }
        }
    }
}
