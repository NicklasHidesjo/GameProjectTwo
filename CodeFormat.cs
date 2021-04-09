using System;

public class Class1
{
	// skriv ut private o public på variabler

	// variable naming
	// private camelCase
	// beskrivande
	private int	korvMedBröd; // ex
	// public 

	// för unity 
	[SerializedField] int health = 100;
	[SerializedField] string blabla = "";

	// enkla gets på detta sätt (set är alltid en funktion)
	public int AsdKorv { get { return korvMedBröd; }}

	// const = ALLCAPS 
	private const int ASDKORV;

	// function format
	// PascalCase
	// Radbryt innan måsvinge
	// skriv ut private  o public innan functionen
	public void BlaaaBla()
	{

	}
	private void BlaBla()
	{

	}

	// if- statements
	private void ifStatements()
	{
		// om vi inte ska göra något ifall bool inte är sann/ är sann.
		if(isJumping) { return; }

		if (canJump)
		{

		}
		else
		{

		}
	}

	// parameters naming, tydliga.
	public void functionStuff(int korvMedBröd)
	{
		this.korvMedBröd = korvMedBröd;
	}


	// använd denna för att sätta värdet gör inte variablen public.
	public void SetAsdKorv(int value)
	{
		AsdKorv += value;
	}
	// bools
	// gör dem beskrivande 
	bool isJumping; // gör du något så är det is
	bool canJump; // kan du så är det can

	// functions namn beskrivande o korta 
	// ska bara göra en sak
	// max 4 parametrar in.
	// om funktionen anropar en massa andra funktioner relaterade till det så döp den till handle[vad funktionen hanterar]
	public void HandleScoreIncrease(int korvar)
	{
		IncreaseScore();
		UpdateScore();
		PlayScoreEffects();
		//etc...
	}

	// singletons format
	public static KorvManager instance;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
			Destroy(gameObject);
	}


	// interface naming
	// stort I i början
	// beskrivande namn
	public interface IAmHotDog
	public interface IAmJoakim


}
