using System;

namespace LittleCmsNet.Demo
{
	class Program
	{

		static void PrintColor<T>(Span<T> color)
		{
			foreach(var c in color)
			{
				Console.Write($"{c:0.000}  ");
			}
			Console.WriteLine();
		}

		static void Main(string[] args)
		{
			using (LcmsProfile cmyk = LcmsProfile.Load("ISOcoated_v2_300_eci.icc"))
			{
				Console.WriteLine($"{nameof(LcmsProfile.ColorSpace)}: {cmyk.ColorSpace}");
				Console.WriteLine($"Channels: {cmyk.ColorSpace.GetChannels()}");
				Console.WriteLine($"{nameof(LcmsProfile.DeviceClass)}: {cmyk.DeviceClass}");
				Console.WriteLine($"{nameof(LcmsProfile.EncodedICCversion)}: {cmyk.EncodedICCversion}");
				Console.WriteLine($"{nameof(LcmsProfile.ProfileConnectionSpace)}: {cmyk.ProfileConnectionSpace}");
				Console.WriteLine($"{nameof(LcmsProfile.HeaderRenderingIntent)}: {cmyk.HeaderRenderingIntent}");
				Console.WriteLine($"{nameof(CmsInfoType.CmsInfoDescription)}: {cmyk.GetProfileInfo(CmsInfoType.CmsInfoDescription)}");
				Console.WriteLine($"{nameof(CmsInfoType.CmsInfoCopyright)}: {cmyk.GetProfileInfo(CmsInfoType.CmsInfoCopyright)}");
				Console.WriteLine($"{nameof(CmsInfoType.CmsInfoManufacturer)}: {cmyk.GetProfileInfo(CmsInfoType.CmsInfoManufacturer)}");
				Console.WriteLine($"{nameof(CmsInfoType.CmsInfoModel)}: {cmyk.GetProfileInfo(CmsInfoType.CmsInfoModel)}");
				//using (LcmsProfile rgb = LcmsProfile.Load("sRGB Color Space Profile.icm"))
				using (LcmsProfile rgb = LcmsProfile.CreateSRGBProfile())
				{
					using (LcmsTransform<double, byte> trans = LcmsTransform<double, byte>.Create(cmyk, rgb))
					{
						Span<double> cmykColor = new double[] { 0.0f, 50f, 0f, 50.0f };
						Span<byte> rgbColor = stackalloc byte[3];
						trans.Transform(cmykColor, rgbColor);
						PrintColor(cmykColor);
						PrintColor(rgbColor);
					}
				}
			}
		}
	}
}
