namespace LitteCmsNet
{
	public static class CmsColorSpaceSignatureExtensions
	{
		public static uint GetChannels(this CmsColorSpaceSignature colorSpace)
		{
			return lcms2.CmsChannelsOf(colorSpace);
		}
	}
}
