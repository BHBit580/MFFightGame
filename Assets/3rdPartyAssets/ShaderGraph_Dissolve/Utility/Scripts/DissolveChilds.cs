using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DissolveExample
{
    public class DissolveChilds : MonoBehaviour
    {
        [SerializeField] private float speed = 0.5f;
        List<Material> materials = new List<Material>();
        float value = 0;
        void Start()
        {
            var renders = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renders.Length; i++)
            {
                materials.AddRange(renders[i].materials);
            }
        }
        
        void Update()
        {
            if (value >= 1)
            {
               Destroy(gameObject.GetComponent<DissolveChilds>());
            }
            
            value += Time.deltaTime * speed;
            SetValue(value);
        }

        public bool CheckDissolveCompleted()
        {
            return value >= 1;
        }

        private void SetValue(float value)
        {
            for (int i = 0; i < materials.Count; i++)
            {
                materials[i].SetFloat("_Dissolve", value);
            }
        }
    }
}