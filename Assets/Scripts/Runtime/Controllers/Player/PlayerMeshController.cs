using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerMeshController : MonoBehaviour
{

    #region SerializeField Variables

    [SerializeField] private Renderer myRenderer;
    [SerializeField] private TextMeshPro scaleText;
    [SerializeField] private ParticleSystem confetti;

    #endregion

    #region Private Variables

    private PlayerMeshData _data;
    #endregion



    internal void SetData(PlayerMeshData data)
    {
        _data = data;
    }

    internal void ScaleUpPlayer()
    {
        myRenderer.gameObject.transform.DOScaleX(_data.ScaleCounter,1).SetEase(Ease.Flash);
    }

    internal void ShowUpText()
    {
        scaleText.DOFade(1, 0).SetEase(Ease.Flash).OnComplete(() =>
        {
            scaleText.DOFade(0, 0).SetDelay(0.35f);
            scaleText.rectTransform.DOAnchorPosY(1f,.65f).SetEase(Ease.Linear);
        });
    }

    internal void PlayConfetti()
    {
        confetti.Play();

        //confetti.Emit(new ParticleSystem.EmitParams()
        //{
        //    position = transform1.position,
        //    rotation = transform1.eulerAngles,
        //    velocity = Vector3.zero,
        //});
    }

    internal void OnReset()
    {
        myRenderer.gameObject.transform.DOScale(1,1).SetEase(Ease.Linear);
    }

}
