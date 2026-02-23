using UnityEngine;

public class SubtractHealthButtonView: MonoBehaviour
{
  public void OnClick()
  {
    Services.Get<EventBus>().Raise(new SubtractHealthPointEvent());
  }
}