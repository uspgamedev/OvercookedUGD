using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Ingredient
{	
	//Dados gerais
	public string[] ingredientArray;

	//Bools para as transições de estado
	public bool overcookeable;
	public bool sliceable;
	public bool mixeable;

	//Bools para checar se já passou por tal estado?

	public static Ingredient None =  new Ingredient { ingredientArray = new string[] { "" }, sliceable = false, mixeable = false, overcookeable = false };

	public Ingredient() { }
	public Ingredient(string[] array) { ingredientArray = array; }

	public bool CanTrasition(State state)
    {
		switch (state)
        {
			case State.Sliced : return sliceable;
			case State.Mixed: return mixeable;
			case State.Overcooked: return overcookeable;
			case State.None : return false;
			default: return false;
        }
    }

	public static void Concat(Ingredient ing)
    {
		
    }

    #region Overrides

    public override bool Equals(object obj)
    {
        if(obj== null || GetType() != obj.GetType())
        {
			return false;
        }
		Ingredient other = (Ingredient)obj;
		bool condition = ingredientArray.OrderBy(x=>x).SequenceEqual(other.ingredientArray.OrderBy(x=>x))
			 && sliceable==other.sliceable && mixeable==other.mixeable && overcookeable==other.overcookeable;

		return condition;
    }

	public static bool operator ==(Ingredient a, Ingredient b)
    {
        if (ReferenceEquals(a, null))
        {
			return ReferenceEquals(b, null);
        }
		return a.Equals(b);
    }
	public static bool operator !=(Ingredient a, Ingredient b)
    {
		return !(a == b);
    }
	#endregion
}

//Enums n são escalonáveis
public enum State
{
	None,
	Sliced,
	Mixed,
	Overcooked
}

[System.Serializable]
public class Tuple
{
	public Ingredient ingredient;
	public State state;

	public Tuple(Ingredient ing, State stt)
	{
		ingredient = ing;
		state = stt;
	}

	public static Tuple None = new Tuple(Ingredient.None, State.None);

	public static Ingredient GetIngredient(Tuple tuple)
	{
		return tuple.ingredient;
	}

	public static void ChangeState(Tuple tuple, State state)
    {
		Ingredient ing = GetIngredient(tuple);
		if (ing.CanTrasition(state))
		{
            switch (state)
            {
				case State.Sliced : ing.sliceable = false; break;
				case State.Mixed: ing.mixeable = false; break;
				case State.Overcooked: ing.sliceable = false; ing.mixeable = false; ing.overcookeable = false; break; 
            }
			tuple.state = state;
		}
	}

    #region Overrides
    public override bool Equals(object obj)
	{
		if (obj == null || GetType() != obj.GetType())
		{
			return false;
		}
		Tuple other = (Tuple)obj;
		bool condition = ingredient == other.ingredient && state == other.state;

		return condition;
	}

	public static bool operator ==(Tuple a, Tuple b)
	{
		if (ReferenceEquals(a, null))
		{
			return ReferenceEquals(b, null);
		}
		return a.Equals(b);
	}
	public static bool operator !=(Tuple a, Tuple b)
	{
		return !(a == b);
	}
    #endregion
}








