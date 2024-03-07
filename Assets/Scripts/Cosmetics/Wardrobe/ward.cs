using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class ward : MonoBehaviour
{
    public GameObject HeadCosmeticPoint;
    public GameObject BodyCosmeticPoint;
    public GameObject HandCosmeticPoint;
    private GameObject cosmetic;
    private GameObject Prefab;
    private GameObject cosmeticParent;
    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/Cosmetics");
		FileInfo[] info = dir.GetFiles("*.prefab");
		info.Select(f => f.FullName).ToArray();
		foreach (FileInfo f in info) 
		{ 
            //Debug.Log((f.Name).Replace(@"\","/").Replace(@".prefab",""));
			Prefab = Resources.Load<GameObject>("Cosmetics/"+(f.Name).Replace(@"\","/").Replace(@".prefab",""));
            
            cosmeticParent = new GameObject("");
            
            
            cosmetic = (GameObject)GameObject.Instantiate(Prefab, cosmeticParent.transform.position, Quaternion.identity);
            cosmetic.SetActive(true);
            cosmeticParent.SetActive(false);

            cosmetic.transform.name = cosmetic.transform.name.Replace(@"(Clone)","");

            cosmetic.transform.parent = cosmeticParent.transform;

            if (cosmetic.gameObject.tag == "Head"){
                cosmeticParent.transform.parent = HeadCosmeticPoint.transform;
            }else if (cosmetic.gameObject.tag == "Hand"){
                cosmeticParent.transform.parent = HandCosmeticPoint.transform;
            }else if (cosmetic.gameObject.tag == "Body"){
                cosmeticParent.transform.parent = BodyCosmeticPoint.transform;
            }

            cosmeticParent.transform.position = cosmeticParent.transform.parent.position;
            cosmeticParent.transform.name = (cosmeticParent.transform.parent.childCount-1).ToString();

            if (cosmeticParent.transform.name == "0"){
                cosmeticParent.SetActive(true);
            }


		}
    }

}
