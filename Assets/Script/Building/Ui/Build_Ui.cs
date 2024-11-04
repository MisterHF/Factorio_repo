using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Build_Ui : MonoBehaviour
{
   [SerializeField] private GameObject PanelUi;
   [SerializeField] private Slider TimerSlider;
   public GameObject PanelUi1 => PanelUi;

   public void OpenUI()
   {
      PanelUi.SetActive(!PanelUi.activeSelf);
   }

   private void Start()
   {
      TimerSlider.maxValue = GetComponent<FurnaceController>().EndTimer1;
   }


   public void UpdateValueSlider(float value)
   {
      TimerSlider.value = value;
   }
}
