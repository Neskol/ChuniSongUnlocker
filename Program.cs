using System;
using System.IO;
using System.Xml;

namespace ChuniSongUnlocker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ChuniSongUnlocker");
            string dir = Console.ReadLine();
            FileSearcher(dir);
        }

        static void FileSearcher(string dir)
        {
            DirectoryInfo ChuniMusicFolder = new DirectoryInfo(dir);
            FileSystemInfo[] Structure = ChuniMusicFolder.GetFileSystemInfos();
            foreach (FileSystemInfo Candidate in Structure)
            {
                if (Candidate is DirectoryInfo)
                {
                    FileSearcher(Candidate.FullName);
                }
                else if (Candidate.FullName.Split('.')[Candidate.FullName.Split('.').Length-1]=="xml"&& Candidate.FullName.Split('.')[Candidate.FullName.Split('.').Length - 2].Contains("usic"))
                {
                    Console.WriteLine(Candidate.FullName);
                    DisableFlagRemover(Candidate.FullName);
                }
            }
        }

        static void DisableFlagRemover(string dir)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(dir);
            //Display all the book titles.
            XmlNodeList elemList = doc.GetElementsByTagName("disableFlag");
            foreach (XmlNode x in elemList)
            {
                if (x.InnerText.Equals("true"))
                    x.InnerText = "false";
                Console.WriteLine("Successfully changed attribute in "+dir+", now it is "+x.InnerText);
            }
        }
    }
}
