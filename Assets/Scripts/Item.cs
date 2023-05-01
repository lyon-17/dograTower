using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
	private string name;
	private int x;
	private int y;
	

	public Item(string name, int x, int y)
	{
		this.name = name;
		this.x = x;
		this.y = y;
	}

	public void setName(string name)
    {
		this.name = name;
    }
	public string getName()
    {
		return name;
    }
	public void setPos(int x, int y)
    {
		this.x = x;
		this.y = y;
    }

	public int getX()
    {
		return x;
    }
	public int getY()
    {
		return y;
    }
}
