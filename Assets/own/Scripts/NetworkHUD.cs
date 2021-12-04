using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Mirror.Discovery
{
    public class NetworkHUD : MonoBehaviour
    {
        readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();


        public NetworkDiscovery networkDiscovery;
        public GameObject FindServerBtn;
        public GameObject StartServerBtn;
        public GameObject StartHostBtn;
        public GameObject StopHostBtn;
        public GameObject StopServerBtn;
        public GameObject StopClientBtn;
        public GameObject servers;
        public Button[] buttons;
        void Start()
        {
            if (!NetworkClient.isConnected && !NetworkServer.active && !NetworkClient.active && NetworkManager.singleton != null)
            {
                FindServerBtn.SetActive(true);
                StartHostBtn.SetActive(true);
                StartServerBtn.SetActive(true);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!NetworkClient.isConnected && !NetworkServer.active && !NetworkClient.active)
            {
                FindServerBtn.SetActive(true);
                StartHostBtn.SetActive(true);
                StartServerBtn.SetActive(true);
                StopServerBtn.SetActive(false);
                StopClientBtn.SetActive(false);
                StopHostBtn.SetActive(false);
            }
        }
        void Connect(ServerResponse info)
        {
            StopAllCoroutines();
            FindServerBtn.SetActive(false);
            StartHostBtn.SetActive(false);
            StartServerBtn.SetActive(false);
            StopServerBtn.SetActive(false);
            StopClientBtn.SetActive(true);
            StopHostBtn.SetActive(false);
            servers.SetActive(false);
            networkDiscovery.StopDiscovery();
            NetworkManager.singleton.StartClient(info.uri);
        }

        public void OnDiscoveredServer(ServerResponse info)
        {
            // Note that you can check the versioning to decide if you can connect to the server or not using this method
            discoveredServers[info.serverId] = info;
        }
        public void OnFindServerBtn()
        {
            discoveredServers.Clear();
            networkDiscovery.StartDiscovery();
            StartCoroutine(addServers());
        }
        IEnumerator addServers()
        {
            while (true)
            {
                Debug.Log(1);
                servers.SetActive(true);
                int i = 0;
                foreach (ServerResponse info in discoveredServers.Values)
                {
                    buttons[i].GetComponentInChildren<Text>().text = info.EndPoint.Address.ToString();
                    buttons[i].onClick.AddListener(delegate ()
                    {
                        Connect(info);
                    });
                    i++;
                }
                yield return null;
            }
        }
        public void OnStartHostBtn()
        {
            FindServerBtn.SetActive(false);
            StartHostBtn.SetActive(false);
            StartServerBtn.SetActive(false);
            StopServerBtn.SetActive(false);
            StopClientBtn.SetActive(false);
            discoveredServers.Clear();
            NetworkManager.singleton.StartHost();
            networkDiscovery.AdvertiseServer();
            if (NetworkServer.active||NetworkClient.active)
            {
                if (NetworkServer.active&&NetworkClient.isConnected)
                {
                    StopHostBtn.SetActive(true);
                }
            }
        }
        public void OnStartServerBtn()
        {
            FindServerBtn.SetActive(false);
            StartHostBtn.SetActive(false);
            StartServerBtn.SetActive(false);
            StopClientBtn.SetActive(false);
            StopHostBtn.SetActive(false);
            discoveredServers.Clear();
            NetworkManager.singleton.StartServer();
            networkDiscovery.AdvertiseServer();
            if (NetworkServer.active || NetworkClient.active)
            {
                if (NetworkServer.active && !NetworkClient.isConnected)
                {
                    StopServerBtn.SetActive(true);
                }
            }
        }
        public void OnStopServerBtn()
        {
            FindServerBtn.SetActive(true);
            StartHostBtn.SetActive(true);
            StopHostBtn.SetActive(false);
            StartServerBtn.SetActive(true);
            StopServerBtn.SetActive(false);
            StopClientBtn.SetActive(false);

            NetworkManager.singleton.StopServer();
            networkDiscovery.StopDiscovery();
        }
        public void OnStopHostBtn()
        {
            FindServerBtn.SetActive(true);
            StartHostBtn.SetActive(true);
            StopHostBtn.SetActive(false);
            StartServerBtn.SetActive(true);
            StopServerBtn.SetActive(false);
            StopClientBtn.SetActive(false);

            NetworkManager.singleton.StopHost();
            networkDiscovery.StopDiscovery();
        }
        public void OnStopClientBtn()
        {
            FindServerBtn.SetActive(true);
            StartHostBtn.SetActive(true);
            StopHostBtn.SetActive(false);
            StartServerBtn.SetActive(true);
            StopServerBtn.SetActive(false);
            StopClientBtn.SetActive(false);

            NetworkManager.singleton.StopClient();
            networkDiscovery.StopDiscovery();
        }
    }
}
