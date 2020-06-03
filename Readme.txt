
En este ejercicio he incluido los siguientes conceptos:

1) Simplificada versi�n del builder pattern (sin director ni clases abstractas), solo para dotar de mayor flexibilidad a la creaci�n de cubos,
que me permita hacer algunas validaciones (valor positivio del lado del cubo), y dar un aire m�s funcional y legible al c�digo de creaci�n de cubos.

2) C�lculo de si hay intersecci�n o no y volumen de la intersecci�n con un patr�n Chain of Responsability de dos pasos. Tampoco 
creo que en este caso fuera necesario, pero me pareci� bien mostrar esta implementaci�n.

3) Uso de Task<T> para tratar de sacar provecho del paralelismo en los procesos de c�culo, presentando 2 opciones 
(una simple con Task.Factory, otra con Task.Run, reflection, y concurrentBag)
No he usado m�todos as�ncronos para no tener que cambiar la firma de las funciones, pero tambi�n se podr�a haber hecho esto, 
pudiendo usar de esta manera usar el await Task.WhenAll para liberar el thread mientras se hacen los c�lculos correspondientes.
De hecho me he guiado por el nombre de las funciones (que incluyen 'parallel'), para desarrollarlas de esa manera.
Si comparamos la versi�n Before y After, seg�n el tiempo de los tests unitarios, es m�s r�p�da la versi�n sin multithreading.
Es normal, estamos hablando de tareas muy peque�as en n�mero y en tiempo de ejecuci�n. Todo ello crea un overhead que
nos indica que para este caso concreto no ser�a necesario usar paralelismo, el beneficio lo ver�amos para varias tareas m�s largas.

4) He hecho algunos cambios en los tests unitarios para tratar de usar el mismo test con varios casos de prueba, con el tag [DataTestMethod].
En este caso concreto no creo que fuera necesario el uso de mocks, aunque me ha parecido interesante el uso de NMock con sus expectations.
No conoc�a esta herramienta, y todav�a le tengo que dar vueltas sobre su utilidad real. Yo siempre he usado Moq cuando he necesitado objectos Mock.

5) He decidido mantener el tipo 'decimal' de todas las unidades, a pesar de que me ha dado alg�n problema en los DataTestMethod. Parece ser que 
no se pueden poner n�meros tipo decimal como par�metro, y he tenido que hacer conversiones de double a decimal,
para no tener que cambiar tampoco los tipos de los datos num�ricos. 

6) Intento que el mismo c�digo sea autoexplicativo, y no suelo poner comentarios en el c�digo. Esta vez, trat�ndose de un ejercicio, he abusado de los mismos,
con el objetivo de tratar de explicar mi forma de pensar al ir haciendo el desarrollo.

6) Posibles mejoras:
- Hacer un UI m�s amigable (y con uso de ficheros de recursos para internacionalizaci�n).
- Otro aspecto de la internacionalizaci�n ser�a el formato de los n�meros en punto flotante, y ver como podemos usar las unidades de medida
de cada cultura (cm? pulgadas? etc?)
- Tratamiento de excepciones y logging de las mismas
- Se puede pensar en hacer este proyecto m�s escalable, en el sentido de incorporar m�s figuras aparte de cubos, 
para obtener la intersecci�n de esas figuras y el c�lculo del volumen de interseci�n. Aqu� habr�a que a�adir varios interfaces, 
que se implementar�an para cada figura concreta (posiblemente con un stretegy pattern, de forma que a cada figura le corresponder�an sus algoritmos 
de contrucci�n de cada una de ellas y c�lculo del vol�men de intersecci�n), aparte de saber matem�ticas.
- En este caso concreto tampoco veo la opci�n de usar una pol�tica de cache, para tener resultados ya precalculados y no tener que hacerlo cada vez.
No veo que en este caso tenga sentido, pero siempre es un tema a considerar en todas las aplicaciones. Se podr�a estudiar si hubiera alg�n
tipo de cubos que supi�ramos de antemano que aparecen muy frecuentemente en la aplicaci�n.
- Uso de herramientas de profiler (Miniprofiler) o Benchmarking, que permitan medir las mejoras o no de rendimiento 
despu�s de alguna refactorizaci�n. Los tests unitarios tambi�n pueden ser �tiles para esto.

En cualquier caso, si echas en falta alg�n patr�n o desarrollo que esperabas encontrar en este ejercicio, 
me puedes explicar cual es el enfoque que quieres darle y porqu� crees que deber�a estar este patr�n, y yo tratar� de desarrollar tu idea.

Espero que haya captado bien la idea que se pretend�a con este ejercicio, he tratado de centrarme en "correctness, performance and code clarity", 
tal como se ped�a en el enunciado. Tambi�n he desarrollado c�digo "sospechoso", en el sentido de que posiblemente para este caso concreto no era necesario
pero me ha parecido interesante mostrar conocimientos de multithreading y reflection. Lo he dejado claro en los comentarios que hay en el mismo c�digo.

Para acabar, en este ejercicio he tratado de mostrar mis intereses sobre:
- Programaci�n funcional 
- Multithreading/Concurrencia/Paralelismo
- Tests Unitarios
- Orientaci�n a Objetos
- Clean Code
- SOLID y Pattern design

Para mi, estos son los pilares sobre los que trato de mejorar en el desarrollo de software.


