
using UnityEngine;
using TMPro;

public class SortingBox : MonoBehaviour
{
    public string acceptedToyType;

    public TMP_Text scoreText;
    public TMP_Text SortedText;
    public TMP_Text feedbackText;

    private static int score = 0;
    private static int SortedToy = 0;

    public int totalToys = 15;

    private float feedbackTimer = 0f;
    private bool isShowingFeedback = false;
    private static bool hasWon = false;

    private void OnTriggerEnter(Collider other)
    {
        Toy toy = other.GetComponent<Toy>();

        if (toy == null || toy.IsSorted)
            return;

        if (toy.toyType == acceptedToyType)
        {
            toy.MarkAsSorted();

            score++;
            SortedToy++;

            if (scoreText != null)
                scoreText.text = "Score: " + score;

            if (SortedText != null)
                SortedText.text = "Sorted: " + SortedToy + "/" + totalToys;
            if (SortedToy >= totalToys && !hasWon)
            {
                hasWon = true;
                GameManager.Instance.ShowVictory();
            }

            ShowFeedback(true);
        }
        else
        {
            ShowFeedback(false);

            Rigidbody rb = toy.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 direction =
                    (toy.transform.position - transform.position).normalized;

                rb.AddForce(
                    direction * 10f + Vector3.up * 10f,
                    ForceMode.Impulse
                );
            }
        }
    }

    void ShowFeedback(bool correct)
    {
        if (feedbackText == null)
            return;

        feedbackText.text = correct ? "CORRECT!" : "TRY AGAIN!";
        feedbackText.color = correct ? Color.green : Color.red;

        feedbackText.gameObject.SetActive(true);

        feedbackTimer = 0.8f;
        isShowingFeedback = true;
    }

    void Update()
    {
        if (isShowingFeedback)
        {
            feedbackTimer -= Time.deltaTime;

            if (feedbackTimer <= 0f)
            {
                feedbackText.gameObject.SetActive(false);
                isShowingFeedback = false;
            }
        }
    }
}
