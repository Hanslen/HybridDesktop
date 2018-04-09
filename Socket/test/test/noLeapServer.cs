using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			TcpListener serverSocket = new TcpListener(8888);
			TcpClient clientSocket = default(TcpClient);


			// server start
			serverSocket.Start();
			Console.WriteLine(" >> Server Started");
			clientSocket = serverSocket.AcceptTcpClient();
			Console.WriteLine(" >> Accept connection from client");
			while (true) // Loop indefinitely
	        {
	            Console.WriteLine("Enter input:"); // Prompt
	            String textinput = Console.ReadLine(); // Get string from user
	            if (textinput == "exit") // Check string
	            {
	            	clientSocket.Close();
					serverSocket.Stop();
					Console.WriteLine(" >>Server exit");
	                break;
	            }
	            Console.WriteLine("You are transfering the msg: "+textinput);
	            NetworkStream serverStream = clientSocket.GetStream ();
				byte[] outStream = System.Text.Encoding.ASCII.GetBytes (textinput + "$");
				serverStream.Write (outStream, 0, outStream.Length);
				serverStream.Flush ();
	        }

	        // updateinstruction
	        // Previous
	        // Next
		}

	}
}