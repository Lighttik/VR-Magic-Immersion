using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private int currentNumberOfParticles = 0;

    public List<AudioClip> sounds;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = this.GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {

        var amount = Mathf.Abs(currentNumberOfParticles - particleSystem.particleCount);
        if (particleSystem.particleCount < currentNumberOfParticles)
        {
            StartCoroutine(PlaySound(amount));
        }

        currentNumberOfParticles = particleSystem.particleCount;

    }

    private AudioClip RandomSound()
    {
        return sounds[Random.Range(0, sounds.Count)];
    }

    private IEnumerator PlaySound(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject soundGO = new GameObject();
            soundGO.name = "FireworkSound";
            soundGO.transform.position = transform.position;

            AudioSource source = soundGO.AddComponent<AudioSource>();
            source.clip = RandomSound();
            source.pitch = Random.Range(0.9f, 1.1f);

            yield return new WaitForSeconds(0.001f);

            source.Play();

            Destroy(soundGO, 2f);
        }
    }
}