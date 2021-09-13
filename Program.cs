using System;
using System.IO;
using System.Xml;

namespace ChuniSongUnlocker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ChuniSongUnlocker R 1.0 by Neskol");
            Console.WriteLine("Please input the folder which contains music.xml:");
            Console.WriteLine("WARNING: For the sake of safety, type ONLY the path to the OPTION folder to prevent unintentional changes (Also works with A000)");
            string dir = Console.ReadLine();
            FileSearcher(dir);
            //CheckValidity(dir);
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
            //XmlDocument Candidate = new XmlDocument();
            //Candidate.Load(dir);
            //XmlNodeList elemList = Candidate.GetElementsByTagName("disableFlag");
            //foreach (XmlNode x in elemList)
            //{
            //    if (x.InnerText.Equals("true"))
            //        x.InnerText = "false";
            //    Console.WriteLine("Successfully changed attribute in " + dir + ", now it is " + x.InnerText);
            //}
            //Candidate.Save(dir);
            //Candidate.Save("D:\\Analysis\\" + dir.Split('\\')[dir.Split('\\').Length - 2] + ".xml");

            XmlDocument Candidate = new XmlDocument();
            Candidate.Load(dir);
            XmlNodeList elemList = Candidate.GetElementsByTagName("disableFlag");
            foreach (XmlNode x in elemList)
            {
                if (x.InnerText.Equals("true"))
                    Console.WriteLine("In " + dir + ", now it is " + x.InnerText + ", test Failed!");
            }
            Candidate.Save(dir);
            Candidate.Save("D:\\Analysis\\" + dir.Split('\\')[dir.Split('\\').Length - 2] + ".xml");
        }

        static void CheckValidity(string dir)
        {
            XmlDocument Candidate = new XmlDocument();
            Candidate.Load(dir);
            XmlNodeList elemList = Candidate.GetElementsByTagName("disableFlag");
            foreach (XmlNode x in elemList)
            {
                if (x.InnerText.Equals("false"))
                Console.WriteLine("In " + dir + ", now it is " + x.InnerText+", test Failed!");
            }
            Candidate.Save(dir);
            Candidate.Save("D:\\Analysis\\" + dir.Split('\\')[dir.Split('\\').Length - 2] + ".xml");
        }
    }
}
