using System;

namespace LitteCmsNet.Demo
{
	class Program
	{

		static void PrintColor(Span<float> color)
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
					LcmsFormat cmykFormat = new LcmsFormat()
					{
						FloatingPoint = true,
						BytesPerChannel = sizeof(float),
						Channels = cmyk.Channels,
						PixelType = LcmsPixelType.CMYK,
					};
					LcmsFormat rgbFormat = new LcmsFormat()
					{
						FloatingPoint = true,
						BytesPerChannel = sizeof(float),
						Channels = rgb.Channels,
						PixelType = LcmsPixelType.RGB,
					};
					using (LcmsTransform trans = LcmsTransform.Create(cmyk, cmykFormat, rgb, rgbFormat, LcmsRenderingIntent.PERCEPTUAL, LcmsFlags.NONE))
					{
						float[] cmykColor = new float[] { 0.0f, 100f, 0f, 0.0f };
						Span<float> rgbColor = stackalloc float[3];
						trans.Transform(cmykColor, rgbColor);
						PrintColor(cmykColor);
						PrintColor(rgbColor);
					}
				}
			}
		}
	}
}
