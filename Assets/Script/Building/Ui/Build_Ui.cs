using UnityEngine;
using UnityEngine.UI;

public class Build_Ui : MonoBehaviour
{
   [SerializeField] private GameObject panelUi;
   [SerializeField] private Slider TimerSlider;

   public void OpenUI()
   {
      panelUi.SetActive(true);
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
