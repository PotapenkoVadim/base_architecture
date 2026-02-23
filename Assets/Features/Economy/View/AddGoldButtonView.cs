using UnityEngine;

public class AddGoldButtonView: MonoBehaviour
{
  public void OnClick()
  {
    Services.Get<EventBus>().Raise(new AddGoldPointEvent());
  }
}