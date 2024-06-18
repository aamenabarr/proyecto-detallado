using Fire_Emblem;
using Fire_Emblem_GUI;
using Fire_Emblem_View;


// RunConsoleView(); // Descomentar para correr la vista en consola

FireEmblemWindow window = new FireEmblemWindow();
window.Start(RunGuiView);

void RunGuiView()
{
    // Acá se forman los equipos y son retornados como un string
    string dataTeam1 = window.GetTeam1();
    string dataTeam2 = window.GetTeam2();

    // Harcodeamos los equipos para propósitos de este ejemplo
    MyUnit[] team1 = [new MyUnit("Marth", "Sword", 54, 62, 53, 43, 37)];
    MyUnit[] team2 =
    [
        new MyUnit("Seliph", "Sword", 55, 68, 27, 48, 39),
        new MyUnit("Soren", "Magic", 49, 63, 41, 25, 53)
    ];

    // Mostramos la vista con ambos equipos. Desde esta vista se puede elegir a la unidad que ataca
    window.UpdateTeams(team1, team2);

    // El primer equipo elige a la unidad con la que atacará (retorna el id de la unidad seleccionada)
    int idSelectedUnitTeam1 = window.SelectUnitTeam1();
    MyUnit unitTeam1 = team1[idSelectedUnitTeam1];

    // El segundo equipo elige la unidad con la que defiende
    int idSelectedUnitTeam2 = window.SelectUnitTeam2();
    MyUnit unitTeam2 = team2[idSelectedUnitTeam2];

    // Acá cambiamos a la ventana de batalla (con UpdateUnitsStatsDuringBattle) y luego simulamos un ataque que hace 
    // 20 de daño
    window.UpdateUnitsStatsDuringBattle(unitTeam1, unitTeam2);
    window.ShowAttackFromTeam1(unitTeam1, unitTeam2);
    unitTeam2.Hp -= 20;

    // Actualizamos los estadísticas de ambas unidades y ahora simulamos un contraataque que daña 15 
    window.UpdateUnitsStatsDuringBattle(unitTeam1, unitTeam2);
    window.ShowAttackFromTeam2(unitTeam1, unitTeam2);
    unitTeam1.Hp -= 15;

    // Mostramos las stats luego de la batalla
    window.UpdateUnitsStatsDuringBattle(unitTeam1, unitTeam2);

    // Una vez terminada la batalla, volvemos a la ventana que muestra todas las unidades con sus stats actualizados
    window.UpdateTeams(team1, team2);

    // Esta vez hacemos que el team2 seleccione primero la unidad a atacar y luego team1 selecciona la unidad con que
    // defiende
    idSelectedUnitTeam2 = window.SelectUnitTeam2();
    unitTeam2 = team2[idSelectedUnitTeam2];
    idSelectedUnitTeam1 = window.SelectUnitTeam1();
    unitTeam1 = team1[idSelectedUnitTeam1];

    // Volvemos a emular un ataque. Esta vez ataca primero el team2
    window.UpdateUnitsStatsDuringBattle(unitTeam1, unitTeam2);
    window.ShowAttackFromTeam2(unitTeam1, unitTeam2);
    unitTeam1.Hp -= 7;
    window.UpdateUnitsStatsDuringBattle(unitTeam1, unitTeam2);
    window.ShowAttackFromTeam1(unitTeam1, unitTeam2);
    unitTeam2.Hp -= 3;
    window.UpdateUnitsStatsDuringBattle(unitTeam1, unitTeam2);

    // Finalmente, decimos (arbitrariamente) que el team1 ganó
    window.CongratulateTeam1(team1);
}

void RunConsoleView()
{
    string testFolder = SelectTestFolder();
    string test = SelectTest(testFolder);
    string teamsFolder = testFolder.Replace("-Tests","");
    AnnounceTestCase(test);

    var view = View.BuildManualTestingView(test);
    var game = new Game(view, teamsFolder);
    game.Play();
}

string SelectTestFolder()
{
    Console.WriteLine("¿Qué grupo de test quieres usar?");
    string[] dirs = GetAvailableTestsInOrder();
    ShowArrayOfOptions(dirs);
    return AskUserToSelectAnOption(dirs);
}

string[] GetAvailableTestsInOrder()
{
    string[] dirs = Directory.GetDirectories("data", "*-Tests", SearchOption.TopDirectoryOnly);
    Array.Sort(dirs);
    return dirs;
}

void ShowArrayOfOptions(string[] options)
{
    for(int i = 0; i < options.Length; i++)
        Console.WriteLine($"{i}- {options[i]}");
}

string AskUserToSelectAnOption(string[] options)
{
    int minValue = 0;
    int maxValue = options.Length - 1;
    int selectedOption = AskUserToSelectNumber(minValue, maxValue);
    return options[selectedOption];
}

int AskUserToSelectNumber(int minValue, int maxValue)
{
    Console.WriteLine($"(Ingresa un número entre {minValue} y {maxValue})");
    int value;
    bool wasParsePossible;
    do
    {
        string? userInput = Console.ReadLine();
        wasParsePossible = int.TryParse(userInput, out value);
    } while (!wasParsePossible || IsValueOutsideTheValidRange(minValue, value, maxValue));

    return value;
}

bool IsValueOutsideTheValidRange(int minValue, int value, int maxValue)
    => value < minValue || value > maxValue;

string SelectTest(string testFolder)
{
    Console.WriteLine("¿Qué test quieres ejecutar?");
    string[] tests = Directory.GetFiles(testFolder, "*.txt" );
    Array.Sort(tests);
    return AskUserToSelectAnOption(tests);
}

void AnnounceTestCase(string test)
{
    Console.WriteLine($"----------------------------------------");
    Console.WriteLine($"Replicando test: {test}");
    Console.WriteLine($"----------------------------------------\n");
}