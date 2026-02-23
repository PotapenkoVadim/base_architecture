using UnityEngine;

public class SubtractGoldButtonView: MonoBehaviour
{
  public void OnClick()
  {
    Services.Get<EventBus>().Raise(new SubtractGoldPointEvent());
  }
}