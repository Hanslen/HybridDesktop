using System;
using System.Net.Sockets;
using System.Text; 

namespace WindowsApplication1
{
	class Form1
	{
		static void Main(string[] args)
		{
			System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
			clientSocket.Connect("10.154.145.161", 8888);
			Console.WriteLine ("Running the client");
			bool run = true;
			while ((run))
			{
				try
				{
					NetworkStream networkStream = clientSocket.GetStream();
					byte[] bytesFrom = new byte[10025];
					networkStream.Read(bytesFrom, 0, (int)bytesFrom.Length);
					string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
					dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
					Console.WriteLine(" >> Data from Server - " + dataFromClient);
					string serverResponse = "Last Message from Server" + dataFromClient;
					Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
					networkStream.Write(sendBytes, 0, sendBytes.Length);
					networkStream.Flush();
					Console.WriteLine(" >> " + serverResponse);
				}
				catch (Exception ex)
				{

					// Console.WriteLine(ex.ToString());
					run = false;
					Console.WriteLine("Disconnect with the server");
				}
			}
			// byte[] inStream = new byte[10025];
			// serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);

		}
	}
}
