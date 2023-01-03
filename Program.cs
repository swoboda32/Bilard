using ConsoleApp1.DTO;
using ConsoleApp1.Models;
using System;
using System.Numerics;

namespace ConsoleApp1;

public class Program
{
	public static void Main(string[] args)
	{
		var inputT = int.Parse(Console.ReadLine());
		var dtos = new List<InputDataDTO>();
		var bilardTables = new List<BilardTable>();
		for (int i = 0; i <= inputT; i++)
		{
			var input = Console.ReadLine();
			var inputTable = input.Split(" ");
			if(inputTable.Length < 6)
			{
				Console.WriteLine("Błędnie wprowadzone dane, uruchom program ponownie");
				break;
			}

			InputDataDTO dto = new();
			
			dto.Sx = CheckIntValues(inputTable[0], 2);
			dto.Sy = CheckIntValues(inputTable[1], 1);
			dto.Px = CheckIntValues(inputTable[2], 3, dto.Sx);
			dto.Py = CheckIntValues(inputTable[3], 4, dto.Sx, dto.Sy);
			dto.Wx = CheckIntValues(inputTable[4], 5);
			dto.Wy = CheckIntValues(inputTable[5], 5);
			dtos.Add(dto);
			var bilardTable = BilardTable.Factory.Create(dto.Sx, dto.Sy);
			bilardTables.Add(bilardTable);

			var v = new Vector(1, 3, 5);
		}
	}

	public static int CheckIntValues(string stringValue, int param, int sx = 0 , int sy = 0)
	{
		if(int.TryParse(stringValue, out int value))
		{
			switch (param)
			{
				case 1:
					{
						if (1 <= value && value <= 1000000)
						{
							return value;
						}
						else
						{
							Console.WriteLine("Błędnie wprowadzone dane, uruchom program ponownie");
						}
						break;
					}
				case 2:
					{
						if (1 <= value && value <= 1000000 && value % 2 == 0)
						{
							return value;
						}
						else
						{
							Console.WriteLine("Błędnie wprowadzone dane, uruchom program ponownie");
						}
						break;
					}
				case 3:
					{
						if (0 <= value && value <= sx)
						{
							return value;
						}
						else
						{
							Console.WriteLine("Błędnie wprowadzone dane, uruchom program ponownie");
						}
						break;
					}
				case 4:
					{
						if (0 <= value && value <= sy)
						{
							return value;
						}
						else
						{
							Console.WriteLine("Błędnie wprowadzone dane, uruchom program ponownie");
						}
						break;
					}
				case 5:
					{
						if (-1000 <= value && value <= 1000)
						{
							return value;
						}
						else
						{
							Console.WriteLine("Błędnie wprowadzone dane, uruchom program ponownie");
						}
						break;
					}
				default:
					break;
			}
		}
		else
		{
			Console.WriteLine("Błędne wprowadzone wartosći");
		}
		return 0;
	}
}