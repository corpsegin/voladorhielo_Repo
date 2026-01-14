using UnityEngine;

public class PlayerSFXTriggers : MonoBehaviour
{
    public void AnimationSFX(int SFXToPLay)
    {
        AudioManager.Instance.PlaySFX(SFXToPLay);
    }
}
