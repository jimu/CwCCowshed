using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Leaderboard
{
    public string playerName;

    public string ranks = "";
    public string names = "";
    public string scores = "";
    public string dates = "";

    public string pranks = "";
    public string pnames = "";
    public string pscores = "";


    void AppendRecord(HighScore score)
    {
        if (score.name == playerName)
        {
            pranks += score.rank + "\n";
            pnames += score.name + "\n";
            pscores += score.score + "\n";
        }
        else
        {
            pranks += "\n";
            pnames += "\n";
            pscores += "\n";
        }

        ranks += score.rank + "\n";
        names += score.name + "\n";
        scores += score.score + "\n";
        dates += score.date + "\n";
    }


    void AppendSeparator()
    {
        ranks += "\n";
        names += "...\n";
        scores += "\n";
        dates += "\n";
    }


    public Leaderboard(HighScore[] highscores, string playerName)
    {
        this.playerName = playerName;

        Debug.Log("BuildLeaderboard.Build. Scores length=" + scores.Length);

        int numRows = highscores.Length;
        int playerRow = 0;

        for (; playerRow < numRows; ++playerRow)
            if (highscores[playerRow].name == playerName)
                break;

        const int MAXROWS = 8;

        // if total fewer or equal to 8, print all
        // if player not found or in top 8, print all 8.
        // otherwise print top 4 and last four lines are:  --- p-1 p p+1
        // #9, so print 1-2-3-4-*-8-9-10
        int toprows = numRows <= MAXROWS ? numRows :             
            playerRow >= numRows || playerRow < MAXROWS ? MAXROWS :
            MAXROWS - 4;

        Debug.Log("Leaderboard: toprows=" + toprows);
        for (int i = 0; i < toprows; ++i)
            AppendRecord(highscores[i]);

        if (playerRow >= toprows && playerRow < numRows)
        {
            AppendSeparator();
            for (int i = playerRow - 1; i < playerRow + 2; ++i)
                AppendRecord(highscores[i]);
        }

    }

}
