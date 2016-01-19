using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputFieldToText : MonoBehaviour {

    InputField input;
    InputField.SubmitEvent se;
    public Text output;
	public Text m_placeholder;

    void Start()
    {
        input = gameObject.GetComponent<InputField>();
        se = new InputField.SubmitEvent();
        se.AddListener(SubmitInput);
        input.onEndEdit = se;
		m_placeholder.text = Network.player.ipAddress.ToString(); //default is your local ip
		output.text = Network.player.ipAddress.ToString(); //default is your local ip
    }

    private void SubmitInput(string arg0)
    {
        //string currentText = output.text; //maybe add ToString()?
        string l_inputText = arg0;
		
		output.text = l_inputText;
        input.text = "";
        input.ActivateInputField();
    }


}
