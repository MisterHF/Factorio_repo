using UnityEngine;

public class Build_Ui : MonoBehaviour
{
   [SerializeField] private GameObject panelUI;

   public void OpenUI()
   {
      panelUI.SetActive(true);
   }
}
