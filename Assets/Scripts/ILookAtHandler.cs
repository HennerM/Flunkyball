using UnityEngine;
using System.Collections;

// This is an interface, not a class.
// It defines 3 public methods (even though there is no "public" keyword used)
// Every class implementing this interface needs to contain these public methods.
// https://msdn.microsoft.com/en-us/library/87d83y5b.aspx
public interface ILookAtHandler
{
	void OnLookatEnter();
	void OnLookatExit();
	void OnLookatInteraction();
}
