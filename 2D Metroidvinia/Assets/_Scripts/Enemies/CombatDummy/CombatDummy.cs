using Solymi.Interfaces;
using UnityEngine;

namespace Solymi.Enemies.CombatDummy
{
    public class CombatDummy : MonoBehaviour, IDamageable
    {
        [SerializeField] private GameObject hitParticles;
        [SerializeField] private GameObject player;
    
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Damage(float damage)
        {
            Debug.Log("CombatDummy took " + damage + " damage");
        
            Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            _animator.SetTrigger("damage");
            if (player.transform.position.x - transform.position.x > 0)
            {
                _animator.SetBool("playerOnLeft", false);
            }
            else
            {
                _animator.SetBool("playerOnLeft", true);
            }
        }
    }
}
