namespace Bilard;

public class Program
{
	public static void Main(string[] args)
	{
		var inputT = int.Parse(Console.ReadLine());
		var listaDanychWejsciowych = new List<DaneWejsciowe>();
		var listaStolowBilardowych = new List<StolBilardowy>();
		for (int i = 0; i <= inputT; i++)
		{
			var input = Console.ReadLine();
			var inputTable = input.Split(" ");

			DaneWejsciowe daneWejsciowe = new();

			daneWejsciowe.Sx = decimal.Parse(inputTable[0]);
			daneWejsciowe.Sy = decimal.Parse(inputTable[1]);
			daneWejsciowe.Px = decimal.Parse(inputTable[2]);
			daneWejsciowe.Py = decimal.Parse(inputTable[3]);
			daneWejsciowe.Wx = decimal.Parse(inputTable[4]);
			daneWejsciowe.Wy = decimal.Parse(inputTable[5]);
			listaDanychWejsciowych.Add(daneWejsciowe);
			var bilardTable = StolBilardowy.Factory.Create(daneWejsciowe.Sx, daneWejsciowe.Sy);
			listaStolowBilardowych.Add(bilardTable);
			Bila bila = Bila.Factory.Create(daneWejsciowe.Px, daneWejsciowe.Py, daneWejsciowe.Wx, daneWejsciowe.Wy);
			Bila bilaPoPierwszymOdbiciu = new();
			int odbicia = 0;
			do
			{
				if (bila.PX == daneWejsciowe.Px && bila.PY == daneWejsciowe.Py)
				{
					bilaPoPierwszymOdbiciu = bila;
				}

				do
				{
					bila.PX += bila.Prosta.Wx;
					bila.PY += bila.Prosta.Wy;
					if ((bila.PX == bilardTable.GL[0] && bila.PY == bilardTable.GL[1])
					|| (bila.PX == bilardTable.GS[0] && bila.PY == bilardTable.GS[1])
					|| (bila.PX == bilardTable.GP[0] && bila.PY == bilardTable.GP[1])
					|| (bila.PX == bilardTable.DL[0] && bila.PY == bilardTable.DL[1])
					|| (bila.PX == bilardTable.DS[0] && bila.PY == bilardTable.DS[1])
					|| (bila.PX == bilardTable.DP[0] && bila.PY == bilardTable.DP[1]))
					{
						break;
					}
					else if (bila.PY >= bilardTable.GL[1] || bila.PY <= bilardTable.DL[1])
					{
						bila.Prosta.Wy *= -1;
						if (bila.PY >= bilardTable.GL[1])
						{
							bila.PX = (bilardTable.ProstaGora.B - bila.Prosta.B) / (bila.Prosta.M - bilardTable.ProstaGora.M);
							bila.PY = bila.Prosta.M * bila.PX + bila.Prosta.B;
							Bila.Factory.UpdateProsta(bila);
						}
						else
						{
							bila.PX = (bilardTable.ProstaDol.B - bila.Prosta.B) / (bila.Prosta.M - bilardTable.ProstaDol.M);
							bila.PY = bila.Prosta.M * bila.PX + bila.Prosta.B;
							Bila.Factory.UpdateProsta(bila);
						}
						break;
					}
					else if (bila.PX <= bilardTable.GL[0] || bila.PX >= bilardTable.GP[0])
					{
						bila.Prosta.Wx *= -1;
						if (bila.PX >= bilardTable.GL[0])
						{
							bila.PX = (bilardTable.ProstaLewo.B - bila.Prosta.B) / (bila.Prosta.M - bilardTable.ProstaLewo.M);
							bila.PY = bila.Prosta.M * bila.PX + bila.Prosta.B;
							Bila.Factory.UpdateProsta(bila);
						}
						else
						{
							bila.PX = (bilardTable.ProstaPrawo.B - bila.Prosta.B) / (bila.Prosta.M - bilardTable.ProstaPrawo.M);
							bila.PY = bila.Prosta.M * bila.PX + bila.Prosta.B;
							Bila.Factory.UpdateProsta(bila);
						}
						break;
					}
				} while (true);


				if (bila.PX == bilardTable.GL[0] && bila.PY == bilardTable.GL[1])
				{
					Console.WriteLine("GL " + odbicia);
					break;
				}
				if (bila.PX == bilardTable.GS[0] && bila.PY == bilardTable.GS[1])
				{
					Console.WriteLine("GS " + odbicia);
					break;
				}
				if (bila.PX == bilardTable.GP[0] && bila.PY == bilardTable.GP[1])
				{
					Console.WriteLine("GP " + odbicia);
					break;
				}
				if (bila.PX == bilardTable.DL[0] && bila.PY == bilardTable.DL[1])
				{
					Console.WriteLine("DL " + odbicia);
					break;
				}
				if (bila.PX == bilardTable.DS[0] && bila.PY == bilardTable.DS[1])
				{
					Console.WriteLine("DS " + odbicia);
					break;
				}
				if (bila.PX == bilardTable.DP[0] && bila.PY == bilardTable.DP[1])
				{
					Console.WriteLine("DP " + odbicia);
					break;
				}
				//if (bila.Prosta == bilaPoPierwszymOdbiciu.Prosta)
				//{
				//	Console.WriteLine("NIE");
				//	break;
				//}
				else
				{
					odbicia++;
				}
			} while (true);
		}
	}
}

