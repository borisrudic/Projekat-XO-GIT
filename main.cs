using System;

class MainClass {
  static int[,] Tabla = {{0,0,0},{0,0,0},{0,0,0}};

  //Polja table predstavljaju:
  //0 nema nista
  //1 X
  //2 O
  static bool izlaz = false; //napustanje programa
  static int kurX = 7;
  static int kurY = 6;
  public static void CrtanjeTable (){
    //Iskoristiti kod iz prez. "Tipovi podataka" za crtanje table
    //Za promenu boje: komanda
    //Console.BackgroundColor = Console.Color.DarkGreen;
    //Vracanje: ResetColor
    //jos jedna: SetCursorPosition za postavljanje kursora od koga se pocinje
    int PocRed = 5;
    int PocKol = 5;
    int visina = 2 * Tabla.GetLength(0) + 1;
    int sirina = 2 * Tabla.GetLength(1) + 1;
    char hor = '\u2500', ver = '\u2502';
    char gLevi = '\u250C', gDesni = '\u2510';
    char dLevi = '\u2514', dDesni = '\u2518';
    char levi = '\u251C', desni = '\u2524';
    char gornji = '\u252C', donji = '\u2534';
    char centralni = '\u253C';
    char X = 'X';
    char O = 'O';
    Console.BackgroundColor = ConsoleColor.Green;
    Console.ForegroundColor = ConsoleColor.Black;
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

  }
  public static void PomeriDesno(){
    if(kurX<13)kurX+=4;
  }
  public static void PomeriDole(){
    if(kurY<10)kurY+=2;
  }
  public static void PomeriLevo(){
    if(kurX>7)kurX-=4;
  }
  public static void PomeriGore(){
    if(kurY>6)kurY-=2;
  }
  
  public static void OdaberiPolje(){
    ConsoleKeyInfo cki;
    cki = Console.ReadKey(true);
    do{
      Console.SetCursorPosition(kurX, kurY);
      cki = Console.ReadKey();
      if(cki.Key == ConsoleKey. UpArrow) PomeriGore();
      else if(cki.Key == ConsoleKey. DownArrow) PomeriDole();
      else if(cki.Key == ConsoleKey. RightArrow) PomeriDesno();
      else if(cki.Key == ConsoleKey. LeftArrow) PomeriLevo();
      else if(cki.Key == ConsoleKey. Escape) izlaz = true; 
      
    }while(cki.Key != ConsoleKey.Enter && cki.Key != ConsoleKey.Escape);
  }
  public static void Main () {
    do{
      CrtanjeTable();
      
      OdaberiPolje();

      
    }while(!izlaz);
    Console.SetCursorPosition(0,12);
  }
}