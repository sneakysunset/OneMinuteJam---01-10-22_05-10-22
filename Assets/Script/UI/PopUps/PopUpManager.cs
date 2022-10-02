using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PopUpManager : MonoBehaviour
{
    public string titleArc, titleBoucle, titleEnd, titleGlace, titlePlaque, titleTapis, titleTeleporteur;
    public string corpusArc, corpusBoucle, corpusEnd, corpusGlace, corpusPlaque, corpusTapis, corpusTeleporteur;

    public float xOffSet, yOffSet;

    GridTiles tile;
    public GameObject popUp;
    GameObject currentPopUp;
    Camera cam;

    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        PopUpLauncher();
    }

    void PopUpLauncher()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {

            if(tile != hit.transform.GetComponent<GridTiles>() && hit.transform.GetComponent<GridTiles>())
            {
                if(currentPopUp != null)
                {
                    Destroy(currentPopUp);
                }
                tile = hit.transform.GetComponent<GridTiles>();
                switch (tile.tileType)
                {
                    case GridTiles.TileVariant.Tile:
                    
                        break;
                    case GridTiles.TileVariant.Arc_Electrique:
                        createPopUp(tile, titleArc, corpusArc);
                        break;
                    case GridTiles.TileVariant.Boucle:
                        createPopUp(tile, titleBoucle, corpusBoucle);
                        break;
                    case GridTiles.TileVariant.End_Tile:
                        createPopUp(tile, titleEnd, corpusEnd);

                        break;
                    case GridTiles.TileVariant.Glace:
                        createPopUp(tile, titleGlace, corpusGlace);

                        break;
                    case GridTiles.TileVariant.Plaque_De_Pression:
                        createPopUp(tile, titlePlaque, corpusPlaque);

                        break;
                    case GridTiles.TileVariant.Tapis_Roulant:
                        createPopUp(tile, titleTapis, corpusTapis);

                        break;
                    case GridTiles.TileVariant.Teleporteur:
                        createPopUp(tile, titleTeleporteur, corpusTeleporteur);

                        break;
                }
            }
        }
        else
        {
            if (currentPopUp != null)
            {
                Destroy(currentPopUp);
            }
                tile = null;

        }
    }

    void createPopUp(GridTiles tile, string title, string corpus) 
    {
        Vector2 pos = cam.WorldToScreenPoint(tile.transform.position);
        pos.x -= (Screen.width / 2) +xOffSet;
        pos.y -= (Screen.height / 2) +yOffSet;
        RectTransform inst = Instantiate(popUp, transform).GetComponent<RectTransform>();
        currentPopUp = inst.gameObject;
        inst.anchoredPosition = pos;
        TextMeshProUGUI[] texts = inst.GetComponentsInChildren<TextMeshProUGUI>();
        texts[0].text = title;
        texts[1].text = corpus;
    }
}
