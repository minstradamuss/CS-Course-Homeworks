﻿interface IFirst
{
    void Display();
}

interface ISecond
{
    void Display();
}

abstract class AbstractBase
{
    public abstract void Display();
}

class MyClass : AbstractBase, IFirst, ISecond
{
    public override void Display()
    {
        Console.WriteLine("Реализация из абстрактного класса");
    }
    void IFirst.Display()
    {
        Console.WriteLine("Реализация из первого интерфейса");
    }
    void ISecond.Display()
    {
        Console.WriteLine("Реализация из второго интерфейса");
    }
}

class Program
{
    static void Main(string[] args)
    {
        MyClass myClass = new MyClass();
        myClass.Display();
        ((IFirst)myClass).Display();
        ((ISecond)myClass).Display();
    }
}