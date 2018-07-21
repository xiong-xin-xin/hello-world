using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ConsoleApp1
{
   public class Bitmap图片
    {
        //GDI创建图片
        public void Bitmap()
        {
            Random random = new Random();

            Bitmap bt = new Bitmap(400,400, PixelFormat.Format24bppRgb);

            Graphics g = Graphics.FromImage(bt);

            Font fn1 = new Font("Tahoma", 10, FontStyle.Bold);
            Font fn = new Font("Tahoma", 9, FontStyle.Bold);
            g.Clear(Color.Honeydew);
            g.DrawString("中国电子学会电子设计初级工程师认证考试名", fn1, Brushes.Black, new PointF(20, 20));
            g.DrawString("xxx", fn1, Brushes.Black, new PointF(140, 50));
            g.DrawString("aaa", fn, Brushes.Black, new PointF(14, 85));
            g.DrawString("111111", fn, Brushes.Black, new PointF(90, 85));
            g.DrawString("bbb", fn, Brushes.Black, new PointF(14, 115));
            g.DrawString("222", fn, Brushes.Black, new PointF(90, 115));
            g.DrawString("ccc", fn, Brushes.Black, new PointF(14, 145));
            g.DrawString("1", fn, Brushes.Black, new PointF(90, 145));
            g.DrawString("dddd", fn, Brushes.Black, new PointF(14, 175));
            g.DrawString("4101231231233872032", fn, Brushes.Black, new PointF(90, 175));
            g.DrawString("ee", fn, Brushes.Black, new PointF(14, 205));
            g.DrawString("2007年11月30号", fn, Brushes.Black, new PointF(90, 205));
            g.DrawString("ada", fn, Brushes.Black, new PointF(14, 235));
            g.DrawString("11111", fn, Brushes.Black, new PointF(90, 235));
            g.DrawString("名称", fn, Brushes.Black, new PointF(14, 265));
            g.DrawString("43567865643", fn, Brushes.Black, new PointF(90, 265));
            g.DrawString("编号", fn, Brushes.Black, new PointF(14, 295));
            g.DrawString("9101080004", fn, Brushes.Black, new PointF(90, 295));
            g.DrawString("编号", fn, Brushes.Black, new PointF(196, 295));
            g.DrawString("080004", fn, Brushes.Black, new PointF(275, 295));
            g.DrawString("(提供)", fn, Brushes.Black, new PointF(14, 325));

     
            //产生杂点
            for (int i = 0; i < 500; i++)
            {
                int x1 = random.Next(bt.Width - 20);
                int y1 = random.Next(bt.Height - 20);
                bt.SetPixel(x1, y1, Color.FromArgb(random.Next()));
            }
            //产生随机曲线
            for (int i = 0; i < 50; i++)
            {
                int x1 = random.Next(bt.Width - 20);
                int y1 = random.Next(bt.Height - 20);
                int x2 = random.Next(1, 30);
                int y2 = random.Next(1, 20);
                int x3 = random.Next(15, 45);
                int y3 = random.Next(70, 270);
                g.DrawArc(new Pen(Color.FromArgb(random.Next())), x1, y1, x2, y2, x3, y3);
            }
            //画直线
            g.DrawLine(new Pen(Color.Black), 10, 75, 341, 75);
            g.DrawLine(new Pen(Color.Black), 10, 105, 243, 105);
            g.DrawLine(new Pen(Color.Black), 10, 135, 243, 135);
            g.DrawLine(new Pen(Color.Black), 10, 165, 243, 165);
            g.DrawLine(new Pen(Color.Black), 10, 195, 341, 195);
            g.DrawLine(new Pen(Color.Black), 10, 225, 341, 225);
            g.DrawLine(new Pen(Color.Black), 10, 255, 341, 255);
            g.DrawLine(new Pen(Color.Black), 10, 285, 341, 285);
            g.DrawLine(new Pen(Color.Black), 10, 315, 341, 315);
            g.DrawLine(new Pen(Color.Black), 80, 75, 80, 314);
            g.DrawLine(new Pen(Color.Black), 185, 285, 185, 314);
            g.DrawLine(new Pen(Color.Black), 265, 285, 265, 314);
            g.DrawLine(new Pen(Color.Black), 243, 75, 243, 195);
            //画方框
            g.DrawRectangle(new Pen(Color.Black), 10, 10, 331, 382);
            //填充图像在页面
            Image newimage = Image.FromFile(@"C:\Users\凉生凉忆亦凉心i\Pictures\Camera Roll\QQ图片20170714111258.jpg");
            //图像定位
            g.DrawImage(newimage, 245, 77, 95, 117);
            //释放图像缓存
            g.Dispose();

            string lujing = @"E:\" + DateTime.Now.ToString("yyyy-mm-dd") + ".jpg";
            bt.Save(lujing, ImageFormat.Jpeg);

            //释放位图缓存
            bt.Dispose();
        }

        public void Bitmap操作()
        {
            MemoryStream stream =new MemoryStream(getImageByte(""));

            //根据流获取图片
            Bitmap bt1= new Bitmap(stream);

            bt1.Save(@"d:\",ImageFormat.Png);
        }
        // 图片转换成字节流
        public static byte[] ImgToByt(Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms,ImageFormat.Jpeg);
            byte[] imagedata = ms.GetBuffer();
            return imagedata;
        }
        // 字节流转换成图片
        public static Image BytToImg(byte[] byt)
        {
            MemoryStream ms = new MemoryStream(byt);
            Image img = Image.FromStream(ms);
            return img;
        }
        //获取文件字节流byte[]
        private static byte[] getImageByte(string imagePath)
        {
            FileStream files = new FileStream(imagePath, FileMode.Open);
            byte[] imgByte = new byte[files.Length];
            files.Read(imgByte, 0, imgByte.Length);
            files.Close();
            return imgByte;
        }
    }
}
