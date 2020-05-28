using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Renamerator : EditorWindow
{
    Label numSelectedLbl;
    TextField searchTxt;
    TextField replaceTxt;

    [MenuItem("Custom Tools/Renamerator %#t")]
    public static void OpenWindow()
    {
        GetWindow<Renamerator>();
    }

    void OnEnable()
    {
        var template = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Renamerator.uxml");
        var ui = template.CloneTree();
        rootVisualElement.Add(ui);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/Renamerator.uss");
        ui.styleSheets.Add(styleSheet);

        var renameBtn = ui.Q<Button>("renameBtn");
        renameBtn.clicked += RenameSelected;

        numSelectedLbl = ui.Q<Label>("numSelectedLbl");
        UpdateNumSelectedLabel();
        Selection.selectionChanged += UpdateNumSelectedLabel;

        searchTxt = ui.Q<TextField>("searchTxt");
        replaceTxt = ui.Q<TextField>("replaceTxt");
    }

    void RenameSelected()
    {
        Debug.Log($"Renaming {Selection.gameObjects.Length} GameObjects: " +
                  $"{searchTxt.value} -> {replaceTxt.value}");

        foreach (var gameObj in Selection.gameObjects)
        {
            gameObj.name = gameObj.name.Replace(searchTxt.value, replaceTxt.value);
        }
    }

    void UpdateNumSelectedLabel()
    {
        var numSelected = Selection.gameObjects.Length;
        numSelectedLbl.text = $"GameObjects Selected: {numSelected}";
    }
}
