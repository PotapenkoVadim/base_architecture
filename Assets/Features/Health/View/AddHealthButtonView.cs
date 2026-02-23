using UnityEngine;

public class AddHealthButtonView: MonoBehaviour
{
  public void OnClick()
  {
    Services.Get<EventBus>().Raise(new AddHealthPointEvent());
  }
}