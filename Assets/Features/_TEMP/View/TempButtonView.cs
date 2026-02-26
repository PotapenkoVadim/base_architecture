using UnityEngine;

public class TempButtonView: MonoBehaviour
{
  public void OnClick()
  {
    Services.Get<PersistenceManager>().SaveAll();
  }
}