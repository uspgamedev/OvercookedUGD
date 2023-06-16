using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Ingredient
{	
	//Dados gerais
	public string[] ingredientArray;

	//Bools para as transi��es de estado
	public bool overcookable;
	public bool sliceable;
	public bool mixeable;

	//Bools para checar se j� passou por tal estado?

	//Criar um None � mais seguro do que ficar usando null pra checar as coisas. Abaixo, None s� tem um get, sem nenhum set.
	public static Ingredient None { get { return new Ingredient { ingredientArray = new string[] { "" }, sliceable = false, mixeable = false, overcookable = false }; } }

	public Ingredient() { }
	public Ingredient(string[] array) { ingredientArray = array; }

	public Ingredient(string[] array, bool overcookable, bool sliceable, bool mixeable)
    {
		ingredientArray = array;
		this.overcookable = overcookable;
		this.sliceable = sliceable;
		this.mixeable = mixeable;
    }

	//M�todo que v� os bools acima e decide se, dentro de um Tuple (vide abaixo), o ingrediente pode transicionar pra outro estado
	public bool CanTrasition(State state)
    {
		switch (state)
        {
			case State.Sliced : return sliceable;
			case State.Mixed: return mixeable;
			case State.Overcooked: return overcookable;
			case State.None : return false;
			default: return false;
        }
    }

	//Abaixo, umas fun��es �teis s� para n�o cair em problemas envolvendo refer�ncias com arrays
	public void SetIngredientArray(string[] newArray)
    {
		string[] copiedArray = new string[newArray.Length];
		Array.Copy(newArray, copiedArray, newArray.Length);
		ingredientArray = copiedArray;
	}

	public static Ingredient CopyIngredient(Ingredient original)
    {
		string[] copiedArray = new string[original.ingredientArray.Length];
		Array.Copy(original.ingredientArray, copiedArray, original.ingredientArray.Length);
		Ingredient final = new Ingredient(copiedArray, original.overcookable, original.sliceable, original.mixeable);
		return final;
    }

	public static Ingredient ConcatIngredients(Ingredient merger, Ingredient merged)
	{ 
		string[] mergedCopiedArray = new string[merged.ingredientArray.Length];
		Array.Copy(merged.ingredientArray, mergedCopiedArray, merged.ingredientArray.Length);
		merger.ingredientArray = merger.ingredientArray.Concat(mergedCopiedArray).ToArray();
		Ingredient result = CopyIngredient(merger);
		return result;
	}

	//Overrides para igualdades
    #region Overrides
    public override bool Equals(object obj)
    {
        if(obj== null || GetType() != obj.GetType())
        {
			return false;
        }
		Ingredient other = (Ingredient)obj;
		bool condition = ingredientArray.OrderBy(x => x).SequenceEqual(other.ingredientArray.OrderBy(x => x))
			 && sliceable==other.sliceable && mixeable==other.mixeable && overcookable==other.overcookable;

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

//Enums n�o s�o escalon�veis
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

	public Sprite sprite;

	public static Tuple None { get { return new Tuple(Ingredient.None, State.None, null); } }

	public Tuple()
    {
		ingredient = new Ingredient();
    }
	public Tuple(Ingredient ing, State stt)
	{
		ingredient = ing;
		state = stt;
	}
	public Tuple (Ingredient ing, State stt, Sprite spt)
    {
		ingredient = ing;
		state = stt;
		sprite = spt;
    }

	//O m�todo abaixo � um tanto desnecess�rio enquanto tudo for p�blico
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
				case State.Overcooked: ing.sliceable = false; ing.mixeable = false; ing.overcookable = false; break; 
            }
			tuple.state = state;
		}
	}

	public static Tuple CopyTuple(Tuple origin)
    {
		Ingredient finalIngredient = Ingredient.CopyIngredient(origin.ingredient);
		Tuple final = new Tuple(finalIngredient, origin.state, origin.sprite);
		return final;
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








