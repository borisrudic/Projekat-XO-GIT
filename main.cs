using System;
class MainClass {
  static int[,] Tabla = {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}};
	static bool Igrac; //ako je igrac 1 na potezu, true
  /*
  Polja table predstavljaju:
  - 0 = nema nista
  - 1 = X
  - 2 = O
  */

  static bool izlaz = false; //napuštanje programa

  static bool popunjenaTabla (int [,] Tabla) //ako je true, nema vise slobodnih polja na tabli
  {
		for (int i = 0; i < 3; i++)
    {
			for (int j = 0; j < 3; j++)
      {
				if (Tabla[i, j] == 0) return false;
			}
		}
		return true;
	}

  static int kurX = 2;
  static int kurY = 4;
	static int tablaX = 0;
	static int tablaY = 0;
	static int BrIgraca;
	static int KoPrvi; //Ako KoPrvi = 1 znaci da mi igramo prvi a CPU igra sa O
	//U suprotnom ako je KoPrvi = 2 racunar igra prvi i to sa X
	static string Igrac1;
	static string Igrac2;

	static int Rezultat1 = 0;
	static int Rezultat2 = 0;

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
      else if (dugme.Key == ConsoleKey.Escape) izlaz = true;
    } while (dugme.Key != ConsoleKey.Enter && dugme.Key != ConsoleKey.Escape);
  }

  static void unosPoteza (ref bool Igrac, int[,] Tabla, int KoPrvi)
  {	
		if (Tabla[tablaX, tablaY] == 0)
    {
      if (BrIgraca == 1)
      {
        if (KoPrvi == 1)
        {
          Tabla[tablaX, tablaY] = 1; //X
          Console.SetCursorPosition(kurX, kurY);
          Console.Write("X");
        } 
        else
        {
          Tabla[tablaX, tablaY] = 2; //O
          Console.SetCursorPosition(kurX, kurY);
          Console.Write("O");
        }
      }
			else
			{
				if (Igrac)
        {
          Tabla[tablaX, tablaY] = 1; //X
          Console.SetCursorPosition(kurX, kurY);
          Console.Write("X");
        } 
        else
        {
          Tabla[tablaX, tablaY] = 2; //O
          Console.SetCursorPosition(kurX, kurY);
          Console.Write("O");
        }
			}
			Console.SetCursorPosition(0,13);
			Console.WriteLine("                   ");
			Igrac = !Igrac;
		}
		else
    {
			Console.SetCursorPosition(0, 13);
			Console.WriteLine("Nedozvoljen potez.");
			Console.SetCursorPosition(kurX,kurY);
		}
	}

	public static bool Pobeda (int [,] Tabla)
  {
		//provera uspravno
		for(int i = 0; i < 3; i++)
    {
			if (Tabla[0, i] != 0 && Tabla[1, i] == Tabla[0, i] && Tabla[2, i] == Tabla[0, i]) return true;
		}
		//provera vodoravno
		for(int i = 0; i < 3; i++)
    {
			if (Tabla[i, 0] != 0 && Tabla[i, 1] == Tabla[i, 0] && Tabla[i, 2] == Tabla[i, 0]) return true;
		}
		//provera dijagonala
		if (Tabla[0, 0] != 0 && Tabla[1, 1] == Tabla[0, 0] && Tabla[2, 2] == Tabla[0, 0]) return true;
		if (Tabla[2, 0] != 0 && Tabla[1, 1] == Tabla[2, 0] && Tabla[0, 2] == Tabla[2, 0]) return true;
		//ako nema pogotka
		return false;
	}

	public static bool BrojanjeHor (int[,] Tabla, int red, int broj)
	//Metoda gleda da li je moguce da kompjuter zavrsi
	//svoj potez unutar jednog reda, ili blokira potez igraca
  {
		int BrojacNeNula = 0;
		int BrojacNula = 0;
		for (int i = 0; i <= 2; i++)
    {
			if (Tabla[red, i] == 0)
      {
				BrojacNula++;
			}
			else if (Tabla[red, i] == broj)
      {
        BrojacNeNula++;
      }
		}
		if (BrojacNula == 1 && BrojacNeNula == 2) return true;
		return false;
	}
  
	public static bool BrojanjeVer (int[,] Tabla, int kol, int broj)
	//Metoda gleda da li je moguce da kompjuter zavrsi
	//svoj potez unutar jedne kolone, ili blokira potez igraca
  {
		int BrojacNeNula = 0;
		int BrojacNula = 0;
		for (int i = 0; i <= 2; i++)
    {
			if (Tabla[i, kol] == 0)
      {
				BrojacNula++;
			}
			else if (Tabla[i, kol] == broj)
      {
        BrojacNeNula++;
      }
		}
		if (BrojacNula == 1 && BrojacNeNula == 2) return true;
		return false;
	}

	public static bool BrojanjeGDij (int[,] Tabla, int broj)
	//Metoda gleda da li je moguce da kompjuter zavrsi
	//svoj potez unutar glavne dijagonale, ili blokira potez igraca
  {
		int BrojacNeNula = 0;
		int BrojacNula = 0;
		for (int i = 0; i <= 2; i++)
    {
			if (Tabla[i, i] == 0)
      {
				BrojacNula++;
			}
			else if (Tabla[i, i] == broj)
      {
        BrojacNeNula++;
      }
		}
		if (BrojacNula == 1 && BrojacNeNula == 2) return true;
		return false;
	}

	public static bool BrojanjeSDij (int[,] Tabla, int broj)
	//Metoda gleda da li je moguce da kompjuter zavrsi
	//svoj potez unutar druge dijagonale, ili blokira potez igraca
  {
		int BrojacNeNula = 0;
		int BrojacNula = 0;
		for (int i = 0; i <= 2; i++)
    {
			if (Tabla[i, 2 - i] == 0)
      {
				BrojacNula++;
			}
			else if (Tabla[i, 2 - i] == broj)
      {
        BrojacNeNula++;
      }
		}
		if (BrojacNula == 1 && BrojacNeNula == 2) return true;
		return false;
	}

  public static bool zamka (int[,] Tabla, int KoPrvi) //metoda koja detektuje da li protivnik sprema zamku na tabli
  {
    if (Tabla[0, 1] == KoPrvi && Tabla[1, 0] == KoPrvi && Tabla[0, 0] == 0) return true;
    else if (Tabla[0, 1] == KoPrvi && Tabla[1, 2] == KoPrvi && Tabla[0, 2] == 0) return true;
    else if (Tabla[1, 2] == KoPrvi && Tabla[2, 1] == KoPrvi && Tabla[2, 2] == 0) return true;
    else if (Tabla[1, 0] == KoPrvi && Tabla[2, 1] == KoPrvi && Tabla[2, 0] == 0) return true;
    else return false;
  }

  public static void potezKojiSprecavaZamku (int[,] Tabla, int KoPrvi)
  {
    if (Tabla[0, 1] == KoPrvi && Tabla[1, 0] == KoPrvi && Tabla[0, 0] == 0)
    {
      Console.SetCursorPosition(2, 4);
      if (KoPrvi == 1) 
			{
				Console.Write("O");
				Tabla[0, 0] = 2;
			}
      else 
			{
				Console.Write("X");
				Tabla[0, 0] = 1;
			}
      Console.SetCursorPosition(2, 4);
      Igrac = !Igrac;
    }
    else if (Tabla[1, 0] == KoPrvi && Tabla[2, 1] == KoPrvi && Tabla[2, 0] == 0)
    {
      Console.SetCursorPosition(14, 4);
      if (KoPrvi == 1)
      {
        Console.Write("O");
        Tabla[2, 0] = 2;
      }
      else
      {
        Console.Write("X");
        Tabla[2, 0] = 1;
      }
      Console.SetCursorPosition(14, 4);
      Igrac = !Igrac;
    }
    else if (Tabla[1, 2] == KoPrvi && Tabla[2, 1] == KoPrvi && Tabla[2, 2] == 0)
    {
      Console.SetCursorPosition(14, 10);
      if (KoPrvi == 1) 
			{
				Console.Write("O");
				Tabla[2, 2] = 2;
			}
      else 
			{
				Console.Write("X");
				Tabla[2, 2] = 1;
			}
      Console.SetCursorPosition(14, 10);
      Igrac = !Igrac;
    }
    else
    {
      Console.SetCursorPosition(2, 10);
			if (KoPrvi == 1) 
			{
				Console.Write("O");
				Tabla[0, 2] = 2;
			}
      else 
			{
				Console.Write("X");
				Tabla[0, 2] = 1;
			}
      Console.SetCursorPosition(2, 10);
      Igrac = !Igrac;
    }
  }

  public static void potezAkoNijeMoguceNiPobeditiNiBlokiratiNiSprecitiZamku (int[,] Tabla, int KoPrvi)
  {
    if ((Tabla[0, 0] == KoPrvi && Tabla [2, 2] == KoPrvi) || (Tabla[0, 2] == KoPrvi && Tabla[2, 0] == KoPrvi))
    {
      Random nasumicanBroj = new Random();
      int slucaj = nasumicanBroj.Next(0, 4);
      if (slucaj == 0)
      {
        Console.SetCursorPosition(8, 4);
				if (KoPrvi == 1) 
				{
					Console.Write("O");
					Tabla[1, 0] = 2;
				}
      	else 
				{
					Console.Write("X");
					Tabla[1, 0] = 1;
				}
        Console.SetCursorPosition(8, 4);
        Igrac = !Igrac;
      }
      else if (slucaj == 1)
      {
        Console.SetCursorPosition(2, 7);
				if (KoPrvi == 1) 
				{
					Console.Write("O");
					Tabla[0, 1] = 2;
				}
      	else 
				{
					Console.Write("X");
					Tabla[0, 1] = 1;
				}
        Console.SetCursorPosition(2, 7);
        Igrac = !Igrac;
      }
      else if (slucaj == 2)
      {
        Console.SetCursorPosition(14, 7);
				if (KoPrvi == 1) 
				{
					Console.Write("O");
					Tabla[2, 1] = 2;
				}
      	else 
				{
					Console.Write("X");
					Tabla[2, 1] = 1;
				}
        Console.SetCursorPosition(14, 7);
        Igrac = !Igrac;
      }
      else if (slucaj == 3)
      {
        Console.SetCursorPosition(8, 10);
				if (KoPrvi == 1) 		
				{
					Console.Write("O");
					Tabla[1, 2] = 2;
				}
      	else 
				{
					Console.Write("X");
					Tabla[1, 2] = 1;
				}
        Console.SetCursorPosition(8, 10);
        Igrac = !Igrac;
      }
    }
    else
    {
      if (Tabla[1, 1] == 0 && KoPrvi == 1)
      {
        Console.SetCursorPosition(8, 7);
        Console.Write("O");
        Tabla[1, 1] = 2;
				Igrac = !Igrac;
      }
      else if (Tabla[1, 1] == 0 && KoPrvi == 2)
      {
        Console.SetCursorPosition(8, 7);
        Console.Write("X");
        Tabla[1, 1] = 1;
				Igrac = !Igrac;
      }
      else
      {
        if (Tabla[0, 0] == 0 || Tabla[0, 2] == 0 || Tabla[2, 0] == 0 || Tabla[2, 2] == 0)
        {
          if (Tabla[0, 0] == 0)
          {
            if (KoPrvi == 1)
            {
              Tabla[0, 0] = 2;
              Console.SetCursorPosition(2, 4);
              Console.Write("O");
            }
            else
            {
              Tabla[0, 0] = 1;
              Console.SetCursorPosition(2, 4);
              Console.Write("X");
            }
          }
          else if (Tabla[0, 2] == 0)
          {
            if (KoPrvi == 1)
            {
              Tabla[2, 0] = 2;
              Console.SetCursorPosition(14, 4);
              Console.Write("O");
            }
            else
            {
              Tabla[2, 0] = 1;
              Console.SetCursorPosition(14, 4);
              Console.Write("X");
            }
          }
          else if (Tabla[2, 0] == 0)
          {
            if (KoPrvi == 1)
            {
              Tabla[0, 2] = 2;
              Console.SetCursorPosition(2, 10);
              Console.Write("O");
            }
            else
            {
              Tabla[0, 2] = 1;
              Console.SetCursorPosition(2, 10);
              Console.Write("X");
            }
          }
          else
          {
            if (KoPrvi == 1)
            {
              Tabla[2, 2] = 2;
              Console.SetCursorPosition(14, 10);
              Console.Write("O");
            }
            else
            {
              Tabla[2, 2] = 1;
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
          while (Tabla[xPotez, yPotez] != 0)
          {
            xPotez = nasumicanBroj.Next(0, 3);
            yPotez = nasumicanBroj.Next(0, 3);
          }
          Console.SetCursorPosition(2 + 6 * xPotez, 4 + 3 * yPotez);
          if (KoPrvi == 1) 
          {
            Console.Write("O");
            Tabla[xPotez, yPotez] = 2;
          }
          else 
          {
            Console.Write("X");
            Tabla[xPotez, yPotez] = 1;
          }
        }
        Igrac = !Igrac;
      }
    }
  }
  public static void KompPotez (int[,] Tabla, int KoPrvi)
  {
		if (KoPrvi == 1) //Komp igra drugi
		{
			//CPU nalazi potez za pobedu:
			if (BrojanjeHor(Tabla, 0, 2)) //prva vrsta
			{
				for (int i = 0; i < 3; i++)
				{
					if (Tabla[0, i] == 0)
					{
						Console.SetCursorPosition(2, 4 + 3 * i);
						Console.Write("O");
						Tabla[0, i] = 2;
						break;
					}
				}
				Igrac = !Igrac;
			}
			else if (BrojanjeHor (Tabla, 1, 2)) //druga vrsta
			{
				for (int i = 0; i < 3; i++)
				{
					if (Tabla[1, i] == 0)
					{
						Console.SetCursorPosition(8, 4 + 3 * i);
						Console.Write("O");
						Tabla[1, i] = 2;
						break;
					}
				}
				Igrac = !Igrac;
			}
			else if (BrojanjeHor (Tabla, 2, 2)) //treca vrsta
			{ 
				for (int i = 0; i < 3; i++)
				{
					if (Tabla[2, i] == 0)
					{
						Console.SetCursorPosition(14, 4 + 3 * i);
						Console.Write("O");
						Tabla[2, i] = 2;
						break;
					}
				}
				Igrac = !Igrac;
			}
			else if (BrojanjeVer(Tabla, 0, 2)) //prva kolona
			{ 
				for (int i = 0; i < 3; i++)
				{
					if (Tabla[i, 0] == 0)
					{
						Console.SetCursorPosition(2 + 6 * i, 4);
						Console.Write("O");
						Tabla[i, 0] = 2;
						break;
					}
				}
				Igrac = !Igrac;
			}
			else if (BrojanjeVer (Tabla, 1, 2)) //druga kolona
			{ 
				for (int i = 0; i < 3; i++)
				{
					if (Tabla[i, 1] == 0)
					{
						Console.SetCursorPosition(2 + 6 * i, 7);
						Console.Write("O");
						Tabla[i, 1] = 2;
						break;
					}
				}
				Igrac = !Igrac;

			}
			else if (BrojanjeVer (Tabla, 2, 2)) //treca kolona
			{ 
				for (int i = 0; i < 3; i++)
				{
					if (Tabla[i, 2] == 0)
					{
						Console.SetCursorPosition(2 + 6 * i, 10);
						Console.Write("O");
						Tabla[i, 2] = 2;
						break;
					}
				}
				Igrac = !Igrac;
			}
			else if (BrojanjeGDij (Tabla, 2)) //glavna dijagonala
			{
				for (int i = 0; i < 3; i++)
				{
					if (Tabla[i, i] == 0)
					{
						Console.SetCursorPosition(2 + 6 * i, 4 + 3 * i);
						Console.Write("O");
						Tabla[i, i] = 2;
						break;
					}
				}
				Igrac = !Igrac;
			}
			else if (BrojanjeSDij (Tabla, 2)) //sporedna dijagonala
			{
				for (int i = 0; i < 3; i++)
				{
					if (Tabla[i, 2 - i] == 0)
					{
						if (i == 2) Console.SetCursorPosition(14, 4);
						else if(i == 1) Console.SetCursorPosition(8, 7);
						else Console.SetCursorPosition(2, 10);
						//i = 2: 2, 0 = hor 2, ver 10
						//i = 1: 1, 1 = hor 8, ver 7
						//i = 0: 0, 2 = hor 14, ver 4
						Console.Write("O");
						Tabla[i, 2 - i] = 2;
						break;
					}
				}
				Igrac = !Igrac;
			}
			
			//CPU nalazi odbrambeni potez:
			else if (BrojanjeHor(Tabla, 0, 1)) //prva vrsta
			{
				for (int i = 0; i < 3; i++)
				{
					if (Tabla[0, i] == 0)
					{
						Console.SetCursorPosition(2, 4 + 3 * i);
						Console.Write("O");
						Tabla[0, i] = 2;
						break;
					}
				}
				Igrac = !Igrac;
			}
			else if (BrojanjeHor(Tabla, 1, 1)) //druga vrsta
			{
				for (int i = 0; i < 3; i++)
				{
					if (Tabla[1, i] == 0)
					{
						Console.SetCursorPosition(8, 4 + 3 * i);
						Console.Write("O");
						Tabla[1, i] = 2;
						break;
					}
				}
				Igrac = !Igrac;
			}
			else if (BrojanjeHor (Tabla, 2, 1)) //treca vrsta
			{ 
				for (int i = 0; i < 3; i++)
				{
					if (Tabla[2, i] == 0)
					{
						Console.SetCursorPosition(14, 4 + 3 * i);
						Console.Write("O");
						Tabla[2, i] = 2;
						break;
					}
				}
				Igrac = !Igrac;
			}
			else if (BrojanjeVer (Tabla, 0, 1)) //prva kolona 
			{
				for (int i = 0; i < 3; i++)
				{
					if (Tabla[i, 0] == 0)
					{
						Console.SetCursorPosition(2 + 6 * i, 4);
						Console.Write("O");
						Tabla[i, 0] = 2;
						break;
					}
				}
				Igrac = !Igrac;
			}
			else if (BrojanjeVer (Tabla, 1, 1)) //druga kolona
			{
				for (int i = 0; i < 3; i++)
				{
					if (Tabla[i, 1] == 0)
					{
						Console.SetCursorPosition(2 + 6 * i, 7);
						Console.Write("O");
						Tabla[i, 1] = 2;
						break;
					}
				}
				Igrac = !Igrac;
			}
			else if (BrojanjeVer (Tabla, 2, 1)) //treca kolona
			{
				for (int i = 0; i < 3; i++)
				{
					if (Tabla[i, 2] == 0)
					{
						Console.SetCursorPosition(2 + 6 * i, 10);
						Console.Write("O");
						Tabla[i, 2] = 2;
						break;
					}
				}
				Igrac = !Igrac;
			}
			else if (BrojanjeGDij (Tabla, 1)) //glavna dijagonala
			{
				for (int i = 0; i < 3; i++)
				{
					if (Tabla[i, i] == 0)
					{
						Console.SetCursorPosition(2 + 6 * i, 4 + 3 * i);
						Console.Write("O");
						Tabla[i, i] = 2;
						break;
					}
				}
				Igrac = !Igrac;
			}
			else if (BrojanjeSDij (Tabla, 1)) //sporedna dijagonala
			{
				for (int i = 0; i < 3; i++)
				{
					if (Tabla[i, 2 - i] == 0)
					{
						if (i == 2) Console.SetCursorPosition(14, 4);
						else if(i == 1) Console.SetCursorPosition(8, 7);
						else Console.SetCursorPosition(2, 10);
						Console.Write("O");
						Tabla[i, 2 - i] = 2;
						break;
					}
				}
				Igrac = !Igrac;
			}
			//CPU ispituje da li protivnik pravi zamku na tabli
			//i sprečava je ako je tako
			else if (zamka(Tabla, 1)) potezKojiSprecavaZamku(Tabla, 1);

      else if (Tabla[1, 1] == 1 && Tabla[0, 0] == 0 && Tabla[0, 2] == 0 && Tabla[2, 0] == 0 && Tabla[2, 2] == 0)
      {
				//igra se random potez u praznom cosku
        Random nasumicanBroj1 = new Random();
        int slucaj = nasumicanBroj1.Next(0, 4);
        if (slucaj == 0)
        {
          Tabla[0, 0] = 2;
          Console.SetCursorPosition(2, 4);
          Console.Write("O");
          Igrac = !Igrac;
        }
        else if (slucaj == 1)
        {
          Tabla[2, 0] = 2;
          Console.SetCursorPosition(14, 4);
          Console.Write("O");
          Igrac = !Igrac;
        }
        else if (slucaj == 2)
        {
          Tabla[0, 2] = 2;
          Console.SetCursorPosition(2, 10);
          Console.Write("O");
          Igrac = !Igrac;
        }
        else
        {
          Tabla[2, 2] = 2;
          Console.SetCursorPosition(14, 10);
          Console.Write("O");
					Igrac = !Igrac;
        }
      }
			//CPU stavlja potez u sredinu ako nije zauzeta
			//ili bira random potez jer ne može ni da pobedi ni da blokira:
			else potezAkoNijeMoguceNiPobeditiNiBlokiratiNiSprecitiZamku(Tabla, 1);
		}
		else //Komp igra prvi
		{
      //CPU nalazi potez za pobedu:
      if (BrojanjeHor(Tabla, 0, 1)) //prva vrsta
      {
        for (int i = 0; i < 3; i++)
        {
          if (Tabla[0, i] == 0)
          {
            Console.SetCursorPosition(2, 4 + 3 * i);
            Console.Write("X");
            Tabla[0, i] = 1;
            break;
          }
        }
        Igrac = !Igrac;
      }
      else if (BrojanjeHor (Tabla, 1, 1)) //druga vrsta
      {
        for (int i = 0; i < 3; i++)
        {
          if (Tabla[1, i] == 0)
          {
            Console.SetCursorPosition(8, 4 + 3 * i);
            Console.Write("X");
            Tabla[1, i] = 1;
            break;
          }
        }
        Igrac = !Igrac;
      }
      else if (BrojanjeHor (Tabla, 2, 1)) //treca vrsta
      { 
        for (int i = 0; i < 3; i++)
        {
          if (Tabla[2, i] == 0)
          {
            Console.SetCursorPosition(14, 4 + 3 * i);
            Console.Write("X");
            Tabla[2, i] = 1;
            break;
          }
        }
        Igrac = !Igrac;
      }
      else if (BrojanjeVer(Tabla, 0, 1)) //prva kolona
      { 
        for (int i = 0; i < 3; i++)
        {
          if (Tabla[i, 0] == 0)
          {
            Console.SetCursorPosition(2 + 6 * i, 4);
            Console.Write("X");
            Tabla[i, 0] = 1;
            break;
          }
        }
        Igrac = !Igrac;
      }
      else if (BrojanjeVer (Tabla, 1, 1)) //druga kolona
      { 
        for (int i = 0; i < 3; i++)
        {
          if (Tabla[i, 1] == 0)
          {
            Console.SetCursorPosition(2 + 6 * i, 7);
            Console.Write("X");
            Tabla[i, 1] = 1;
            break;
          }
        }
        Igrac = !Igrac;

      }
      else if (BrojanjeVer (Tabla, 2, 1)) //treca kolona
      { 
        for (int i = 0; i < 3; i++)
        {
          if (Tabla[i, 2] == 0)
          {
            Console.SetCursorPosition(2 + 6 * i, 10);
            Console.Write("X");
            Tabla[i, 2] = 1;
            break;
          }
        }
        Igrac = !Igrac;
      }
      else if (BrojanjeGDij (Tabla, 1)) //glavna dijagonala
      {
        for (int i = 0; i < 3; i++)
        {
          if (Tabla[i, i] == 0)
          {
            Console.SetCursorPosition(2 + 6 * i, 4 + 3 * i);
            Console.Write("X");
            Tabla[i, i] = 1;
            break;
          }
        }
        Igrac = !Igrac;
      }
      else if (BrojanjeSDij (Tabla, 1)) //sporedna dijagonala
      {
        for (int i = 0; i < 3; i++)
        {
          if (Tabla[i, 2 - i] == 0)
          {
            if (i == 2) Console.SetCursorPosition(14, 4);
            else if(i == 1) Console.SetCursorPosition(8, 7);
            else Console.SetCursorPosition(2, 10);
            //i = 2: 2, 0 = hor 2, ver 10
            //i = 1: 1, 1 = hor 8, ver 7
            //i = 0: 0, 2 = hor 14, ver 4
            Console.Write("X");
            Tabla[i, 2 - i] = 1;
            break;
          }
        }
        Igrac = !Igrac;
      }
      
      //CPU nalazi odbrambeni potez:
      else if (BrojanjeHor(Tabla, 0, 2)) //prva vrsta
      {
        for (int i = 0; i < 3; i++)
        {
          if (Tabla[0, i] == 0)
          {
            Console.SetCursorPosition(2, 4 + 3 * i);
            Console.Write("X");
            Tabla[0, i] = 1;
            break;
          }
        }
        Igrac = !Igrac;
      }
      else if (BrojanjeHor(Tabla, 1, 2)) //druga vrsta
      {
        for (int i = 0; i < 3; i++)
        {
          if (Tabla[1, i] == 0)
          {
            Console.SetCursorPosition(8, 4 + 3 * i);
            Console.Write("X");
            Tabla[1, i] = 1;
            break;
          }
        }
        Igrac = !Igrac;
      }
      else if (BrojanjeHor (Tabla, 2, 2)) //treca vrsta
      { 
        for (int i = 0; i < 3; i++)
        {
          if (Tabla[2, i] == 0)
          {
            Console.SetCursorPosition(14, 4 + 3 * i);
            Console.Write("X");
            Tabla[2, i] = 1;
            break;
          }
        }
        Igrac = !Igrac;
      }
      else if (BrojanjeVer (Tabla, 0, 2)) //prva kolona 
      {
        for (int i = 0; i < 3; i++)
        {
          if (Tabla[i, 0] == 0)
          {
            Console.SetCursorPosition(2 + 6 * i, 4);
            Console.Write("X");
            Tabla[i, 0] = 1;
            break;
          }
        }
        Igrac = !Igrac;
      }
      else if (BrojanjeVer (Tabla, 1, 2)) //druga kolona
      {
        for (int i = 0; i < 3; i++)
        {
          if (Tabla[i, 1] == 0)
          {
            Console.SetCursorPosition(2 + 6 * i, 7);
            Console.Write("X");
            Tabla[i, 1] = 1;
            break;
          }
        }
        Igrac = !Igrac;
      }
      else if (BrojanjeVer (Tabla, 2, 2)) //treca kolona
      {
        for (int i = 0; i < 3; i++)
        {
          if (Tabla[i, 2] == 0)
          {
            Console.SetCursorPosition(2 + 6 * i, 10);
            Console.Write("X");
            Tabla[i, 2] = 1;
            break;
          }
        }
        Igrac = !Igrac;
      }
      else if (BrojanjeGDij (Tabla, 2)) //glavna dijagonala
      {
        for (int i = 0; i < 3; i++)
        {
          if (Tabla[i, i] == 0)
          {
            Console.SetCursorPosition(2 + 6 * i, 4 + 3 * i);
            Console.Write("X");
            Tabla[i, i] = 1;
            break;
          }
        }
        Igrac = !Igrac;
      }
      else if (BrojanjeSDij (Tabla, 2)) //sporedna dijagonala
      {
        for (int i = 0; i < 3; i++)
        {
          if (Tabla[i, 2 - i] == 0)
          {
            if (i == 2) Console.SetCursorPosition(14, 4);
            else if(i == 1) Console.SetCursorPosition(8, 7);
            else Console.SetCursorPosition(2, 10);
            Console.Write("X");
            Tabla[i, 2 - i] = 1;
            break;
          }
        }
        Igrac = !Igrac;
      }
      //CPU ispituje da li protivnik pravi zamku na tabli
      //i sprečava je ako je tako
      else if (zamka(Tabla, 2)) potezKojiSprecavaZamku(Tabla, 2);
      //CPU stavlja potez u sredinu ako nije zauzeta
      //ili bira random potez jer ne može ni da pobedi ni da blokira:
      else potezAkoNijeMoguceNiPobeditiNiBlokiratiNiSprecitiZamku(Tabla, 2);
		}
  }

  static string UnosPrviIgrac()
  {
		string pomocni;
    Console.WriteLine("Upisati ime prvog igrača(max. 12 karaktera): ");
		pomocni = Console.ReadLine();
		while(pomocni.Length > 12 || pomocni.Length == 0)
    {
			Console.WriteLine("Invalidno ime. Upisati opet (max. 12 karaktera): ");
			pomocni = Console.ReadLine();
		}
		Igrac1 = pomocni;
    return Igrac1;
  }

  static string UnosDrugiIgrac()
  {
		string pomocni2;
    Console.WriteLine("Upisati ime drugog igrača (max. 12 karaktera):");
		pomocni2 = Console.ReadLine();
		while(pomocni2.Length>12 || pomocni2.Length == 0){
			Console.WriteLine("Invalidno ime. Upisati opet (max. 12 karaktera): ");
			pomocni2 = Console.ReadLine();
		}
		Igrac2 = pomocni2;
    return Igrac2;
  }

  static void IspisPobednika()
  {
    if (!Igrac && Pobeda(Tabla)) {
			Console.WriteLine("Partija se završila pobedom igrača " + Igrac1 + ".");
			Rezultat1++;
		}
		else if (Igrac && Pobeda(Tabla) && (BrIgraca == 2)) {
			Console.WriteLine("Partija se završila pobedom igrača " + Igrac2 + ".");
			Rezultat2++;
		}
		else if (Igrac && Pobeda(Tabla) && (BrIgraca == 1)) {
			Console.WriteLine("Partija se završila pobedom računara.");
			Rezultat2++;
		}
		else Console.WriteLine("Partija se završila nerešenim rezultatom.");
    Console.WriteLine();
		if(BrIgraca == 2) Console.WriteLine("Trenutni rezultat: {0} {1}:{2} {3}", Igrac1, Rezultat1, Rezultat2, Igrac2);
		else Console.WriteLine("Trenutni rezultat: {0} {1}:{2} Računar", Igrac1, Rezultat1, Rezultat2);
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
  
  public static int[,] PocetnaTabla (int[,] Tabla)
  {
    for (int i = 0; i < 3; i++)
    {
			for (int j = 0; j < 3; j++)
      {
				Tabla[i,j] = 0;
			}
		}
    return Tabla;
  }

  public static int BrojIgraca ()
  {
    Console.WriteLine("Igrate li sami ili u 2 igrača? (1/2)");
		while (!(int.TryParse(Console.ReadLine(), out BrIgraca)) || BrIgraca < 1 || BrIgraca > 2) Console.WriteLine("Pogrešan unos, unesite broj igrača ponovo. (1/2)");
    return BrIgraca;
  }
  
  public static string ProveraNaziva (int BrIgraca, string Igrac1)
  {
    if (BrIgraca == 2)
    {
			do
      {
				UnosDrugiIgrac();
        if (Igrac1.CompareTo(Igrac2) == 0)
        {
			    Console.WriteLine("Greška. Nazivi ne smeju biti isti.");
		    }
      } while (Igrac1.CompareTo(Igrac2) == 0);
		}
    return Igrac2;
  }

  public static bool KoPrviIgra()
  {
    Console.WriteLine("Upisati koji igrač igra prvi. (1/2)");
		while (!(int.TryParse(Console.ReadLine(), out KoPrvi)) || KoPrvi < 1 || KoPrvi > 2) Console.WriteLine("Pogrešan unos, unesite broj igrača koji igra prvi ponovo. (1/2)");
		if (KoPrvi == 2) Igrac = false;
		else Igrac = true;
    return Igrac;
  }

  public static void Main () {
    //Crtanje pocetne poruke
		PocetnaPoruka();
    //Unos broja igraca
		string ponovnaIgra;
    int PonovnaIgraMiliR=0;
		BrojIgraca();
    UnosPrviIgrac();
    ProveraNaziva(BrIgraca, Igrac1);
    KoPrviIgra();
    PocetnaTabla(Tabla);
    do
    {
			if (PonovnaIgraMiliR==1) ProveraNaziva(BrIgraca, Igrac1);
			if(PonovnaIgraMiliR==2 || PonovnaIgraMiliR==1)
			{
				KoPrviIgra();
				PocetnaTabla(Tabla);
			}
			//Glavni deo koda
			CrtanjeTable();
			do {
				Console.SetCursorPosition(0, 14);
				Console.Write("Trenutno igra: ");
				if (Igrac) Console.WriteLine(Igrac1);
				else if (BrIgraca == 2) Console.WriteLine(Igrac2);
				else if (BrIgraca == 1) Console.WriteLine("Računar");
				Console.SetCursorPosition(kurX, kurY);
				if (Igrac)
				{
					OdaberiPolje();
					unosPoteza(ref Igrac, Tabla, KoPrvi);
				}
				else
				{
					if (BrIgraca == 1) KompPotez(Tabla, KoPrvi);
					//ovo je CPU potez
					else 
					{
						OdaberiPolje();
						unosPoteza(ref Igrac, Tabla, KoPrvi);
					}
				}
				Console.SetCursorPosition(0, 14);
				Console.WriteLine("                           ");
			} while(!Pobeda(Tabla) && !popunjenaTabla(Tabla) && !izlaz);
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
					PonovnaIgraMiliR=2;
				}
				else if (ponovnaIgra.CompareTo("M") == 0)
				{
					BrIgraca = 2;
					Rezultat1 = 0;
					Rezultat2 = 0;
					PonovnaIgraMiliR=1;
				}
				else if (ponovnaIgra.CompareTo("I") == 0)
				{
					Console.WriteLine("Napuštate program. Doviđenja!");
				}
			} while (ponovnaIgra.CompareTo("R") != 0 && ponovnaIgra.CompareTo("M") != 0 && ponovnaIgra.CompareTo("I") != 0);
    } while(ponovnaIgra.CompareTo("I") != 0);
	}
}