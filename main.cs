using System;

class MainClass {
  static int[,] Tabla = {{0, 0, 0}, {0, 0, 0},{0, 0, 0}};
  //Polja table predstavljaju:
  //0 nema nista
  //1 X
  //2 O
  static bool izlaz = false; // napuštanje programa
  static bool popunjenaTabla (int [,] Tabla){
		for(int i = 0; i < 3; i++){
			for(int j = 0; j < 3; j++){
				if(Tabla[i,j] == 0) return true;
			}
		}
		return false;
		//Znaci, ako ima bar 1 prazno polje, igra i dalje moze da se igra
	}
  static int kurX = 7;
  static int kurY = 10;
  public static void CrtanjeTable (){
    //Iskoristiti kod iz prez. "Tipovi podataka" za crtanje table
    //Za promenu boje: komanda
    //Console.BackgroundColor = Console.Color.DarkGreen;
    //Vracanje: ResetColor
    //jos jedna: SetCursorPosition za postavljanje kursora od koga se pocinje
    int PocRed = 9;
    int PocKol = 5;
    int visina = 2 * Tabla.GetLength(0) + 1;
    int sirina = 2 * Tabla.GetLength(1) + 1;
    char hor = '\u2500', ver = '\u2502';
    char gLevi = '\u250C', gDesni = '\u2510';
    char dLevi = '\u2514', dDesni = '\u2518';
    char levi = '\u251C', desni = '\u2524';
    char gornji = '\u252C', donji = '\u2534';
    char centralni = '\u253C';
    //char X = 'X';
    //char O = 'O';
    Console.BackgroundColor = ConsoleColor.DarkBlue;
    Console.ForegroundColor = ConsoleColor.Blue;
    for (int red = 0; red < visina; red++)
    {
      Console.SetCursorPosition(PocKol, PocRed + red);
        for (int kol = 0; kol < sirina; kol++)
            if (red % 2 == 1)
                if (kol % 2 == 0) Console.Write(ver);          
                else { 
                  Console.Write(' ');
                  if(Tabla[red/2, kol/2] == 0) Console.Write(' ');
                  else if(Tabla[red/2, kol/2] == 1) {
                    Console.Write('X');
                  }
                  else{
                    Console.Write('O');
                  } 
                  Console.Write(' '); 
                }
            else
                if (red == 0)
                    if (kol == 0) Console.Write(gLevi);    
                    else if (kol == sirina - 1) Console.Write(gDesni);
                    else if (kol % 2 == 0) Console.Write(gornji);
                    else { Console.Write(hor); Console.Write(hor); Console.Write(hor);}        
                else if (red == visina - 1)
                    if (kol == 0) Console.Write(dLevi);      
                    else if (kol == sirina - 1) Console.Write(dDesni);     
                    else if (kol % 2 == 0) Console.Write(donji);      
                    else { Console.Write(hor); Console.Write(hor); Console.Write(hor); }
                else
                    if (kol == 0) Console.Write(levi);    
                    else if (kol == sirina - 1) Console.Write(desni);     
                    else if (kol % 2 == 0) Console.Write(centralni); 
                    else { Console.Write(hor); Console.Write(hor); Console.Write(hor); }    
        
        Console.WriteLine();
    }
    Console.ResetColor();  
    Console.SetCursorPosition(kurX, kurY);
  }
  public static void PomeriDesno(){
    if(kurX < 13) kurX+=4;
  }
  public static void PomeriDole(){
    if(kurY < 14 )kurY+=2;
  }
  public static void PomeriLevo(){
    if(kurX > 7) kurX-=4;
  }
  public static void PomeriGore(){
    if (kurY > 10) kurY-=2;
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

	//public static int[,] KompPotez(int[,] Tabla){}

  public static void Main () {
    Console.Clear();
		for(int i = 1; i<= 20; i++) Console.Write("-");
		Console.WriteLine();
		for(int i = 1; i<=4; i++) Console.Write(" ");
		Console.Write("IGRA IKS OKS");
		for(int i = 1; i<=4; i++) Console.Write(" ");
		Console.WriteLine();
		for(int i = 1; i<= 20; i++) Console.Write("-");
		Console.WriteLine();
		Console.WriteLine("Igrate li sami ili u 2 igraca? (1/2)");
		int BrIgraca;
		while (!(int.TryParse(Console.ReadLine(), out BrIgraca)) || BrIgraca < 1 || BrIgraca>2) Console.WriteLine("Unesite broj igraca ponovo. (1/2)");
		bool Igrac = true;
		//ako Igrac = true igra igrac 1, ako false igra 2
    do{
      CrtanjeTable();

      OdaberiPolje();
			//if(BrIgraca == 1) KompPotez(Tabla);
			//CPU potez
      if(BrIgraca == 2) Igrac = !Igrac;
    }while(!Pobeda(Tabla) || !popunjenaTabla(Tabla) || !izlaz);
    Console.SetCursorPosition(0,12);
  }
}