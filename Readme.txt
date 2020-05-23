En este ejercicio he incluido los siguientes conceptos:
1) Simplificada versión del builder pattern (sin director ni clases abstractas), solo para dotar de mayor flexibilidad a la creación de cubos,
que me permita hacer algunas validaciones (valor positivio del lado del cubo), y dar un aire más funcional y legible al código de creación de cubos.
2) Uso de Task<T> para tratar de sacar provecho del paralelismo en los procesos de cáculo, presentando 2 opciones 
(una simple con Task.Factory, otra con Task.Run y reflection)
No he usado métodos asíncronos para no tener que cambiar la firma de las funciones, pero también se podría haber hecho esto, pudiendo usar de 
esta manera usar el await Task.WhenAll para liberar el thread mientras se hacen los cálculos correspondientes.
3) He hecho algunos cambios en los tests unitarios para tratar de usar el mismo test con varios casos de prueba, con el tag [DataTestMethod].
En este caso concreto no creo que fuera necesario el uso de mocks, aunque me ha parecido interesante el uso de NMock con sus expectations.
No conocía esta herramienta, y todavía le tengo que dar vueltas sobre su utilidad real. Yo siempre he usado Moq cuando he necesitado objectos Mock.
4) He decidido mantener el tipo 'decimal' de todas las unidades, a pesar de que me ha dado algún problema en los DataTestMethod. Parece ser que 
no se pueden poner números tipo decimal como parámetro, y he tenido que hacer conversiones de double a decimal. 
5) Intento que el mismo código sea autoexplicativo, y no suelo poner comentarios en el código. Esta vez, tratándose de un ejercicio, he abusado de los mismos,
con el objetivo de tratar de explicar mi forma de pensar al ir haciendo el desarrollo.
6) Teniendo en cuenta que el ejercicio estaba pensado para realizarlo en una hora, he decidido dejarlo aquí, aunque se me ocurren posibles mejoras:
- Hacer un UI más amigable (y con uso de ficheros de recursos para internacionalización).
- Otro aspecto de la internacionalización sería el formato de los números en punto flotante, y ver como podemos usar las unidades de medida
de cada cultura (cm? pulgadas? etc?)
- Tratamiento de excepciones y logging de las mismas
- Se puede pensar en hacer este proyecto más escalable, en el sentido de incorporar más figuras aparte de cubos, 
para obtener la intersección de esas figuras y el cálculo del volumen de interseción. Aquí habría que añadir varios interfaces, 
que se implementarían para cada figura concreta (posiblemente con un stretegy pattern, de forma que a cada figura le corresponderían sus algoritmos 
de contrucción de cada una de ellas y cálculo del volúmen de intersección), aparte de saber matemáticas.
- En este caso concreto tampoco veo la opción usar una política de cache, para tener resultados ya precalculados y no tener que hacerlo cada vez.
No veo que en este caso tenga sentido, pero siempre es un tema a considerar en todas las aplicaciones. Se podría estudiar si hubiera algún
tipo de cubos que supiéramos de antemano que aparecen muy frecuentemente en la aplicación.
- Uso de herramientas de profiler (Miniprofiler) o Benchmarking, que permitan medir las mejoras o no de rendimiento 
después de alguna refactorización. Los tests unitarios también pueden ser útiles para esto.


En cualquier caso, si echas en falta algún patrón o desarrollo que esperabas encontrar en este ejercicio, 
me puedes explicar cual es el enfoque que quieres darle y porqué crees que debería estar este patrón, y yo trataré de desarrollar tu idea.

Espero que haya captado bien la idea que se pretendía con este ejercicio, he tratado de centrarme en "correctness, performance and code clarity", 
tal como se pedía en el enunciado. También he desarrollado código "sospechoso", en el sentido de que posiblemente para este caso concreto no era necesario
pero me ha parecido interesante mostrar conocimientos de multithreading y reflection. Lo he dejado claro en los comentarios que hay en el mismo código.

Para acabar, en este ejercicio he tratado de mostrar mis intereses sobre:
- Programación funcional 
- Multithreading/Concurrencia/Paralelismo
- Tests Unitarios
- Orientación a Objetos
- Clean Code
- SOLID y Pattern design

Para mi, estos son los pilares sobre los que trato de mejorar en el desarrollo de software.

Te devuelvo un zip con mi solución, en la carpeta After. En la carpeta Before está el código inicial.

