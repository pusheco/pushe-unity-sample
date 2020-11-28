using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    [SerializeField]
    private GameObject restartButtonField;

    [Header("Knife count display")] [SerializeField]
    private GameObject panelKnives;

    [SerializeField] private GameObject iconKnife;

    [SerializeField] private Color usedKnifeIconColor;

    public void ShowRestartButton()
    {
        restartButtonField.SetActive(true);
    }

    public void SetInitialDisplayedKnifeCount(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(iconKnife, panelKnives.transform);
        }
    }

    private int knifeIconIndexToChange = 0;

    public void DecrementDisplayedKnifeCount()
    {
        panelKnives.transform.GetChild(knifeIconIndexToChange++)
            .GetComponent<Image>().color = usedKnifeIconColor;
    }

}