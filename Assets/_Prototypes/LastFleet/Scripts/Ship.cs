﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames.LastFleet
{
    public class Ship : MonoBehaviour
    {
        public Fleet Fleet;

        public float MaxSpeed;
        public float MaxAcceleration;
        public float VelocityError = 0.1f;
        public float FuelBurnRate;


        Rigidbody2D rb;
        Vector2 desiredVelocity;
        Vector2 speedAndAccel;

        GameObject formationPoint;

        private void OnEnable()
        {
            formationPoint = new GameObject(gameObject.name + "FormationSlot");
            formationPoint.transform.SetPositionAndRotation(transform.position, gameObject.transform.rotation);
            formationPoint.transform.SetParent(Fleet.gameObject.transform);
            speedAndAccel = new Vector2(MaxSpeed, MaxAcceleration);
        }

        // Use this for initialization
        void Start()
        {
            Fleet.AddShip(speedAndAccel);
            Fleet.SupplyUse += FuelBurnRate;
            rb = gameObject.GetRequiredComponent<Rigidbody2D>();
        }

        private void OnDisable()
        {
            Destroy(formationPoint);
            Fleet.SupplyUse -= FuelBurnRate;
            Fleet.RemoveShip(speedAndAccel);
        }

        private void Update()
        {
            desiredVelocity = Fleet.DesiredVelocity + (Vector2)(formationPoint.transform.position - transform.position);
        }

        private void FixedUpdate()
        {
            Vector2 steering = Vector2.ClampMagnitude(desiredVelocity - rb.velocity, MaxAcceleration);

            if (steering.magnitude > VelocityError)
            {
                rb.AddForce(steering);
            }
        }

        private void OnMouseDown()
        {
            
        }

    }
}
