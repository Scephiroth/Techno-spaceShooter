using System;
using UnityEngine;

namespace Mika
{
    [RequireComponent(typeof(AudioSource))]
    public class Enemy : EnemyBaseClass
    {
        [SerializeField] protected float maxLifeTime = 20f;
        protected float lifeTime = 0f;
        [Range(1, 100)]
        [SerializeField] protected int maxLife = 1;
        [Range(1, 100)]
        [SerializeField] protected int points = 1;
        public bool IsAlive { get => this.gameObject.activeInHierarchy && m_lives > 0 && this.lifeTime < this.maxLifeTime; }
        protected AudioSource audioSource;

        protected virtual void Awake()
        {
            ResetLife();
            ResetLifetime();
            this.audioSource = GetComponent<AudioSource>();
        }

        internal override void TakeDamage(int m_damagePoints)
        {
            base.TakeDamage(m_damagePoints);
            if (m_lives <= 0)
            {
                EventManager.InvokeEnemyDeathEvent(this);
                gameObject.SetActive(false);
            }
        }

        protected virtual void Update()
        {
            if (m_lives <= 0)
            {
                return;
            }
            // d�sactiv� apr�s 5s
            lifeTime += Time.deltaTime;
            if (lifeTime > maxLifeTime)
            {
                gameObject.SetActive(false);
            }
        }

        public void ResetLifetime()
        {
            lifeTime = 0f;
        }

        public void ResetLife()
        {
            m_lives = Math.Max(1, this.maxLife);
        }

        public virtual int GetPointValue()
        {
            return points;
        }
    }
}