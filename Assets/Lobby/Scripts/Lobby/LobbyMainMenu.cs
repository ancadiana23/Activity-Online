using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking.Types;
using System;
using System.Collections.Generic;
using UnityEngine.Networking.Match;

namespace Prototype.NetworkLobby
{
    //Main menu, mainly only a bunch of callback called by the UI (setup throught the Inspector)
    public class LobbyMainMenu : MonoBehaviour 
    {
        public LobbyManager lobbyManager;

        public RectTransform lobbyServerList;
        public RectTransform lobbyPanel;

        public InputField matchNameInput;
		public InputField passwordInput;

		public Button createJoinButton;
		bool isCreateMode;


		public void OnEnable()
        {
            lobbyManager.topPanel.ToggleVisibility(true);

            /*ipInput.onEndEdit.RemoveAllListeners();
            ipInput.onEndEdit.AddListener(onEndEditIP);
			*/
            matchNameInput.onEndEdit.RemoveAllListeners();
            matchNameInput.onEndEdit.AddListener(onEndEditGameName);

			isCreateMode = GameObject.Find("GameManager").GetComponent<GameManager>().getCreateMode();
			if (isCreateMode)
			{
				createJoinButton.onClick.AddListener(OnClickCreateMatchmakingGame);
			}
			else
			{
				createJoinButton.onClick.AddListener(OnClickJoinMatch);
			}
		}

		public void OnClickJoinMatch()
		{
			lobbyManager.StartMatchMaker();
			lobbyManager.matchMaker.ListMatches(0, 10, matchNameInput.text, false, 0, 0, OnMatchList);
		}

		private void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
		{
			for(int i = 0; i < matches.Count; ++i)
			{
				if (matchNameInput.text.Equals(matches[i].name))
				{
					JoinMatch(matches[i].networkId, lobbyManager);
					return;
				}
			}
			Debug.Log("No match with this name found!");
		}

		void JoinMatch(NetworkID networkID, LobbyManager lobbyManager)
		{
			lobbyManager.matchMaker.JoinMatch(networkID, passwordInput.text, "", "", 0, 0, lobbyManager.OnMatchJoined);
			lobbyManager.backDelegate = lobbyManager.StopClientClbk;
			lobbyManager._isMatchmaking = true;
			lobbyManager.DisplayIsConnecting();
		}
		public void OnClickHost()
        {
            lobbyManager.StartHost();
        }

        public void OnClickJoin()
        {
            lobbyManager.ChangeTo(lobbyPanel);

            lobbyManager.StartClient();

            lobbyManager.backDelegate = lobbyManager.StopClientClbk;
            lobbyManager.DisplayIsConnecting();

            lobbyManager.SetServerInfo("Connecting...", lobbyManager.networkAddress);
        }

        public void OnClickDedicated()
        {
            lobbyManager.ChangeTo(null);
            lobbyManager.StartServer();

            lobbyManager.backDelegate = lobbyManager.StopServerClbk;

            lobbyManager.SetServerInfo("Dedicated Server", lobbyManager.networkAddress);
        }

        public void OnClickCreateMatchmakingGame()
        {
            lobbyManager.StartMatchMaker();
            lobbyManager.matchMaker.CreateMatch(
                matchNameInput.text,
                (uint)lobbyManager.maxPlayers,
                true,
				passwordInput.text,
				"", "", 0, 0,
				lobbyManager.OnMatchCreate);

            lobbyManager.backDelegate = lobbyManager.StopHost;
            lobbyManager._isMatchmaking = true;
            lobbyManager.DisplayIsConnecting();

            lobbyManager.SetServerInfo("Matchmaker Host", lobbyManager.matchHost);
        }

        public void OnClickOpenServerList()
        {
            lobbyManager.StartMatchMaker();
            lobbyManager.backDelegate = lobbyManager.SimpleBackClbk;
            lobbyManager.ChangeTo(lobbyServerList);
        }

        void onEndEditIP(string text)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnClickJoin();
            }
        }

        void onEndEditGameName(string text)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnClickCreateMatchmakingGame();
            }
        }

    }
}
