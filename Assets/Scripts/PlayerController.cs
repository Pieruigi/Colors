using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zom.Pie
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        bool disabled = false;
        public bool Disabled
        {
            get { return disabled; }
            set { disabled = value; }
        }
        bool busy = false;

        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
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
            if (disabled || busy)
                return;

                    

            // Check mouse input
            if (Input.GetMouseButtonDown(0))
            {
                // Cast a ray 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    // Get the hit pillar 
                    Pillar pillar = hit.transform.GetComponent<Pillar>();

                    // Rotate
                    busy = true;
                    float angle = pillar.transform.localEulerAngles.y + 90f;
                    pillar.transform.DORotate(Vector3.up * angle, 0.25f).SetEase(Ease.OutBounce).OnComplete(() => { busy = false; LevelManager.Instance.CheckCompleted(); });
                }
            }
            
            
        }
    }

}
