using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Build_Ui : MonoBehaviour
{
   [SerializeField] private GameObject PanelUI;
   [SerializeField] private Slider TimerSlider;

   public void OpenUI()
   {
      PanelUI.SetActive(true);
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
