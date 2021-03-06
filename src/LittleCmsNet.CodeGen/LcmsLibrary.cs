﻿using CppSharp;
using CppSharp.AST;
using CppSharp.Generators;

namespace LittleCmsNet.CodeGen
{
	public class LcmsLibrary : ILibrary
	{
		public void Setup(Driver driver)
		{
			DriverOptions options = driver.Options;
			options.GeneratorKind = GeneratorKind.CSharp;
			Module module = options.AddModule("lcms2");
			module.OutputNamespace = "LittleCmsNet";
			module.IncludeDirs.Add(@"..\..\..\LittleCmsNet\runtimes\win-x64\native");
			module.Headers.Add("lcms2.h");
			module.LibraryDirs.Add(@"..\..\..\LittleCmsNet\runtimes\win-x64\native");
			module.Libraries.Add("lcms2.dll");
		}

		public void SetupPasses(Driver driver)
		{
		}

		public void Postprocess(Driver driver, ASTContext ctx)
		{
		}

		public void Preprocess(Driver driver, ASTContext ctx)
		{
		}

	}
}
