using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestGarage
{
    public class Shelf : MonoBehaviour
    {
        [SerializeField] private List<ItemView> _listItemsView;
        public List<ItemView> ListItemsView => _listItemsView;

        public void Awake()
        {
            _listItemsView = transform.GetComponentsInChildren<ItemView>().ToList();
        }
    }
}