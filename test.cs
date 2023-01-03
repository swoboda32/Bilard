using System;

namespace Bilard
{
	class Program
	{
		static void Main(string[] args)
		{
			// Wczytaj liczbę testów
			int T = int.Parse(Console.ReadLine());

			// Pętla po każdym teście
			for (int i = 0; i < T; i++)
			{
				// Wczytaj dane testu
				string[] input = Console.ReadLine().Split();
				int sx = int.Parse(input[0]);
				int sy = int.Parse(input[1]);
				int px = int.Parse(input[2]);
				int py = int.Parse(input[3]);
				int wx = int.Parse(input[4]);
				int wy = int.Parse(input[5]);

				// Oblicz, do której łuzy wpadnie kula
				string pocket = "NIE";
				int bounces = 0;
				if (wx == 0) // Kula porusza się poziomo
				{
					if (px == 0 || px == sx) // Kula znajduje się na boku stołu
					{
						pocket = (py == 0) ? "A" : "C"; // Łuza w lewym lub prawym rogu
					}
					else if (py == 0 || py == sy) // Kula znajduje się na górnym lub dolnym boku stołu
					{
						pocket = (px < sx / 2) ? "B" : "D"; // Łuza na górnym lub dolnym środku
					}
				}
				else if (wy == 0) // Kula porusza się pionowo
				{
					if (py == 0 || py == sy) // Kula znajduje się na boku stołu
					{
						pocket = (px == 0) ? "A" : "C"; // Łuza w lewym lub prawym rogu
					}
					else if (px == 0 || px == sx) // Kula znajduje się na lewym lub prawym boku stołu
					{
						pocket = (py < sy / 2) ? "B" : "D"; // Łuza na górnym lub dolnym środku
					}
				}
				else
				{
					// Oblicz, czy kula zmieni swoje położenie poziome lub pionowe
					bool changeX = (wx < 0 && px == sx) || (wx > 0 && px == 0);
					bool changeY = (wy < 0 && py == sy) || (wy > 0 && py == 0);

					// Oblicz liczbę odbić od band
					if (changeX || changeY)
					{
						bounces++;
						while (true)
						{
							px += wx;
							py += wy;
							if (px < 0 || px > sx || py < 0 || py > sy) // Kula wypadła poza stół
							{
								pocket = "NIE";
								break;
							}
							if (px == 0 || px == sx) // Kula znajduje się na boku stołu
							{
								if (py == 0) // Łuza A
								{
									pocket = "A";
									break;
								}
								else if (py == sy) // Łuza C
								{
									pocket = "C";
									break;
								}
							}
							else if (py == 0 || py == sy) // Kula znajduje się na górnym lub dolnym boku stołu
							{
								if (px < sx / 2) // Łuza B
								{
									pocket = "B";
									break;
								}
								else // Łuza D
								{
									pocket = "D";
									break;
								}
							}

							// Oblicz, czy kula zmieni swoje położenie poziome lub pionowe
							changeX = (wx < 0 && px == sx) || (wx > 0 && px == 0);
							changeY = (wy < 0 && py == sy) || (wy > 0 && py == 0);
							if (changeX || changeY)
							{
								bounces++;
							}
							else
							{
								break;
							}
						}
					}

					// Wypisz wynik dla tego testu
					Console.WriteLine(pocket + " " + bounces);
				}
			}
		}
	}

