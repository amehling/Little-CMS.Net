using CppSharp;
using System;

namespace LittleCmsNet.CodeGen
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
