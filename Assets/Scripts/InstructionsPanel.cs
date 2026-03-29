using UnityEngine;

public class InstructionsPanel : MonoBehaviour
{
    public GameObject instructionsPanel;
    
    void Start()
    {
        // 确保开始时说明面板是隐藏的
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(false);
        }
    }
    
    // 显示说明面板
    public void ShowInstructions()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(true);
        }
    }
    
    // 隐藏说明面板
    public void HideInstructions()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(false);
        }
    }
}
