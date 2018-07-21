#define aaa
using ConsoleApp1.DBHelper;
using ConsoleApp1.配置文件;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.WriteLine("000");
            Trace.WriteLine("111");

            //using (SqlConnection conn=new SqlConnection("Data Source=.;Initial Catalog=Shane_Account;Integrated Security=False;User ID=sa;Password=sa;Encrypt=False;"))
            //{
            //    string sql = "select top 0 * from [base.User] ";
            //    using (SqlCommand cmd=new SqlCommand(sql,conn))
            //    {
            //        try
            //        {
            //            conn.Open();
            //            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
            //            sqlDataAdapter.SelectCommand = new SqlCommand(sql, conn);
            //            sqlDataAdapter.UpdateBatchSize = 0;
            //            SqlCommandBuilder builder = new SqlCommandBuilder(sqlDataAdapter);
            //            DataTable dt = new DataTable();
            //            sqlDataAdapter.Fill(dt);
            //            dt.Rows.Add(1, "1");
            //            dt.Rows.Add(2, "2");
            //            dt.Rows.Add(3, "3");
            //            sqlDataAdapter.Update(dt);

            //        }
            //        catch (Exception)
            //        {


            //        }
            //    }
            //}

            //ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            //map.ExeConfigFilename = AppDomain.CurrentDomain.BaseDirectory + "/App.config";
            //Configuration a = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

            
             OtherConfigInfo configInfo = OtherConfigInfo.GetConfig();
            Console.WriteLine(configInfo.a);
           

        Console.Read();
        }

     

        public static async Task TestAsync()
        {
            var httpClient = new HttpClient();
            var a = httpClient.GetAsync("http://www.8kktv.com/");


            await Task.Run(async () => {

            await Task.Run(() => CreateNo());
            });
        }


        public static string CreateNo()
        {
            Random random = new Random();
            string strRandom = random.Next(1000, 10000).ToString(); //生成编号 
            string code = DateTime.Now.ToString("yyyyMMddHHmmss") + strRandom;//形如
            return code;
        }



        public static void 文件操作()
        {
            string dirPath = "TextFile1.txt";
            //文件夹操作
            DirectoryInfo dirinfo = new DirectoryInfo(dirPath);
            if (!Directory.Exists(dirinfo.ToString()))
            {
                // Directory.CreateDirectory(dirPath);
            }

            //文件操作
            FileInfo fileInfo = new FileInfo("TextFile1.txt");
            if (!File.Exists(dirPath))//判断该文件是否存在
            {
                fileInfo.Create();
            }
            //File.Delete("tt.txt");
            //fileInfo.CopyTo("tt.txt");//复制
            //fileInfo.MoveTo("aa.txt");//重命名
            string[] strarry = File.ReadAllLines("TextFile1.txt");

            strarry.ToList().ForEach((x) => Console.WriteLine(x));
            File.WriteAllLines("TextFile2.txt", new string[] { "aaa", "bbb" });

            byte[] data = File.ReadAllBytes("TextFile1.txt");
            //转string
            string s = Encoding.Default.GetString(data);
            File.WriteAllBytes("111.txt", Encoding.Default.GetBytes(s));

            //path
            string a = Path.Combine(@"c:\my documents", "Readme.txt");
            Path.GetDirectoryName(a);//c:\my documents
            Path.GetExtension(a); //.txt
            Path.GetFileName(a); //Readme.txt



            //文件流
            using (FileStream fileStream = new FileStream(dirPath, FileMode.Open))
            {
                using (StreamWriter sw = new StreamWriter(fileStream, Encoding.Default))
                {
                    sw.Write(DateTime.Now.ToString("HH:mm:ss") + " " + "发生bug" + "\r\n");
                }
            }
            //内存流
            MemoryStream stream = new MemoryStream(File.ReadAllBytes(""));



            //读写文本文件
            StreamReader streamReader = new StreamReader(dirPath);
            string str = streamReader.ReadToEnd();
            Console.WriteLine(str);

            StreamWriter streamWriter = new StreamWriter(dirPath, true);
            streamWriter.WriteLine("11111111111");
            streamWriter.Close();
        }


        static void ThreadMethod()
        {
            Console.WriteLine("任务开始");
            Thread.Sleep(2000);
            Console.WriteLine("任务结束");
        }
        public void TaskDemo()
        {
            //Task.Run((Action)ThreadMethod);

            //TaskFactory tf = new TaskFactory();
            //Task t1 = tf.StartNew(ThreadMethod);

            List<int> list = new List<int>();

            int threadNumber = 13;

            List<Task> tasks = new List<Task>();
            TaskFactory taskFactory = new TaskFactory();
            foreach (var item in list)
            {
                tasks.Add(taskFactory.StartNew(() =>
                {
                    Console.WriteLine($"线程id{Thread.CurrentThread.ManagedThreadId}");
                }));
                if (tasks.Count >= threadNumber)
                {
                    Task.WaitAny(tasks.ToArray());
                    tasks = tasks.Where(t => t.Status == TaskStatus.Running).ToList();
                }
            }
        }
    }

}
