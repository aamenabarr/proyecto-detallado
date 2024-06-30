using Fire_Emblem_View;
using Fire_Emblem;

namespace Fire_Emblem_Controller;

public class Game
{
    private string _teamsFolder;
    private TeamsBuilder _teamsBuilder = new();
    private Printer _printer;
    private Battle _battle;
    private Team _team1;
    private Team _team2;
    
    public Game(View view, string teamsFolder)
    {
        _printer = new Printer(view);
        _teamsFolder = teamsFolder;
    }

    public void Play()
    {
        var teamFile = ChooseTeamFile();
        BuildTeams(teamFile);
        VerifyTeams();
    }

    private string[] ChooseTeamFile()
    {
        _printer.PrintChoiceTeamMessage();
        
        var files = Utils.GetFiles(_teamsFolder);
        PrintTeamOptions(files);

        var input = _printer.Read();
        return Utils.ReadFile(files[input]);
    }

    private void PrintTeamOptions(string[] files)
    {
        for (var i = 0; i < files.Length; i++)
            _printer.PrintFileOption(i, files);
    }
    
    private void BuildTeams(string[] teamFile)
    {
        Teams teams = _teamsBuilder.PopulateTeams(teamFile);
        _team1 = teams.Team1;
        _team2 = teams.Team2;
    }

    private void VerifyTeams()
    {
        if (AreValidTeams())
            StartBattle();
        else
            _printer.PrintInvalidTeamMessage();
    }

    private bool AreValidTeams()
    {
        return _team1.IsValid() && _team2.IsValid();
    }

    private void StartBattle()
    {
        _battle =  new Battle(new Player(1, _team1), new Player(2, _team2), _printer);
        _battle.Start();
    }
}