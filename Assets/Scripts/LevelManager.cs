using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Zom.Pie
{
    public class LevelManager : MonoBehaviour
    {
        public UnityAction OnGameCompleted;

        public static LevelManager Instance { get; private set; }

        [SerializeField]
        GameObject pillarPrefab;


        Pillar[] pillars;

        float pillarDistance = 3;

        int numOfColumns;
        public int NumOfColumns
        {
            get { return numOfColumns; }
        }
        int numOfRows;
        public int NumOfRows
        {
            get { return numOfRows; }
        }
        int numOfColors;

        LevelConfig levelConfig;

        // Top, right and left
        [SerializeField]
        Collider[] boundaries = new Collider[3];
        public Collider NorthBoundary
        {
            get { return boundaries[0]; }
        }
        public Collider EastBoundary
        {
            get { return boundaries[1]; }
        }
        public Collider WestBoundary
        {
            get { return boundaries[2]; }
        }

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
                levelConfig = new LevelConfig(LevelParams.Instance);
                CreateLevel();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CheckCompleted()
        {
            // Check every pillar
            for(int i=0; i<pillars.Length; i++)
            {
                // We are going to check the others
                if (pillars[i] == null)
                    continue;

                // Just check right and south branches
                int row, col;
                MathUtility.ArrayIndexToMatrixCoords(i, numOfColumns, out row, out col);
                for(int j=0; j<4; j++)
                {
                    if(pillars[i].HasBranch(j))
                    {
                        // Get the current connection collider
                        Branch currentBranch = pillars[i].GetBranch(j);
                        SphereCollider coll = currentBranch.GetComponentInChildren<SphereCollider>();
                        // Overlap sphere
                        Vector3 pos = coll.transform.TransformPoint(coll.center);
                        LayerMask mask = LayerMask.GetMask(new string[] { "Connection" });
                        coll.enabled = false; // Disable current collider
                        Collider[] others = Physics.OverlapSphere(pos, coll.radius, mask);
                        coll.enabled = true;

                        foreach (Collider other in others)
                            Debug.Log("Other:" + other.transform.parent.name);

                        // We overlap for sure the current connection branch
                        if (others.Length == 0)
                            return;

                        // Get the other branch and
                        Branch otherBranch = others[0].GetComponentInParent<Branch>();
                        // Check colors
                        if (currentBranch.ColorId != otherBranch.ColorId)
                            return;
                    }
                   
                        
                }
            }

            Debug.Log("Game Has Completed");
            // Disable player 
            PlayerController.Instance.Disabled = true;

            // Move connectors
            Connect();

            // Play audio
            GetComponent<AudioSource>().Play();

            OnGameCompleted?.Invoke();
            
        }

        void Connect()
        {
            for(int i=0; i<pillars.Length; i++)
            {
                if (pillars[i] == null)
                    continue;

                for(int j=0; j<4; j++)
                {
                    if (pillars[i].HasBranch(j))
                        pillars[i].GetBranch(j).Connect();
                }
            }
        }

        void CreateLevel()
        {
            // Set columns and rows
            numOfColumns = levelConfig.NumOfColumns;
            numOfRows = levelConfig.NumOfRows;
            numOfColors = levelConfig.NumOfColors;
            pillars = new Pillar[numOfRows * numOfColumns];

            // The horizontal starting position
            float startX = 0;
            if(numOfColumns % 2 == 0)
            {
                startX = - pillarDistance / 2f - ((float)numOfColumns / 2f - 1) * pillarDistance; 
            }
            else
            {
                startX = -((float)numOfColumns - 1 ) / 2f * pillarDistance;
            }

            // Vertical starting position
            float startZ = 0;
            if (numOfRows % 2 == 0)
            {
                startZ = pillarDistance / 2f + ((float)numOfRows / 2f - 1) * pillarDistance;
            }
            else
            {
                startZ = ((float)numOfRows - 1) / 2f * pillarDistance;
            }
            float z = startZ;

            //
            // Creates pillars depending on the rows and columns; we don't take into account
            // the load factor yet.
            for (int i=0; i < numOfRows; i++)
            {
                float x = startX;
                for (int j=0; j< numOfColumns; j++)
                {
                    // Create the game object
                    Pillar pillar = GameObject.Instantiate(pillarPrefab).GetComponent<Pillar>();
                    pillar.transform.localPosition = new Vector3(x, 0, z);
                    x += pillarDistance;
                    pillars[MathUtility.MatrixCoordsToArrayIndex(i, j, numOfColumns)] = pillar;

                    // We must check if the pillar is not on the edge before we can set
                    // a specific branch.
                    // We start by checking the north edge
                    if (i > 0)
                    {
                        // We can add the north branch but we need to check the pillar to the 
                        // north first ( in the specific we must check the south branch ).
                        // In fact we move from north to south while creating pillars, so we 
                        // expect we already have some pillar to the north when we must set
                        // color on the north branch of a specific pillar.
                        // Get the index of the pillar to the north
                        int northIndex = MathUtility.MatrixCoordsToArrayIndex(i - 1, j, numOfColumns);
                        // Set the north color of the current branch as the south color
                        // of the north branch
                        pillar.AddBranch(0);
                        pillar.GetBranch(0).ColorId = pillars[northIndex].GetBranch(2).ColorId;
                    }
                    // Checking the west edge
                    if (j > 0)
                    {
                        // It works like the north branch; in this case we must check the east
                        // branch of the pillar to the west.
                        int westIndex = MathUtility.MatrixCoordsToArrayIndex(i, j - 1, numOfColumns);
                        // Set the west color
                        pillar.AddBranch(3);
                        pillar.GetBranch(3).ColorId = pillars[westIndex].GetBranch(1).ColorId;
                        //pillar.colors[3] = pillars[westIndex].colors[1];
                    }

                    // Checking south
                    if (i < numOfRows - 1)
                    {
                        // South color ( random )
                        pillar.AddBranch(2);
                        pillar.GetBranch(2).ColorId = Random.Range(0, numOfColors);
                        //pillar.colors[2] = Random.Range(0, numOfColors) + 1;
                    }
                    // Checking east
                    if (j < numOfColumns - 1)
                    {
                        // East color ( random )
                        pillar.AddBranch(1);
                        pillar.GetBranch(1).ColorId = Random.Range(0, numOfColors);
                        //pillar.colors[1] = Random.Range(0, numOfColors) + 1;
                    }
                }
                z -= pillarDistance;
            }

            //
            // Remove some pillars depending on the load factor
            int count = pillars.Length - (int)(pillars.Length * levelConfig.LoadFactor);

            // If we need simmetry we must cut the number of pillars to remove
            if (levelConfig.HorizontalSymmetry)
                count /= 2;
            if (levelConfig.VerticalSymmetry)
                count /= 2;

            for (int i=0; i<count; i++)
            {
                // Availables
                List<Pillar> tmp = new List<Pillar>(pillars).FindAll(p => p.gameObject.activeSelf);
                // Get random pillar to remove
                Pillar toRemove = tmp[Random.Range(0, tmp.Count)];
                RemovePillar(toRemove);

                // If symmetry is on we must remove the corresponding pillar
                if (levelConfig.HorizontalSymmetry || levelConfig.VerticalSymmetry)
                {
                    int toRemoveIndex = new List<Pillar>(pillars).IndexOf(toRemove);
                    int toRemoveRow, toRemoveColumn;
                    MathUtility.ArrayIndexToMatrixCoords(toRemoveIndex, numOfColumns, out toRemoveRow, out toRemoveColumn);

                    // Horizontal symmetry
                    int otherIndexForVertical = -1;
                    if (levelConfig.HorizontalSymmetry)
                    {
                        if (numOfColumns % 2 == 0 || toRemoveColumn != (numOfColumns - 1) / 2)
                        {
                            int otherColumn = numOfColumns - 1 - toRemoveColumn;
                            int otherRow = toRemoveRow;
                            int otherIndex = MathUtility.MatrixCoordsToArrayIndex(otherRow, otherColumn, numOfColumns);
                           
                            otherIndexForVertical = otherIndex;
                            RemovePillar(pillars[otherIndex]);
                        }
                    }

                    // Vertical symmetry
                    if (levelConfig.VerticalSymmetry)
                    {
                        if (numOfRows % 2 == 0 || toRemoveRow != (numOfRows - 1) / 2)
                        {
                            int otherRow = numOfRows - 1 - toRemoveRow;
                            int otherColumn = toRemoveColumn;
                            int otherIndex = MathUtility.MatrixCoordsToArrayIndex(otherRow, otherColumn, numOfColumns);
                            
                            RemovePillar(pillars[otherIndex]);

                            // The other in the horizontal step
                            if(otherIndexForVertical >= 0)
                            {
                                int toRemoveRow2, toRemoveColumn2;
                                MathUtility.ArrayIndexToMatrixCoords(otherIndexForVertical, numOfColumns, out toRemoveRow2, out toRemoveColumn2);

                                int otherRow2 = numOfRows - 1 - toRemoveRow2;
                                int otherColumn2 = toRemoveColumn2;
                                int otherIndex2 = MathUtility.MatrixCoordsToArrayIndex(otherRow2, otherColumn2, numOfColumns);
                                
                                RemovePillar(pillars[otherIndex2]);
                            }
                        }
                    }


                }
            }
            
            //
            // Clear and shuffle; we remove deactivated pillars and randomly rotate the others
            for(int i=0; i<pillars.Length; i++)
            {
                if (!pillars[i].gameObject.activeSelf)
                {
                    DestroyImmediate(pillars[i].gameObject);
                    pillars[i] = null;
                }
                else
                {
                    float angle = Random.Range(0, 4) * 90f;
                    pillars[i].transform.Rotate(Vector3.up, angle);
                }
                    
            }

            SetBoundaries();
        }

        void SetBoundaries()
        {
            int left = -1;
            int top = -1;
            int right = -1;
            // We checke the boundaries we found until now; for example if we found the 
            // boundary to the left has col = 0 we don't need to check anymore for that boundary.
            for(int i=0; i<pillars.Length; i++)
            {
                int col, row;

                MathUtility.ArrayIndexToMatrixCoords(i, numOfColumns, out row, out col);

                Debug.Log("Check pillar " + i);
                // Check left
                if(left != 0)
                {
                    if (left < 0 || col < left)
                    {
                        if (pillars[i])
                        {
                            left = col;
                            boundaries[2] = pillars[i].Boundary;
                        }                        
                        
                    }
                        
                }
                // Check top
                if (top != 0)
                {
                    if (top < 0 || row < top)
                    {
                        if (pillars[i])
                        {
                            top = row;
                            boundaries[0] = pillars[i].Boundary;
                        }
                            
                    }
                        
                }
                // Check right
                if (right != numOfColumns-1)
                {
                    if (right < 0 || col > right)
                    {
                        if (pillars[i])
                        {
                            right = col;
                            boundaries[1] = pillars[i].Boundary;
                        }
                            
                    }
                        
                }
            }
        }

        void RemovePillar(Pillar toRemove)
        {
            toRemove.gameObject.SetActive(false);

            // Get index
            int index = new List<Pillar>(pillars).IndexOf(toRemove);

            // Get rows and cols
            int row, col;
            MathUtility.ArrayIndexToMatrixCoords(index, numOfColumns, out row, out col);

            // Remove branches of the neighbour pillars
            // Pillar to the left
            if (col - 1 >= 0)
            {
                pillars[MathUtility.MatrixCoordsToArrayIndex(row, col - 1, numOfColumns)].RemoveBranch(1);
            }
            // To the right
            if (col + 1 < numOfColumns)
            {
                pillars[MathUtility.MatrixCoordsToArrayIndex(row, col + 1, numOfColumns)].RemoveBranch(3);
            }
            // North
            if (row - 1 >= 0)
            {
                pillars[MathUtility.MatrixCoordsToArrayIndex(row - 1, col, numOfColumns)].RemoveBranch(2);
            }
            // South
            if (row + 1 < numOfRows)
            {
                pillars[MathUtility.MatrixCoordsToArrayIndex(row + 1, col, numOfColumns)].RemoveBranch(0);
            }
        }
    }

}
