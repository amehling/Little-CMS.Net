namespace LittleCmsNet
{
	public struct LcmsFormat
	{
		private const uint A = 0b1_0_00000_0_0_0_0_0_0_000_0000_000;
		private const uint O = 0b0_1_00000_0_0_0_0_0_0_000_0000_000;
		private const uint T = 0b0_0_11111_0_0_0_0_0_0_000_0000_000;
		private const uint U = 0b0_0_00000_1_0_0_0_0_0_000_0000_000;
		private const uint Y = 0b0_0_00000_0_1_0_0_0_0_000_0000_000;
		private const uint F = 0b0_0_00000_0_0_1_0_0_0_000_0000_000;
		private const uint P = 0b0_0_00000_0_0_0_1_0_0_000_0000_000;
		private const uint X = 0b0_0_00000_0_0_0_0_1_0_000_0000_000;
		private const uint S = 0b0_0_00000_0_0_0_0_0_1_000_0000_000;
		private const uint E = 0b0_0_00000_0_0_0_0_0_0_111_0000_000;
		private const uint C = 0b0_0_00000_0_0_0_0_0_0_000_1111_000;
		private const uint B = 0b0_0_00000_0_0_0_0_0_0_000_0000_111;

		public LcmsFormat(uint flags = 0)
		{
			this.Flags = flags;
		}

		public uint Flags { get; private set; }

		public bool FloatingPoint
		{
			get { return (Flags & A) != 0; }
			set { Flags = value ? Flags | A : Flags & ~A; }
		}

		public bool Optimized
		{
			get { return (Flags & O) != 0; }
			set { Flags = value ? Flags | O : Flags & ~O; }
		}

		public LcmsPixelType PixelType
		{
			get { return (LcmsPixelType)((Flags & T) >> 16); }
			set { Flags = Flags & ~T | (uint)value << 16; }
		}

		public bool SwapFirstChannel
		{
			get { return (Flags & Y) != 0; }
			set { Flags = value ? Flags | Y : Flags & ~Y; }
		}

		public bool VanillaFlavor
		{
			get { return (Flags & F) != 0; }
			set { Flags = value ? Flags | F : Flags & ~F; }
		}

		public bool Planar
		{
			get { return (Flags & P) != 0; }
			set { Flags = value ? Flags | P : Flags & ~P; }
		}

		public bool Swap16bpsEndianess
		{
			get { return (Flags & X) != 0; }
			set { Flags = value ? Flags | X : Flags & ~X; }
		}

		public bool SwapChannels
		{
			get { return (Flags & S) != 0; }
			set { Flags = value ? Flags | S : Flags & ~S; }
		}

		public uint Channels
		{
			get { return (Flags & C) >> 3; }
			set { Flags = Flags & ~C | value << 3; }
		}

		public uint BytesPerChannel
		{
			get { return (Flags & B); }
			set { Flags = Flags & ~B | value; }
		}

	}
}
