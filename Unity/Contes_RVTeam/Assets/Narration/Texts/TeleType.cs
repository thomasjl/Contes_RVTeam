using UnityEngine;
using System.Collections;


namespace TMPro.Examples {

    public class TeleType : MonoBehaviour {

        new AudioSource audio;

        TMP_Text m_textMeshPro;

        void Awake()
        {
            // Get Reference to TextMeshPro Component
            m_textMeshPro = gameObject.GetComponent<TMP_Text>();
            m_textMeshPro.enableWordWrapping = true;
            m_textMeshPro.alignment = TextAlignmentOptions.Top;

            audio = GetComponent<AudioSource>();
        }

        void OnEnable()
        {
            StartCoroutine(Animating());
        }

        IEnumerator Animating()
        {
            // Force and update of the mesh to get valid information.
            m_textMeshPro.ForceMeshUpdate();

            int totalVisibleCharacters = m_textMeshPro.textInfo.characterCount; // Get # of Visible Character in text object
            int counter = 0;

            yield return null;
            audio.Play();

            while (counter <= totalVisibleCharacters)
            {
                int visibleCount = counter % (totalVisibleCharacters + 1);

                m_textMeshPro.maxVisibleCharacters = visibleCount; // How many characters should TextMeshPro display?

                // Once the last character has been revealed, wait 1.0 second and start over.
                if (visibleCount >= totalVisibleCharacters)
                {
                    yield return new WaitForSeconds(1.0f);
                }

                counter += 1;

                yield return new WaitForSeconds(0.06f);
            }

            //Debug.Log("Done revealing the text.");
        }

    }
}