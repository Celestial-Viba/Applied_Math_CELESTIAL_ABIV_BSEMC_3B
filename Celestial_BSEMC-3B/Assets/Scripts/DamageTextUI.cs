using UnityEngine;
using TMPro;
using DG.Tweening;
public class DamageTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageTxt;
    [SerializeField] private GameObject critIcon;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color critColor;
    [SerializeField] private Vector3 startPos;

 

    public void Initialize()
    {
        damageTxt = GetComponent<TextMeshProUGUI>();
        damageTxt.text = string.Empty;
        transform.localScale = Vector3.one;
    }

public void ShowDamage(float damage, bool isCrit, Transform location)
{
    var pos = Camera.main.WorldToScreenPoint(location.position + offset);
    Sequence sequence = DOTween.Sequence();

    if (isCrit)
    {
        critIcon.SetActive(true);
        damageTxt.color = critColor;
        damageTxt.text = Mathf.RoundToInt(damage).ToString() + "!";


        damageTxt.transform.localScale = Vector3.one * 1.5f;
        damageTxt.transform.position = pos;


        sequence.Append(damageTxt.DOFade(1f, 0.1f));
        sequence.Append(damageTxt.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack));
        sequence.Append(damageTxt.transform.DOMove(pos + new Vector3(0, -50f, 0), 0.6f).SetEase(Ease.InSine));
        sequence.Join(damageTxt.DOFade(0f, 0.6f));

    }

    else
    {
        critIcon.SetActive(false);
        damageTxt.color = normalColor;
        damageTxt.text = Mathf.RoundToInt(damage).ToString();

        damageTxt.transform.localScale = Vector3.one * 0.5f;
        damageTxt.transform.position = pos;


        sequence.Append(damageTxt.transform.DOScale(Vector3.one * 1.2f, 0.2f).SetEase(Ease.OutBack));
        sequence.Join(damageTxt.DOFade(1f, 0.2f));
        sequence.Append(damageTxt.transform.DOMove(pos + new Vector3(Random.Range(-30, 30), 120f, 0), 1.0f).SetEase(Ease.OutSine));
        sequence.Append(damageTxt.transform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InBack));
    }

    sequence.OnComplete(() => { Destroy(gameObject); });
}


}
