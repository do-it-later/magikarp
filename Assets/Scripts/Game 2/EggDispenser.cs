using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EggDispenser : MonoBehaviour {

    public float columnGap;
    public int eggsToDispense;

    private int eggsRemaining;
    public int EggsRemaining { get { return eggsRemaining; } }

    private Vector3 initialPosition;

    private bool dropEgg, dropBomb, dropDoubleBomb;
    private int eggCol, bombCol, doubleBombCol;

    private List<int> eggDropList = new List<int> { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, -1, -1, -1, -1, -1 };

    void Start()
    {
        initialPosition = transform.position;
        eggsRemaining = eggsToDispense;
    }

    public void StartDispenser()
    {
        StartCoroutine("Dispense");
    }

    public void StopDispenser()
    {
        StopCoroutine("Dispense");
    }

    IEnumerator Dispense()
    {
        while (eggsRemaining > 0)
        {
            var minEggs = Mathf.Min(3, eggsToDispense);
            var maxEggs = Mathf.Min(8, eggsToDispense);
            var eggs = Random.Range(minEggs, maxEggs);

            for( var i = 0; i < eggs; )
            {
                // 90% chance that the egg will drop
                dropEgg = Random.Range(0.0f, 1.0f) < 0.9f;
                // 40% chance that a bomb will also drop OR 100% chance if an egg is not dropping
                dropBomb = Random.Range(0.0f, 1.0f) < 0.4f || !dropEgg;
                // 30% chance that a double bomb will appear IF above are already met
                dropDoubleBomb = dropEgg && dropBomb && Random.Range(0.0f, 1.0f) < 0.3f;

                if (dropEgg)
                {
                    if( eggDropList.Count < 5 )
                    {
                        eggDropList = new List<int> { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, -1, -1, -1, -1, -1 };
                        eggDropList.ShuffleList();
                    }

                    eggCol = eggDropList[0];
                    eggDropList.RemoveAt(0);
                }
                else
                    eggCol = -100; // arbitrary

                if( dropBomb )
                {
                    // Keep randomising until you get a unique col
                    bombCol = Random.Range(0, 2);
                    while (bombCol == eggCol)
                    {
                        bombCol = Random.Range(0, 2) - 1;
                    }
                }

                if( dropDoubleBomb )
                {
                    // If (0, -1), 1
                    // If (0, 1), -1
                    // If (-1, 1), 0
                    doubleBombCol = -1 * (bombCol + eggCol);
                }

                // Dispense
                if( dropEgg )
                {
                    GameObject eggObj = ObjectPool.instance.GetObject("Egg", false);
                    Vector2 position = new Vector2(initialPosition.x + columnGap * eggCol, initialPosition.y);
                    eggObj.transform.position = position;
                    ++i;
                    eggsRemaining--;
                }
                if( dropBomb )
                {
                    GameObject bombObj = ObjectPool.instance.GetObject("Bomb", false);
                    Vector2 position = new Vector2(initialPosition.x + columnGap * bombCol, initialPosition.y);
                    bombObj.transform.position = position;
                }
                if( dropDoubleBomb )
                {
                    GameObject bombObj = ObjectPool.instance.GetObject("Bomb", false);
                    Vector2 position = new Vector2(initialPosition.x + columnGap * doubleBombCol, initialPosition.y);
                    bombObj.transform.position = position;
                }

                yield return new WaitForSeconds( 0.2f );
            }

            yield return new WaitForSeconds( 1.5f );
        }
    }
}
