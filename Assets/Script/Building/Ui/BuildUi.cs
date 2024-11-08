using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BuildUi : MonoBehaviour
{
   private GameObject PanelUi;
   private GetPanel canva;
   [SerializeField] private string PanelName;


   private void OnEnable()
   {
      canva = GetPanel.Instance;
      PanelUi = canva.GetPanelByName(PanelName);
   }

   public void OpenUI()
   {
      PanelUi.SetActive(!PanelUi.activeSelf);
   }
}
