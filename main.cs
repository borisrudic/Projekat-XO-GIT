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
				if (Tabla[i,j] == 0) return false;
			}
		}
		return true;
	}

  static int kurX = 2;
  static int kurY = 4;
	static int tablaX = 0;
	static int tablaY = 0;
	static int BrIgraca;

	static string Igrac1;
	static string Igrac2 = "igrac";

	static int Rezultat1 = 0;
	static int Rezultat2 = 0;
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

  static void unosPoteza (ref bool Igrac, int[,] Tabla){
		
		if(Tabla[tablaX, tablaY] == 0){
			
			Console.SetCursorPosition(2, 4);
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
			Console.SetCursorPosition(0, 13);
			Console.WriteLine("                           ");
			Console.SetCursorPosition(0, 14);
			Console.WriteLine("                           ");
			Console.SetCursorPosition(2,4);
			Igrac = !Igrac;
		}
		else{
			Console.SetCursorPosition(0, 13);
			Console.WriteLine("Nepravilan potez.");
			Console.SetCursorPosition(2,4);
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

	/*
  public static int[,] KompPotez(int[,] Tabla)
  {
		Igrac = !Igrac;
  }
  */

  public static void Main () {
    //Crtanje pocetne poruke
		for(int i = 1; i<= 20; i++) Console.Write("-");
		Console.WriteLine();
		for(int i = 1; i<=4; i++) Console.Write(" ");
		Console.Write("IGRA IKS OKS");
		for(int i = 1; i<=4; i++) Console.Write(" ");
		Console.WriteLine();
		for(int i = 1; i<= 20; i++) Console.Write("-");
		Console.WriteLine();

    //Unos broja igraca
		string ponovnaIgra;
		int KoPrvi;

		Console.WriteLine("Igrate li sami ili u 2 igrača? (1/2)");
		while (!(int.TryParse(Console.ReadLine(), out BrIgraca)) || BrIgraca < 1 || BrIgraca > 2) Console.WriteLine("Pogrešan unos, unesite broj igrača ponovo. (1/2)");
		Console.WriteLine("Upisati ime prvog igrača:");
		Igrac1 = Console.ReadLine();
Pocetak2:
	if(BrIgraca == 2) {
		Console.WriteLine("Upisati ime drugog igrača:");
		Igrac2 = Igrac2.Replace(Igrac2,Console.ReadLine());
	}
Pocetak1:
		Console.WriteLine("Upisati koji igrač igra prvi. (1/2)");
		while (!(int.TryParse(Console.ReadLine(), out KoPrvi)) || KoPrvi < 1 || KoPrvi > 2) Console.WriteLine("Pogrešan unos, unesite broj igrača koji igra prvi ponovo. (1/2)");
		if(KoPrvi == 2) Igrac = false;
		else Igrac = true;
		
		
    //Glavni deo koda
		CrtanjeTable();
    do {
			Console.SetCursorPosition(0, 14);
			Console.Write("Trenutno igra: ");
			if(Igrac) Console.WriteLine(Igrac1);
			else if (BrIgraca == 2) Console.WriteLine(Igrac2);
			else if (BrIgraca == 1) Console.WriteLine("Računar");
			Console.SetCursorPosition(2, 4);
			if (Igrac)
      {
				OdaberiPolje();
				unosPoteza(ref Igrac, Tabla);
			}
			else
      {
				//if(BrIgraca == 1) KompPotez(Tabla);
				//ovo je CPU potez
				//else {
					OdaberiPolje();
					unosPoteza(ref Igrac, Tabla);
				//}
			}
    } while(!Pobeda(Tabla) && !popunjenaTabla(Tabla) && !izlaz);//ako je true, nema vise upisa i program ide dalje
		Console.SetCursorPosition(0, 13);
		//Ispis pobednika:
		if(!Igrac && Pobeda(Tabla)) {
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
		if(BrIgraca == 2) Console.WriteLine("Trenutni rezultat: {0} {1}:{2} {3}", Igrac1, Rezultat1, Rezultat2, Igrac2);
		else Console.WriteLine("Trenutni rezultat: {0} {1}:{2} Računar", Igrac1, Rezultat1, Rezultat2);
Pocetak3:
		Console.WriteLine("Želite li menjanje drugog igrača, revanš ili izlazite iz programa? (M/R/I)");
		ponovnaIgra = Console.ReadLine();
		ponovnaIgra = ponovnaIgra.ToUpper();
		if(ponovnaIgra.CompareTo("R") == 0) {
			for(int i = 0; i<3; i++){
				for(int j = 0; j<3; j++){
					Tabla[i,j] = 0;
				}
			}
			goto Pocetak1;
		}
		else if (ponovnaIgra.CompareTo("M") == 0){
			BrIgraca = 2;
			Rezultat1 = 0;
			Rezultat2 = 0;
			for(int i = 0; i<3; i++){
				for(int j = 0; j<3; j++){
					Tabla[i,j] = 0;
				}
			}
			goto Pocetak2;
		}
		else if (ponovnaIgra.CompareTo("I") == 0){
			Console.WriteLine("Napuštate program. Doviđenja!");
		}
		else goto Pocetak3;
	}
}