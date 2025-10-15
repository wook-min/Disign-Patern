using UnityEngine;

public class Bat : Creature
{
    [SerializeField] Vector3 direction = Vector3.up;
    

    private void Update()
    {
        Behaviour();
    }

    public override void Behaviour()
    {
        transform.Rotate(direction, speed * Time.deltaTime);
    }
}
