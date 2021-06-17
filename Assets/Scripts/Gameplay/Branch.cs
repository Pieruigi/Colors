using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Zom.Pie
{
    public class Branch : MonoBehaviour
    {
        [SerializeField]
        GameObject connection;

        int colorId = -1;
        public int ColorId
        {
            get { return colorId; }
            set { colorId = value; SetMaterial(colorId); }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void SetMaterial(int colorId)
        {
            // Get connection renderer
            connection.GetComponent<MeshRenderer>().material = GetComponentInParent<Pillar>().GetMaterial(colorId);
        }
    }

}
