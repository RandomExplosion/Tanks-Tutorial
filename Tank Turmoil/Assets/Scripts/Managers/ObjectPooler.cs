using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pooling
{
    public class ObjectPooler : MonoBehaviour
    {
        [System.Serializable]
        public class Pool
        {
            public string tag;
            public GameObject prefab;
            public Transform parent;
            public int size;
        }

        #region Singleton
        //Object pooler in the 
        public static ObjectPooler Instance;

        private void Awake()
        {
            Instance = this;
        } 
        #endregion

        //List of pools to be created
        public List<Pool> pools;
        //Key value pairs for all the different queues in the scene
        public Dictionary<string, Queue<GameObject>> poolDictionary;

        private void Start()
        {
            //Instantiate the pooldictionary
            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            //Loop through and create each pool adding them to the poolDictionary
            foreach (Pool pool in pools)
            {
                //The queue for this pool
                Queue<GameObject> objectPool = new Queue<GameObject>();

                //Create all the objects
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);  //Instantiate the object
                    obj.SetActive(false);                       //Disable the object
                    if (pool.parent != null)                    //If the parent object has been specified
                    {
                        obj.transform.parent = pool.parent;     //Set the parent of the object to the pool parent 
                    }
                    try
                    {
                        obj.GetComponent<IPooledObject>().m_poolName = pool.tag;
                    }
                    catch (System.Exception e)
                    {

                        Debug.LogError(e);
                    }
                    objectPool.Enqueue(obj);                    //Add the object to the pool
                }

                poolDictionary.Add(pool.tag, objectPool);       //Add the pool to the dictionary
            }
        }

        //Method to spawn an object from a pool
        public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
        {

            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Pool with tag " + tag + " doesn't exist");//Log it to the console
                return null;
            }
            else
            {
                GameObject objectToSpawn = poolDictionary[tag].Dequeue();   //Get the first object from the queue
                objectToSpawn.SetActive(true);                              //Enable it
                objectToSpawn.transform.position = position;                //Set the position of the new object
                objectToSpawn.transform.rotation = rotation;                //Set the rotation of the new object

                poolDictionary[tag].Enqueue(objectToSpawn);                 //Add the object back to the pool to be reused
                return objectToSpawn;                                       //Return the instantiated object
            }
            
        }

        //Method to forcibly return an object to the queue
        public void ReturnToPool(string tag, GameObject obj)
        {
            obj.SetActive(false);                       //Disable the object
            poolDictionary[tag].Enqueue(obj);           //Return it to the queue
        }

        public IEnumerator ReturnToPoolDelay(string tag, GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);     //Delay
            obj.SetActive(false);                       //Disable the object
            poolDictionary[tag].Enqueue(obj);           //Return it to the queue
        }
    }
}