﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpInfoScript : MonoBehaviour {

    public GameObject HelpPanel;

    private GameManagerScript Game;

    void Start()
    {
        Game = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
    }

    public void UpdateHelpInfo()
    {
        if (Game == null) Game = GameObject.Find("GameManager").GetComponent<GameManagerScript>();

        HelpPanel.transform.Find("PhaseText").GetComponent<Text>().text = Game.PhaseManager.CurrentPhase.Name;
        HelpPanel.transform.Find("SubPhaseText").GetComponent<Text>().text = Game.PhaseManager.CurrentSubPhase.Name;
        HelpPanel.transform.Find("PlayerNoText").GetComponent<Text>().text = "PLAYER: " + PlayerToInt(Game.PhaseManager.CurrentSubPhase.RequiredPlayer);
        HelpPanel.transform.Find("PilotSkillText").GetComponent<Text>().text = (Game.PhaseManager.CurrentPhase.GetType() == typeof(PlanningPhase)) ? "" : "PILOTS WITH SKILL: " + Game.PhaseManager.CurrentSubPhase.RequiredPilotSkill.ToString();
    }

    public void UpdateTemporaryState(string temporaryStateName)
    {
        HelpPanel.transform.Find("SubPhaseText").GetComponent<Text>().text = temporaryStateName;
    }

    protected int PlayerToInt(Player playerNo)
    {
        int result = -1;
        if (playerNo == Player.Player1) result = 1;
        if (playerNo == Player.Player2) result = 2;
        return result;
    }

}
