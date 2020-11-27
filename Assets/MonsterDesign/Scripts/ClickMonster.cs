using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ClickMonster : MonoBehaviour
{
    private Text leveltxt;
    private Text nametxt;
    private Image hpImage;
    private State state;
    private CanvasGroup group; 
    private bool isClick = false;
    private void Awake()
    {
        state = GetComponent<State>();
    }
    private void Start()
    {
        group = transform.Find("/Canvas/MonsterHP").GetComponent<CanvasGroup>();
        hpImage = transform.Find("/Canvas/MonsterHP/HPGround/HP").GetComponent <Image>();
        leveltxt = transform.Find("/Canvas/MonsterHP/Level/Value").GetComponent<Text>();
        nametxt = transform.Find("/Canvas/MonsterHP/Name/Value").GetComponent<Text>();
    }

    private RaycastHit hit;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.transform.name);
                if (hit.transform == transform)
                {
                    isClick = true;
                    group.alpha = 1;
                }
                else if (hit.transform.CompareTag("Plane"))
                {
                    isClick = false;
                    group.alpha = 0;
                }
            }
        }
        if (isClick)
            ValueChange();
    }

    private void ValueChange()
    {
        leveltxt.text = state.level.ToString();
        nametxt.text = state.name;
        hpImage.fillAmount = (float)state.hP / (float)state.monsterData.MonsterHP;
    }

}
