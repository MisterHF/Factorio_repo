using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BuildUi : MonoBehaviour
{
   [SerializeField] private GameObject PanelUi;
   [SerializeField] private Slider TimerSlider;
   [SerializeField] private FurnaceController FurnaceController;
   private GetPanel canva;
   public GameObject PanelUi1
   {
      get { return PanelUi; }
   }

   [SerializeField] private string PanelName;


   private void OnEnable()
   {
      canva = GetPanel.Instance;
      PanelUi = canva.GetPanelByName(PanelName);
      FurnaceController.SetupFurnace();
      TimerSlider = PanelUi.GetComponent<FurnacePanelInfo>().Slider1;
      TimerSlider.maxValue = GetComponent<FurnaceController>().EndTimer1;
   }

   public void OpenUI()
   {
      PanelUi.SetActive(!PanelUi.activeSelf);
   }

   public void UpdateValueSlider(float value)
   {
      TimerSlider.value = value;
   }
}
