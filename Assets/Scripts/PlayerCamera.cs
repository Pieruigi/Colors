using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zom.Pie
{
    public class PlayerCamera : MonoBehaviour
    {
        //float dispY = 0;
        //float minY = 22f;
        //float maxY = 80;

        float minZ = -.8f;
        float maxZ = -3.2f;
        float dispZ = 0;

        Collider n, e, w;
        Plane[] planes;
    
        bool adjust  = true;
        float adjustDisp = 10f;
        float adjustSpeed = 50f;

        private void Awake()
        {
            int diff = LevelConfig.RowStepDisp * LevelConfig.RowStepCount;
            //dispY = (maxY - minY) / (float)diff;
            dispZ = (maxZ - minZ) / diff;


        }

        // Start is called before the first frame update
        void Start()
        {
            int rows = LevelManager.Instance.NumOfRows;
            float d = (rows - LevelConfig.MinNumOfRows);

            Vector3 pos = transform.position;
            //pos.y = minY + d * dispY;
            pos.z = minZ + d * dispZ;
            pos.y = 1;
            transform.position = pos;

            n = LevelManager.Instance.NorthBoundary;
            e = LevelManager.Instance.EastBoundary;
            w = LevelManager.Instance.WestBoundary;

            
        }

        // Update is called once per frame
        void Update()
        {
            if (!adjust)
                return;

            planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            bool v = GeometryUtility.TestPlanesAABB(planes, n.bounds) &&
                     GeometryUtility.TestPlanesAABB(planes, e.bounds) &&
                     GeometryUtility.TestPlanesAABB(planes, w.bounds);

            if (!v)
            {
                transform.position += Vector3.up * adjustSpeed * Time.deltaTime;
            }
            else
            {
                if(adjustDisp > 0)
                {
                    float disp = adjustSpeed * Time.deltaTime;
                    adjustDisp -= disp;
                    transform.position += Vector3.up * disp;

                    if (adjustDisp < 0)
                        adjust = false;
                }
            }

            
        }
    }

}
