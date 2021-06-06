using UnityEngine;
using System.Collections.Generic;
using System;

public enum DialogType
{
    SETTING,
    WIN,
    LOSE,
    SHOP,
    INVENTORY,
    SELLING,
    COOKING,
    INTRO
};

public enum DialogShow
{
    DONT_SHOW_IF_OTHERS_SHOWING,
    REPLACE_CURRENT,
    STACK,
    SHOW_PREVIOUS,
    OVER_CURRENT
};

public class DialogController : MonoBehaviour
{
    public static DialogController Instance;

    [HideInInspector]
    public Dialog current;
    [HideInInspector]
    public Dialog[] baseDialogs;

    public Action onDialogsOpened;
    public Action onDialogsClosed;
    public Stack<Dialog> dialogs = new Stack<Dialog>();

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

    }



    public void ShowDialog(int type)
    {
        ShowDialog((DialogType)type, DialogShow.DONT_SHOW_IF_OTHERS_SHOWING);
    }

    public Dialog ShowDialog(DialogType type, DialogShow option = DialogShow.REPLACE_CURRENT)
    {
        //if (ConfigController.instance.IsShowingTutorialLevelOne == true) return;

        Dialog dialog = GetDialog(type);
        ShowDialog(dialog, option);
        return dialog;
        //if (Sound.instance != null)
        //{
        //    Sound.instance.Play(Sound.instance.ui_slider_show);
        //}
        //if (TextPreview.instance)
        //{
        //    TextPreview.instance.SetText("");
        //}
    }
    public void ShowDialog(Dialog dialog, DialogShow option = DialogShow.REPLACE_CURRENT)
    {
        if (current != null)
        {
            if (option == DialogShow.DONT_SHOW_IF_OTHERS_SHOWING)
            {
                Destroy(dialog.gameObject);
                return;
            }
            else if (option == DialogShow.REPLACE_CURRENT)
            {
                current.Close();
            }
            else if (option == DialogShow.STACK)
            {
                current.Hide();
            }
        }

        current = dialog;
        if (option != DialogShow.SHOW_PREVIOUS)
        {
            current.onDialogOpened += OnOneDialogOpened;
            current.onDialogClosed += OnOneDialogClosed;
            dialogs.Push(current);
        }

        if (current == null)
        {
            Debug.Log("ERROR ShowDialog");
        }
        else
        {
            current.Show();
        }

        if (onDialogsOpened != null)
            onDialogsOpened();
    }

    public Dialog GetDialog(DialogType type)
    {
        Dialog dialog = baseDialogs[(int)type];
        dialog.dialogType = type;
        return (Dialog)Instantiate(dialog, transform.position, transform.rotation);
    }

    public void CloseCurrentDialog()
    {
        if (current != null)
            current.Close();
    }

    public void CloseDialog(DialogType type)
    {
        if (current == null) return;
        if (current.dialogType == type)
        {
            current.Close();
        }
    }

    public bool IsDialogShowing()
    {
        return current != null;
    }

    public bool IsDialogShowing(DialogType type)
    {
        if (current == null) return false;
        return current.dialogType == type;
    }

    private void OnOneDialogOpened(Dialog dialog)
    {
        //Debug.Log ("OnOneDialogOpened");
    }

    private void OnOneDialogClosed(Dialog dialog)
    {
        if (current == dialog)
        {
            current = null;
            dialogs.Pop();
            if ((onDialogsClosed != null && dialogs.Count == 0))
            {
                onDialogsClosed();
            }

            //if (dialogs.Count > 0)
            //{
            //    ShowDialog(dialogs.Peek(), DialogShow.SHOW_PREVIOUS);
            //}
        }
    }

    public void CleanUpDialog()
    {
        if (dialogs.Count > 0)
        {
            dialogs.Clear();
        }
    }
}

