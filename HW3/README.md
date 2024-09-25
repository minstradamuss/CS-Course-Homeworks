# CS-Course-Homeworks
Лоскутова Мария, СП Б09

# Task 1

var x = new 
 { 
	Items = new List<int> { 1, 2, 3 }.GetEnumerator() 
 }; 
 while (x.Items.MoveNext()) 
	Console.WriteLine(x.Items.Current); 
 
 Этот код уходит в бесконечный цикл и печатает 0.
 Скорее всего это происходит из-за того, что в while мы каждый раз получаем новую копию enumerator, MoveNext() работает с копией (а не с исходным объектом) и
 всегда применяется для новой копии и поэтому никогда не переходит к следующему элементу.  
 Исправить можно так: 
 
 var x = new 
 { 
	Items = new List<int> { 1, 2, 3 }.GetEnumerator() 
 };  
 var enumerator = x.Items;
 while (enumerator.MoveNext()) 
    Console.WriteLine(enumerator.Current);

# Task 2, 3, 4, 5 в соответствующих папках

