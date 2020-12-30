/*
* ArmorItemHook -
* Created by : Allan N. Murillo
* Last Edited : 8/19/2020
*/

using UnityEngine;

namespace ANM.TPC.Items
{
    public class ArmorItemHook : MonoBehaviour
    {
        public ArmorItemType armorItemType;
        public SkinnedMeshRenderer meshRenderer;
        public Mesh defaultMesh;
        public Material defaultMaterial;

        public void Initialize()
        {
            var armorManager = GetComponentInParent<ArmorManager>();
            armorManager.RegisterArmorHook(this);
            if (meshRenderer != null) return;
            meshRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        internal void AttachArmor(ArmorItem armorItem)
        {
            AssignMeshAndMaterial(armorItem.mesh, armorItem.armorMaterial);
        }

        internal void DetachArmor()
        {
            if (armorItemType.isDisabledWhenNoItem)
            {
                meshRenderer.enabled = false;
            }
            else
            {
                meshRenderer.sharedMesh = defaultMesh;
                meshRenderer.material = defaultMaterial;
            }
        }

        private void AssignMeshAndMaterial(Mesh mesh, Material material)
        {
            meshRenderer.enabled = true;
            meshRenderer.sharedMesh = mesh;
            meshRenderer.material = material;
        }
    }
}
