using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA
{
    public enum Gene { MovementSpeed, MoveChangeFrequency, RotationSpeed, RotationFrequency, Width, Height};
    float[] genes;


    public DNA(int species)
    {
        genes = new float[20];

        if (species == 0) // Prey
        {
            genes[(int)Gene.MovementSpeed] = 25f;
            genes[(int)Gene.MoveChangeFrequency] = 5f;
            genes[(int)Gene.RotationSpeed] = 20f;
            genes[(int)Gene.RotationFrequency] = 3f;
            genes[(int)Gene.Width] = 3f;
            genes[(int)Gene.Height] = 3f;
        }
        else if(species == 1) // Predator
        {
            genes[(int)Gene.MovementSpeed] = 30f;
            genes[(int)Gene.MoveChangeFrequency] = 5f;
            genes[(int)Gene.RotationSpeed] = 30f;
            genes[(int)Gene.RotationFrequency] = 3f;
            genes[(int)Gene.Width] = 3f;
            genes[(int)Gene.Height] = 3f;
        }

    }

    public void CopyDNA(DNA parentDNA, DNA childDNA)
    {
        Mutation(parentDNA, childDNA, Gene.MovementSpeed, 0.2f, 80f, 4.5f);
        Mutation(parentDNA, childDNA, Gene.MoveChangeFrequency, 0.3f, 10f, 1.5f);

        Mutation(parentDNA, childDNA, Gene.RotationSpeed, 0.5f, 40f, 2f);
        Mutation(parentDNA, childDNA, Gene.RotationFrequency, 0.05f, 10f, 2f);

        Mutation(parentDNA, childDNA, Gene.Width, 0.2f, 25f, 0.5f);
        Mutation(parentDNA, childDNA, Gene.Height, 0.2f, 25f, 0.5f);
    }

    public void CopyDNA_Predator(DNA parentDNA, DNA childDNA)
    {
        Mutation(parentDNA, childDNA, Gene.MovementSpeed, 0.5f, 80f, 5.75f);
        Mutation(parentDNA, childDNA, Gene.MoveChangeFrequency, 0.3f, 10f, 1.5f);

        Mutation(parentDNA, childDNA, Gene.RotationSpeed, 0.5f, 60f, 1.75f);
        Mutation(parentDNA, childDNA, Gene.RotationFrequency, 0.01f, 10f, 2f);

        Mutation(parentDNA, childDNA, Gene.Width, 0.2f, 25f, 0.5f);
        Mutation(parentDNA, childDNA, Gene.Height, 0.2f, 25f, 0.5f);
    }

    void Mutation(DNA parentDNA, DNA childDNA, Gene gene, float min, float max, float fAddition)
    {
        float parentValue = parentDNA.genes[(int)gene];
        float childValue = childDNA.genes[(int)gene];


        int probability = Random.Range(0, 10);
        

        if(probability == 1) // 1/10 chance
        {
            childValue = parentValue + CustomMath.GaussianRandom(0, (fAddition - 0) / 1.64f, min - parentValue, max - parentValue); // 5% chance to add fAddition
            childDNA.genes[(int)gene] = childValue;
            parentDNA.genes[(int)gene] = childValue;
            //Debug.Log("Mutation Occured! " + gene + " changed to " + childDNA.genes[(int)gene]);
        }
        else
        {
            childDNA.genes[(int)gene] = parentDNA.genes[(int)gene];
        }
    }

        public float MovementSpeed
        {
            get
            {
            return genes[(int)Gene.MovementSpeed];
            }
            set
            {
                genes[(int)Gene.MovementSpeed] = value;
            }
          }
    public float MoveChangeFrequency
    {
        get
        {
            return genes[(int)Gene.MoveChangeFrequency];
        }
        set
        {
            genes[(int)Gene.MoveChangeFrequency] = value;
        }
    }

    public float RotationSpeed
        {
            get
            {
                return genes[(int)Gene.RotationSpeed];
            }
            set
            {
                genes[(int)Gene.RotationSpeed] = value;
            }
        }

        public float RotationFrequency
        {
            get
            {
                return genes[(int)Gene.RotationFrequency];
            }
            set
            {
                genes[(int)Gene.RotationFrequency] = value;
            }
        }

    public float Width
    {
        get
        {
            return genes[(int)Gene.Width];
        }
        set
        {
            genes[(int)Gene.Width] = value;
        }
    }

    public float Height
    {
        get
        {
            return genes[(int)Gene.Height];
        }
        set
        {
            genes[(int)Gene.Height] = value;
        }
    }
}
