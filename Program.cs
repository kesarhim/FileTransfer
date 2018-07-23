using SevenZipExtractor;
using System;
using System.IO;
using System.Xml;

namespace FILETransfer
{
    class Program
    {
        static int Main(string[] args)
        {
            string fileName = string.Empty ;
            string target = string.Empty;
            string source = string.Empty ;
            int selectedValue = SelectDeployment(ref fileName,ref source);
            if (fileName != null && fileName != "" && source != null && source != "")
            {
                target = GetTargetPath(selectedValue);
                string source1 = Path.Combine(source, fileName);
                if (target != null && target != string.Empty)
                {
                    string target1 = Path.Combine(target, fileName);
                    if (File.Exists(source1))
                    {
                        DeleteFiles(target);
                        System.IO.File.Copy(source1, target1, true);
                        if (File.Exists(target1))
                        {
                            System.Console.WriteLine("Copy deployment Initiated...............................");
                            using (ArchiveFile file = new ArchiveFile(target1))
                            {
                                file.Extract(target, true);
                            }
                        }
                        else
                            System.Console.WriteLine("File Not Found in Directory");

                    }
                    else
                        System.Console.WriteLine("File Not Found in Network");
                    System.Console.Read();
                }
            }
            else
                Console.WriteLine("File.Source Not Found");
            return 0;
        }

        private static string GetTargetPath(int selectedValue)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                string path = selectedValue == 1 ? "/Check/Himanshu[@build = '999.999']" : selectedValue == 2 ? "/Check/Himanshu[@build = '823.999']" : selectedValue == 3 ? "/Check/Himanshu[@build = '822.999']" :  selectedValue == 4 ? "/Check/Himanshu[@build = 'NG999.999']" : selectedValue == 5 ? "/Check/Himanshu[@build = 'NG823.999']" : string.Empty ;
                xmlDoc.Load(@"XMLFile1.xml");
                if (path != null && path != "")
                {
                    XmlNode node = xmlDoc.SelectSingleNode(path);
                    if (node != null)
                        return node.Attributes?[0]?.InnerText;
                }
            }
            catch(Exception ex)
            {
                return string.Empty;
            }
           
            return string.Empty;
        }

        private static int SelectDeployment (ref string fileName ,ref string source)
        {
            Console.WriteLine("Please Select Deployment...........................");
            Console.WriteLine("1.999.999");
            Console.WriteLine("2.823.999");
            Console.WriteLine("3.822.999");
            Console.WriteLine("4.NG 999.999");
            Console.WriteLine("5.NG 823.999");
            int selectedDeply = int.Parse(Console.ReadLine());
            switch (selectedDeply)
            {
                case 1:
                    {
                        fileName = "iMedClient_999.999.7z";
                        source = @"\\CSPUDEP01\Client\Install\ITBMED.ins\iMed_999.999";
                        break;
                    }
                case 2:
                    {
                        fileName = "iMedClient_823.999.7z";
                        source = @"\\CSPUDEP01\Client\Install\ITBMED.ins\iMed_823.999";
                        break;
                    }
                case 3:
                    {
                        fileName = "iMedClient_822.999.7z";
                        source = @"\\CSPUDEP01\Client\Install\ITBMED.ins\iMed_822.999";
                        break;
                    }
                case 4:
                    {
                        fileName = "iM1Web_999.999.7z";
                        source = @"\\CSPUDEP01\Client\Install\ITBMED.ins\iMed_999.999";
                        break;
                    }
                case 5:
                    {
                        fileName = "iM1Web_823.999.7z";
                        source = @"\\CSPUDEP01\Client\Install\ITBMED.ins\iMed_823.999";
                        break;
                    }
            }
            return selectedDeply;
        }
        private static void DeleteFiles(string target)
        {
            DirectoryInfo dir = new DirectoryInfo(target);
            FileInfo[] info = dir.GetFiles();
            DirectoryInfo[] dirInfo = dir.GetDirectories();
            if (info != null && info.Length > 0)
            {
                foreach (FileInfo item in info)
                {
                    System.Console.WriteLine(item.FullName);
                    if (item.Name != "iMed.Config" && item.Name != "LabFindingsSimulator.exe.config" && item.Name != "LabFindingsSimulator" && item.Name != "LabFindingsSimulator.exe" && item.Name != "Web.config")
                        item.Delete();
                }
                if (dirInfo != null && dirInfo.Length > 0)
                {
                    foreach (DirectoryInfo item in dirInfo)
                        item.Delete(true);
                }
            }
        }
    }
}
