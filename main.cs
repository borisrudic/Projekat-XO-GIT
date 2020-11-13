using System;
class MainClass {
  static int[,] tabla = {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}};
	static bool igrac; //ako je igrac 1 na potezu, true
  /*
  Polja table predstavljaju:
  - 0 = nema nista
  - 1 = X
  - 2 = O
  */

  static bool PopunjenaTabla (int [,] tabla) //ako je true, nema vise slobodnih polja na tabli
  {
		for (int i = 0; i < 3; i++)
    {
			for (int j = 0; j < 3; j++)
      {
				if (tabla[i, j] == 0) return false;
			}
		}
		return true;
	}

  static int kurX = 2;
  static int kurY = 4;
	static int tablaX = 0;
	static int tablaY = 0;
	static int brIgraca;
	static int koIgraPrvi; //Ako koIgraPrvi = 1 znaci da mi igramo prvi a CPU igra sa O
	//U suprotnom ako je koIgraPrvi = 2 racunar igra prvi i to sa X
	static string igrac1;
	static string igrac2;

	static int rezultat1 = 0;
	static int rezultat2 = 0;

  static void CrtanjeTable()
  {
    Console.Clear();
		for (int i = 1; i <= 20; i++) Console.Write("-");
		Console.WriteLine();
		for (int i = 1; i <= 4; i++) Console.Write(" ");
		Console.Write("IGRA IKS OKS");
		for (int i = 1; i <= 4; i++) Console.Write(" ");
		Console.WriteLine();
		for (int i = 1; i <= 20; i++) Console.Write("-");
		Console.WriteLine();
		for (int i = 1; i <= 9; i++)
    {
			if (i ==  3 || i == 6) Console.WriteLine("_____|_____|_____");
			else Console.WriteLine("     |     |     ");
		}
		Console.SetCursorPosition(kurX, kurY);
  }

  //Metode za pomeranje kursora
  public static void PomeriDesno()
  {
    if (kurX <= 8)
    {
			kurX += 6;
			tablaX++;
		}
  }
  public static void PomeriDole()
  {
    if (kurY <= 9)
    {
			kurY += 3;
			tablaY++;
		}
  }
  public static void PomeriLevo()
  {
    if (kurX > 7)
    {
			kurX -= 6;
			tablaX--;
		}
  }
  public static void PomeriGore()
  {
    if (kurY > 4)
    {
			kurY -= 3;
			tablaY--;
		}
  }
  
  public static void OdaberiPolje()
  {
    ConsoleKeyInfo dugme;
    do
    {
      Console.SetCursorPosition(kurX, kurY);
      dugme = Console.ReadKey(true);
      if (dugme.Key == ConsoleKey.UpArrow) PomeriGore();
      else if (dugme.Key == ConsoleKey.DownArrow) PomeriDole();
      else if (dugme.Key == ConsoleKey.RightArrow) PomeriDesno();
      else if (dugme.Key == ConsoleKey.LeftArrow) PomeriLevo();
    } while (dugme.Key != ConsoleKey.Enter);
  }

  static void unosPoteza (ref bool igrac, int[,] tabla, int koIgraPrvi)
  {	
		if (tabla[tablaX, tablaY] == 0)
    {
      if (brIgraca == 1)
      {
        if (koIgraPrvi == 1)
        {
          tabla[tablaX, tablaY] = 1; //X
          Console.SetCursorPosition(kurX, kurY);
          Console.Write("X");
        } 
        else
        {
          tabla[tablaX, tablaY] = 2; //O
          Console.SetCursorPosition(kurX, kurY);
          Console.Write("O");
        }
      }
			else
			{
				if (igrac)
        {
          tabla[tablaX, tablaY] = 1; //X
          Console.SetCursorPosition(kurX, kurY);
          Console.Write("X");
        } 
        else
        {
          tabla[tablaX, tablaY] = 2; //O
          Console.SetCursorPosition(kurX, kurY);
          Console.Write("O");
        }
			}
			Console.SetCursorPosition(0,13);
			Console.WriteLine("                   ");
			igrac = !igrac;
		}
		else
    {
			Console.SetCursorPosition(0, 13);
			Console.WriteLine("Nedozvoljen potez.");
			Console.SetCursorPosition(kurX,kurY);
		}
	}

	public static bool Pobeda (int [,] tabla)
  {
		//provera uspravno
		for(int i = 0; i < 3; i++)
    {
			if (tabla[0, i] != 0 && tabla[1, i] == tabla[0, i] && tabla[2, i] == tabla[0, i]) return true;
		}
		//provera vodoravno
		for(int i = 0; i < 3; i++)
    {
			if (tabla[i, 0] != 0 && tabla[i, 1] == tabla[i, 0] && tabla[i, 2] == tabla[i, 0]) return true;
		}
		//provera dijagonala
		if (tabla[0, 0] != 0 && tabla[1, 1] == tabla[0, 0] && tabla[2, 2] == tabla[0, 0]) return true;
		if (tabla[2, 0] != 0 && tabla[1, 1] == tabla[2, 0] && tabla[0, 2] == tabla[2, 0]) return true;
		//ako nema pogotka
		return false;
	}

  //Metoda gleda da li je moguce da kompjuter zavrsi
	//svoj potez unutar jednog reda, ili blokira potez igraca
	public static bool BrojanjeHor (int[,] tabla, int red, int broj)
  {
		int BrojacNeNula = 0;
		int BrojacNula = 0;
		for (int i = 0; i <= 2; i++)
    {
			if (tabla[red, i] == 0)
      {
				BrojacNula++;
			}
			else if (tabla[red, i] == broj)
      {
        BrojacNeNula++;
      }
		}
		if (BrojacNula == 1 && BrojacNeNula == 2) return true;
		return false;
	}
  
  //Metoda gleda da li je moguce da kompjuter zavrsi
	//svoj potez unutar jedne kolone, ili blokira potez igraca
	public static bool BrojanjeVer (int[,] tabla, int kol, int broj)
  {
		int BrojacNeNula = 0;
		int BrojacNula = 0;
		for (int i = 0; i <= 2; i++)
    {
			if (tabla[i, kol] == 0)
      {
				BrojacNula++;
			}
			else if (tabla[i, kol] == broj)
      {
        BrojacNeNula++;
      }
		}
		if (BrojacNula == 1 && BrojacNeNula == 2) return true;
		return false;
	}

  //Metoda gleda da li je moguce da kompjuter zavrsi
	//svoj potez unutar glavne dijagonale, ili blokira potez igraca
	public static bool BrojanjeGDij (int[,] tabla, int broj)
  {
		int BrojacNeNula = 0;
		int BrojacNula = 0;
		for (int i = 0; i <= 2; i++)
    {
			if (tabla[i, i] == 0)
      {
				BrojacNula++;
			}
			else if (tabla[i, i] == broj)
      {
        BrojacNeNula++;
      }
		}
		if (BrojacNula == 1 && BrojacNeNula == 2) return true;
		return false;
	}

	//Metoda gleda da li je moguce da kompjuter zavrsi
	//svoj potez unutar druge dijagonale, ili blokira potez igraca
	public static bool BrojanjeSDij (int[,] tabla, int broj)
  {
		int BrojacNeNula = 0;
		int BrojacNula = 0;
		for (int i = 0; i <= 2; i++)
    {
			if (tabla[i, 2 - i] == 0)
      {
				BrojacNula++;
			}
			else if (tabla[i, 2 - i] == broj)
      {
        BrojacNeNula++;
      }
		}
		if (BrojacNula == 1 && BrojacNeNula == 2) return true;
		return false;
	}

  //Metoda koja detektuje da li protivnik sprema zamku na tabli
  //Metoda detektuje zamku koja se pravi igranjem poteza na dve nenaspramne ivice, a
  //kompjuter je sprecava igranjem coska izmedju tih ivica (metoda PotezKojiSprecavaZamku)
  public static bool Zamka (int[,] tabla, int koIgraPrvi) 
  {
    if (tabla[0, 1] == koIgraPrvi && tabla[1, 0] == koIgraPrvi && tabla[0, 0] == 0) return true;
    else if (tabla[0, 1] == koIgraPrvi && tabla[1, 2] == koIgraPrvi && tabla[0, 2] == 0) return true;
    else if (tabla[1, 2] == koIgraPrvi && tabla[2, 1] == koIgraPrvi && tabla[2, 2] == 0) return true;
    else if (tabla[1, 0] == koIgraPrvi && tabla[2, 1] == koIgraPrvi && tabla[2, 0] == 0) return true;
    else return false;
  }

  public static void PotezKojiSprecavaZamku (int[,] tabla, int koIgraPrvi)
  {
    if (tabla[0, 1] == koIgraPrvi && tabla[1, 0] == koIgraPrvi && tabla[0, 0] == 0)
    {
      Console.SetCursorPosition(2, 4);
      if (koIgraPrvi == 1) 
			{
				Console.Write("O");
				tabla[0, 0] = 2;
			}
      else 
			{
				Console.Write("X");
				tabla[0, 0] = 1;
			}
      Console.SetCursorPosition(2, 4);
      igrac = !igrac;
    }
    else if (tabla[1, 0] == koIgraPrvi && tabla[2, 1] == koIgraPrvi && tabla[2, 0] == 0)
    {
      Console.SetCursorPosition(14, 4);
      if (koIgraPrvi == 1)
      {
        Console.Write("O");
        tabla[2, 0] = 2;
      }
      else
      {
        Console.Write("X");
        tabla[2, 0] = 1;
      }
      Console.SetCursorPosition(14, 4);
      igrac = !igrac;
    }
    else if (tabla[1, 2] == koIgraPrvi && tabla[2, 1] == koIgraPrvi && tabla[2, 2] == 0)
    {
      Console.SetCursorPosition(14, 10);
      if (koIgraPrvi == 1) 
			{
				Console.Write("O");
				tabla[2, 2] = 2;
			}
      else 
			{
				Console.Write("X");
				tabla[2, 2] = 1;
			}
      Console.SetCursorPosition(14, 10);
      igrac = !igrac;
    }
    else
    {
      Console.SetCursorPosition(2, 10);
			if (koIgraPrvi == 1) 
			{
				Console.Write("O");
				tabla[0, 2] = 2;
			}
      else 
			{
				Console.Write("X");
				tabla[0, 2] = 1;
			}
      Console.SetCursorPosition(2, 10);
      igrac = !igrac;
    }
  }

  public static void PotezAkoNijeMoguceNiPobeditiNiBlokiratiNiSprecitiZamku (int[,] tabla, int koIgraPrvi)
  {
    if ((tabla[0, 0] == koIgraPrvi && tabla [2, 2] == koIgraPrvi) || (tabla[0, 2] == koIgraPrvi && tabla[2, 0] == koIgraPrvi))
    {
      Random nasumicanBroj = new Random();
      int slucaj = nasumicanBroj.Next(0, 4);
      if (slucaj == 0)
      {
        Console.SetCursorPosition(8, 4);
				if (koIgraPrvi == 1) 
				{
					Console.Write("O");
					tabla[1, 0] = 2;
				}
      	else 
				{
					Console.Write("X");
					tabla[1, 0] = 1;
				}
        Console.SetCursorPosition(8, 4);
        igrac = !igrac;
      }
      else if (slucaj == 1)
      {
        Console.SetCursorPosition(2, 7);
				if (koIgraPrvi == 1) 
				{
					Console.Write("O");
					tabla[0, 1] = 2;
				}
      	else 
				{
					Console.Write("X");
					tabla[0, 1] = 1;
				}
        Console.SetCursorPosition(2, 7);
        igrac = !igrac;
      }
      else if (slucaj == 2)
      {
        Console.SetCursorPosition(14, 7);
				if (koIgraPrvi == 1) 
				{
					Console.Write("O");
					tabla[2, 1] = 2;
				}
      	else 
				{
					Console.Write("X");
					tabla[2, 1] = 1;
				}
        Console.SetCursorPosition(14, 7);
        igrac = !igrac;
      }
      else if (slucaj == 3)
      {
        Console.SetCursorPosition(8, 10);
				if (koIgraPrvi == 1) 		
				{
					Console.Write("O");
					tabla[1, 2] = 2;
				}
      	else 
				{
					Console.Write("X");
					tabla[1, 2] = 1;
				}
        Console.SetCursorPosition(8, 10);
        igrac = !igrac;
      }
    }
    else
    {
      if (tabla[1, 1] == 0 && koIgraPrvi == 1)
      {
        Console.SetCursorPosition(8, 7);
        Console.Write("O");
        tabla[1, 1] = 2;
				igrac = !igrac;
      }
      else if (tabla[1, 1] == 0 && koIgraPrvi == 2)
      {
        Console.SetCursorPosition(8, 7);
        Console.Write("X");
        tabla[1, 1] = 1;
				igrac = !igrac;
      }
      else
      {
        if (tabla[0, 0] == 0 || tabla[0, 2] == 0 || tabla[2, 0] == 0 || tabla[2, 2] == 0)
        {
          if (tabla[0, 0] == 0)
          {
            if (koIgraPrvi == 1)
            {
              tabla[0, 0] = 2;
              Console.SetCursorPosition(2, 4);
              Console.Write("O");
            }
            else
            {
              tabla[0, 0] = 1;
              Console.SetCursorPosition(2, 4);
              Console.Write("X");
            }
          }
          else if (tabla[2, 0] == 0)
          {
            if (koIgraPrvi == 1)
            {
              tabla[2, 0] = 2;
              Console.SetCursorPosition(14, 4);
              Console.Write("O");
            }
            else
            {
              tabla[2, 0] = 1;
              Console.SetCursorPosition(14, 4);
              Console.Write("X");
            }
          }
          else if (tabla[0, 2] == 0)
          {
            if (koIgraPrvi == 1)
            {
              tabla[0, 2] = 2;
              Console.SetCursorPosition(2, 10);
              Console.Write("O");
            }
            else
            {
              tabla[0, 2] = 1;
              Console.SetCursorPosition(2, 10);
              Console.Write("X");
            }
          }
          else
          {
            if (koIgraPrvi == 1)
            {
              tabla[2, 2] = 2;
              Console.SetCursorPosition(14, 10);
              Console.Write("O");
            }
            else
            {
              tabla[2, 2] = 1;
              Console.SetCursorPosition(14, 10);
              Console.Write("X");
            }
          }
        }
        else
        {
          Random nasumicanBroj = new Random();
          int xPotez = 1;
          int yPotez = 1;
          while (tabla[xPotez, yPotez] != 0)
          {
            xPotez = nasumicanBroj.Next(0, 3);
            yPotez = nasumicanBroj.Next(0, 3);
          }
          Console.SetCursorPosition(2 + 6 * xPotez, 4 + 3 * yPotez);
          if (koIgraPrvi == 1) 
          {
            Console.Write("O");
            tabla[xPotez, yPotez] = 2;
          }
          else 
          {
            Console.Write("X");
            tabla[xPotez, yPotez] = 1;
          }
        }
        igrac = !igrac;
      }
    }
  }

  public static void KompPotez (int[,] tabla, int koIgraPrvi)
  {
		if (koIgraPrvi == 1) //Komp igra drugi
		{
			//CPU nalazi potez za pobedu:
			if (BrojanjeHor(tabla, 0, 2)) //prva vrsta
			{
				for (int i = 0; i < 3; i++)
				{
					if (tabla[0, i] == 0)
					{
						Console.SetCursorPosition(2, 4 + 3 * i);
						Console.Write("O");
						tabla[0, i] = 2;
						break;
					}
				}
				igrac = !igrac;
			}
			else if (BrojanjeHor (tabla, 1, 2)) //druga vrsta
			{
				for (int i = 0; i < 3; i++)
				{
					if (tabla[1, i] == 0)
					{
						Console.SetCursorPosition(8, 4 + 3 * i);
						Console.Write("O");
						tabla[1, i] = 2;
						break;
					}
				}
				igrac = !igrac;
			}
			else if (BrojanjeHor (tabla, 2, 2)) //treca vrsta
			{ 
				for (int i = 0; i < 3; i++)
				{
					if (tabla[2, i] == 0)
					{
						Console.SetCursorPosition(14, 4 + 3 * i);
						Console.Write("O");
						tabla[2, i] = 2;
						break;
					}
				}
				igrac = !igrac;
			}
			else if (BrojanjeVer(tabla, 0, 2)) //prva kolona
			{ 
				for (int i = 0; i < 3; i++)
				{
					if (tabla[i, 0] == 0)
					{
						Console.SetCursorPosition(2 + 6 * i, 4);
						Console.Write("O");
						tabla[i, 0] = 2;
						break;
					}
				}
				igrac = !igrac;
			}
			else if (BrojanjeVer (tabla, 1, 2)) //druga kolona
			{ 
				for (int i = 0; i < 3; i++)
				{
					if (tabla[i, 1] == 0)
					{
						Console.SetCursorPosition(2 + 6 * i, 7);
						Console.Write("O");
						tabla[i, 1] = 2;
						break;
					}
				}
				igrac = !igrac;

			}
			else if (BrojanjeVer (tabla, 2, 2)) //treca kolona
			{ 
				for (int i = 0; i < 3; i++)
				{
					if (tabla[i, 2] == 0)
					{
						Console.SetCursorPosition(2 + 6 * i, 10);
						Console.Write("O");
						tabla[i, 2] = 2;
						break;
					}
				}
				igrac = !igrac;
			}
			else if (BrojanjeGDij (tabla, 2)) //glavna dijagonala
			{
				for (int i = 0; i < 3; i++)
				{
					if (tabla[i, i] == 0)
					{
						Console.SetCursorPosition(2 + 6 * i, 4 + 3 * i);
						Console.Write("O");
						tabla[i, i] = 2;
						break;
					}
				}
				igrac = !igrac;
			}
			else if (BrojanjeSDij (tabla, 2)) //sporedna dijagonala
			{
				for (int i = 0; i < 3; i++)
				{
					if (tabla[i, 2 - i] == 0)
					{
						if (i == 2) Console.SetCursorPosition(14, 4);
						else if(i == 1) Console.SetCursorPosition(8, 7);
						else Console.SetCursorPosition(2, 10);
						//i = 2: 2, 0 = hor 2, ver 10
						//i = 1: 1, 1 = hor 8, ver 7
						//i = 0: 0, 2 = hor 14, ver 4
						Console.Write("O");
						tabla[i, 2 - i] = 2;
						break;
					}
				}
				igrac = !igrac;
			}
			
			//CPU nalazi odbrambeni potez:
			else if (BrojanjeHor(tabla, 0, 1)) //prva vrsta
			{
				for (int i = 0; i < 3; i++)
				{
					if (tabla[0, i] == 0)
					{
						Console.SetCursorPosition(2, 4 + 3 * i);
						Console.Write("O");
						tabla[0, i] = 2;
						break;
					}
				}
				igrac = !igrac;
			}
			else if (BrojanjeHor(tabla, 1, 1)) //druga vrsta
			{
				for (int i = 0; i < 3; i++)
				{
					if (tabla[1, i] == 0)
					{
						Console.SetCursorPosition(8, 4 + 3 * i);
						Console.Write("O");
						tabla[1, i] = 2;
						break;
					}
				}
				igrac = !igrac;
			}
			else if (BrojanjeHor (tabla, 2, 1)) //treca vrsta
			{ 
				for (int i = 0; i < 3; i++)
				{
					if (tabla[2, i] == 0)
					{
						Console.SetCursorPosition(14, 4 + 3 * i);
						Console.Write("O");
						tabla[2, i] = 2;
						break;
					}
				}
				igrac = !igrac;
			}
			else if (BrojanjeVer (tabla, 0, 1)) //prva kolona 
			{
				for (int i = 0; i < 3; i++)
				{
					if (tabla[i, 0] == 0)
					{
						Console.SetCursorPosition(2 + 6 * i, 4);
						Console.Write("O");
						tabla[i, 0] = 2;
						break;
					}
				}
				igrac = !igrac;
			}
			else if (BrojanjeVer (tabla, 1, 1)) //druga kolona
			{
				for (int i = 0; i < 3; i++)
				{
					if (tabla[i, 1] == 0)
					{
						Console.SetCursorPosition(2 + 6 * i, 7);
						Console.Write("O");
						tabla[i, 1] = 2;
						break;
					}
				}
				igrac = !igrac;
			}
			else if (BrojanjeVer (tabla, 2, 1)) //treca kolona
			{
				for (int i = 0; i < 3; i++)
				{
					if (tabla[i, 2] == 0)
					{
						Console.SetCursorPosition(2 + 6 * i, 10);
						Console.Write("O");
						tabla[i, 2] = 2;
						break;
					}
				}
				igrac = !igrac;
			}
			else if (BrojanjeGDij (tabla, 1)) //glavna dijagonala
			{
				for (int i = 0; i < 3; i++)
				{
					if (tabla[i, i] == 0)
					{
						Console.SetCursorPosition(2 + 6 * i, 4 + 3 * i);
						Console.Write("O");
						tabla[i, i] = 2;
						break;
					}
				}
				igrac = !igrac;
			}
			else if (BrojanjeSDij (tabla, 1)) //sporedna dijagonala
			{
				for (int i = 0; i < 3; i++)
				{
					if (tabla[i, 2 - i] == 0)
					{
						if (i == 2) Console.SetCursorPosition(14, 4);
						else if (i == 1) Console.SetCursorPosition(8, 7);
						else Console.SetCursorPosition(2, 10);
						Console.Write("O");
						tabla[i, 2 - i] = 2;
						break;
					}
				}
				igrac = !igrac;
			}
			//CPU ispituje da li protivnik pravi zamku na tabli
			//i sprečava je ako je tako
			else if (Zamka(tabla, 1)) PotezKojiSprecavaZamku(tabla, 1);
      else if (tabla[1, 1] == 1 && tabla[0, 0] == 0 && tabla[0, 2] == 0 && tabla[2, 0] == 0 && tabla[2, 2] == 0)
      {
				//igra se random potez u praznom cosku
        Random nasumicanBroj = new Random();
        int slucaj = nasumicanBroj.Next(0, 4);
        if (slucaj == 0)
        {
          tabla[0, 0] = 2;
          Console.SetCursorPosition(2, 4);
          Console.Write("O");
          igrac = !igrac;
        }
        else if (slucaj == 1)
        {
          tabla[2, 0] = 2;
          Console.SetCursorPosition(14, 4);
          Console.Write("O");
          igrac = !igrac;
        }
        else if (slucaj == 2)
        {
          tabla[0, 2] = 2;
          Console.SetCursorPosition(2, 10);
          Console.Write("O");
          igrac = !igrac;
        }
        else
        {
          tabla[2, 2] = 2;
          Console.SetCursorPosition(14, 10);
          Console.Write("O");
					igrac = !igrac;
        }
      }
			//CPU stavlja potez u sredinu ako nije zauzeta
			//ili bira random potez jer ne može ni da pobedi ni da blokira:
			else PotezAkoNijeMoguceNiPobeditiNiBlokiratiNiSprecitiZamku(tabla, 1);
		}
		else //Komp igra prvi
		{
      //CPU nalazi potez za pobedu:
      if (BrojanjeHor(tabla, 0, 1)) //prva vrsta
      {
        for (int i = 0; i < 3; i++)
        {
          if (tabla[0, i] == 0)
          {
            Console.SetCursorPosition(2, 4 + 3 * i);
            Console.Write("X");
            tabla[0, i] = 1;
            break;
          }
        }
        igrac = !igrac;
      }
      else if (BrojanjeHor(tabla, 1, 1)) //druga vrsta
      {
        for (int i = 0; i < 3; i++)
        {
          if (tabla[1, i] == 0)
          {
            Console.SetCursorPosition(8, 4 + 3 * i);
            Console.Write("X");
            tabla[1, i] = 1;
            break;
          }
        }
        igrac = !igrac;
      }
      else if (BrojanjeHor(tabla, 2, 1)) //treca vrsta
      { 
        for (int i = 0; i < 3; i++)
        {
          if (tabla[2, i] == 0)
          {
            Console.SetCursorPosition(14, 4 + 3 * i);
            Console.Write("X");
            tabla[2, i] = 1;
            break;
          }
        }
        igrac = !igrac;
      }
      else if (BrojanjeVer(tabla, 0, 1)) //prva kolona
      { 
        for (int i = 0; i < 3; i++)
        {
          if (tabla[i, 0] == 0)
          {
            Console.SetCursorPosition(2 + 6 * i, 4);
            Console.Write("X");
            tabla[i, 0] = 1;
            break;
          }
        }
        igrac = !igrac;
      }
      else if (BrojanjeVer(tabla, 1, 1)) //druga kolona
      { 
        for (int i = 0; i < 3; i++)
        {
          if (tabla[i, 1] == 0)
          {
            Console.SetCursorPosition(2 + 6 * i, 7);
            Console.Write("X");
            tabla[i, 1] = 1;
            break;
          }
        }
        igrac = !igrac;

      }
      else if (BrojanjeVer(tabla, 2, 1)) //treca kolona
      { 
        for (int i = 0; i < 3; i++)
        {
          if (tabla[i, 2] == 0)
          {
            Console.SetCursorPosition(2 + 6 * i, 10);
            Console.Write("X");
            tabla[i, 2] = 1;
            break;
          }
        }
        igrac = !igrac;
      }
      else if (BrojanjeGDij(tabla, 1)) //glavna dijagonala
      {
        for (int i = 0; i < 3; i++)
        {
          if (tabla[i, i] == 0)
          {
            Console.SetCursorPosition(2 + 6 * i, 4 + 3 * i);
            Console.Write("X");
            tabla[i, i] = 1;
            break;
          }
        }
        igrac = !igrac;
      }
      else if (BrojanjeSDij(tabla, 1)) //sporedna dijagonala
      {
        for (int i = 0; i < 3; i++)
        {
          if (tabla[i, 2 - i] == 0)
          {
            if (i == 2) Console.SetCursorPosition(14, 4);
            else if(i == 1) Console.SetCursorPosition(8, 7);
            else Console.SetCursorPosition(2, 10);
            //i = 2: 2, 0 = hor 2, ver 10
            //i = 1: 1, 1 = hor 8, ver 7
            //i = 0: 0, 2 = hor 14, ver 4
            Console.Write("X");
            tabla[i, 2 - i] = 1;
            break;
          }
        }
        igrac = !igrac;
      }
      
      //CPU nalazi odbrambeni potez:
      else if (BrojanjeHor(tabla, 0, 2)) //prva vrsta
      {
        for (int i = 0; i < 3; i++)
        {
          if (tabla[0, i] == 0)
          {
            Console.SetCursorPosition(2, 4 + 3 * i);
            Console.Write("X");
            tabla[0, i] = 1;
            break;
          }
        }
        igrac = !igrac;
      }
      else if (BrojanjeHor(tabla, 1, 2)) //druga vrsta
      {
        for (int i = 0; i < 3; i++)
        {
          if (tabla[1, i] == 0)
          {
            Console.SetCursorPosition(8, 4 + 3 * i);
            Console.Write("X");
            tabla[1, i] = 1;
            break;
          }
        }
        igrac = !igrac;
      }
      else if (BrojanjeHor(tabla, 2, 2)) //treca vrsta
      { 
        for (int i = 0; i < 3; i++)
        {
          if (tabla[2, i] == 0)
          {
            Console.SetCursorPosition(14, 4 + 3 * i);
            Console.Write("X");
            tabla[2, i] = 1;
            break;
          }
        }
        igrac = !igrac;
      }
      else if (BrojanjeVer(tabla, 0, 2)) //prva kolona 
      {
        for (int i = 0; i < 3; i++)
        {
          if (tabla[i, 0] == 0)
          {
            Console.SetCursorPosition(2 + 6 * i, 4);
            Console.Write("X");
            tabla[i, 0] = 1;
            break;
          }
        }
        igrac = !igrac;
      }
      else if (BrojanjeVer(tabla, 1, 2)) //druga kolona
      {
        for (int i = 0; i < 3; i++)
        {
          if (tabla[i, 1] == 0)
          {
            Console.SetCursorPosition(2 + 6 * i, 7);
            Console.Write("X");
            tabla[i, 1] = 1;
            break;
          }
        }
        igrac = !igrac;
      }
      else if (BrojanjeVer(tabla, 2, 2)) //treca kolona
      {
        for (int i = 0; i < 3; i++)
        {
          if (tabla[i, 2] == 0)
          {
            Console.SetCursorPosition(2 + 6 * i, 10);
            Console.Write("X");
            tabla[i, 2] = 1;
            break;
          }
        }
        igrac = !igrac;
      }
      else if (BrojanjeGDij(tabla, 2)) //glavna dijagonala
      {
        for (int i = 0; i < 3; i++)
        {
          if (tabla[i, i] == 0)
          {
            Console.SetCursorPosition(2 + 6 * i, 4 + 3 * i);
            Console.Write("X");
            tabla[i, i] = 1;
            break;
          }
        }
        igrac = !igrac;
      }
      else if (BrojanjeSDij(tabla, 2)) //sporedna dijagonala
      {
        for (int i = 0; i < 3; i++)
        {
          if (tabla[i, 2 - i] == 0)
          {
            if (i == 2) Console.SetCursorPosition(14, 4);
            else if (i == 1) Console.SetCursorPosition(8, 7);
            else Console.SetCursorPosition(2, 10);
            Console.Write("X");
            tabla[i, 2 - i] = 1;
            break;
          }
        }
        igrac = !igrac;
      }
      //CPU ispituje da li protivnik pravi zamku na tabli
      //i sprečava je ako je tako
      else if (Zamka(tabla, 2)) PotezKojiSprecavaZamku(tabla, 2);
      //CPU stavlja potez u sredinu ako nije zauzeta
      //ili bira random potez jer ne može ni da pobedi ni da blokira:
      else PotezAkoNijeMoguceNiPobeditiNiBlokiratiNiSprecitiZamku(tabla, 2);
		}
  }

  static string UnosPrviIgrac()
  {
		string unos;
    Console.WriteLine("Upisati ime prvog igrača (max. 12 karaktera): ");
		unos = Console.ReadLine();
		while (unos.Length > 12 || unos.Length == 0)
    {
			Console.WriteLine("Invalidno ime. Upisati opet (max. 12 karaktera): ");
			unos = Console.ReadLine();
		}
		igrac1 = unos;
    return igrac1;
  }

  static string UnosDrugiIgrac()
  {
		string unos;
    Console.WriteLine("Upisati ime drugog igrača (max. 12 karaktera):");
		unos = Console.ReadLine();
		while (unos.Length>12 || unos.Length == 0)
    {
			Console.WriteLine("Invalidno ime. Upisati opet (max. 12 karaktera): ");
			unos = Console.ReadLine();
		}
		igrac2 = unos;
    return igrac2;
  }

  static void IspisPobednika()
  {
    if (!igrac && Pobeda(tabla))
    {
			Console.WriteLine("Partija se završila pobedom igrača " + igrac1 + ".");
			rezultat1++;
		}
		else if (igrac && Pobeda(tabla) && brIgraca == 2)
    {
			Console.WriteLine("Partija se završila pobedom igrača " + igrac2 + ".");
			rezultat2++;
		}
		else if (igrac && Pobeda(tabla) && brIgraca == 1)
    {
			Console.WriteLine("Partija se završila pobedom računara.");
			rezultat2++;
		}
		else Console.WriteLine("Partija se završila nerešenim rezultatom.");
    Console.WriteLine();
		if(brIgraca == 2) Console.WriteLine("Trenutni rezultat: {0} {1}:{2} {3}", igrac1, rezultat1, rezultat2, igrac2);
		else Console.WriteLine("Trenutni rezultat: {0} {1}:{2} Računar", igrac1, rezultat1, rezultat2);
  }

  public static void PocetnaPoruka()
  {
    for (int i = 1; i <= 20; i++) Console.Write("-");
		Console.WriteLine();
		for (int i = 1; i <= 4; i++) Console.Write(" ");
		Console.Write("IGRA IKS OKS");
		for (int i = 1; i <= 4; i++) Console.Write(" ");
		Console.WriteLine();
		for (int i = 1; i <= 20; i++) Console.Write("-");
		Console.WriteLine();
  }
  
  public static int[,] PocetnaTabla (int[,] tabla)
  {
    for (int i = 0; i < 3; i++)
    {
			for (int j = 0; j < 3; j++)
      {
				tabla[i,j] = 0;
			}
		}
    return tabla;
  }

  public static int BrojIgraca ()
  {
    Console.WriteLine("Igrate li sami ili u 2 igrača? (1/2)");
		while (!(int.TryParse(Console.ReadLine(), out brIgraca)) || brIgraca < 1 || brIgraca > 2) Console.WriteLine("Pogrešan unos, unesite broj igrača ponovo. (1/2)");
    return brIgraca;
  }
  
  public static string ProveraNaziva (int brIgraca, string igrac1)
  {
    if (brIgraca == 2)
    {
			do
      {
				UnosDrugiIgrac();
        if (igrac1.CompareTo(igrac2) == 0)
        {
			    Console.WriteLine("Greška. Nazivi ne smeju biti isti.");
		    }
      } while (igrac1.CompareTo(igrac2) == 0);
		}
    return igrac2;
  }

  public static bool KoPrviIgra()
  {
    Console.WriteLine("Upisati koji igrač igra prvi. (1/2)");
		while (!(int.TryParse(Console.ReadLine(), out koIgraPrvi)) || koIgraPrvi < 1 || koIgraPrvi > 2)
    {
      Console.WriteLine("Pogrešan unos, unesite broj igrača koji igra prvi ponovo. (1/2)");
    }
		if (koIgraPrvi == 2) igrac = false;
		else igrac = true;
    return igrac;
  }

  public static void Main () {
    //Crtanje pocetne poruke
		PocetnaPoruka();
    //Unos broja igraca
		string ponovnaIgra;
    int ponovnaIgraMiliR = 0;
		BrojIgraca();
    UnosPrviIgrac();
    ProveraNaziva(brIgraca, igrac1);
    KoPrviIgra();
    PocetnaTabla(tabla);
    do
    {
			if (ponovnaIgraMiliR == 1) ProveraNaziva(brIgraca, igrac1);
			if (ponovnaIgraMiliR == 2 || ponovnaIgraMiliR == 1)
			{
				KoPrviIgra();
				PocetnaTabla(tabla);
			}
			//Glavni deo koda
			CrtanjeTable();
			do
      {
				Console.SetCursorPosition(0, 14);
				Console.Write("Trenutno igra: ");
				if (igrac) Console.WriteLine(igrac1);
				else if (brIgraca == 2) Console.WriteLine(igrac2);
				else if (brIgraca == 1) Console.WriteLine("Računar");
				Console.SetCursorPosition(kurX, kurY);
				if (igrac)
				{
					OdaberiPolje();
					unosPoteza(ref igrac, tabla, koIgraPrvi);
				}
				else
				{
					if (brIgraca == 1) KompPotez(tabla, koIgraPrvi);
					//ovo je CPU potez
					else 
					{
						OdaberiPolje();
						unosPoteza(ref igrac, tabla, koIgraPrvi);
					}
				}
				Console.SetCursorPosition(0, 14);
				Console.WriteLine("                           ");
			} while (!Pobeda(tabla) && !PopunjenaTabla(tabla));
			//ako je true, nema vise upisa i program ide dalje
			Console.SetCursorPosition(0, 13);
			//Ispis pobednika:
			IspisPobednika();
			Console.WriteLine();
			do
			{
				Console.WriteLine("Želite li menjanje drugog igrača, revanš ili izlazite iz programa? (M/R/I)");
				ponovnaIgra = Console.ReadLine();
				ponovnaIgra = ponovnaIgra.ToUpper();
				if (ponovnaIgra.CompareTo("R") == 0)
				{
					ponovnaIgraMiliR=2;
				}
				else if (ponovnaIgra.CompareTo("M") == 0)
				{
					brIgraca = 2;
					rezultat1 = 0;
					rezultat2 = 0;
					ponovnaIgraMiliR=1;
				}
				else if (ponovnaIgra.CompareTo("I") == 0)
				{
					Console.WriteLine("Napuštate program. Doviđenja!");
				}
			} while (ponovnaIgra.CompareTo("R") != 0 && ponovnaIgra.CompareTo("M") != 0 && ponovnaIgra.CompareTo("I") != 0);
    } while (ponovnaIgra.CompareTo("I") != 0);
	}
}