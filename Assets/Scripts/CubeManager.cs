using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CubeManager : MonoBehaviour {


    //-------------------variabls finished menu -----------
    FinishedMenu finishedGM = new FinishedMenu();
    public GameObject finishedLevel;
    public GameObject fullPanelPause;
    public Text movetxtFinish;
    public Text timeTxtFinish;

    //------------varibale start game------------------
    Renderer rend;

    int moves = 0;
    public Text movesText;
    public Text timerText;
    private float StartTime;
    bool startbtn , startplaying = false;
    public Button startbtnn;


 //-----------------------------------------------------
    public Material mat;
    public GameObject CubePref;
    Transform CubeTrans;
    List<GameObject> allRubik = new List<GameObject>();
    GameObject cubeConter;
    bool canRetat = true,
         canShuffle = true;

    List<GameObject> UpPieces
    {
        get
        {
            return allRubik.FindAll(x => Mathf.Round(x.transform.localPosition.y) == 0);
        }
    }
    List<GameObject> DownPieces
    {
        get
        {
            return allRubik.FindAll(x => Mathf.Round(x.transform.localPosition.y) == -2);
        }
    }
    List<GameObject> FrontPieces
    {
        get
        {
            return allRubik.FindAll(x => Mathf.Round(x.transform.localPosition.x) == 0);
        }
    }
    List<GameObject> BackPieces
    {
        get
        {
            return allRubik.FindAll(x => Mathf.Round(x.transform.localPosition.x) == -2);
        }
    }
    List<GameObject> LeftPieces
    {
        get
        {
            return allRubik.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 0);
        }
    }
    List<GameObject> RightPieces
    {
        get
        {
            return allRubik.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 2);
        }
    }

    List<GameObject> UpHorizontalPieces
    {
        get
        {
            return allRubik.FindAll(x => Mathf.Round(x.transform.localPosition.x) == -1);
        }
    }
    List<GameObject> UpVerticalPieces
    {
        get
        {
            return allRubik.FindAll(x => Mathf.Round(x.transform.localPosition.z) == 1);
        }
    }
    List<GameObject> FrontHorizontalPieces
    {
        get
        {
            return allRubik.FindAll(x => Mathf.Round(x.transform.localPosition.y) == -1);
        }
    }


    Vector3[] retationV =
    {
        new Vector3(0,1,0),new Vector3(0,-1,0),
        new Vector3(0,0,-1),new Vector3(0,0,1),
        new Vector3(1,0,0),new Vector3(-1,0,0),
    };

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();

        if (startbtn)
        {
            StartTime = Time.time;
        }
        CubeTrans = transform;
        RubiksCube();
    }
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = 1f;

        if (canRetat)
            checkInput();

        //----start timer----------
        if (startplaying)
        {
            float T = Time.time - StartTime;
            
            string hour = ((int)T / 3600).ToString();
            string minutes = ((int)T / 60).ToString();
            string seconds = (T % 60).ToString("f0");

            timerText.text = hour + "h:" + minutes + "min:" + seconds+"s";
        }

    }

    void RubiksCube()
    {
        foreach (GameObject cb in allRubik)
            DestroyImmediate(cb);

        allRubik.Clear();

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int z = 0; z < 3; z++)
                {
                    GameObject rubik = Instantiate(CubePref, CubeTrans, false);
                    rubik.transform.localPosition = new Vector3(-x, -y, z);
                    rubik.GetComponent<CubePrefabs>().setColor(-x, -y, z);
                    allRubik.Add(rubik);
                }
            }

        }
        cubeConter = allRubik[13];
    }
    void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            StartCoroutine(retation(UpPieces, new Vector3(0, 1, 0)));
        else if (Input.GetKeyDown(KeyCode.S))
            StartCoroutine(retation(DownPieces, new Vector3(0, -1, 0)));
        else if (Input.GetKeyDown(KeyCode.Q))
            StartCoroutine(retation(LeftPieces, new Vector3(0, 0, -1)));
        else if (Input.GetKeyDown(KeyCode.D))
            StartCoroutine(retation(RightPieces, new Vector3(0, 0, 1)));
        else if (Input.GetKeyDown(KeyCode.F))
            StartCoroutine(retation(FrontPieces, new Vector3(1, 0, 0)));
        else if (Input.GetKeyDown(KeyCode.B))
            StartCoroutine(retation(BackPieces, new Vector3(-1, 0, 0)));
        else if (Input.GetKeyDown(KeyCode.R) && canShuffle)
            StartCoroutine(Shuffle());
        else if (Input.GetKeyDown(KeyCode.E) && canShuffle)
            RubiksCube();

    }

   public void btnStartShuffle()
    {
      
        if (canShuffle)
            StartCoroutine(Shuffle());
        startbtn = true;
        startplaying = false;
        startbtnn.interactable = false;
       /* timerText.text = "0:0:0";
        moves = 0;
        movesText.text = "Moves : " + moves.ToString();
        */
    }

    public void btnReset()
    {
        if (canShuffle)
            RubiksCube();
        startbtn = false;
        startplaying = false;
        timerText.text = "0:0:0";
        moves = 0;
        movesText.text = "Moves : " + moves.ToString();
        startbtnn.interactable = true;
    }

    IEnumerator Shuffle()
    {

        canShuffle = false;
        //Random.Range(15, 30)
        for (int moveCount = NextLevel.levelRetat ; moveCount >= 0; moveCount--)
        {
            int edge = Random.Range(0, 6);
            List<GameObject> edgePieces = new List<GameObject>();
            switch (edge)
            {
                case 0: edgePieces = UpPieces; break;
                case 1: edgePieces = DownPieces; break;
                case 2: edgePieces = LeftPieces; break;
                case 3: edgePieces = RightPieces; break;
                case 4: edgePieces = FrontPieces; break;
                case 5: edgePieces = BackPieces; break;
            }
            StartCoroutine(retation(edgePieces, retationV[edge], 15));
            yield return new WaitForSeconds(.3f);
        }
        canShuffle = true;
        mat.color =Color.black;
        CubePref.GetComponent<Renderer>().material = mat;
    }

    IEnumerator retation(List<GameObject> pieace , Vector3 retatVec, int speed = 5)
    {
        //-------change color of cube
        mat.color = new Color(0, 242, 255);
        CubePref.GetComponent<Renderer>().material = mat;

        if (startbtn)
        {
            mat.color = Random.ColorHSV();
            CubePref.GetComponent<Renderer>().material = mat;
        }

        //--------calcule moves-------------
        if (startplaying)
        {
            moves += 1;
            movesText.text = "Moves : " + moves.ToString();
        }
      // -------retation --------------
        canRetat = false;
        int angle = 0;

        while (angle < 90)
        {
            foreach(GameObject rubik in pieace)
            {
                rubik.transform.RotateAround(cubeConter.transform.position, retatVec, speed);
            }
            angle += speed;
            yield return null;
        }

        if (startplaying)
        {
            CheckComplete();
        }
        canRetat = true;

        //-------return black color to cube
        mat.color = Color.black;
        CubePref.GetComponent<Renderer>().material = mat;
    }


    public void detectRetate(List<GameObject> pieces , List<GameObject> planes)
    {
        //detect if player start playing
        if (startbtn)
        {
            startplaying = true;
            StartTime = Time.timeSinceLevelLoad;
            startbtn = false;
        }

        if (!canRetat || !canShuffle)
            return;

        if (UpVerticalPieces.Exists(x => x == pieces[0]) && UpVerticalPieces.Exists(x => x == pieces[1]))
            StartCoroutine(retation(UpVerticalPieces, new Vector3(0, 0, 1 * detectLeftMiddleRightSign(pieces))));

        else if (UpHorizontalPieces.Exists(x => x == pieces[0]) && UpHorizontalPieces.Exists(x => x == pieces[1]))
            StartCoroutine(retation(UpHorizontalPieces, new Vector3(1 * detectFrontMiddleBackSign(pieces), 0, 0)));
        else if (FrontHorizontalPieces.Exists(x => x == pieces[0]) && FrontHorizontalPieces.Exists(x => x == pieces[1]))
            StartCoroutine(retation(FrontHorizontalPieces, new Vector3(0, 1 * detectUpMiddleDownSign(pieces), 0)));

        else if (detectSide(planes, new Vector3(1, 0, 0), new Vector3(0, 0, 1), UpPieces))
            StartCoroutine(retation(UpPieces, new Vector3(0, 1 * detectUpMiddleDownSign(pieces), 0)));

        else if (detectSide(planes, new Vector3(1, 0, 0), new Vector3(0, 0, 1), DownPieces))
            StartCoroutine(retation(DownPieces, new Vector3(0, 1 * detectUpMiddleDownSign(pieces), 0)));

        else if (detectSide(planes, new Vector3(0, 0, 1), new Vector3(0, 1, 0), FrontPieces))
            StartCoroutine(retation(FrontPieces, new Vector3(1 * detectFrontMiddleBackSign(pieces), 0, 0)));

        else if (detectSide(planes, new Vector3(0, 0, 1), new Vector3(0, 1, 0), BackPieces))
            StartCoroutine(retation(BackPieces, new Vector3(1 * detectFrontMiddleBackSign(pieces), 0, 0)));

        else if (detectSide(planes, new Vector3(1, 0, 0), new Vector3(0, 1, 0), LeftPieces))
            StartCoroutine(retation(LeftPieces, new Vector3(0, 0, 1 * detectLeftMiddleRightSign(pieces))));

        else if (detectSide(planes, new Vector3(1, 0, 0), new Vector3(0, 1, 0), RightPieces))
            StartCoroutine(retation(RightPieces, new Vector3(0, 0, 1 * detectLeftMiddleRightSign(pieces))));
    }

    bool detectSide(List<GameObject> planes , Vector3 fDirection, Vector3 sDirection, List<GameObject> side)
    {
        GameObject centerPiece = side.Find(x => x.GetComponent<CubePrefabs>().Planes.FindAll(y => y.activeInHierarchy).Count == 1);
        List<RaycastHit> hit1 = new List<RaycastHit>(Physics.RaycastAll(planes[1].transform.position, fDirection)),
                         hit2 = new List<RaycastHit>(Physics.RaycastAll(planes[0].transform.position, fDirection)),
                         hit1_m = new List<RaycastHit>(Physics.RaycastAll(planes[1].transform.position, -fDirection)),
                         hit2_m = new List<RaycastHit>(Physics.RaycastAll(planes[0].transform.position, -fDirection)),
                         hit3 = new List<RaycastHit>(Physics.RaycastAll(planes[1].transform.position, sDirection)),
                         hit4 = new List<RaycastHit>(Physics.RaycastAll(planes[0].transform.position, sDirection)),
                         hit3_m = new List<RaycastHit>(Physics.RaycastAll(planes[1].transform.position, -sDirection)),
                         hit4_m = new List<RaycastHit>(Physics.RaycastAll(planes[0].transform.position, -sDirection));

        return hit1.Exists(x => x.collider.gameObject == centerPiece) ||
               hit2.Exists(x => x.collider.gameObject == centerPiece) ||
               hit1_m.Exists(x => x.collider.gameObject == centerPiece) ||
               hit2_m.Exists(x => x.collider.gameObject == centerPiece) ||
               hit3.Exists(x => x.collider.gameObject == centerPiece) ||
               hit4.Exists(x => x.collider.gameObject == centerPiece) ||
               hit3_m.Exists(x => x.collider.gameObject == centerPiece) ||
               hit4_m.Exists(x => x.collider.gameObject == centerPiece);
    }

    float detectLeftMiddleRightSign(List<GameObject> pieces)
    {
        float sign = 0;

        if(Mathf.Round(pieces[1].transform.position.y) != Mathf.Round(pieces[0].transform.position.y))
        {
            if (Mathf.Round(pieces[0].transform.position.x) == -2)
                sign = Mathf.Round(pieces[0].transform.position.y) - Mathf.Round(pieces[1].transform.position.y);
            else
                sign = Mathf.Round(pieces[1].transform.position.y) - Mathf.Round(pieces[0].transform.position.y);
        }
        else
        {
            if (Mathf.Round(pieces[0].transform.position.y) == -2)
                sign = Mathf.Round(pieces[1].transform.position.x) - Mathf.Round(pieces[0].transform.position.x);
            else
                sign = Mathf.Round(pieces[0].transform.position.x) - Mathf.Round(pieces[1].transform.position.x);

        }

        return sign;
    }

    float detectFrontMiddleBackSign(List<GameObject> pieces)
    {
        float sign = 0;

        if (Mathf.Round(pieces[1].transform.position.z) != Mathf.Round(pieces[0].transform.position.z))
        {
            if (Mathf.Round(pieces[0].transform.position.y) == 0)
                sign = Mathf.Round(pieces[1].transform.position.z) - Mathf.Round(pieces[0].transform.position.z);
            else
                sign = Mathf.Round(pieces[0].transform.position.z) - Mathf.Round(pieces[1].transform.position.z);
        }
        else
        {
            if (Mathf.Round(pieces[0].transform.position.z) == 0)
                sign = Mathf.Round(pieces[1].transform.position.y) - Mathf.Round(pieces[0].transform.position.y);
            else
                sign = Mathf.Round(pieces[0].transform.position.y) - Mathf.Round(pieces[1].transform.position.y);

        }

        return sign;
    }

    float detectUpMiddleDownSign(List<GameObject> pieces)
    {
        float sign = 0;

        if (Mathf.Round(pieces[1].transform.position.z) != Mathf.Round(pieces[0].transform.position.z))
        {
            if (Mathf.Round(pieces[0].transform.position.x) == -2)
                sign = Mathf.Round(pieces[1].transform.position.z) - Mathf.Round(pieces[0].transform.position.z);
            else
                sign = Mathf.Round(pieces[0].transform.position.z) - Mathf.Round(pieces[1].transform.position.z);
        }
        else
        {
            if (Mathf.Round(pieces[0].transform.position.z) == 0)
                sign = Mathf.Round(pieces[0].transform.position.x) - Mathf.Round(pieces[1].transform.position.x);
            else
                sign = Mathf.Round(pieces[1].transform.position.x) - Mathf.Round(pieces[0].transform.position.x);

        }

        return sign;
    }



    void checkLevelCleared()
    {
        if(NextLevel.levelRetat == 2)
        {
            PlayerPrefs.SetInt("lvl2", 1);
        }
        else if (NextLevel.levelRetat == 4)
        {
            PlayerPrefs.SetInt("lvl3", 1);
        }
        else if (NextLevel.levelRetat == 6)
        {
            PlayerPrefs.SetInt("lvl4", 1);
        }
        else if (NextLevel.levelRetat == 8)
        {
            PlayerPrefs.SetInt("lvl5", 1);
        }
        else if (NextLevel.levelRetat == 10)
        {
            PlayerPrefs.SetInt("lvl6", 1);
        }

    }

    void CheckComplete()
    {
        if (IsComplete(UpPieces) && IsComplete(DownPieces) &&
           IsComplete(LeftPieces) && IsComplete(RightPieces) &&
           IsComplete(FrontPieces) && IsComplete(BackPieces))
        //  Debug.Log("Complete!");
        {
            movetxtFinish.text = "Moves : " + moves.ToString();
            timeTxtFinish.text = "Time : " + timerText.text;
            finishedGM.finished(finishedLevel, fullPanelPause);
            checkLevelCleared();
        }


    }

    bool IsComplete(List<GameObject> pieces)
    {
        int planeIndex = pieces[4].GetComponent<CubePrefabs>().Planes.FindIndex(x => x.activeInHierarchy);

        for(int i = 0; i<pieces.Count; i++)
        {
            if(!pieces[i].GetComponent<CubePrefabs>().Planes[planeIndex].activeInHierarchy ||
                pieces[i].GetComponent<CubePrefabs>().Planes[planeIndex].GetComponent<Renderer>().material.color !=
                pieces[4].GetComponent<CubePrefabs>().Planes[planeIndex].GetComponent<Renderer>().material.color)

         return false;

        }
        return true;
    }
}