public class DaneWejsciowe
{
	public decimal Sx { get; set; }
	public decimal Sy { get; set; }
	public decimal Px { get; set; }
	public decimal Py { get; set; }
	public decimal Wx { get; set; }
	public decimal Wy { get; set; }
}

public class StolBilardowy
{
	public decimal[] GL { get; set; } // 0, sy 
	public decimal[] GP { get; set; } // sx, sy
	public decimal[] GS { get; set; } // sx/2, sy
	public decimal[] DL { get; set; } // 0, 0 
	public decimal[] DP { get; set; } // sx, 0
	public decimal[] DS { get; set; } // sx/2, 0
									 //prosta góra
	public Prosta ProstaGora { get; set; }
	//prosta lewo
	public Prosta ProstaLewo { get; set; }
	//prosta prawo
	public Prosta ProstaPrawo { get; set; }
	//prosta dół
	public Prosta ProstaDol { get; set; }

	public static class Factory
	{
		public static StolBilardowy Create(decimal sx, decimal sy)
		{
			StolBilardowy stolBilardowy = new();
			stolBilardowy.GL = new decimal[2] { 0, sy };
			stolBilardowy.GP = new decimal[2] { sx, sy };
			stolBilardowy.GS = new decimal[2] { (sx / 2), sy };
			stolBilardowy.DL = new decimal[2] { 0, 0 };
			stolBilardowy.DP = new decimal[2] { sx, 0 };
			stolBilardowy.DS = new decimal[2] { (sx / 2), 0 };
			stolBilardowy.ProstaGora = Prosta.Factory.Create(stolBilardowy.GL, stolBilardowy.GP);
			stolBilardowy.ProstaLewo = Prosta.Factory.Create(stolBilardowy.GL, stolBilardowy.DL);
			stolBilardowy.ProstaPrawo = Prosta.Factory.Create(stolBilardowy.GP, stolBilardowy.DP);
			stolBilardowy.ProstaDol = Prosta.Factory.Create(stolBilardowy.DL, stolBilardowy.DP);
			return stolBilardowy;
		}
	}
}

public class Prosta
{
	public decimal M { get; set; }
	public decimal B { get; set; }
	//wektor kierunkowy
	public decimal Wx { get; set; } = 0;
	public decimal Wy { get; set; } = 0;

	public static class Factory
	{
		public static Prosta Create(decimal[] pointA, decimal[] pointB)
		{
			Prosta prosta = new();
			prosta.M = (pointB[1] - pointA[1]) / (pointB[0] - pointA[0]);
			prosta.B = pointB[1] - (prosta.M * pointA[0]);
			return prosta;
		}
	}
}

public class Bila
{
	public decimal PX { get; set; }
	public decimal PY { get; set; }
	public Prosta Prosta { get; set; }

	public static class Factory
	{
		public static Bila Create(decimal px, decimal py, decimal wx, decimal wy)
		{
			Bila bila = new();
			bila.PX = px;
			bila.PY = py;

			bila.Prosta = new();

			bila.Prosta.Wx = wx;
			bila.Prosta.Wy = wy;

			bila.Prosta.M = wy / wx;
			bila.Prosta.B = py - (bila.Prosta.M * px);

			return bila;
		}

		public static void UpdateProsta(Bila bila)
		{
			bila.Prosta.M = bila.Prosta.Wy / bila.Prosta.Wx;
			bila.Prosta.B = bila.PY - (bila.Prosta.M * bila.PX);
		}
	}
}