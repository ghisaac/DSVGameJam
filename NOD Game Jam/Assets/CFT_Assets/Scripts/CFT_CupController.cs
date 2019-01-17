using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFT_CupController : MonoBehaviour
{
    public int playerID;
    public GameObject CupPrefab;
    public float CupHeightMargin;
    public float HorizontalCupPositionDelta;
    public LayerMask CupLayer;
    public float cameraLerpMultiplier = 1;
    [HideInInspector]public GameObject Cup;
    int timermutliplier = 1;
    float cupsPlaced = 5f;
    float lerpValue;
    bool letGo = false; //avgör om koppen ska falla eller inte
    float RangeCheckHeight;
    float topHeight;
    public Camera camera;
    private Vector3 cameraOffset;
    private float cameraLerpValue;
    private List<GameObject> cupList = new List<GameObject>();
    public Color playerColor;


    // Start is called before the first frame update
    public void Init(int ID)
    {
        playerID = ID;
        playerColor = Player.GetPlayerByRewindID(playerID).PlayerColor;
        SubcribeToClickEvent();
    }

    private void SubcribeToClickEvent()
    {
        switch (playerID)
        {
            case 0:
                CFT_EventManager.OnClicked_Player1 += Drop;
                break;
            case 1:
                CFT_EventManager.OnClicked_Player2 += Drop;
                break;
            case 2:
                CFT_EventManager.OnClicked_Player3 += Drop;
                break;
            case 3:
                CFT_EventManager.OnClicked_Player4 += Drop;
                break;
        }
    }
    private void OnDisable()
    {
        switch (playerID)
        {
            case 0:
                CFT_EventManager.OnClicked_Player1 -= Drop;
                break;
            case 1:
                CFT_EventManager.OnClicked_Player2 -= Drop;
                break;
            case 2:
                CFT_EventManager.OnClicked_Player3 -= Drop;
                break;
            case 3:
                CFT_EventManager.OnClicked_Player4 -= Drop;
                break;
        }
    }
    void Start()
    {
        
        InstantiateCup();
        cameraOffset = camera.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!letGo)
        {
            lerpValue += Time.deltaTime * timermutliplier;
            if (lerpValue <= 0f)
                timermutliplier = 1;
            if (lerpValue >= 1f)
                timermutliplier = -1;
            Cup.transform.position = transform.position + new Vector3(lerpValue * HorizontalCupPositionDelta - HorizontalCupPositionDelta / 2, CupHeightMargin, 0);
        }
        
        transform.localPosition = new Vector3(0, BoxCastHeight(), 0);
       
        camera.transform.position = Vector3.Lerp(camera.transform.position, transform.position + cameraOffset, cameraLerpValue);

        cameraLerpValue += Time.deltaTime * cameraLerpMultiplier;


    }

    public void Drop()
    {
        if (!letGo)
        {
            cupList.Add(Cup);
            letGo = true;
            Cup.GetComponent<Rigidbody>().isKinematic = false;
            Cup.layer = 0;
            cameraLerpValue = 0f;
        }
    }

    public void InstantiateCup()
    {
        letGo = false;
        if (Cup != null)
            Cup.layer = 0;
        Cup = Instantiate(CupPrefab, transform.position, Quaternion.identity);
        SetColor();
        topHeight = BoxCastHeight();

    }

    private void SetColor()
    {
        MeshRenderer meshRenderer = Cup.GetComponentInChildren<MeshRenderer>();
        Material material = meshRenderer.materials[2];
        meshRenderer.materials[2] = Instantiate(meshRenderer.materials[2]);
        meshRenderer.materials[2].color = playerColor;
        Cup.GetComponent<CFT_CupLogic>()._controller = this;
    }

    public float BoxCastHeight()
    {
        RaycastHit hit;
        Physics.BoxCast(new Vector3(transform.position.x, 50, transform.position.z), new Vector3(5, 1, 5), Vector3.down, out hit, Quaternion.Euler(0, 0, 0), 1000f, CupLayer);
        
        return hit.point.y;
    }

    public void RemoveCups()
    {
        foreach (GameObject c in cupList)
        {
            if (c == null) continue;
            if (!c.GetComponent<CFT_CupLogic>().collided)
                InstantiateCup();
            Destroy(c);
        }
        cupList.Clear();
            
    }

}


