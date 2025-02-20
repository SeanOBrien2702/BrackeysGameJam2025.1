using UnityEngine;

public class Spring {

    private static readonly float SMALL_FLOAT = 0.00001f;

    private readonly SpringConfig config;
    private float delay;

    private float velocity = 0;

    private float to;
    private float position;

    private bool isAnimating = false;
    private float animateStartTime = 0;

    public bool Tick(float deltaTime) {
        if (!isAnimating) {
            return false;
        }

        if (Time.time < animateStartTime + delay) {
            return false;
        }

        float restVelocity = SMALL_FLOAT / 10;

        var numSteps = Mathf.CeilToInt(deltaTime * 1000);
        for (int i = 0; i < numSteps; i++) {
            if (Mathf.Abs(velocity) <= restVelocity && Mathf.Abs(to - position) <= SMALL_FLOAT) {
                // Finished.
                isAnimating = false;
                return false;
            }

            float springForce = -config.tension * 0.000001f * (position - to);
            float dampingForce = -config.friction * 0.001f * velocity;
            float acceleration = (springForce + dampingForce) / config.mass;

            velocity += acceleration;
            position += velocity;
        }

        return true;
    }

    public Spring(SpringConfig config, float from, float to) {
        this.config = config;
        position = from;
        this.to = to;
    }

    public void To(float value, float delay = 0f) {
        to = value;
        isAnimating = true;
        animateStartTime = Time.time;
        this.delay = delay;
    }

    public void Instant(float value) {
        position = value;
        isAnimating = false;
    }

    public float Get() {
        return position;
    }

}