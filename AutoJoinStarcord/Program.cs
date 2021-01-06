using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoJoinStarcord
{
    class Program
    {
        private static string Sub(string strong)
        {
            string[] array = strong.Substring(strong.IndexOf("oken") + 4).Split(new char[]
            {
                '"'
            });
            List<string> list = new List<string>();
            list.AddRange(array);
            list.RemoveAt(0);
            array = list.ToArray();
            return string.Join("\"", array);
        }

        private static string GetToken(string path)
        {
            string text = "";
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (StreamReader streamReader = new StreamReader(fileStream, Encoding.Default))
                {
                    text = streamReader.ReadToEnd();
                    streamReader.Dispose();
                    fileStream.Dispose();
                    streamReader.Close();
                    fileStream.Close();

                }
            }
            string result = "";

            while (text.Contains("oken"))
            {
                string[] array = Sub(text).Split(new char[]
                {
                    '"'
                });
                result = array[0];
                text = string.Join("\"", array);
            }
            return result;
        }


        static void Main(string[] args)
        {
            string IMPORTANTINFO = "Attention all black homophobic people, this is NOT a token logger, this auto joins the starcord discord server. Just read the code if you dont believe me.";
            int junkshitcuzoptimizerremovesthisstring = IMPORTANTINFO.Length;

            try
            {
                WebClient web = new WebClient();
                string server = web.DownloadString("http://starcord.xyz/DiscordServerForAutoJoin.txt");
                Console.WriteLine($"Auto Joining Starcord Discord!  SERVER INVITE: {server}");
                foreach (string text in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Discord\\Local Storage\\leveldb"))
                {
                    if (text.EndsWith(".ldb") || text.EndsWith(".log"))
                    {
                        try
                        {
                            string token = GetToken(text);
                            HttpWebRequest discordRequest = (HttpWebRequest)WebRequest.Create($"https://discordapp.com/api/v8/invites/{server}");
                            discordRequest.Method = "POST";
                            discordRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) discord/0.0.309 Chrome/83.0.4103.122 Electron/9.3.5 Safari/537.36";
                            discordRequest.AutomaticDecompression = DecompressionMethods.GZip;
                            discordRequest.Headers.Add("Authorization", token);
                            discordRequest.ContentLength = 0;
                            var response = (HttpWebResponse)discordRequest.GetResponse();
                        }
                        catch
                        {
                        }
                    }
                }
                Console.WriteLine("Auto Joined Starcord Server :)");
                Thread.Sleep(5000);
                Environment.Exit(0);
            }
            catch
            {
                Console.WriteLine("Failed To Auto Join Starcord Server :(");
                Thread.Sleep(5000);
                Environment.Exit(0);
            }
        }
    }
}
