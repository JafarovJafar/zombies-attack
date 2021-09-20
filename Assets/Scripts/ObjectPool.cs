using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region Vars
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private string _itemName;

    [SerializeField] private List<GameObject> _items = new List<GameObject>();
    #endregion

    #region Methods
    public void Init(GameObject itemPrefab)
    {
        _items = new List<GameObject>();
        _itemPrefab = itemPrefab;
    }

    public GameObject GetItem()
    {
        GameObject tempGO = _items.Find(x => !x.activeInHierarchy);

        if (!tempGO)
        {
            tempGO = InstantiatePrefab();
        }

        tempGO.SetActive(true);
        tempGO.GetComponent<IPoolItem>().Enable();

        return tempGO;
    }

    private GameObject InstantiatePrefab()
    {
        GameObject tempGO = Instantiate(_itemPrefab);

        tempGO.SetActive(false);
        tempGO.transform.name = $"{_itemName}_{_items.Count}";
        _items.Add(tempGO);

        return tempGO;
    }
    #endregion
}