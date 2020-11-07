using System;

class MainClass {
  static int[,] Tabla = {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}};
	static int[,] stara;
	static int[,] nova;
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
				if (Tabla[i,j] == 0) return false;
			}
		}
		return true;
	}

  static int kurX = 2;
  static int kurY = 4;
	static int tablaX = 0;
	static int tablaY = 0;

  static void CrtanjeTable()
  {
    Console.Clear();
		for(int i = 1; i<= 20; i++) Console.Write("-");
		Console.WriteLine();
		for(int i = 1; i<=4; i++) Console.Write(" ");
		Console.Write("IGRA IKS OKS");
		for(int i = 1; i<=4; i++) Console.Write(" ");
		Console.WriteLine();
		for(int i = 1; i<= 20; i++) Console.Write("-");
		Console.WriteLine();
		for(int i = 1; i<= 9; i++){
			if(i ==  3 || i == 6) Console.WriteLine("_____|_____|_____");
			else Console.WriteLine("     |     |     ");
		}
		Console.SetCursorPosition(kurX, kurY);
  }

  //Metode za pomeranje kursora
  public static void PomeriDesno(){
    if(kurX <= 8) {
			kurX += 6;
			tablaX++;
		}
  }
  public static void PomeriDole(){
    if(kurY <= 9){
			kurY += 3;
			tablaY++;
		}
  }
  public static void PomeriLevo(){
    if(kurX > 7) {
			kurX -= 6;
			tablaX--;
		}
  }
  public static void PomeriGore(){
    if(kurY > 4) {
			kurY -= 3;
			tablaY--;
		}
  }
  
  public static void OdaberiPolje(){
    ConsoleKeyInfo cki;
    cki = Console.ReadKey(true);
    do
    {
      Console.SetCursorPosition(kurX, kurY);
      cki = Console.ReadKey();
      if(cki.Key == ConsoleKey. UpArrow) PomeriGore();
      else if(cki.Key == ConsoleKey. DownArrow) PomeriDole();
      else if(cki.Key == ConsoleKey. RightArrow) PomeriDesno();
      else if(cki.Key == ConsoleKey. LeftArrow) PomeriLevo();
      else if(cki.Key == ConsoleKey. Escape) izlaz = true; 
    } while(cki.Key != ConsoleKey.Enter && cki.Key != ConsoleKey.Escape);
  }

  static void unosPoteza (bool Igrac, int[,] Tabla){
		if(Tabla[tablaX, tablaY] == 0){
			stara = Tabla;
			if (Igrac){
				Tabla[tablaX, tablaY] = 1; //X
				Console.SetCursorPosition(kurX, kurY);
				Console.Write("X");
				Console.SetCursorPosition(kurX, kurY);
			} 
			else {
				Tabla[tablaX, tablaY] = 2; //O
				Console.SetCursorPosition(kurX, kurY);
				Console.Write("O");
				Console.SetCursorPosition(kurX, kurY);
			}
			nova = Tabla;
		}
		
	}
  

	public static bool Pobeda (int [,] Tabla){
		//provera uspravno
		for(int i = 0; i<3; i++) {
			if (Tabla[0,i] != 0 && Tabla[1,i] == Tabla[0,i] && Tabla[2,i] == Tabla[0,i]) return true;
		}
		//provera vodoravno
		for(int i = 0; i<3; i++) {
			if (Tabla[i,0] != 0 && Tabla[i,1] == Tabla[i,0] && Tabla[i,2] == Tabla[i,0]) return true;
		}
		//provera dijagonala
		if (Tabla[0,0] != 0 && Tabla[1,1] == Tabla[0,0] && Tabla[2,2] == Tabla[0,0]) return true;
		if (Tabla[2,0] != 0 && Tabla[1,1] == Tabla[2,0] && Tabla[0,2] == Tabla[2,0]) return true;
		//ako nema pogotka
		return false;
	}

	public static bool ImaLiPromene(int[,] stara, int[,] nova){
		for(int i = 0; i<3; i++){
			for(int j = 0; j<3; j++){
				if(stara[i,j] != nova[i,j]) return true;
			}
		}
		return false;
	} //ako ima promene, vrati true, ako je sve isto, vrati false

	/*
  public static int[,] KompPotez(int[,] Tabla)
  {

  }
  */

  public static void Main () {
    //Crtanje pocetne poruke
    //Console.Clear();
		for(int i = 1; i<= 20; i++) Console.Write("-");
		Console.WriteLine();
		for(int i = 1; i<=4; i++) Console.Write(" ");
		Console.Write("IGRA IKS OKS");
		for(int i = 1; i<=4; i++) Console.Write(" ");
		Console.WriteLine();
		for(int i = 1; i<= 20; i++) Console.Write("-");
		Console.WriteLine();

    //Unos broja igraca
		Console.WriteLine("Igrate li sami ili u 2 igraca? (1/2)");
		int BrIgraca;
		while (!(int.TryParse(Console.ReadLine(), out BrIgraca)) || BrIgraca < 1 || BrIgraca > 2) Console.WriteLine("Pogresan unos, unesite broj igraca ponovo. (1/2)");
		bool Igrac = true; //ako Igrac = true igra igrac 1, ako je false igra igrac 2 ili kompjuter
		
    //Glavni deo koda
		CrtanjeTable();
    do {
			if (Igrac)
      {
				OdaberiPolje();
				unosPoteza(Igrac, Tabla);
			}
			else
      {
				//if(BrIgraca == 1) KompPotez(Tabla);
				//ovo je CPU potez
				//else {
					OdaberiPolje();
					unosPoteza(Igrac, Tabla);
				//}
			}
			if(ImaLiPromene(stara, nova)) Igrac = !Igrac;
    } while(!Pobeda(Tabla) && !popunjenaTabla(Tabla) && !izlaz);//ako je true, nema vise upisa i program ide dalje
		Console.SetCursorPosition(0, 13);

	}
}