//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.Windows.Speech;

//public class DictationScript : MonoBehaviour
//{
//    [SerializeField]
//    private Text m_Hypotheses;

//    [SerializeField]
//    private Text m_Recognitions;

//    private DictationRecognizer m_DictationRecognizer;

//    public string Text;
//    public string ResultText;

//    private VoiceCMDS voicecmds;

//    public string ProgramName;

//    public int SelectedProgram;

//    void Start()
//    {
//        voicecmds = GetComponent<VoiceCMDS>();

//        m_DictationRecognizer = new DictationRecognizer();

//        m_DictationRecognizer.DictationResult += (Text, confidence) =>
//        {
//            Debug.LogFormat("Dictation result: {0}", Text);
//            ResultText = Text;
//            m_Recognitions.text += Text + "\n";
//        };

//        m_DictationRecognizer.DictationHypothesis += (Text) =>
//        {
//            Debug.LogFormat("Dictation hypothesis: {0}", Text);
//            m_Hypotheses.text += Text;
//        };

//        m_DictationRecognizer.DictationComplete += (completionCause) =>
//        {
//            if (completionCause != DictationCompletionCause.Complete)
//                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
//        };

//        m_DictationRecognizer.DictationError += (error, hresult) =>
//        {
//            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
//        };

//        m_DictationRecognizer.Start();
//    }
//}