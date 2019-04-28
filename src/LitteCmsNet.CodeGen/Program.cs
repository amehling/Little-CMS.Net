using CppSharp;
using System;

namespace LitteCmsNet.CodeGen
{
	class Program
	{
		static void Main(string[] args)
		{
			ConsoleDriver.Run(new LcmsLibrary());
			Console.ReadLine();
		}
	}
}
