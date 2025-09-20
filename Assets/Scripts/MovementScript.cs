using UnityEngine;

public class WordBubble : MonoBehaviour
{
    private RectTransform rect;
    private Vector2 currentDir;
    private Vector2 targetDir;

    public float speed = 30f;
    public float changeDirTime = 2f;
    private float timer;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        PickNewDirection();
    }

    void Update()
    {
        // Smoothly blend toward the target direction
        currentDir = Vector2.Lerp(currentDir, targetDir, Time.deltaTime * 2f);

        // Move bubble
        rect.anchoredPosition += currentDir * speed * Time.deltaTime;

        // Count down
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            PickNewDirection();
        }

        KeepInsideParent();
    }

    void PickNewDirection()
    {
        targetDir = Random.insideUnitCircle.normalized;
        timer = Random.Range(changeDirTime * 0.5f, changeDirTime * 1.5f); // staggered timing
    }

    void KeepInsideParent()
    {
        RectTransform parent = rect.parent as RectTransform;

        Vector3 pos = rect.anchoredPosition;
        float halfW = parent.rect.width / 2;
        float halfH = parent.rect.height / 2;

        // bounce if hitting edges
        if (pos.x < -halfW || pos.x > halfW)
        {
            currentDir.x *= -1;
            targetDir.x = currentDir.x;
        }

        if (pos.y < -halfH || pos.y > halfH)
        {
            currentDir.y *= -1;
            targetDir.y = currentDir.y;
        }

        // clamp inside
        pos.x = Mathf.Clamp(pos.x, -halfW, halfW);
        pos.y = Mathf.Clamp(pos.y, -halfH, halfH);

        rect.anchoredPosition = pos;
    }
}
