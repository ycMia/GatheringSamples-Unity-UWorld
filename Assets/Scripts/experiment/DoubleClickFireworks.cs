using UnityEngine;
using Unity;

using MyScripts.Interactable;
using MyScripts.Logics.Time;

namespace MyScripts.Experiment
{
    public class DoubleClickFireworks : MonoBehaviour , ICursorDoubleClickable
    {
        public GameObject molePrefab;
        public int fireworkCount = 25;

        public float f1;
        public float f2;
        public float f3;

        public void OnDoubleClick()
        {
            DoFirework();
        }

        private void DoFirework()
        {
            GameObject molecules_parent = new GameObject("Molecules Parent");
            molecules_parent.AddComponent<SelfBoomTruck>();
            molecules_parent.GetComponent<SelfBoomTruck>().ActivateCountdown(1.8f);
            molecules_parent.transform.position = gameObject.transform.position;
            for (int i=0;i<fireworkCount;i++)
            {
                GameObject molecule = Instantiate(molePrefab, new Vector2(molecules_parent.transform.position.x+Random.Range(-0.5f, 0.5f), molecules_parent.transform.position.y+Random.Range(-0.5f, 0.5f)), molecules_parent.transform.rotation,molecules_parent.transform);
                molecule.AddComponent<SelfBoomTruck>();
                molecule.GetComponent<SelfBoomTruck>().ActivateCountdown(Random.Range(1.5f, 1.75f));
                molecule.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.7f, 1f), Random.Range(0.7f, 1f), Random.Range(0.7f, 1f));
                molecule.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(f1, f2), Random.Range(-0.2f, f3));
            }
        }
    }
}