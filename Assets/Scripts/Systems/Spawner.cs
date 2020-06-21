using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public static void reSpawnMutant(GameObject Mutant, GameObject Parent)
    {
        int hp = Mutant.GetComponent<Mutant_AI>().startHealth+1;

        GameObject player = GameObject.Find("Player");
        Config currentLevel = (Config) Resources.Load("Level" + Application.loadedLevel);
        float mutant_x = Random.Range(player.transform.position.x - currentLevel.spawnRadius, player.transform.position.x + currentLevel.spawnRadius);
        float mutant_z = Random.Range(player.transform.position.z - currentLevel.spawnRadius, player.transform.position.z + currentLevel.spawnRadius);

        
        Vector3 spawner = new Vector3(mutant_x, 100, mutant_z);
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(spawner, Vector3.down, out hit);
        if(hit.collider.transform.name == "Terrain")
        {
            GameObject newMutant = Instantiate(Parent, hit.point, Mutant.transform.rotation);
            newMutant.GetComponent<Collider>().enabled = true;
            newMutant.GetComponent<Mutant_AI>().hpBar.enabled = true;
            newMutant.GetComponent<Mutant_AI>().startHealth = hp;
        }

        
        //Destroy(Mutant);
        
    }
}
