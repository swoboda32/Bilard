namespace Bilard.Models
{
	public class BilardTable
	{
		public int[] GL { get; set; } // 0, sy 
		public int[] GP { get; set; } // sx, sy
		public int[] GS { get; set; } // sx/2, sy
		public int[] DL { get; set; } // 0, 0 
		public int[] DP { get; set; } // sx, 0
		public int[] DS { get; set; } // sx/2, 0

		public static class Factory
		{
			public static BilardTable Create(int sx, int sy)
			{
				BilardTable bilardTable = new();
				bilardTable.GL = new int[2] {0, sy };
				bilardTable.GP = new int[2] {sx, sy };
				bilardTable.GS = new int[2] {(sx/2), sy };
				bilardTable.DL = new int[2] {0, 0 };
				bilardTable.DP = new int[2] {sx, 0 };
				bilardTable.DS = new int[2] {(sx/2), 0 };
				return bilardTable;
			}
		}
	}
}
