using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IDSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject _idPrefab;
    private GameObject _currentId;
    [SerializeField]
    private Vector2Int idResolution = new Vector2Int(25, 25);
    [SerializeField]
    private DateTime baseDate = new DateTime(1990, 01, 01);
    
    private List<string> names;

    public void Start()
    {
        TextAsset namesFile = (TextAsset)Resources.Load("names", typeof(TextAsset));
        names = new List<string>(namesFile.text.Split('\n'));
    }
    public string GenerateName()
    {
        string name = $"{names[Random.Range(0, names.Count)]}\n{names[Random.Range(0, names.Count)]}";
        return name;
    }

    public DateTime GenerateBirthday()
    {
        var randomDays = Random.Range(0, 6000);
        var date = baseDate.AddDays(randomDays);
        return date;
    }

    public void SpawnId(Camera camera)
    {
        var texture = new RenderTexture(idResolution.x, idResolution.y, 16);
        camera.targetTexture = texture;
        if( _currentId != null)
        {
            Destroy(_currentId);
        }

        _currentId = Instantiate(_idPrefab, this.transform);
        var idObject = _currentId.GetComponent<CharacterID>();
        idObject.Initialize( GenerateName(), GenerateBirthday(), texture );
        
    }
}
