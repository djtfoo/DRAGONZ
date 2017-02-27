using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeButtonColor : MonoBehaviour {

    public Color[] colorArr;

    public void ChangeNormalColorToAnotherColor(int idx)
    {
        ColorBlock colorblock = this.GetComponent<Button>().colors;
        colorblock.normalColor = colorArr[idx];
        this.GetComponent<Button>().colors = colorblock;
    }

    //public void ChangeHighlightedColorToAnotherColor(int idx)
    //{
    //    ColorBlock colorblock = this.GetComponent<Button>().colors;
    //    colorblock.highlightedColor = colorArr[idx];
    //    this.GetComponent<Button>().colors = colorblock;
    //}

}
