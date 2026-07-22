using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _startingTiles;
    [SerializeField] private GameObject[] _tiles;
    //[SerializeField] private int _length = 4;
    private InputAction _inputAction;
    private Transform _fromTile;
    private Transform _toTile;

    private void Start()
    {
        // initial connection
        _fromTile = CreateStartingTile();
        _toTile = CreateTile();
        ConnectTiles();

        for(int i=0; i<9; i++)
        {
            _fromTile = _toTile;
            _toTile = CreateTile();
            ConnectTiles();
        }
    }

    private void ConnectTiles()
    {
        // get two connects from _from and _to tiles
        Transform connectFrom = GetRandomConnector(_fromTile);
        Transform connectTo = GetRandomConnector(_toTile);

        if (!connectFrom || !connectTo) return;

        // connect to by setting from as parent
        connectTo.SetParent(connectFrom.transform);
        _toTile.SetParent(connectTo);

        // rotate 180d and reset local position to "connect"
        connectTo.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        connectTo.Rotate(0, 180f, 0);

        // reset 
        _toTile.SetParent(this.transform);
        connectTo.SetParent(_toTile.Find("Connectors"));

    }

    private Transform GetRandomConnector(Transform tile)
    {
        if (!tile) return null;

        var connections = tile.GetComponentsInChildren<Connector>()
            .ToList()
            .FindAll(c => !c.IsConnected);

        if(connections.Count > 0)
        {
            // randomly choose one of the open connections
            int index = Random.Range(0, connections.Count);
            connections[index].IsConnected = true;
            return connections[index].transform;

        }
        return null;
    }

    private Transform CreateTile()
    {
        // spawn randowm tile
        int index = Random.Range(0, _tiles.Length);
        GameObject tile = Instantiate(_tiles[index], Vector3.zero, Quaternion.identity);
        tile.name = _tiles[index].name;
        return tile.transform;

    }
    private Transform CreateStartingTile()
    {
        // spawn randowm starting tile
        int index = Random.Range(0, _startingTiles.Length);
        GameObject startingTile = Instantiate(_startingTiles[index], Vector3.zero, Quaternion.identity);
        startingTile.name = "Starting Room";

        // randomize its rotation
        var yRot = Random.Range(0, 3) * 90f;
        startingTile.transform.Rotate(0f, yRot, 0f);

        return startingTile.transform;
    }

    private void Update()
    {
        if (_inputAction.WasPressedThisFrame())
        {
            SceneManager.LoadScene("Game");
        }
    }
    private void Awake()
    {
        _inputAction = InputSystem.actions.FindAction("Interact");
    }
}
