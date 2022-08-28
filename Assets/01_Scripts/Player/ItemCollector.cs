using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    private int _resourceLayer;
    private Player _player;
    levelManager _levelManager;

    private void Awake()
    {
        _resourceLayer = LayerMask.NameToLayer("Resource");
        _player = GetComponent<Player>();
        _levelManager = GameObject.Find("Manager").GetComponent<levelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == _resourceLayer)
        {
            Resource resource = collision.gameObject.GetComponent<Resource>();

            if (resource != null)
            {
                switch (resource.ResourceData.resourceType)
                {
                    case ResourceType.LevelPoint:
                        int value1 = resource.ResourceData.GetAmount();
                        _levelManager.totalExp += value1;
                        PopupText(value1, resource);
                        resource.PickUpResource();
                        break;
                    case ResourceType.Health:
                        float value = resource.ResourceData.GetAmount();
                        _player.Health += value;
                        //??????? ????????
                        PopupText(value, resource);
                        Debug.Log(1);
                        resource.PickUpResource();
                        break;
                }
            }
        }
    }

    private void PopupText(float value, Resource res)
    {
        PopUpText text = PoolManager.Instance.Pop("PopupText") as PopUpText;
        text?.SetUp(value, res.transform.position + new Vector3(0, 0.5f, 0), false, res.ResourceData.popupTextColor);
    }
}
